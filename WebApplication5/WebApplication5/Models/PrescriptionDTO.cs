namespace WebApplication5.Models;

public class PrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
}