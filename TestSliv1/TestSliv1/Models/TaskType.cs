using System.ComponentModel.DataAnnotations;

namespace TestSliv1.Models;

public class TaskType
{
    public int IdTaskType { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
}