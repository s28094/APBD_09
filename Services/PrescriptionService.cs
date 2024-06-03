using Microsoft.EntityFrameworkCore;
using APBD_09.Data;
using APBD_09.DTOs;
using APBD_09.Models;

namespace APBD_09.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly HospitalContext _context;

        public PrescriptionService(HospitalContext context)
        {
            _context = context;
        }

        public async Task<PrescriptionDto> AddPrescription(PrescriptionDto prescriptionDto)
        {
            var patient = await _context.Patients.FindAsync(prescriptionDto.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    IdPatient = prescriptionDto.IdPatient,
                    FirstName = prescriptionDto.FirstName,
                    LastName = prescriptionDto.LastName
                };
                _context.Patients.Add(patient);
            }

            var medicaments = await _context.Medicaments
                .Where(m => prescriptionDto.MedicamentIds.Contains(m.IdMedicament))
                .ToListAsync();

            if (medicaments.Count != prescriptionDto.MedicamentIds.Count)
            {
                throw new Exception("One or more medicaments not found");
            }

            var prescription = new Prescription
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                IdDoctor = prescriptionDto.IdDoctor,
                Patient = patient,
                Medicaments = medicaments
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return new PrescriptionDto
            {
                IdPatient = prescription.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                IdDoctor = prescription.IdDoctor,
                MedicamentIds = prescription.Medicaments.Select(m => m.IdMedicament).ToList()
            };
        }

        public async Task<PatientDto> GetPatientData(int idPatient)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Medicaments)
                .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            return new PatientDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = patient.Prescriptions.Select(p => new PrescriptionDto
                {
                    IdPatient = p.IdPatient,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Date = p.Date,
                    DueDate = p.DueDate,
                    IdDoctor = p.IdDoctor,
                    MedicamentIds = p.Medicaments.Select(m => m.IdMedicament).ToList()
                }).OrderBy(p => p.DueDate).ToList()
            };
        }
    }
}
