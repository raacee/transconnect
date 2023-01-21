namespace TreeLib;

public class Node {
    private int _data;
    private string _name;
    private List<Node> _children;

    public Node(int data = 0, string name = "") {
        this._data = data;
        this._name = name;
        this._children = new List<Node>(0);
    }

    public List<Node> Children => _children;

    public string Name => _name;

    public int Data => _data;
}

class Tree {
    private Node _root;

    public Tree(int data, string name) {
        _root = new Node(data, name);
    }

    public void AddChild(Node parent, int data, string name) {
        Node child = new Node(data, name);
        parent.Children.Add(child);
    }
    
    public Node FindNodeByName(string name) {
        return FindNodeByName(_root, name);
    }
    
    private Node FindNodeByName(Node current, string name) {
        if (current.Name == name) {
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
    
}