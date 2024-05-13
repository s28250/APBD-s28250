using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    [MaxLength (100)]
    public string Details { get; set; }
    
}