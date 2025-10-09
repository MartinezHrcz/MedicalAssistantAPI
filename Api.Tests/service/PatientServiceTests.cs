using Api.DTOs;
using Api.Repositories;
using Api.Services;
using Api.Shared.Models;
using Moq;

namespace Api.Tests.service;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _patientRepository;
    private readonly IPatientService _patientService;

    public PatientServiceTests()
    {
        _patientRepository = new Mock<IPatientRepository>();
        _patientService = new PatientService(_patientRepository.Object);
    }
    
    [Fact]
    public async Task GetPatientsAsync_ReturnsPatientsDTO()
    {
        var patients = new List<Patient>
        {
            new Patient{Id = 1, Name = "testName1",Address = "Test Address", Complaints = "Headache", Taj = "111-111-111"},
            new Patient{Id = 2, Name = "testName2",Address = "Test Address2", Complaints = "Headache", Taj = "222-222-222"}
        };
        _patientRepository.Setup(r=> r.GetAllPatients()).ReturnsAsync(patients);
        
        var result = await _patientService.GetPatientsAsync();
        
        Assert.NotNull(result);
        for (int i = 0; i < patients.Count; i++)
        {
            Assert.Equal(patients[i].Name, result.ElementAt(i).Name);
            Assert.Equal(patients[i].Address, result.ElementAt(i).Address);
            Assert.Equal(patients[i].Complaints, result.ElementAt(i).Complaints);
            Assert.Equal(patients[i].Taj, result.ElementAt(i).Taj);
        }
        
        _patientRepository.Verify(r => r.GetAllPatients(), Times.Once);
    }

    [Fact]
    public async Task GetPatientByIdAsync_ReturnsPatientDTO()
    {
        Patient patient = new Patient
            { Id = 1, Name = "testName1", Address = "Test Address", Complaints = "Headache", Taj = "111-111-111" };
        _patientRepository.Setup(r => r.GetPatientById(patient.Id)).ReturnsAsync(patient);
        
        var result = await _patientService.GetPatientByIdAsync(patient.Id);
        
        Assert.NotNull(result);
        Assert.Equal(patient.Name, result.Name);
        Assert.Equal(patient.Address, result.Address);
        Assert.Equal(patient.Complaints, result.Complaints);
        Assert.Equal(patient.Taj, result.Taj);
        
        _patientRepository.Verify(r => r.GetPatientById(patient.Id), Times.Once);
    }

    [Fact]
    public async Task GetPatientsByNameAsync_ReturnsPatientsDTO()
    {
        var patients = new List<Patient>
        {
            new Patient
            {
                Id = 1, Name = "Same Name", Address = "Test Address", Complaints = "Headache", Taj = "111-111-111"
            },
            new Patient
            {
                Id = 2, Name = "Same Name", Address = "Test Address2", Complaints = "Headache", Taj = "222-222-222"
            }
        };
        
        string name = "Same Name";
        
        _patientRepository.Setup(r => r.GetPatientsByName(name)).ReturnsAsync(patients);

        var result = await _patientService.GetPatientByNameAsync(name);
        
        Assert.NotNull(result);
        Assert.Equal(2,result.Count());
        for (int i = 0; i < patients.Count; i++)
        {
            Assert.Equal(patients[i].Name, result.ElementAt(i).Name);
            Assert.Equal(patients[i].Address, result.ElementAt(i).Address);
            Assert.Equal(patients[i].Complaints, result.ElementAt(i).Complaints);
            Assert.Equal(patients[i].Taj, result.ElementAt(i).Taj);
        }
        
        _patientRepository.Verify(r => r.GetPatientsByName(name), Times.Once);
    }

    [Fact]
    public async Task GetPatientByTajAsync_ReturnsPatientDTO()
    {
        var patient = new Patient
        {
            Id = 1, Name = "Test Name1", Address = "Test Address", Complaints = "Headache", Taj = "111-111-111"
        };
        
        string Taj =  "111-111-111";
        
        _patientRepository.Setup(r => r.GetPatientByTaj(Taj)).ReturnsAsync(patient);
        
        var result = await _patientService.GetPatientByTajAsync(Taj);
        Assert.NotNull(result);
        
        Assert.Equal(patient.Name, result.Name);
        Assert.Equal(patient.Address, result.Address);
        Assert.Equal(patient.Complaints, result.Complaints);
        Assert.Equal(patient.Taj, result.Taj);
        
        _patientRepository.Verify(r=> r.GetPatientByTaj(Taj), Times.Once);
    }

    [Fact]
    public async Task UpdatePatientAsync_ReturnUpdatedPatientDTO()
    {
        var patient = new Patient
        {
            Id = 1, Name = "Test Name1", Address = "Test Address", Complaints = "Headache", Taj = "111-111-111"
        };

        var updatedPatient = new Patient
        {
            Id = 1,
            Name = "Updated name",
            Address = "Updated  Address", 
            Complaints = "Updated Complaints", 
            Taj = "111-111-112"
        };

        int updateId = 1;

        var updateDTO = new UpdatePatientDto
        (
            "Updated name",
            "111-111-112",
            "Updated  Address",
            "Updated Complaints"
        );
        
        _patientRepository.Setup(r=> r.GetPatientById(updateId)).ReturnsAsync(patient);
        _patientRepository.Setup(r => r.UpdatePatient(patient)).ReturnsAsync(updatedPatient);
        
        var result =  await _patientService.UpdatePatientAsync(updateId, updateDTO);
        
        Assert.NotNull(result);
        Assert.Equal(patient.Name, result.Name);
        Assert.Equal(patient.Address, result.Address);
        Assert.Equal(patient.Complaints, result.Complaints);
        Assert.Equal(patient.Taj, result.Taj);
        
        _patientRepository.Verify(r=> r.UpdatePatient(patient), Times.Once);
    }

    [Fact]
    public async Task CreatePatientAsync_ReturnPatientDTO()
    {
        var expectedPatient = new Patient
        {
            Id = 0, Name = "Test Name", Address = "Test Address", Complaints = "Headache", Taj = "111-111-111"
        };

        var createPatientDTO = new CreatePatientDto
        (
            "Test Name",
            "111-111-111",
            "Test Address",
            "Headache"
        );
        
        _patientRepository.Setup(r=>r.CreatePatient(It.IsAny<Patient>())).ReturnsAsync(expectedPatient);
        
        var result = await _patientService.CreatePatientAsync(createPatientDTO);
        
        Assert.NotNull(result);
        Assert.Equal(expectedPatient.Name, result.Name);
        Assert.Equal(expectedPatient.Address, result.Address);
        Assert.Equal(expectedPatient.Complaints, result.Complaints);
        Assert.Equal(expectedPatient.Taj, result.Taj);
        
        _patientRepository.Verify(r => r.CreatePatient(It.IsAny<Patient>()), Times.Once);
    }
    
    
    [Fact]
    public async Task CreatePatient_TajExists_ThrowsInvalidOperation()
    {
        var dto = new CreatePatientDto("Name", "Address", "111-111-111", "Complaint");
        _patientRepository.Setup(r => r.PatientTajExists(dto.Taj)).ReturnsAsync(true);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _patientService.CreatePatientAsync(dto));

        _patientRepository.Verify(r => r.PatientTajExists(dto.Taj), Times.Once);
        _patientRepository.Verify(r => r.CreatePatient(It.IsAny<Patient>()), Times.Never);
    }

    [Fact]
    public async Task DeletePatientAsync_SuccessfulDelete()
    {
        int deleteId = 1;
        
        _patientRepository.Setup(r => r.DeletePatient(deleteId)).Returns(Task.CompletedTask);
        
        await _patientService.DeletePatient(deleteId);
        
        _patientRepository.Verify(r => r.DeletePatient(deleteId), Times.Once);
    }
    
    [Fact]
    public async Task DeletePatientByTajAsync_SuccessfulDelete()
    {
        string deleteTaj = "111-111-111";
        
        _patientRepository.Setup(r => r.DeletePatientByTaj(deleteTaj)).Returns(Task.CompletedTask);
        
        await _patientService.DeletePatientByTaj(deleteTaj);
        
        _patientRepository.Verify(r => r.DeletePatientByTaj(deleteTaj), Times.Once);
    }
}