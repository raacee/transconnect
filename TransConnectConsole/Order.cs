namespace TransConnectConsole;

public class Order
{
    private Client _client;
    private City _cityOfOrigin;
    private City _cityOfArrival;
    private DateTime _deliveryDate;
    private string _id;
    private Driver _driver;

    public Order(DateTime deliveryDate, string id, Client client, City cityOfOrigin, City cityOfArrival, Driver driver)
    {
        _deliveryDate = deliveryDate;
        _id = id;
        _client = client;
        _cityOfOrigin = cityOfOrigin;
        _cityOfArrival = cityOfArrival;
        _driver = driver;
    }

    public string Id => _id;

    public Client Client => _client;

    public City CityOfOrigin => _cityOfOrigin;
    
    public DateTime DeliveryDate => _deliveryDate;

    public override string ToString()
    {
        return "client : " + _client + "\n" + "city of origin : " + _cityOfOrigin + "\n" + "city of arrival : " +
               _cityOfArrival + "delivery date" + "\n" + "driver : " + _driver;
    }
}