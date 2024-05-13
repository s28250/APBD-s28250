using System.ComponentModel.DataAnnotations;

namespace TestSliv1.Models;

public class Task
{
    public int IdTask { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(100)]
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public int IdProject { get; set; }
    public int IdTaskType { get; set; }
    public int IdAssignedTo { get; set; }
    public int IdCreator { get; set; }
}