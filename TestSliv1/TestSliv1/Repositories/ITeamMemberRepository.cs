using TestSliv1.Models;

namespace TestSliv1.Repositories;

public interface ITeamMemberRepository
{
     TeamMember GetTeamMemberById(int id);
}