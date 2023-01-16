namespace TransConnectConsole;

public class Driver : Employee
{
    private bool _isWorking;
    public Driver(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address, string email, DateTime entryTime, string position, int salary, bool isWorking) : base(ssNum, firstName, lastName, birthDate, phone, address, email, entryTime, "Driver", salary)
    {
        _isWorking = isWorking;
    }
}