using Microsoft.Data.SqlClient;

namespace WebApplication4.Repositories;

public interface IPrescriptionMedicament
{ 
    IEnumerable<PrescriptionMedicament> GetPrescriptionMedicaments();
    PrescriptionMedicament GetPrescriptionMedicamentByMedicament(int idMedicament);
    PrescriptionMedicament GetPrescriptionMedicamentByPrescription(int idPrescription);
    int DeletePrescriptionMedicamentByPrescriptionId(int idPrescription, SqlTransaction transaction);

}