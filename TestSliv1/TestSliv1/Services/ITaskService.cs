using TestSliv1.Models;

namespace TestSliv1.Services;

public interface ITaskService
{
    IEnumerable<TaskDetails> GetTasksAssigned(int id);
    IEnumerable<TaskDetails> GetTasksCreated(int id);
}