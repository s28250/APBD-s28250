using Microsoft.AspNetCore.Mvc;
using TestSliv1.DTOs;
using TestSliv1.Models;
using TestSliv1.Services;

namespace TestSliv1.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;
    private readonly ITaskService _taskService;

    public Controller(ITeamMemberService teamMemberService, ITaskService taskService)
    {
        _teamMemberService = teamMemberService;
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetTeamMember(int id)
    {
        TeamMember member = _teamMemberService.GetTeamMemberById(id);
        if (member == null)
        {
            return NotFound("there is no such doctor");
        }
        var tasksAssigned = _taskService.GetTasksAssigned(id);
        var tasksCreated = _taskService.GetTasksCreated(id);

        TeamMemberTasksDTO teamMemberTasksDto = new TeamMemberTasksDTO
        {
            TeamMember = member,
            AssignesTasks = tasksAssigned,
            CreatedTasks = tasksCreated
        };
        return Ok(teamMemberTasksDto);
    }
}