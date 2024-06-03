using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APBD_09.Data;
using APBD_09.DTOs;
using APBD_09.Services;
using Xunit;
using Moq;
using APBD_09.Services;
using APBD_09.Data;
using APBD_09.DTOs;
using Microsoft.EntityFrameworkCore;
using APBD_09.Models;

namespace APBD_09.Tests

{
    public class PrescriptionServiceTests
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly Mock<HospitalContext> _contextMock;

        public PrescriptionServiceTests()
        {
            _contextMock = new Mock<HospitalContext>();
            _prescriptionService = new PrescriptionService(_contextMock.Object);
        }

        [Fact]
        public async Task AddPrescription_ShouldReturnPrescription_WhenValidRequest()
        {
            // Arrange
            var prescriptionDto = new PrescriptionDto
            {
                IdPatient = 1,
                FirstName = "John",
                LastName = "Doe",
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(10),
                IdDoctor = 1,
                MedicamentIds = new List<int> { 1 }
            };

            // Act
            var result = await _prescriptionService.AddPrescription(prescriptionDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(prescriptionDto.Date, result.Date);
        }

        [Fact]
        public async Task GetPatientData_ShouldReturnPatientData_WhenPatientExists()
        {
            // Arrange
            int patientId = 1;

            // Act
            var result = await _prescriptionService.GetPatientData(patientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(patientId, result.IdPatient);
        }
    }
}
