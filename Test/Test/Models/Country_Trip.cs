using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models;

public class Country_Trip
{
    public int IdTrip { get; set; }
    [ForeignKey(nameof(IdTrip))]
    public Trip Trip { get; set; }
    public int IdCountry { get; set; }
    [ForeignKey(nameof(IdCountry))]
    public Country Country { get; set; }
}