﻿using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Country
{
    [Key]
    public int IdCountry { get; set; }
    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
    public ICollection<Country_Trip> CountryTrips { get; set; }
}