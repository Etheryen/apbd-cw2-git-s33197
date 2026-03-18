namespace EquipmentRental;

using EquipmentRental.Models;
using EquipmentRental.Services;

class Program
{
    static void Main(string[] args)
    {
        var service = new EquipmentRentalService([], []);
        service.AddUser("Jan", "Kowalski", UserType.STUDENT);
    }
}

