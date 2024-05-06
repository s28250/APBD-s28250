namespace WebApplication4.Repositories;

public interface IPrescriptionMedicament
{ 
    IEnumerable<Prescription_Medicament> GetPrescriptionMedicaments();
    Prescription_Medicament GetPrescriptionMedicamentByMedicament(int idMedicament);
    Prescription_Medicament GetPrescriptionMedicamentByPrescription(int idPrescription);
    
}