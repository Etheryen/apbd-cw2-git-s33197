namespace EquipmentRental;

using EquipmentRental.Models;
using EquipmentRental.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new EquipmentRentalService([], [], []);

        var cameraId = service.AddCamera("Nikon 5", "123D", 8.0, (1920, 1080));
        var laptopId = service.AddLaptop("ThinkPad 490", "9993z", "i5-8365U 4.1 GHz", 13);
        var projectorId = service.AddProjector("Xiaomi 5", "19393AD", (1280, 720), true);

        var studentId = service.AddUser("Jan", "Kowalski", UserType.STUDENT);
        var employeeId = service.AddUser("Maciej", "Nowak", UserType.EMPLOYEE);

        service.Rent(cameraId, studentId);
        service.MarkUnavailable(laptopId);
        try
        {
            service.Rent(laptopId, studentId);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Caught exception: {e.GetType()}: {e.Message}");
        }

        service.Rent(projectorId, studentId, TimeSpan.FromDays(-1)); // Rent till yesterday to test fees in raport

        service.Return(cameraId, studentId);
        service.Return(projectorId, studentId);

        Console.WriteLine(service.GetRaport());
    }
}

