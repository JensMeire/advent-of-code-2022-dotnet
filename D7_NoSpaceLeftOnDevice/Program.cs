// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Xml.Linq;

var lines = File.ReadLines("./data.txt").Skip(1).ToList();
Node root = Node.CreateFolderNode("", null);
string GetCommand(string line) => new string(line.Skip(2).Take(2).ToArray());
string GetChangeDirectoryName(string line) => new string(line.Skip(5).ToArray());
string GetListDirectoryName(string line) => new string(line.Skip(4).ToArray());

var current = root;

foreach(var line in lines)
{
    if (line.StartsWith("$")) {
        var command = GetCommand(line);
        if (command == "cd")
        {
            var name = GetChangeDirectoryName(line);
            if(name == "..")
            {
                current = current!.Parent;
                continue;
            }

            var child = current!.GetChildByName(name);
            current = child;
            continue;
        }
    }

    if(line.StartsWith("dir"))
    {
        var name = GetListDirectoryName(line);
        var newChild = Node.CreateFolderNode(name, current);
        current.AddChild(newChild);
        continue;
    }

    if (char.IsDigit(line[0]))
    {
        var splitted = line.Split(" ");
        var newChild = Node.CreateFileNode(splitted[1], long.Parse(splitted[0]), current);
        current.AddChild(newChild);
        continue;
    }
}

Console.WriteLine("done");
var visitor = new FileSizeVisitor();
root.Accept(visitor);
Console.WriteLine(visitor.Nodes.Sum(x => x.GetSize));

var visitor2 = new ClearSpaceVisitor();
root.Accept(visitor2);
Console.WriteLine(visitor2.Current);

interface IVisitor {
    void Visit(Node node);
}

class FileSizeVisitor: IVisitor
{
    public List<Node> Nodes = new List<Node>();
    public void Visit(Node node)
    {
        if (node.IsFile) return;
        if (node.GetSize > 100000) return;
        Nodes.Add(node);
    }
}

class ClearSpaceVisitor : IVisitor
{
    private long CurrentlyFreeSpace = 0;
    private long MinSizeToDelete = 0;
    public long Current = 0;

    public void Visit(Node node)
    {
        var size = node.GetSize;
        if(node.IsRoot)
        {
            CurrentlyFreeSpace = 70000000 - size;
            MinSizeToDelete = 30000000 - CurrentlyFreeSpace;
            Current = size;
        }

        if (size >= MinSizeToDelete && size <= Current) Current = size;
    }
}

class Node
{
    public Node? Parent { get; set; }
    public List<Node>? Children { get; set; }
    public bool IsFolder { get; set; }
    public bool IsFile => !IsFolder;
    public bool IsRoot => Parent == null;
    public long Size { get; set; }
    public long GetSize => IsFile ? Size : (Children?.Sum(x => x.GetSize) ?? 0);
    public string Name { get; set; }

    public Node()
    {
        Children = null;
        Name = "";
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
        Children?.ForEach(c => c.Accept(visitor));
    }

    public void AddChild(Node child)
    {
        Children!.Add(child);
    }

    public Node? GetChildByName(string name)
    {
        return Children!.FirstOrDefault(child => child.Name == name);
    }

    public static Node CreateFileNode(string name, long size, Node? parent)
    {
        return new Node
        {
            Parent = parent,
            IsFolder = false,
            Size = size,
            Name = name
        };
    }

    public static Node CreateFolderNode(string name, Node? parent)
    {
        return new Node
        {
            Parent = parent,
            IsFolder = true,
            Children = new List<Node>(),
            Name = name
        };
    }
}
