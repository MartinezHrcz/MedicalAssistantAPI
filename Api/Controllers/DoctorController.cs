using Api.Services;
using Api.Shared.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<List<DoctorDto>>> GetAllDoctorsByName()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientDto>>> GetAllPatientsOfDoctor(int id)
    {
        throw new NotImplementedException();
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

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> UpdateDoctor([FromBody] UpdateDoctorDto dto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<ActionResult<DoctorDto>> LoginDoctor([FromBody] LoginDoctorDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        throw new NotImplementedException();
    }
    
}