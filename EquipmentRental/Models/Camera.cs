namespace EquipmentRental.Models;

public sealed class Camera(string name, string serialNumber, double megaPixels, (int, int) maxResolution) : Equipment(name, serialNumber)
{
    public double MegaPixels { get; } = megaPixels;
    public (int, int) MaxResolution { get; } = maxResolution;

    public override string ToString() =>
        $"{base.ToString()}, MegaPixels: {MegaPixels}, MaxResolution: {MaxResolution}";
}


