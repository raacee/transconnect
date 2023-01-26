namespace Company
{
    public class Driver : Employee
    {
        private bool _isWorking;
        private Vehicle _vehicle;
        private List<Order> _orders;

        public List<Order> Orders => _orders;

        public Driver(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address, string email, DateTime entryTime, string position, int salary, bool isWorking, Sex sex, Vehicle vehicle, List<Order> orders) : base(ssNum, firstName, lastName, birthDate, phone, address, email, entryTime, "Driver", salary, sex, new List<Employee>(0))
        {
            _isWorking = isWorking;
            _vehicle = vehicle;
            _orders = orders;
        }

        public override string ToString()
        {
            var res = "";
            res += base.ToString()+"\n";
            foreach (var order in _orders)
            {
                res+="\t"+order+"\n";
            }
            return res;
        }
    }
}