namespace EquipmentRental;

using EquipmentRental.Models;
using EquipmentRental.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new EquipmentRentalService([], []);
        service.AddUser("Jan", "Kowalski", UserType.STUDENT);
        service.AddCamera("Nikon 5", "123D", 8.0, (1920, 1080));
        service.AddCamera("Nikon 5", "123D", 8.0, (1920, 1080));
        service.LogEquipment();
        service.LogAvailableEquipment();
    }
}

