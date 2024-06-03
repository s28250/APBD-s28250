using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models;

public class Prescription_Medicament
{
    public int IdPrescription { get; set; }
        
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
    
    public int IdMedicament { get; set; }
        
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; }

    public int? Dose { get; set; } 
        
    [MaxLength(100)]
    public string Details { get; set; }
    
    
}