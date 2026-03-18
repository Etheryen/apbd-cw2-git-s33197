namespace EquipmentRental.Models;

public sealed class Laptop(string name, string serialNumber, string cpu, double screenSizeInches) : Equipment(name, serialNumber)
{
    public string Cpu { get; init; } = cpu;
    public double ScreenSizeInches { get; } = screenSizeInches;

    public override string ToString() =>
        $"{base.ToString()}, Cpu: {Cpu}, ScreenSizeInches: {ScreenSizeInches}";
}
