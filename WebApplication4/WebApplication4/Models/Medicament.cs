﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models;

public class Medicament
{
    public int IdMedicament { get; set; }
    [MaxLength (100)]
    public string Name { get; set; }
    [MaxLength (100)]
    public string Description { get; set; }
    [MaxLength (100)]
    public string Type { get; set; }
}