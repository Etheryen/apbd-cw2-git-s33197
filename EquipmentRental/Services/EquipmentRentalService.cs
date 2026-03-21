namespace EquipmentRental.Services;

using EquipmentRental.Models;

public sealed class EquipmentRentalService(
    ICollection<User> usersRepository,
    ICollection<Equipment> equipmentRepository,
    ICollection<Rental> rentalsRepository)
{
    public void AddUser(string firstName, string lastName, UserType type) =>
        usersRepository.Add(new User(firstName, lastName, type));

    public void AddCamera(string name, string serialNumber, double megaPixels, (int, int) maxResolution) =>
        equipmentRepository.Add(new Camera(name, serialNumber, megaPixels, maxResolution));

    public void AddLaptop(string name, string serialNumber, string cpu, double screenSizeInches) =>
        equipmentRepository.Add(new Laptop(name, serialNumber, cpu, screenSizeInches));

    public void AddProjector(string name, string serialNumber, (int, int) resolution, bool isWireless) =>
        equipmentRepository.Add(new Projector(name, serialNumber, resolution, isWireless));

    public void LogEquipment()
    {
        Console.WriteLine("---");
        Console.WriteLine("Equipment:");
        Console.WriteLine(string.Join('\n', equipmentRepository));
        Console.WriteLine("---");
    }

    public void LogAvailableEquipment()
    {
        var available = equipmentRepository.Where(canBeRented);
        Console.WriteLine("---");
        Console.WriteLine("Available equipment:");
        Console.WriteLine(string.Join('\n', available));
        Console.WriteLine("---");
    }

    public void Rent(Guid equipmentId, Guid userId)
    {
        var equipment = equipmentRepository.SingleOrDefault(x => x.Id == equipmentId);
        if (equipment is null)
        {
            Console.WriteLine("Equipment not found");
            return;
        }

        var user = usersRepository.SingleOrDefault(x => x.Id == userId);
        if (user is null)
        {
            Console.WriteLine("User not found");
            return;
        }

        rentalsRepository.Add(new Rental(user, equipment));
    }

    public void Return(Guid equipmentId, Guid userId)
    {
        var equipment = equipmentRepository.SingleOrDefault(x => x.Id == equipmentId);
        if (equipment is null)
        {
            Console.WriteLine("Equipment not found");
            return;
        }

        var user = usersRepository.SingleOrDefault(x => x.Id == userId);
        if (user is null)
        {
            Console.WriteLine("User not found");
            return;
        }

        var rental = rentalsRepository.LastOrDefault(x => x.ReturnedAt is null && x.User == user && x.Equipment == equipment);
        if (rental is null)
        {
            Console.WriteLine("Rental not found");
            return;
        }

        rental.ReturnedAt = DateTime.Now;
        rental.Fee = calculateFee(rental.RentedTo, rental.ReturnedAt.Value);
    }

    private double calculateFee(DateTime deadline, DateTime returnedAt)
    {
        var diff = returnedAt - deadline;
        return Math.Max(0, diff.TotalHours * 0.67);
    }

    private bool canBeRented(Equipment equipment) =>
        equipment.IsAvailable && !isRented(equipment);

    private bool isRented(Equipment equipment) =>
         rentalsRepository.Any(x => x.Equipment == equipment);
}
