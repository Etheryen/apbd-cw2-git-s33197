namespace EquipmentRental.Models;

public abstract class Equipment(string name, string serialNumber)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; } = name;
    public bool IsAvailable { get; set; } = true;
    public string SerialNumber { get; init; } = serialNumber;

    public override string ToString() =>
        $"Id: {Id}, Name: {Name}, IsAvailable: {IsAvailable}, SerialNumber: {SerialNumber}";
}
