using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public class PrescriptionMedicament : IPrescriptionRepository
{
    public IEnumerable<Prescription> GetPrescriptions()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Prescription> GetPrescriptionsByDoctorId(int idDoctor)
    {
        throw new NotImplementedException();
    }

    public int DeletePrescription(int idPrescription, SqlTransaction transaction)
    {
        throw new NotImplementedException();
    }
}