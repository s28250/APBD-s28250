namespace Lab0202;

public class Program
{
    private static readonly List<Container?> Containers = [];
    private static readonly List<ContainerShip> Ships = [];
    private static int _containerSerialNumber = 1;

    private static void Main(string[] args)
    {
        const bool exit = false;
        while (!exit)
        {
            Console.WriteLine("CONTAINER MANAGER ");
            Console.WriteLine("List of container ships: " );
            PrintShipsAll();
            Console.WriteLine("List of containers: ");
            PrintContAll();

            Console.WriteLine("1 to create container");
            Console.WriteLine("2 to create a new ship");
            Console.WriteLine("3 to load cargo to created container");
            Console.WriteLine("4 to load container to the ship");
            Console.WriteLine("5 to load list of containers to the ship");
            Console.WriteLine("6 to remove container from the ship");
            Console.WriteLine("7 to unload container");
            Console.WriteLine("8 to replace container with another");
            Console.WriteLine("9 to transfer container from ship to ship");
            Console.WriteLine("10 to print info about certain container");
            Console.WriteLine("11 to print info about ship ");
            switch (Console.ReadLine())
            {
                case "1":
                    CreteContainer();
                    break;
                case "2":
                    CreateShip();
                    break;
                case "3":
                    LoadCargoContainer();
                    break;
                case "4":
                    LoadContainerShip();
                    break;
                case "5":
                    LoadListContainersShip();
                    break;
                case "6":
                    RemoveContainerShip();
                    break;
                case "7":
                    UnloadContainer();
                    break;
                case "8":
                    ReplaceContainerByContainer();
                    break;
                case "9":
                    TransferContainerFromShipShip();
                    break;
                case "10":
                    PrintInfoContainer();
                    break;
                case "11":
                    PrintInfoShip();
                    break;

            }
        }
    }

    private static Container? FindContainerBySerial(string serialNumber)
    {
        return Containers.FirstOrDefault(
            container => container?.SerialNumber != null &&
                         container.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
    }

    private static ContainerShip? FindShipByName(string shipName)
    {
        return Ships.FirstOrDefault(
            ship => ship.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));
    }

    private static void CreteContainer()
    {
        Console.WriteLine("enter height: ");
        var height = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter tareWeight: ");
        var tareWeight = double.Parse(Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter depth: ");
        var depth = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter maxLoad");
        var maxLoad = double.Parse(Console.ReadLine() ?? string.Empty);
        string serialNumber = null;
        Container? container = null;

        Console.WriteLine("1 - gasContainer; 2 - LiquidContainer; 3 - RefrigeratedContainer:");
        switch (Console.ReadLine())
        {
            case "1":
                serialNumber = $"KON-G-{_containerSerialNumber++}";
                Console.WriteLine("enter pressure:");
                var pressure = double.Parse(Console.ReadLine() ?? string.Empty);
                container = new GasContainer(height, tareWeight, depth, serialNumber, maxLoad, pressure);
                break;
            case "2":
                serialNumber = $"KON-L-{_containerSerialNumber++}";
                Console.WriteLine("Is hazardous? yes/no");
                var isHazard = Console.ReadLine() == "yes";
                container = new LiquidContainer(height, tareWeight, depth, serialNumber, maxLoad, isHazard);
                break;
            case "3":
                serialNumber = $"KON-R-{_containerSerialNumber++}";
                Console.WriteLine("enter product type: ");
                var productType = (Console.ReadLine() ?? string.Empty);
                Console.WriteLine("enter temperature: ");
                var temperature = double.Parse(Console.ReadLine() ?? string.Empty);
                container = new RefrigeratedContainer(height, tareWeight, depth, serialNumber, maxLoad, productType,
                    temperature);
                break;
            default:
                Console.WriteLine("Invalid container type selected.");
                break;
        }

        if (container != null) Containers.Add(container);
    }

    private static void CreateShip()
    {
        Console.WriteLine("enter ship name");
        var name = (Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter maxSpeed: ");
        var maxSpeed = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter maxContainers: ");
        var maxContainerNum = int.Parse(Console.ReadLine() ?? string.Empty);
        Console.WriteLine("enter maxWeight: ");
        var maxWeight = double.Parse(Console.ReadLine() ?? string.Empty);
        ContainerShip ship = new ContainerShip(name, maxSpeed, maxContainerNum, maxWeight);
        Ships.Add(ship);
    }

    private static void LoadCargoContainer()
    {
        Console.WriteLine("Enter container serial number:");
        var serial = Console.ReadLine();
        Console.WriteLine("Enter cargo weight to load:");
        var mass = double.Parse(Console.ReadLine() ?? string.Empty);

        if (serial == null) return;
        var container = FindContainerBySerial(serial);

        if (container != null)
        {
            if (container is RefrigeratedContainer refrigeratedContainer)
            {
                Console.WriteLine("enter product type:");
                var productType = Console.ReadLine();

                if (productType != null)
                {
                    var minTemp = refrigeratedContainer.CalcMinTemp(productType);
            
                    if (productType != refrigeratedContainer.ProductType || refrigeratedContainer.Temperature < minTemp)
                    {
                        Console.WriteLine($"Cannot load {productType}. Either product type does not match or temperature is too low.");
                        return;
                    }
                }
            }
            
            container.Load(mass);
            Console.WriteLine("Cargo loaded.");
        }
        else
        {
            Console.WriteLine("Container not found or max load exceeded.");
        }
    }

    private static void LoadContainerShip()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the container's serial number:");
        var serialNumber = Console.ReadLine() ?? string.Empty;


        var ship = FindShipByName(shipName);
        var container = FindContainerBySerial(serialNumber);

        if (ship != null && container != null)
        {
            ship.LoadContainer(container);
            Containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} loaded onto {shipName}.");
        }
        else
        {
            Console.WriteLine("Ship or container not found.");
        }
    }

