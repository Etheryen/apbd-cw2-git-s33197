namespace EquipmentRental.Models;

public abstract class Equipment
{
    public Guid Id { get; }
    public string Name { get; }
    public bool IsAvailable { get; set; }
    public string SerialNumber { get; }
}
