namespace EquipmentRental.Models;

public sealed class Projector(string name, string serialNumber, (int, int) resolution, bool isWireless) : Equipment(name, serialNumber)
{
    public (int, int) Resolution { get; } = resolution;
    public bool IsWireless { get; } = isWireless;

    public override string ToString() =>
        $"{base.ToString()}, Resolution: {Resolution}, IsWireless: {IsAvailable}";
}

