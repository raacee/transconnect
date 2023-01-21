using TreeLib;

namespace Company;

public enum Sex
{
    Male,
    Female
}

public abstract class Person : Node
{
    private string _ssNum;
    private string _firstName;
    private string _lastName;
    private DateTime _birthDate;
    private string _phone;
    private string _address;
    private string _email;
    private Sex _sex;

    protected Person(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, Sex sex) : base()
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

    #region Properties

    public string Email
    {
        get => _email;
        set => _email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Address
    {
        get => _address;
        set => _address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Phone
    {
        get => _phone;
        set => _phone = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string SsNum
    {
        get => _ssNum;
        set => _ssNum = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Sex Sex => _sex;

    #endregion
}

public class Employee : Person
{
    private DateTime _entryTime;
    private string _position;
    private int _salary;
    private List<Employee> _subalternes;

    public List<Employee> Subalternes => _subalternes;

    public Employee(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, DateTime entryTime, string position, int salary, Sex sex, List<Employee> subalternes) : base(ssNum, firstName, lastName,
        birthDate, phone, address, email, sex)
    {
        _entryTime = entryTime;
        _position = position;
        _salary = salary;
        this._subalternes = subalternes;
    }

    public int Salary => _salary;

    public string Position => _position;

    public DateTime EntryTime => _entryTime;
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