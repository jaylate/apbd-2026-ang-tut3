namespace Tutorial3.Entities;

public class Laptop : Equipment
{
    public float Storage { get; set; } // In GiB
    public float RAM { get; set; } // In GiB

    public Laptop(string name, bool isAvailable, float storage, float ram) : base(name, isAvailable)
    {
	Storage = storage;
	RAM = ram;
    }
}
