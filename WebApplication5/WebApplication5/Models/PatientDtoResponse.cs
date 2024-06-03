namespace WebApplication5.Models;

public class PatientDtoResponse
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<PrescriptionDtoResponse> Prescriptions { get; set; }
}