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

        Console.WriteLine("---");
        Console.WriteLine("Equipment:");
        foreach (var e in service.GetEquipment())
            Console.WriteLine(e);
        Console.WriteLine("---");

        Console.WriteLine("---");
        Console.WriteLine("Available equipment:");
        foreach (var e in service.GetAvailableEquipment())
            Console.WriteLine(e);
        Console.WriteLine("---");
    }
}

