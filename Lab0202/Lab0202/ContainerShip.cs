namespace Lab0202;

public class ContainerShip
{
    public List<Container> Containers { get; set; } = [];
    public int MaxSpeed { get;  set; }
    public int MaxContainers { get;  set; }
    public double MaxWeight { get;  set; }
    public string Name { get; set; }
    public double CurrentWeight;

    public ContainerShip(string name,int maxSpeed, int maxContainerNum, double maxWeight)
    {
        Name = name; 
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainerNum;
        MaxWeight = maxWeight;
    }
     public bool CanLoadContainer(Container container)
    {
        double totalWeight = Containers.Sum(c => c.CargoWeight + c.TareWeight) + container.CargoWeight + container.TareWeight;
        return Containers.Count+1 < MaxContainers && totalWeight <= MaxWeight;
    }
    public void LoadContainer(Container container)
    {
        if (CanLoadContainer(container))
        {
            Containers.Add(container);
        }
        else
        {
            Console.WriteLine("error");
        }
    }
    public void LoadContainers(List<Container?> newContainers)
    {
        foreach (var container in newContainers)
        {
            if (CanLoadContainer(container))
            {
                Containers.Add(container);
            }
            else
            {
                Console.WriteLine("error");
            }
        }
    }
    public void RemoveContainer(string serialNumber)    
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            Containers.Remove(container);
            
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            container.Empty();
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    public void ReplaceContainer(string serialNum, Container newContainer)
    {
        RemoveContainer(serialNum);
        if (CanLoadContainer(newContainer))
        {
            Containers.Add(newContainer);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    public  void TransferContainer(ContainerShip from, ContainerShip to, string serialNumber)
    {
        var container = from.Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null && to.CanLoadContainer(container))
        {
            from.RemoveContainer(serialNumber);
            to.LoadContainer(container);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    public void PrintContainerInfo(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            Console.WriteLine("CargoWeight: " + container.CargoWeight + "; Height: "+ container.Height +"; TareWeight: "
                + container.TareWeight +"; depth: " + container.Depth + "; serialnumber " + container.SerialNumber + "; maxLoad: " + container.MaxLoad);
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    public void PrintShipInfo()
    {
        Console.WriteLine("Max speed: " + MaxSpeed +"; Max weight: " + MaxWeight + "; Max containers: " + MaxContainers + "; Current weight: " + CurrentWeight);
        Console.WriteLine("Containers on board:");
        foreach (var container in Containers)
        {
            Console.WriteLine("serial number" + container.SerialNumber);
        }
    }
}
