using TreeLib;
using Newtonsoft.Json;

namespace Company;

public class Company : Tree
{
    private Employee _headOfCompany;
    public Company(Employee headOfCompany) : base(headOfCompany)
    {
        _headOfCompany = headOfCompany;
    }
    
    public Employee HeadOfCompany => _headOfCompany;

    public string SerializeToJsonString(Employee currentEmployee)
    {
        var subalternes = currentEmployee._subalternes;
        var subalternesString = "";

        foreach (var subalterne in subalternes)
        {
            subalternesString += SerializeToJsonString(subalterne);
        }
        var resStr = "{" +"\"name\":"+"\""+currentEmployee._firstName+" "+currentEmployee._lastName+"\""+","+"\"position\":"+"\""+currentEmployee._position+"\","+"\"subalternes\":["+subalternesString+"]"+"}";
        
        return resStr;
    }

    public object Deserialize(string path)
    {
        var jsonString = File.ReadAllLines(path)[0];
        Company root = JsonConvert.DeserializeObject<Company>(jsonString);
        return root;
    }
    
}

