// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

Advent20 advent = new Advent20();
advent.Run();
public class Advent20
{
    StreamReader sr = new StreamReader("Input.txt");
    List<Node> nodes = new List<Node>();
    Queue<Node> nodeQueue = new Queue<Node>();
    HashSet<Node> visitedNodes = new HashSet<Node>();
    HashSet<Node> visitedCheatNodes = new HashSet<Node>();
    Dictionary<Vector2, Node> nodeMap = new Dictionary<Vector2, Node>();
    Dictionary<Node, ulong> posInFullPath = new Dictionary<Node, ulong>();
    List<Node> blocked = new List<Node>();
    List<Vector2> walls = new List<Vector2>();
    Node startNode;
    Node endNode;
    List<ulong> pathLenghts = new List<ulong>();
    ulong pathsSaved100 = 0;
    ulong maxLength = 0;

    int timeNeededToBeSaved = 100;
    public void Run()
    {
        int row = 0;
        while (sr.ReadLine() is { } line)
        {
            var parts = line.ToCharArray();
            for (int i = 0; i < parts.Length; i++)
            {
                Vector2 position = new Vector2(i, row);
                if (parts[i] == '.')
                {
                    Node node = new Node(position);
                    nodes.Add(node);
                    nodeMap[position] = node;
                }
                else if (parts[i] == 'S')
                {
                    Node node = new Node(position);
                    nodes.Add(node);
                    nodeMap[position] = node;
                    startNode = node;
                }
                else if (parts[i] == 'E')
                {
                    Node node = new Node(position);
                    nodes.Add(node);
                    nodeMap[position] = node;
                    endNode = node;
                }
            }
            row += 1;
        }
        ConnectedNodes();
        /*FindShortestPath(startNode, 0);*/
        FindShortestPathBFS(startNode);
        Console.WriteLine(visitedNodes.Count);
        List<Vector2> possibleCheats = new List<Vector2>();
        foreach (var direction in new[] { new Vector2(0, 2), new Vector2(0, -2), new Vector2(2, 0), new Vector2(-2, 0) })
        {
            possibleCheats.Add(direction);
        }
        foreach (Node node in visitedNodes)
        {
            foreach (var direction in possibleCheats)
            {
                Vector2 neighborPos = node.po + direction;
                if (nodeMap.ContainsKey(neighborPos))
                {
                    Console.WriteLine("prevPos: " + posInFullPath[node]);
                    if (cheat(nodeMap[neighborPos], posInFullPath[node]))
                    {
                        pathsSaved100 += 1;
                        Console.WriteLine(pathsSaved100);
                    }
                }

            }
        }
        Console.WriteLine(pathsSaved100);
    }

    public void ConnectedNodes()
    {
        foreach (var node in nodes)
        {
            foreach (var direction in new[] { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) })
            {
                Vector2 neighborPos = node.po + direction;
                if (nodeMap.ContainsKey(neighborPos))
                {
                    node.connectedNodes.Add(nodeMap[neighborPos]);
                }
            }
        }
    }


    public void FindShortestPathBFS(Node startNode)
    {
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(startNode);

        Dictionary<Node, ulong> distances = new Dictionary<Node, ulong>();
        posInFullPath.Add(startNode, 0);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();

            ulong currentDistance = posInFullPath[currentNode];
            visitedNodes.Add(currentNode);

            if (currentNode == endNode)
            {
                maxLength = currentDistance;
                pathLenghts.Add(maxLength);
                return;
            }

            foreach (var connectedNode in currentNode.connectedNodes)
            {
                if (connectedNode != null && !posInFullPath.ContainsKey(connectedNode))
                {
                    posInFullPath.Add(connectedNode, 0);
                    posInFullPath[connectedNode] = currentDistance + 1;
                    Console.WriteLine(posInFullPath[connectedNode]);
                    queue.Enqueue(connectedNode);
                }
            }
        }
    }
    public bool cheat(Node node, ulong prevNodePos)
    {
        ulong currentPos = posInFullPath[node];

        if ((int)(currentPos - prevNodePos) > timeNeededToBeSaved)
        {
            Console.WriteLine("So true");
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class Node(Vector2 pos)
{
    public bool hasCheated = false;
    public Vector2 po = pos;
    public int shortestDist = -1;
    public Node prevNode = null;
    public List<Node> connectedNodes = new List<Node>();
    public int[] oren = new int[4] { 0, 0, 0, 1 };

}