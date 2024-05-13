using Microsoft.Data.SqlClient;
using TestSliv1.Models;
using Task = TestSliv1.Models.Task;

namespace TestSliv1.Repositories;


public class TaskRepository : ITaskRepository
{
    private IConfiguration _configuration;

    public TaskRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<TaskDetails> GetTasksByIdAssigned(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = @"
                          SELECT t.Name, t.Description, t.Deadline, p.Name as ProjectName, tt.Name as TaskTypeName
                          From Task t
                          INNER JOIN Project p on t.IdProject = p.IdProject
                          INNER Join TaskType tt on t.IdTaskType = tt.IdTaskType
                          where t.IdAssignedTo = @IdAssignedTo
                          order by t.Deadline desc";
        command.Parameters.AddWithValue("@IdAssignedTo", id);
        using SqlDataReader reader = command.ExecuteReader();
        var tasks =new  List<TaskDetails>();
        while (reader.Read())
        {
            TaskDetails taskDetails = new TaskDetails
            {
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Deadline = (DateTime)reader["Deadline"],
                ProjectName = reader["ProjectName"].ToString(),
                TaskTypeName = reader["TaskTypeName"].ToString()
            };
            tasks.Add(taskDetails);
        }

        return tasks;
    }

    public IEnumerable<TaskDetails> GeTasksByIdCreated(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = @"
                          SELECT t.Name, t.Description, t.Deadline, p.Name as ProjectName, tt.Name as TaskTypeName
                          From Task t
                          INNER JOIN Project p on t.IdProject = p.IdProject
                          INNER Join TaskType tt on t.IdTaskType = tt.IdTaskType
                          where t.IdCreator = @IdCreator
                          order by t.Deadline desc";
        command.Parameters.AddWithValue("@IdCreator", id);
        using SqlDataReader reader = command.ExecuteReader();
        var tasks =new  List<TaskDetails>();
        while (reader.Read())
        {
            TaskDetails taskDetails = new TaskDetails
            {
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Deadline = (DateTime)reader["Deadline"],
                ProjectName = reader["ProjectName"].ToString(),
                TaskTypeName = reader["TaskTypeName"].ToString()
            };
            tasks.Add(taskDetails);
        }

        return tasks;
    }
}