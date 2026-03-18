namespace EquipmentRental.Models;

public enum UserType
{
    EMPLOYEE,
    STUDENT
}

public class User(string firstName, string lastName, UserType type)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public UserType Type { get; } = type;
}
