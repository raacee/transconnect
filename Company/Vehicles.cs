namespace Company;

public enum VehicleType
{
    Car,
    Van,
    Truck
}

public enum Cargo
{
    People,
    Liquids,
    Colds
}

interface ITruck
{
    Cargo Contains();
}

public class Vehicle
{
    private Driver _driver;

    protected Vehicle(Driver driver)
    {
        _driver = driver;
    }
}

public class Truck : Vehicle, ITruck
{
    private Cargo _cargo;
    protected Truck(Driver driver, Cargo cargo) : base(driver)
    {
        this._cargo = cargo;
    }

    public Cargo Contains()
    {
        return this._cargo;
    }
}