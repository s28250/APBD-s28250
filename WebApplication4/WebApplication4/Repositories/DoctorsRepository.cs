using Microsoft.Data.SqlClient;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private IConfiguration _configuration;

    public DoctorsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Doctor> GetDoctors()
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT IdDoctor, FirstName, LastName, Email FROM Doctor ORDER BY IdDoctor";
        SqlDataReader reader = command.ExecuteReader();
        var doctors = new List<Doctor>();
        while (reader.Read())
        {
            var doctor = new Doctor
            {
                IdDoctor = (int)reader["IdDoctor"],
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString()
            };
            doctors.Add(doctor);
        }

        return doctors;
    }

    public Doctor GetDoctor(int idDoctor)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select IdDoctor, FirstName, LastName, Email From Doctor WHERE IdDoctor = @IdDoctor";
        command.Parameters.AddWithValue("@IdDoctor", idDoctor);
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null!;

        Doctor doctor = new Doctor
        {
            IdDoctor = (int)reader["IdDoctor"],
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            Email = reader["Email"].ToString()
        };
        return doctor;
    }

    public int DeleteDoctor(int idDoctor, SqlTransaction transaction)
    {
        using SqlCommand command = new SqlCommand();
        command.CommandText = "DELETE FROM Doctor WHERE IdDoctor = @IdDoctor";
        command.Connection = transaction.Connection;
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@IdDoctor", idDoctor);
        return command.ExecuteNonQuery();
        
    }

    public int UpdateDoctor(Doctor doctor)
    {
        SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "UPDATE Doctor SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE IdDoctor = @IdDoctor";
        command.Parameters.AddWithValue("@IdDoctor", doctor.IdDoctor);
        command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
        command.Parameters.AddWithValue("@LastName", doctor.LastName);
        command.Parameters.AddWithValue("@Email", doctor.Email);
        return command.ExecuteNonQuery();
    }
}