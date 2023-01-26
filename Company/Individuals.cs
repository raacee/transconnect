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

    public List<Employee> _subordinates { get; set; }
    
    public Employee(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, DateTime entryTime, string position, int salary, Sex sex, List<Employee> subordinates) : base(ssNum, firstName, lastName,
        birthDate, phone, address, email, sex)
    {
        _entryTime = entryTime;
        _position = position;
        _salary = salary;
        _subordinates = subordinates;
    }
    
    public override string ToString()
    {
        return _firstName + " " + _lastName;
    }

    public string AllFieldsString()
    {
        return "1 - Numéro de sécurité sociale :" + _ssNum + "\n"
               + "2 - Prénom" + _firstName + "\n"
               + "3 - Nom de famille :" + _lastName + "\n"
               + "4 - Date de naissance :" + _birthDate + "\n"
               + "5 - Téléphone : " + _phone + "\n"
               + "6 - Addresse :" + _address + "\n"
               + "7 - Adresse email :" + _email + "\n"
               + "8 - Sexe :" + _sex + "\n"
               + "9 - Date d'entrée dans l'entreprise : " + _entryTime + "\n"
               + "10 - Poste :" + _position + "\n"
               + "11 - Salaire :" + _salary;

    }

    public void DisplayModifiables()
    {
        Console.WriteLine(
            "1 - Prénom : " + _firstName + "\n"
            + "2 - Nom de famille : " + _lastName + "\n"
            + "3 - Téléphone : " + _phone + "\n"
            + "4 - Addresse : " + _address + "\n"
            + "5 - Adresse email : " + _email + "\n"
            + "6 - Poste : " + _position + "\n"
            + "7 - Salaire : " + _salary);
    }
}

public class Client : Person, IComparable<Client>
{
    public List<Order> _orders { get; set; }

    public Client(string ssNum, string firstName, string lastName, DateTime birthDate, string phone, string address,
        string email, Sex sex, List<Order> orders) : base(ssNum, firstName, lastName, birthDate, phone, address, email,
        sex)
    {
        _orders = orders;
    }

    public override string ToString()
    {
        var res = "";
        
        res += _firstName + " " + _lastName +"\n";

        foreach (var order in _orders)
        {
            res += "\t" + order.Id + "\n" + order.Price;
        }

        return res;
    }

    public int CompareTo(Client? otherClient)
    {
        return String.CompareOrdinal(this._lastName + " " + this._firstName, otherClient?._lastName + " " + otherClient?._firstName);
    }
}
