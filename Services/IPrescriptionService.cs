using APBD_09.DTOs;

namespace APBD_09.Services
{
    public interface IPrescriptionService
    {
        Task<PrescriptionDto> AddPrescription(PrescriptionDto prescriptionDto);
        Task<PatientDto> GetPatientData(int idPatient);
    }
}
