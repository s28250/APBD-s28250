using System.Security.AccessControl;

namespace Lab0202;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazard { get; set; }

    public LiquidContainer(int height, double tareWeight, int depth, string serialNumber, double maxLoad,bool isHazard) 
        : base(height,tareWeight,depth,serialNumber,maxLoad)
    {
        IsHazard = isHazard;
        
    }


    public override void Empty()
    {
        CargoWeight = 0;
    }

    public override void Load(double mass)
    {
        double allowedCapacity = IsHazard ? MaxLoad * 0.5 : MaxLoad * 0.9;
        if (mass > allowedCapacity)
        {
            throw new OverfillException("the attempt to perform a dangerous operation");
        }
        CargoWeight = mass;
    }

    public void Notify()
    {
        if (IsHazard)
        {
            Console.WriteLine("Hazard situation for: " + SerialNumber);
        }  
    }
}

