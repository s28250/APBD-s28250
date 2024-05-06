using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IPatientRepository
{
    IEnumerable<Patient> GetPatients();
    Patient GetPatient(int idPatient);
    int DeletePatient(int idPatient);
}