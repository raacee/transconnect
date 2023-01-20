namespace TransConnectConsole;

public enum VehicleType
{
    Car,
    Van,
    Truck
}

public class Vehicle
{
    private Driver _driver;
    
    protected Vehicle(Driver driver)
    {
        _driver = driver;
    }
}
