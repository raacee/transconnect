namespace Cities;

public abstract class Graph
{
    private List<Node> _nodes;

    protected Graph(List<Node> nodes)
    {
        _nodes = nodes;
    }
}

public abstract class Node
{
    private List<Node> _edges;

    protected Node(List<Node> edges)
    {
        _edges = edges;
    }
}