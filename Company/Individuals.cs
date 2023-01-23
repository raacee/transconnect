using TreeLib;

namespace Company;

public enum Sex
{
    Male,
    Female
}

public abstract class Person
{
    public string _ssNum {get;set;}
    public string _firstName {get;set;}
    public string _lastName {get;set;}
    public DateTime _birthDate {get;set;}
    public string _phone {get;set;}
    public string _address {get;set;}
    public string _email {get;set;}
    public Sex _sex {get;set;}

    protected Person(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, Sex sex)
    {
        _ssNum = ssNum;
        _firstName = firstName;
        _lastName = lastName;
        _birthDate = birthDate;
        _phone = phone;
        _address = address;
        _email = email;
        _sex = sex;
    }
}

public class Employee : Person
{
    public DateTime _entryTime {get;set;}
    public string _position {get;set;}
    public int _salary {get;set;}

    public List<Employee> _subalternes { get; set; }
    
    public Employee(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, DateTime entryTime, string position, int salary, Sex sex, List<Employee> subalternes) : base(ssNum, firstName, lastName,
        birthDate, phone, address, email, sex)
    {
        _entryTime = entryTime;
        _position = position;
        _salary = salary;
        _subalternes = subalternes;
    }
}

public class Client : Person
{
    private List<Order> _orders;

    public Client(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, List<Order> orders, Sex sex) : base(ssNum, firstName, lastName, birthDate, phone, address, email,
        sex)
    {
        _orders = orders;
    }
}
