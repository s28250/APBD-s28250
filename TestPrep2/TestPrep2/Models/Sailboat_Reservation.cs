using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestPrep2.Models;
[PrimaryKey(nameof(IdSailboat), nameof(IdReservation))]
public class Sailboat_Reservation
{
    public int IdSailboat { get; set; }
    [ForeignKey(nameof(IdSailboat))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Sailboat Sailboat { get; set; }
    public int IdReservation { get; set; }
    [ForeignKey(nameof(IdReservation))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Reservation Reservation { get; set; }
}