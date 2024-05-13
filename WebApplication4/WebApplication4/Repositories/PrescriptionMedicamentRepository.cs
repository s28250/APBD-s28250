using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public class PrescriptionMedicamentRepository : IPrescriptionMedicamentRepository
{
    private IConfiguration _configuration;

    public PrescriptionMedicamentRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<PrescriptionMedicament> GetPrescriptionMedicaments()
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT IdMedicament, IdPrescription, Dose, Details From Prescription_Medicament";
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null!;
        var prescriptionMedicaments = new List<PrescriptionMedicament>();
        while (reader.Read())
        {
            PrescriptionMedicament prescriptionMedicament = new PrescriptionMedicament
            {
                IdMedicament = (int)reader["IdMedicament"],
                IdPrescription = (int)reader["IdPrescription"],
                Dose = (int)reader["Dose"],
                Details = reader["Details"].ToString()
            };
            prescriptionMedicaments.Add(prescriptionMedicament);
        }

        return prescriptionMedicaments;
    }

    public IEnumerable<PrescriptionMedicament> GetPrescriptionMedicamentByMedicament(int idMedicament)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PrescriptionMedicament> GetPrescriptionMedicamentByPrescription(int idPrescription)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT  IdMedicament, IdPrescription, Dose, Details From Prescription_Medicament WHERE IdPrescription = @IdPrescription";
        command.Parameters.AddWithValue("@IdPrescription", idPrescription);
        SqlDataReader reader = command.ExecuteReader();
        var prescriptionMedicaments = new List<PrescriptionMedicament>();
        while (reader.Read())
        {
            PrescriptionMedicament prescriptionMedicament = new PrescriptionMedicament
            {
                IdPrescription = (int)reader["IdPrescription"],
                IdMedicament = (int)reader["IdMedicament"],
                Dose = (int)reader["Dose"],
                Details = reader["Details"].ToString()
            };
            prescriptionMedicaments.Add(prescriptionMedicament);
        }

        return prescriptionMedicaments;
    }

    public int DeletePrescriptionMedicamentByPrescriptionId(int idPrescription, SqlTransaction transaction)
    {
        using SqlCommand command = new SqlCommand();
        command.Connection = transaction.Connection;
        command.Transaction = transaction;
        command.CommandText = "DELETE from Prescription_Medicament where IdPrescription = @IdPrescription";
        command.Parameters.AddWithValue("@IdPrescription", idPrescription);
        return command.ExecuteNonQuery();
    }
}