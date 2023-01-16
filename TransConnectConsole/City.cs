namespace TransConnectConsole;

public class City : Node
{
    private string _name;

    public City(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}