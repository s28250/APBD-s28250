using WebApplication4.Models;

namespace WebApplication4.DTO;

public class DoctorWIthPrescription
{
    public Doctor Doctor { get; set; }
    public IEnumerable<Prescription> Prescriptions { get; set; }
}