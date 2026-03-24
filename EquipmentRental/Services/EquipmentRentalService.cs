namespace EquipmentRental.Services;

using System.Text;

using EquipmentRental.Models;

public sealed class EquipmentRentalService(
    ICollection<User> usersRepository,
    ICollection<Equipment> equipmentRepository,
    ICollection<Rental> rentalsRepository)
{
    const int MAX_STUDENT_CURRENT_RENTALS = 2;
    const int MAX_EMPLOYEE_CURRENT_RENTALS = 5;

    const double HOURLY_FEE_AMOUNT = 0.67;

    public Guid AddUser(string firstName, string lastName, UserType type)
    {
        var newUser = new User(firstName, lastName, type);
        usersRepository.Add(newUser);
        return newUser.Id;
    }

    public Guid AddCamera(string name, string serialNumber, double megaPixels, (int, int) maxResolution)
    {
        var newCamera = new Camera(name, serialNumber, megaPixels, maxResolution);
        equipmentRepository.Add(newCamera);
        return newCamera.Id;
    }

    public Guid AddLaptop(string name, string serialNumber, string cpu, double screenSizeInches)
    {
        var newLaptop = new Laptop(name, serialNumber, cpu, screenSizeInches);
        equipmentRepository.Add(newLaptop);
        return newLaptop.Id;
    }

    public Guid AddProjector(string name, string serialNumber, (int, int) resolution, bool isWireless)
    {
        var newProjector = new Projector(name, serialNumber, resolution, isWireless);
        equipmentRepository.Add(newProjector);
        return newProjector.Id;
    }

    public IReadOnlyList<Equipment> GetEquipment() =>
        equipmentRepository.ToList();

    public IReadOnlyList<Equipment> GetAvailableEquipment() =>
        equipmentRepository.Where(CanBeRented).ToList();


    public void Rent(Guid equipmentId, Guid userId) =>
        Rent(equipmentId, userId, TimeSpan.FromDays(7));

    public void Rent(Guid equipmentId, Guid userId, TimeSpan duration)
    {
        var equipment = equipmentRepository.SingleOrDefault(x => x.Id == equipmentId);
        if (equipment is null) throw new InvalidOperationException("Equipment not found");

        if (!equipment.IsAvailable) throw new InvalidOperationException("Equipment unavailable");

        var user = usersRepository.SingleOrDefault(x => x.Id == userId);
        if (user is null) throw new InvalidOperationException("User not found");

        var currentRentalsCount = rentalsRepository.Where(x => x.User == user && x.ReturnedAt is null).Count();

        if (user.Type == UserType.STUDENT && currentRentalsCount >= MAX_STUDENT_CURRENT_RENTALS)
            throw new InvalidOperationException("Student has max current rentals");

        if (user.Type == UserType.EMPLOYEE && currentRentalsCount >= MAX_EMPLOYEE_CURRENT_RENTALS)
            throw new InvalidOperationException("Employee has max current rentals");

        rentalsRepository.Add(new Rental(user, equipment, duration));
    }

    public void Return(Guid equipmentId, Guid userId)
    {
        var equipment = equipmentRepository.SingleOrDefault(x => x.Id == equipmentId);
        if (equipment is null) throw new InvalidOperationException("Equipment not found");

        var user = usersRepository.SingleOrDefault(x => x.Id == userId);
        if (user is null) throw new InvalidOperationException("User not found");

        var rental = rentalsRepository.LastOrDefault(x => x.ReturnedAt is null && x.User == user && x.Equipment == equipment);
        if (rental is null) throw new InvalidOperationException("Rental not found");

        rental.ReturnedAt = DateTime.Now;
        rental.Fee = CalculateFee(rental.RentedTo, rental.ReturnedAt.Value);
    }

    public void MarkUnavailable(Guid equipmentId)
    {
        var equipment = equipmentRepository.SingleOrDefault(x => x.Id == equipmentId);
        if (equipment is null) throw new InvalidOperationException("Equipment not found");

        if (rentalsRepository.Any(x => x.Equipment == equipment && x.ReturnedAt is null))
            throw new InvalidOperationException("Equipment currently rented");

        equipment.IsAvailable = false;
    }

    public IReadOnlyList<Rental> GetActiveUserRentals(Guid userId) =>
        rentalsRepository.Where(x => x.User.Id == userId && x.ReturnedAt is null).ToList();

    public IReadOnlyList<Rental> GetPrematureRentals() =>
        rentalsRepository.Where(x => x.ReturnedAt < x.RentedTo).ToList();

    public string GetRaport()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Total equipment count: {equipmentRepository.Count}");
        sb.AppendLine($"Unavailable equipment count: {equipmentRepository.Where(x => !x.IsAvailable).Count()}");
        sb.AppendLine($"Rented equipment count: {equipmentRepository.Where(e => rentalsRepository.Any(r => r.Equipment == e && r.ReturnedAt is null)).Count()}");
        sb.AppendLine($"Total fees amount: {rentalsRepository.Sum(x => x.Fee)}");

        return sb.ToString();
    }

    private double CalculateFee(DateTime deadline, DateTime returnedAt)
    {
        var fullHoursExceeded = Math.Floor((returnedAt - deadline).TotalHours);
        return Math.Max(0, fullHoursExceeded) * HOURLY_FEE_AMOUNT;
    }

    private bool CanBeRented(Equipment equipment) =>
        equipment.IsAvailable && !IsRented(equipment);

    private bool IsRented(Equipment equipment) =>
         rentalsRepository.Any(x => x.Equipment == equipment);
}