    private static void LoadListContainersShip()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;
        var ship = FindShipByName(shipName);
        if (ship != null)
        {
            ship.LoadContainers(Containers);
        }
        else
        {
            Console.WriteLine("ship not found");
        }
    }
    
    private static void RemoveContainerShip()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the container's serial number:");
        var serialNumber = Console.ReadLine() ?? string.Empty;
        
        var ship = FindShipByName(shipName);
   
        if (ship != null)
        {
            ship.RemoveContainer(serialNumber);
        }
        else
        {
            Console.WriteLine("container or ship is not found");
        }
    }

    private static void UnloadContainer()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the container's serial number:");
        var serialNumber = Console.ReadLine() ?? string.Empty;
        
        var ship = FindShipByName(shipName);
   
        if (ship != null)
        {
            ship.UnloadContainer(serialNumber);
        }
        else
        {
            Console.WriteLine("container or ship is not found");
        }
    }

    private static void ReplaceContainerByContainer()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the container's serial number:");
        var oldSerial = Console.ReadLine() ?? string.Empty;
        
        var ship = FindShipByName(shipName);
        
        CreteContainer();
        Console.WriteLine("Enter new container's serial number:");
        var newSerial = Console.ReadLine() ?? string.Empty;
        var newContainer = FindContainerBySerial(newSerial);
        
        if (ship != null && newContainer != null)
        {
            ship.ReplaceContainer(oldSerial,newContainer);
        }
        else
        {
            Console.WriteLine("container or ship is not found");
        }
    }

    private static void TransferContainerFromShipShip()
    {
        Console.WriteLine("Enter the ship's name 1 :");
        var shipName1 = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the ship's name 2 :");
        var shipName2 = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Enter the container's serial number:");
        var serialNumber = Console.ReadLine() ?? string.Empty;
        
        var ship1 = FindShipByName(shipName1);
        var ship2 = FindShipByName(shipName2);
        if (ship1 != null && ship2 != null){
            ship1.TransferContainer(ship1,ship2,serialNumber);
        }
        else
        {
            Console.WriteLine("ship1 or ship1 could not be found");
        }
    }

    private static void HelperPrintCont(Container container)
    {
        Console.WriteLine($"Serial Number: {container.SerialNumber}," +
                          $"Height: {container.Height}, Tare Weight: {container.TareWeight}," +
                          $" Depth: {container.Depth}, Max Load: {container.MaxLoad}, Cargo: {container.CargoWeight}");
        switch (container)
        {
            case GasContainer gasContainer:
                Console.WriteLine($"Pressure: {gasContainer.Pressure} atmospheres");
                break;
            case LiquidContainer liquidContainer:
            {
                var hazardous = liquidContainer.IsHazard ? "Yes" : "No";
                Console.WriteLine($"Is Hazardous: {hazardous}");
                break;
            }
            case RefrigeratedContainer refrigeratedContainer:
                Console.WriteLine($"Product Type: {refrigeratedContainer.ProductType}, Temperature: {refrigeratedContainer.Temperature}°C");
                break;
        }
    }

    private static void HelperPrintShips(ContainerShip ship)
    {
        Console.WriteLine();
        Console.WriteLine($"Ship Name: {ship.Name}, Max Speed: {ship.MaxSpeed}, Max Containers: {ship.MaxContainers}, Max Weight: {ship.MaxWeight}");
        Console.WriteLine();
    }
    private static void PrintInfoContainer()
    {
        Console.WriteLine("Enter the container's serial number:");
        var serialNumber = Console.ReadLine() ?? string.Empty;

        var container = FindContainerBySerial(serialNumber);
        if (container != null)
        { 
            Console.WriteLine();
            HelperPrintCont(container);
           Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }
    private static void PrintInfoShip()
    {
        Console.WriteLine("Enter the ship's name:");
        var shipName = Console.ReadLine() ?? string.Empty;

        var ship = FindShipByName(shipName);
        if (ship != null)
        {
            HelperPrintShips(ship);
            Console.WriteLine("Containers on board:");

            foreach (var container in ship.Containers)
            {
                HelperPrintCont(container);
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }
    private static void PrintContAll()
    {
        if (Containers.Count == 0)
        {
            Console.WriteLine("No containers available.");
            return;
        }

        foreach (var container in Containers.OfType<Container>())
        {
            HelperPrintCont(container);
            Console.WriteLine(); 
        }
    }

    private static void PrintShipsAll()
    {
        if (Ships.Count == 0)
        {
            Console.WriteLine("No ships available.");
            return;
        }
        foreach (var ship in Ships.OfType<ContainerShip>())
        {
            HelperPrintShips(ship);
        }
    }

}