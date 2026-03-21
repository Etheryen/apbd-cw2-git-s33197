namespace EquipmentRental;

using EquipmentRental.Models;
using EquipmentRental.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new EquipmentRentalService([], [], []);
        service.AddUser("Jan", "Kowalski", UserType.STUDENT);
        service.AddCamera("Nikon 5", "123D", 8.0, (1920, 1080));
        service.AddLaptop("ThinkPad 490", "9993z", "i5-8365U 4.1 GHz", 13);
        service.LogEquipment();
        service.LogAvailableEquipment();
    }
}

