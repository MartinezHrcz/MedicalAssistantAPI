using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
    {
        IEnumerable<PatientDto> patients = await _patientService.GetPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientDto>> GetPatientById(int id)
    {
        try
        {
            PatientDto patient = await _patientService.GetPatientByIdAsync(id);
            return Ok(patient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

    }

    [HttpGet("byname/{name}")]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientByName(string name)
    {
        IEnumerable<PatientDto> patients = await _patientService.GetPatientByNameAsync(name);
        return Ok(patients);
    }

    [HttpGet("bytaj/{taj}")]
    public async Task<ActionResult<PatientDto>> GetPatientByTaj(string taj)
    {
        try
        {
            PatientDto patient = await _patientService.GetPatientByTajAsync(taj);
            return Ok(patient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] CreatePatientDto dto)
    {
        PatientDto patient = await _patientService.CreatePatientAsync(dto);
        return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientDto>> UpdatePatient(int id, [FromBody] UpdatePatientDto dto)
    {
        try
        {
            PatientDto patient = await _patientService.UpdatePatientAsync(id, dto);
            return Ok(patient);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            await _patientService.DeletePatient(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}