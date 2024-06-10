using System.ComponentModel.DataAnnotations;

namespace Test.Models;

public class Trip
{
    [Key]
    public int IdTrip { get; set; }
    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
    [Required]
    [MaxLength(220)]
    public string Description { get; set; }
    [Required]
    [MaxLength(120)]
    public DateTime DateFrom { get; set; }
    [Required]
    [MaxLength(120)]
    public DateTime DateTo { get; set; }
    [Required]
    public int MaxPeople { get; set; }
    public ICollection<Client_Trip> ClientTrips { get; set; }
    public ICollection<Country_Trip> CountryTrips { get; set; }
}