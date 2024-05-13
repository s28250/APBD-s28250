using TestSliv1.Models;
using Task = TestSliv1.Models.Task;

namespace TestSliv1.DTOs;

public class TeamMemberTasksDTO
{
    public TeamMember? TeamMember { get; set; }
    public IEnumerable<TaskDetails>? AssignesTasks { get; set; }
    public IEnumerable<TaskDetails>? CreatedTasks { get; set; }
}