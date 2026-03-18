namespace EquipmentRental.Models;

public enum UserType
{
    EMPLOYEE,
    STUDENT
}

public abstract class User
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserType Type { get; }
}
