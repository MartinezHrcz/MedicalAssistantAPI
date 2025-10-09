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
        try
        {
            PatientDto patient = await _patientService.CreatePatientAsync(dto);
            return CreatedAtAction(nameof(GetPatientById), patient);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
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
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
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
    
    [HttpDelete("{taj}")]
    public async Task<IActionResult> DeletePatient(string taj)
    {
        try
        {
            await _patientService.DeletePatientByTaj(taj);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}