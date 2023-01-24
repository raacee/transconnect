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
    
    public void RemoveEmployee(Employee employeeToRemove)
    {
        var superior = GetSuperior(employeeToRemove);
        if (superior != null && superior._subordinates.Remove(employeeToRemove))
        {
            return;
        }
        else
        {
            throw new Exception("Error in RemoveEmployee, employee to be removed is not in the tree");
        }
    }

    public Employee? GetSuperior(Employee employee)
    {
        return GetSuperior(_headOfCompany, employee);
    }
    
    private Employee? GetSuperior(Employee currentEmployee, Employee employee)
    {
        if (currentEmployee._subordinates.Contains(employee)) return currentEmployee;
        foreach (var subordinate in currentEmployee._subordinates)
        {
            var found = GetSuperior(subordinate,employee);
            if (found != null) return found;
        }

        return null;
    }

    public void SaveToJson()
    {
        var json = JsonConvert.SerializeObject(_headOfCompany);
        File.WriteAllText("/home/racel/RiderProjects/transconnect/Company/company.json",json);
    }

    public string Treeify()
    {
        return Treeify(_headOfCompany);
    }

    private string Treeify(Employee currentEmployee, int index = 0)
    {
        Console.WriteLine(currentEmployee);
        var name = currentEmployee._firstName + " " + currentEmployee._lastName;
        var branchChar = "├── ";
        var res = "";
        if (currentEmployee._subordinates.Count == 0 || currentEmployee._subordinates == null) return "";
        foreach (var subordinate in currentEmployee._subordinates)
        {
            for (int i = 0; i < index; i++)
            {
                res += "\t";
            }

            res += branchChar + name;
            res += Treeify(subordinate, index + 1);
        }

        return res;
    }
    
}

