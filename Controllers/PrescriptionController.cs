namespace APBD_09.Controllers;

using Microsoft.AspNetCore.Mvc;
using APBD_09.Services;
using APBD_09.DTOs;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto prescriptionDto)
    {
        try
        {
            var prescription = await _prescriptionService.AddPrescription(prescriptionDto);
            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patient/{idPatient}")]
    public async Task<IActionResult> GetPatientData(int idPatient)
    {
        try
        {
            var patientData = await _prescriptionService.GetPatientData(idPatient);
            return Ok(patientData);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
