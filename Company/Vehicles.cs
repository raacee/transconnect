namespace Company;

public enum VehicleType
{
    Car,
    Van,
    Truck
}

interface ITruck<out T>
{
    T Contains();
}

public class Vehicle
{
    private Driver _driver;
    
    protected Vehicle(Driver driver)
    {
        _driver = driver;
    }
}

internal class Cargo
{
    
}