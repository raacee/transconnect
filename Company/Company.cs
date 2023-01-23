using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace Company;

public class Company
{
    private Employee _headOfCompany;

    public Employee HeadOfCompany => _headOfCompany;

    private List<Client> _clients;

    public Company()
    {
        _clients = new List<Client>(0);
        var json = File.ReadAllText("/home/racel/RiderProjects/transconnect/Company/company.json");
        Employee? root = JsonConvert.DeserializeObject<Employee>(json);
        if (root != null)
        {
            _headOfCompany = root;
        }
        else throw new Exception("Root of company is null, check json");
    }
    
    public Company(List<Client> clients)
    {
        _clients = clients;
        var json = File.ReadAllText("/home/racel/RiderProjects/transconnect/Company/company.json");
        Employee? root = JsonConvert.DeserializeObject<Employee>(json);
        if (root != null)
        {
            _headOfCompany = root;
        }
        else throw new Exception("Root of company is null, check json");
    }

    public Employee? SearchByName(string firstname, string lastname)
    {
        return SearchByName(_headOfCompany, firstname, lastname);
    }

    private Employee? SearchByName(Employee employee, string firstname, string lastname)
    {
        if (employee._firstName == firstname && employee._lastName == lastname)
        {
            return employee;
        }

        foreach (var subordinate in employee._subordinates)
        {
            var found = SearchByName(subordinate, firstname, lastname);
            if (found != null) return found;
        }

        return null;
    }

    public Employee? SearchBySSnum(string ssNum)
    {
        return SearchBySSnum(_headOfCompany, ssNum);
    }
    
    private Employee? SearchBySSnum(Employee employee, string ssNum)
    {
        if (employee._ssNum == ssNum)
        {
            return employee;
        }

        foreach (var subordinate in employee._subordinates)
        {
            var found = SearchBySSnum(subordinate, ssNum);
            if (found != null) return found;
        }

        return null;
    }

    public void AddEmployee(Employee newEmployee, Employee superior)
    {
        superior._subordinates.Add(newEmployee);
    }
    
    //TODO:implement these methods
    //to implement
    public void RemoveEmployee(Employee employee)
    {
        
    }
    
    //to implement
    public Employee GetSuperior(Employee employee)
    {
        return null;
    }
    
    
    
    public void SaveToJson()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_headOfCompany);
        File.WriteAllText("/home/racel/RiderProjects/transconnect/Company/company.json",json);
    }
}

