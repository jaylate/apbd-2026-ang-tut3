namespace Tutorial3.Entities;

public class Projector : Equipment
{
    public int Lumens { get; set; }
    public string Resolution { get; set; }

    public Projector(string name, bool isAvailable, int lumens, string resolution) : base(name, isAvailable)
    {
	Lumens = lumens;
	Resolution = resolution;
    }
}
