using TestSliv1.Models;
using TestSliv1.Repositories;

namespace TestSliv1.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public TeamMember GetTeamMemberById(int id)
    {
        return _teamMemberRepository.GetTeamMemberById(id);
    }
}