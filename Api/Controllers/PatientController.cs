using Api.Services;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            PatientDto patient = await _patientService.CreatePatientAsync(dto);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.id }, patient);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientDto>> UpdatePatient(int id, [FromBody] UpdatePatientDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
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

    [HttpPost("login")]
    public async Task<IActionResult> LoginPatient([FromBody] PatientLoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            PatientDto patient = await _patientService.LoginPatientAsync(dto);
            return Ok(patient);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }


}