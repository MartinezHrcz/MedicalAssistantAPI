using Api.Services;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/doctor")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
    {
        IEnumerable<DoctorDto> doctors = await _doctorService.GetDoctorsAsync();
        return Ok(doctors);
    }

    [Authorize(Roles = "Doctor,Patient")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
    {
        try
        {
            DoctorDto doctor = await _doctorService.GetDoctorAsync(id);
            return Ok(doctor);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex);
        }
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet($"byname/{{name}}")]
    public async Task<ActionResult<List<DoctorDto>>> GetAllDoctorsByName(string name)
    {
        IEnumerable<DoctorDto> doctors = await _doctorService.GetDoctorsByNameAsync(name);
        return Ok(doctors);
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("my_patients/{id:int}")]
    public async Task<ActionResult<List<PatientDto>>> GetAllPatientsOfDoctor(int id)
    {
        IEnumerable<PatientDto> patientDtos = await _doctorService.GetPatientsOfDoctor(id);
        return Ok(patientDtos);
    }
    
    [Authorize(Roles = "Doctor")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] RegisterDoctorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            DoctorDto doctor = await _doctorService.CreateDoctorAsync(dto);
            return Ok(doctor);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Roles = "Doctor")]
    [HttpPost("{id:int}")]
    public async Task<ActionResult<DoctorDto>> UpdateDoctor(int id,[FromBody] UpdateDoctorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            DoctorDto doctor = await _doctorService.UpdateDoctorAsync(id,dto);
            return Ok(doctor);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        
    }
    [Authorize(Roles = "Doctor")]
    [HttpPut("addpatient/{doctorId:int}-{patientId:int}")]
    public async Task<ActionResult> AddPatient(int doctorId, int patientId)
    {
        try
        {
            await _doctorService.AddPatientAsync(doctorId, patientId);
            return Ok();
        }
        catch (KeyNotFoundException ex )
        {
            return NotFound(ex.Message);
        }
        
    }
    [Authorize(Roles = "Doctor")]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<DoctorAuthResponseDto>> LoginDoctor([FromBody] LoginDoctorDto dto)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            DoctorAuthResponseDto doctor = await _doctorService.LoginDoctorAsync(dto);
            return Ok(doctor);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize(Roles = "Doctor")]
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteDoctor(int id)
    {
        try
        {
            await _doctorService.DeleteDoctorAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    [Authorize(Roles = "Doctor")]
    [HttpPut("removepatient/{doctorId:int}-{patientId:int}")]
    public async Task<ActionResult> RemovePatient(int doctorId, int patientId)
    {
        try
        {
            await _doctorService.RemovePatientAsync(doctorId, patientId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [Authorize(Roles = "Doctor")]
    [HttpPut("medication/{patientTaj}-{title}-{medication}")]
    public async Task<ActionResult> AddMedication(string patientTaj, string title, string medication)
    {
        try
        {
            await _doctorService.AddPatientMedication(patientTaj, title, medication);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Authorize(Roles = "Doctor")]
    [HttpDelete("medication/{patientTaj}-{medicationId}")]
    public async Task<ActionResult> RemoveMedication(string patientTaj, Guid medicationId)
    {
        try
        {
            await _doctorService.RemovePatientMedication(patientTaj, medicationId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}