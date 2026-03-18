namespace EquipmentRental.Models;

public sealed class Laptop(string name, string serialNumber, string cpu, double screenSizeInches) : Equipment(name, serialNumber)
{
    public string Cpu { get; init; } = cpu;
    public double ScreenSizeInches { get; } = screenSizeInches;
}
