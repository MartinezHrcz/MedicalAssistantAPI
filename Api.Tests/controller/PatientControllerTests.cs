using Api.Controllers;
using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests.controller;

public class PatientControllerTests
{
    private readonly PatientController _controller;
    private readonly Mock<IPatientService> _mockPatientService;
    
    public PatientControllerTests()
    {
        _mockPatientService = new Mock<IPatientService>();
        _controller = new PatientController(_mockPatientService.Object);
    }

    [Fact]
    public async Task GetAllPatients_ReturnsOkWithPatients()
    {

        var patientDtos =  new List<PatientDto>
        {
            new PatientDto(1, "Test Name", "Test Street", "111-111-111", "TestComplaint",
                new DateTime(2025, 10, 8, 10, 30, 9)),
            new PatientDto(2, "Test Name", "Test Street", "111-111-111", "TestComplaint",
                new DateTime(2025, 10, 8, 10, 31, 10))
        };

        _mockPatientService.Setup(s => s.GetPatientsAsync()).ReturnsAsync(patientDtos);
        
        var result = await _controller.GetAllPatients();
        
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<List<PatientDto>>(ok.Value);
        
        Assert.Equal(patientDtos, returned.ToList());
        
        _mockPatientService.Verify(s => s.GetPatientsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetPatientById_ReturnsOkWithPatient()
    {
        
        PatientDto patientDto = new PatientDto(1, "Test Name", "Test Street", "111-111-111", "TestComplaint",
            new DateTime(2025, 10, 8, 10, 30, 9));
        
        _mockPatientService.Setup(s => s.GetPatientByIdAsync(1)).ReturnsAsync(patientDto);
        
        var result = await _controller.GetPatientById(1);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<PatientDto>(ok.Value);
        
        Assert.Equal(patientDto, returned);
        _mockPatientService.Verify(s => s.GetPatientByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetPatientsByName_ReturnsOkWithPatients()
    {
        var patientDtos =  new List<PatientDto>
        {
            new PatientDto(1, "Test Name", "Test Street", "111-111-111", "TestComplaint",
                new DateTime(2025, 10, 8, 10, 30, 9)),
            new PatientDto(2,"Test Name", "Test Street", "111-111-111", "TestComplaint",
                new DateTime(2025, 10, 8, 10, 31, 10))
        };
        string name = "Test Name";
        
        _mockPatientService.Setup(s => s.GetPatientByNameAsync(name)).ReturnsAsync(patientDtos);
        
        var result = await _controller.GetPatientByName("Test Name");
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<IEnumerable<PatientDto>>(ok.Value);
        
        Assert.Equal(patientDtos, returned);
    }

    [Fact]
    public async Task GetPatientByTaj_ReturnsOkWithPatientDto()
    {
        PatientDto patientDto = new PatientDto(1,"Test Name", "Test Street", "111-111-111", "TestComplaint",
            new DateTime(2025, 10, 8, 10, 30, 9));

        string taj = "111-111-111";
        
        _mockPatientService.Setup(s => s.GetPatientByTajAsync(taj)).ReturnsAsync(patientDto);
        
        var result = await _controller.GetPatientByTaj(taj);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<PatientDto>(ok.Value);
        Assert.Equal(patientDto, returned);
        _mockPatientService.Verify(s => s.GetPatientByTajAsync(taj), Times.Once);
    }

    [Fact]
    public async Task CreatePatient_ReturnsOkWithPatient()
    {
        CreatePatientDto createDto = new CreatePatientDto("Test Name", "Test Street", "111-111-111", "TestComplaint");
        
        PatientDto patientDto = new PatientDto(1,"Test Name", "Test Street", "111-111-111", "TestComplaint",
            new DateTime(2025, 10, 8, 10, 30, 9));
        
        _mockPatientService.Setup(s => s.CreatePatientAsync(createDto)).ReturnsAsync(patientDto);
        
        var result = await _controller.CreatePatient(createDto);
        var ok = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returned = Assert.IsAssignableFrom<PatientDto>(ok.Value);
        
        Assert.Equal(patientDto, returned);
        _mockPatientService.Verify(s => s.CreatePatientAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task UpdatePatient_ReturnsOkWithPatient()
    {
        UpdatePatientDto updatePatientDto = new UpdatePatientDto(
            "Update Name", "Update street", "111-111-121", "TestComplaint");
        
        int updateId = 1;

        PatientDto expected =
            new PatientDto(1,"Update Name", "Update Street", "111-111-121", "TestComplaint",new DateTime(2025, 10, 8, 10, 31, 10));
        
        _mockPatientService.Setup(s=> s.UpdatePatientAsync(updateId,updatePatientDto)).ReturnsAsync(expected);
        
        var result = await _controller.UpdatePatient(updateId, updatePatientDto);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<PatientDto>(ok.Value);
        
        Assert.Equal(expected, returned);
        _mockPatientService.Verify(s => s.UpdatePatientAsync(updateId, updatePatientDto), Times.Once);
    }

    [Fact]
    public async Task DeletePatient_ReturnsNoContent()
    {
        int deleteId = 1;
        
        _mockPatientService.Setup(s=>s.DeletePatient(deleteId)).Returns(Task.CompletedTask);
        
        var result = await _controller.DeletePatient(deleteId);
        Assert.IsType<NoContentResult>(result);
        
        _mockPatientService.Verify(s => s.DeletePatient(deleteId), Times.Once);
    }

    [Fact]
    public async Task DeletePatient_ReturnsNotFound()
    {
        int deleteId = 1;
        
        _mockPatientService.Setup(s=>s.DeletePatient(deleteId)).Throws(new KeyNotFoundException());
        
        var result = await _controller.DeletePatient(deleteId);
        Assert.IsType<NotFoundResult>(result);
        
        _mockPatientService.Verify(s => s.DeletePatient(deleteId), Times.Once);
    }
}