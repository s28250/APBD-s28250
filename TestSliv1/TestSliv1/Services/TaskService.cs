using TestSliv1.Models;
using TestSliv1.Repositories;

namespace TestSliv1.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public IEnumerable<TaskDetails> GetTasksAssigned(int id)
    {
        return _taskRepository.GetTasksByIdAssigned(id);
    }

    public IEnumerable<TaskDetails> GetTasksCreated(int id)
    {
        return _taskRepository.GeTasksByIdCreated(id);
    }
}