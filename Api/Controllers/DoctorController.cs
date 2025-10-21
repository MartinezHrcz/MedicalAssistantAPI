using Api.Services;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize(Roles = "doctor")]
[Route("api/doctor")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
    {
        IEnumerable<DoctorDto> doctors = await _doctorService.GetDoctorsAsync();
        return Ok(doctors);
    }

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

    [HttpGet($"byname/{{name}}")]
    public async Task<ActionResult<List<DoctorDto>>> GetAllDoctorsByName(string name)
    {
        IEnumerable<DoctorDto> doctors = await _doctorService.GetDoctorsByNameAsync(name);
        return Ok(doctors);
    }

    [HttpGet("my_patients/{id:int}")]
    public async Task<ActionResult<List<PatientDto>>> GetAllPatientsOfDoctor(int id)
    {
        IEnumerable<PatientDto> patientDtos = await _doctorService.GetPatientsOfDoctor(id);
        return Ok(patientDtos);
    }
    
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
    }

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
}