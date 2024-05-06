using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private IConfiguration _configuration;

    public PrescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Prescription> GetPrescriptions()
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdPrescription, Date, DueDate, IdPatient, IdDoctor From Prescription";
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null!;
        var prescriptions = new List<Prescription>();
        while(reader.Read()){
            Prescription prescription = new Prescription
            {
                IdPrescription = (int)reader["IdPrescription"],
                Date = (DateTime)reader["Date"],
                DueDate = (DateTime)reader["DueDate"],
                IdPatient = (int)reader["IdPatient"],
                IdDoctor = (int)reader["IdDoctor"]
            };
            prescriptions.Add(prescription);
        }
        return prescriptions;
    }

    public IEnumerable<Prescription>  GetPrescriptionsByDoctorId(int idDoctor)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdPrescription, Date, DueDate, IdPatient, IdDOctor FROM Prescription where IdDoctor = @IdDoctor ORDER BY Date DESC";
        command.Parameters.AddWithValue("@IdDoctor", idDoctor);
        SqlDataReader reader = command.ExecuteReader();
        var prescriptions = new List<Prescription>();
        while (reader.Read())
        {
            Prescription prescription = new Prescription
            {
                IdPrescription = (int)reader["IdPrescription"],
                Date = (DateTime)reader["Date"],
                DueDate = (DateTime)reader["DueDate"],
                IdPatient = (int)reader["IdPatient"],
                IdDoctor = (int)reader["IdDoctor"]
            };
            prescriptions.Add(prescription);
        }

        return prescriptions;
    }

    public int DeletePrescription(int idPrescription, SqlTransaction transaction)
    {
        using (var command = new SqlCommand("DELETE FROM Prescription WHERE IdPrescription = @IdPrescription", transaction.Connection, transaction))
        {
            command.Parameters.AddWithValue("@IdPrescription", idPrescription);
            return command.ExecuteNonQuery();
        }
    }
}