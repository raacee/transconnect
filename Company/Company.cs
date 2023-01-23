using Newtonsoft.Json;
using TreeLib;

namespace Company;

public class Company : ITree
{
    private Employee _headOfCompany;
    public Company() : base()
    {
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

    private Employee? SearchByName(Employee currentEmployee, string firstname, string lastname)
    {
        if (currentEmployee._subalternes.Count == 0) return null;
        var employee = currentEmployee._subalternes.Find(
            delegate(Employee employee)
            {
                return employee._firstName == firstname && employee._lastName == lastname;
            });
        if (employee != null)
        {
            return employee;
        }
        foreach (var subalterne in currentEmployee._subalternes)
        {
            var newEmp = SearchByName(subalterne, firstname, lastname);
            if (newEmp != null) return newEmp;
        }
        return null;
    }

    public void SaveToJson()
    {
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(_headOfCompany);
        File.WriteAllText("/home/racel/RiderProjects/transconnect/Company/company-out.json",json);
    }
}

