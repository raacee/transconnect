namespace Company
{
    public class Driver : Employee
    {
        private bool _isWorking;
        private Vehicle _vehicle;
        public Driver(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address, string email, DateTime entryTime, string position, int salary, bool isWorking, Sex sex, Vehicle vehicle) : base(ssNum, firstName, lastName, birthDate, phone, address, email, entryTime, "Driver", salary, sex, new List<Employee>(0))
        {
            _isWorking = isWorking;
            _vehicle = vehicle;
        }
    }
}