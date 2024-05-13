using System.ComponentModel.DataAnnotations;

namespace TestSliv1.Models;

public class TeamMember
{
    public int IdTeamMember { get; set; }
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
}