using TestSliv1.Models;

namespace TestSliv1.Services;

public interface ITeamMemberService
{
    TeamMember GetTeamMemberById(int id);
}