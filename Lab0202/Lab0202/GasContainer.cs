namespace Lab0202;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; set; }
    public GasContainer(int height, double tareWeight, int depth, string serialNumber, double maxLoad, double pressure) 
        : base(height, tareWeight, depth, serialNumber, maxLoad)
    {
        Pressure = pressure;
    }

    public override void Empty()
    {
        CargoWeight *= 0.05;
    }

    public override void Load(double mass)
    {
        if (mass > MaxLoad)
        {
            throw new OverfillException("the attempt to perform a dangerous operation");
        }
        CargoWeight = mass;
    }

    public void Notify()
    {
        Console.WriteLine("Hazard situation for: " + SerialNumber);
    }
}