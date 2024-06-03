namespace WebApplication5.Models;

public class PrescriptionDtoResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDtoResponse> Medicaments { get; set; }
    public DoctorDtoResponse Doctor { get; set; }
}