namespace EquipmentRental.Models;

public sealed class Rental
{
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentedAt { get; }
    public DateTime RentedTo { get; }
    public DateTime ReturnedAt { get; }
}
