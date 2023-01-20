namespace GraphLib;

public class City : Node
{
    private string _name;

    public City(List<Node> edges, string name) : base(edges)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}