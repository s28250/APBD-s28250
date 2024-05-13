using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IPrescriptionMedicamentRepository
{ 
    IEnumerable<PrescriptionMedicament> GetPrescriptionMedicaments();
    IEnumerable<PrescriptionMedicament> GetPrescriptionMedicamentByMedicament(int idMedicament);
    IEnumerable<PrescriptionMedicament> GetPrescriptionMedicamentByPrescription(int idPrescription);
    int DeletePrescriptionMedicamentByPrescriptionId(int idPrescription, SqlTransaction transaction);

}