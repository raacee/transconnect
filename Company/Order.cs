using Cities;

namespace Company;

public class Order
{
    private Client _client;
    private City _cityOfOrigin;
    private City _cityOfArrival;
    private DateTime _orderDate;
    private string _id;
    private Driver? _driver;
    private List<City> _citiesShortestPath;

    public string Id => _id;
    public Client Client => _client;
    public City CityOfOrigin => _cityOfOrigin;    
    public DateTime OrderDate => _orderDate;
    public List<City> CitiesShortestPath => _citiesShortestPath;

    public Order(DateTime orderDate, string id, Client client, City cityOfOrigin, City cityOfArrival, List<City> _citiesPath,Driver? driver = null)
    { 
        _orderDate = orderDate;
        _id = id;
        _client = client;
        _cityOfOrigin = cityOfOrigin;
        _cityOfArrival = cityOfArrival;
        _driver = driver;
        _citiesShortestPath = _citiesPath;
    }

    

    public override string ToString()
    {
        return "client : " + _client + "\n" + "city of origin : " + _cityOfOrigin + "\n" + "city of arrival : " +
               _cityOfArrival + "delivery date" + "\n" + "driver : " + _driver;
    }
}