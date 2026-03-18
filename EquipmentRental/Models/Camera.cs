namespace EquipmentRental.Models;

public sealed class Camera : Equipment
{
    public double MegaPixels { get; }
    public (int, int) MaxResolution { get; }
}


