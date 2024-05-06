using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IPrescriptionRepository
{
    IEnumerable<Prescription> GetPrescriptions();
    IEnumerable<Prescription>  GetPrescriptionsByDoctorId(int idDoctor);
    int DeletePrescription(int idPrescription, SqlTransaction transaction);
    
}