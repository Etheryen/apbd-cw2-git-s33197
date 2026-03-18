namespace EquipmentRental.Services;

using EquipmentRental.Models;

public sealed class EquipmentRentalService(ICollection<User> users, ICollection<Equipment> equipment)
{
    public void AddUser(string firstName, string lastName, UserType type) =>
        users.Add(new User(firstName, lastName, type));

    public void AddCamera(string name, string serialNumber, double megaPixels, (int, int) maxResolution) =>
        equipment.Add(new Camera(name, serialNumber, megaPixels, maxResolution));

    public void AddLaptop(string name, string serialNumber, string cpu, double screenSizeInches) =>
        equipment.Add(new Laptop(name, serialNumber, cpu, screenSizeInches));

    public void AddProjector(string name, string serialNumber, (int, int) resolution, bool isWireless) =>
        equipment.Add(new Projector(name, serialNumber, resolution, isWireless));

    public void LogEquipment()
    {
        Console.WriteLine("---");
        Console.WriteLine("Equipment:");
        Console.WriteLine(string.Join('\n', equipment));
        Console.WriteLine("---");
    }

    public void LogAvailableEquipment()
    {
        Console.WriteLine("---");
        Console.WriteLine("Available equipment:");
        Console.WriteLine(string.Join('\n', equipment.Where(x => x.IsAvailable)));
        Console.WriteLine("---");
    }
}
