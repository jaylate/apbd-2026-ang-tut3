namespace Tutorial3.Entities;

public class Camera : Equipment
{
    public string Resolution { get; set; }
    public bool HasFlash { get; set; }

    public Camera(string name, bool isAvailable, string resolution, bool hasflash) : base(name, isAvailable)
    {
	Resolution = resolution;
	HasFlash = hasflash;
    }
}
