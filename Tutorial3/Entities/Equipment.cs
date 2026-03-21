namespace Tutorial3.Entities;

public abstract class Equipment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public Equipment(string name, bool isAvailable)
    {
        Name = name;
        IsAvailable = isAvailable;
    }
}
