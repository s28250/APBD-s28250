using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IDoctorsRepository
{
    IEnumerable<Doctor> GetDoctors();
    Doctor GetDoctor(int idDoctor);
    int DeleteDoctor(int idPrescription, SqlTransaction transaction);
    int UpdateDoctor(Doctor doctor);
}