namespace Lab0202;

public abstract class Container
{
    public double CargoWeight { get; set; }
    public int Height { get; set; }
    public double TareWeight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; set; }
    public double MaxLoad { get; set; }
    
    protected Container(int height, double tareWeight, int depth, string serialNumber, double maxLoad)
    {
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        SerialNumber = serialNumber;
        MaxLoad = maxLoad;
    }

    public abstract void Empty();
    public abstract void Load(double mass);
}