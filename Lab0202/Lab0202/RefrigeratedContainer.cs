namespace Lab0202;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }
    
    public RefrigeratedContainer(int height, double tareWeight, int depth, string serialNumber, double maxLoad, string productType, double temperature) 
        : base(height, tareWeight, depth, serialNumber, maxLoad)
    {
        ProductType = productType;
        Temperature = temperature;
    }

    public override void Empty()
    {
        CargoWeight = 0;
    }

    public override void Load(double mass)
    {
        if (mass > MaxLoad)
        {
            throw new OverfillException("the attempt to perform a dangerous operation");
        }
        CargoWeight = mass;
    }

    public double CalcMinTemp(string product)
    {
        return product switch
        {
            "bananas" => 13.3,
            "chocolate" => 18,
            "fish" => 2,
            "meat" => -15,
            "ice cream" => -18,
            "frozen pizza" => -30,
            "cheese" => 7.2,
            "sausages" => 5,
            "butter" => 20.5,
            "eggs" => 19,
            _ => 0.0
        };
    }
}