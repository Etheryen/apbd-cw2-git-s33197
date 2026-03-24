namespace EquipmentRental.Models;

public sealed class Rental(User user, Equipment equipment, TimeSpan duration)
{
    public User User { get; } = user;
    public Equipment Equipment { get; } = equipment;
    public DateTime RentedAt { get; } = DateTime.Now;
    public DateTime RentedTo { get; } = DateTime.Now + duration;
    public DateTime? ReturnedAt { get; set; } = null;
    public double Fee { get; set; } = default;
}
