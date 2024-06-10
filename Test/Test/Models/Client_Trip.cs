using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models;

public class Client_Trip
{
    public int IdClient { get; set; }
    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }
    public int IdTrip { get; set; }
    [ForeignKey(nameof(IdTrip))]
    public Trip Trip { get; set; }
    [Required]
    public int RegisteredAt { get; set; }
    public int PaymentDate { get; set; }
}