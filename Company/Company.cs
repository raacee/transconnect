using Newtonsoft.Json;

namespace Company;

public class Company
{
    private Employee _headOfCompany;
    private List<Order> _orders;
    private List<Client> _clients;
    
    public Employee HeadOfCompany => _headOfCompany;

    public Company()
    {
        var employeesJson = File.ReadAllText("../../../../Company/company.json");
        _headOfCompany = JsonConvert.DeserializeObject<Employee>(employeesJson) ?? throw new InvalidOperationException();
        //read clients from json
        var clientsJson = File.ReadAllText("../../../../Company/clients.json");
        _clients = JsonConvert.DeserializeObject<List<Client>>(clientsJson) ?? throw new InvalidOperationException();
        //read orders from json
        var ordersJson = File.ReadAllText("../../../../Company/orders.json");
        _orders = JsonConvert.DeserializeObject<List<Order>>(ordersJson) ?? throw new InvalidOperationException();
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

    private Employee? SearchByName(Employee employee, string? firstname, string lastname)
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
        var employeesJson = JsonConvert.SerializeObject(_headOfCompany);
        File.WriteAllText("../../../../Company/company.json",employeesJson);
        var ordersJson = JsonConvert.SerializeObject(_orders);
        File.WriteAllText("../../../../Company/orders.json",ordersJson);
        var clientsJson = JsonConvert.SerializeObject(_clients);
        File.WriteAllText("../../../../Company/clients.json",clientsJson);
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
    
    public static void PrintEmployeeTree(List<Employee> employees, int level)
    {
        foreach (var employee in employees)
        {
            for (int i = 0; i < level; i++)
            {
                Console.Write("  ");
            }
            Console.WriteLine("- {0}", employee);
            PrintEmployeeTree(employee._subordinates, level + 1);
        }
    }
}
    


