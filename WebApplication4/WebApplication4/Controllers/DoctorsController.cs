using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTO;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private IDoctorsService _doctorsService;
    private IPrescriptionsService _prescriptionsService;
    public DoctorsController(IDoctorsService doctorsService, IPrescriptionsService prescriptionsService)
    {
        _prescriptionsService = prescriptionsService;
        _doctorsService = doctorsService;
    }

    [HttpGet]
    public IActionResult GetDoctors()
    {
        IEnumerable<Doctor> doctors = _doctorsService.GetDoctors();
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetDoctor(int id)
    {
        Doctor doctor = _doctorsService.GetDoctor(id);
        var prescriptions = _prescriptionsService.GetPrescriptionsByDoctorId(id);
        if (doctor == null)
        {
            return NotFound("Doctor with such ID wasn't found");
        }
        
        DoctorWIthPrescription doctorWIthPrescription = new DoctorWIthPrescription
        {
            Doctor = doctor,
            Prescriptions = prescriptions
        };

        return Ok(doctorWIthPrescription);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteDoctor(int id)
    {
        int affectedCount = _doctorsService.DeleteDoctor(id);
        return NoContent();
    }
}