
using TestSliv1.Models;
using Task = TestSliv1.Models.Task;

namespace TestSliv1.Repositories;

public interface ITaskRepository
{
    IEnumerable<TaskDetails> GetTasksByIdAssigned(int id);
    IEnumerable<TaskDetails> GeTasksByIdCreated(int id);
}