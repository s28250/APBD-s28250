using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models;

public class Doctor
{
    
    public int IdDoctor { get; set; }
    [MaxLength (100)]
    public string? FirstName { get; set; }
    [MaxLength (100)]
    public string? LastName { get; set; }
    [MaxLength (100)]
    [EmailAddress]
    public string? Email { get; set; }
}