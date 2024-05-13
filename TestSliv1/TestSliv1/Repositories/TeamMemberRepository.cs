using Microsoft.Data.SqlClient;
using TestSliv1.Models;

namespace TestSliv1.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private IConfiguration _configuration;

    public TeamMemberRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TeamMember GetTeamMemberById(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdTeamMember, FirstName, LastName, Email From TeamMember WHERE IdTeamMember = @IdTeamMember";
        command.Parameters.AddWithValue("@IdTeamMember", id);
        SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null!;
        TeamMember teamMember = new TeamMember
        {
            IdTeamMember = (int)reader["IdTeamMember"],
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            Email = reader["Email"].ToString()
        };
        return teamMember;
    }
}