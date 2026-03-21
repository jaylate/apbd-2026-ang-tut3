namespace Tutorial3.Entities;

public abstract class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
