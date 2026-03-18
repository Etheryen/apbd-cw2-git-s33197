namespace EquipmentRental.Models;

public sealed class Projector : Equipment
{
    public (int, int) Resolution { get; }
    public bool IsWireless { get; }
}

