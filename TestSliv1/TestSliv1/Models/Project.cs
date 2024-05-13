using System.ComponentModel.DataAnnotations;

namespace TestSliv1.Models;

public class Project
{
    public Project(string name)
    {
        Name = name;
    }

    public int IdProject { get; set; }
    [MaxLength (100)]
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
}