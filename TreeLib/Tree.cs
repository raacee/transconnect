namespace TreeLib;

public class Node {
    private string _data;
    private List<Node> _children;

    public Node(string data)
    {
        this._data = data;
        this._children = new List<Node>(0);
    }

    public Node(int data = 0, string name = "") {
        this._data = data.ToString();
        this._children = new List<Node>(0);
    }

    public List<Node> Children => _children;

    public string Data => _data;
}

public abstract class Tree {
    private Node _root;

    protected Tree(int data, string name) {
        _root = new Node(data, name);
    }

    protected Tree(Node root)
    {
        this._root = root;
    }

    protected Tree()
    {
        this._root = new Node();
    }
    
    public void AddChild(Node parent, int data, string name) {
        Node child = new Node(data, name);
        parent.Children.Add(child);
    }
    
    public Node FindNodeByName(string name) {
        return FindNodeByName(_root, name);
    }
    
    private Node FindNodeByName(Node current, string name) {
        if (current.Data == name) {
            return current;
        }
        foreach (Node child in current.Children) {
            Node found = FindNodeByName(child, name);
            if (found != null) {
                return found;
            }
        }
        return null;
    }
    /*
    public void DisplayTree() {
        DisplayTree(_root, 0);
    }
    
    private void DisplayTree(Node current, int level) {
        for (int i = 0; i < level; i++) {
            Console.Write("  ");
        }
        Console.WriteLine(current.Name);
        foreach (Node child in current.Children) {
            DisplayTree(child, level + 1);
        }
    }
    */
}