// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Numerics;
using System.Security;

Console.WriteLine("Hello, World!");
Advent16 advent16 = new Advent16();
advent16.Start();
public class Advent16
{

    StreamReader sr = new StreamReader("Input.txt");
    List<Node> nodes = new List<Node>();
    PriorityQueue<Node, int> nodeQueue = new PriorityQueue<Node, int>();
    List<Node> visitedNodes = new List<Node>();
    Dictionary<Vector2, Node> nodeMap = new Dictionary<Vector2, Node>();
    int[] oren = new int[4] { 0, 0, 0, 1 };
    Node startNode;
    Node endNode;
    int minDist = 0;
    public void Start()
    {
        int row = 0;
        while (sr.ReadLine() is { } line)
        {
            Console.WriteLine(row);
            var parts = line.ToCharArray();
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == '.')
                {
                    Node node = new Node(new Vector2(i, row));
                    nodes.Add(node);
                    nodeMap[new Vector2(i, row)] = node;
                }
                else if (parts[i] == 'S')
                {
                    Node node = new Node(new Vector2(i, row));
                    nodes.Add(node);
                    nodeMap[new Vector2(i, row)] = node;
                    startNode = node;
                }
                else if (parts[i] == 'E')
                {
                    Node node = new Node(new Vector2(i, row));
                    nodes.Add(node);
                    nodeMap[new Vector2(i, row)] = node;
                    endNode = node;
                }
            }
            row += 1;
        }
        ConnectedNodes();
        FindShortestPath();
    }
    public void ConnectedNodes()
    {
        foreach (var node in nodes)
        {
            if (nodeMap.ContainsKey(new Vector2(node.po.X, node.po.Y + 1))) node.connectedNodes.Add(nodeMap[new Vector2(node.po.X, node.po.Y + 1)]);
            if (nodeMap.ContainsKey(new Vector2(node.po.X, node.po.Y - 1))) node.connectedNodes.Add(nodeMap[new Vector2(node.po.X, node.po.Y - 1)]);
            if (nodeMap.ContainsKey(new Vector2(node.po.X + 1, node.po.Y))) { node.connectedNodes.Add(nodeMap[new Vector2(node.po.X + 1, node.po.Y)]); }
            if (nodeMap.ContainsKey(new Vector2(node.po.X - 1, node.po.Y))) node.connectedNodes.Add(nodeMap[new Vector2(node.po.X - 1, node.po.Y)]);
        }
    }

    public void FindShortestPath()
    {
        nodeQueue.Enqueue(startNode, 0);

        while (nodeQueue.Count > 0)
        {
            Node node = nodeQueue.Dequeue();
            for (int i = 0; i < node.connectedNodes.Count; i++)
            {
                if (!visitedNodes.Contains(node.connectedNodes[i]))
                {
                    int nodeDist = 1;
                    if (node.connectedNodes[i].po == new Vector2(node.po.X, node.po.Y - 1))
                    {
                        if (node.oren[0] != 1)
                            nodeDist += 1000;
                        node.connectedNodes[i].oren = new int[] { 1, 0, 0, 0 };
                    }
                    else if (node.connectedNodes[i].po == new Vector2(node.po.X, node.po.Y + 1))
                    {
                        if (node.oren[1] != 1)
                            nodeDist += 1000;
                        node.connectedNodes[i].oren = new int[] { 0, 1, 0, 0 };
                    }
                    else if (node.connectedNodes[i].po == new Vector2(node.po.X - 1, node.po.Y))
                    {
                        if (node.oren[2] != 1)
                            nodeDist += 1000;
                        node.connectedNodes[i].oren = new int[] { 0, 0, 1, 0 };
                    }
                    else if (node.connectedNodes[i].po == new Vector2(node.po.X + 1, node.po.Y))
                    {
                        if (node.oren[3] != 1)
                            nodeDist += 1000;
                        node.connectedNodes[i].oren = new int[] { 0, 0, 0, 1 };
                    }

                    if (node.connectedNodes[i].shortestDist > nodeDist + node.shortestDist || node.connectedNodes[i].shortestDist < 0)
                    {

                        node.connectedNodes[i].shortestDist = nodeDist + node.shortestDist;
                        node.connectedNodes[i].prevNode = node;
                        if (node.connectedNodes[i] == endNode)
                            Console.WriteLine(node.connectedNodes[i].prevNode.po);
                        nodeQueue.Enqueue(node.connectedNodes[i], node.connectedNodes[i].shortestDist);
                    }

                }
                visitedNodes.Add(node);
            }
        }
        Node curNode = endNode;
        minDist = endNode.shortestDist;
        while (curNode != startNode)
        {
            Console.WriteLine(curNode.po);
            Console.WriteLine("prevNode");
            Console.WriteLine(curNode.prevNode.po);
            curNode = curNode.prevNode;
        }
        Console.WriteLine(minDist + 1);

    }
}

public class Node(Vector2 pos)
{
    public Vector2 po = pos;
    public int shortestDist = -1;
    public Node prevNode = null;
    public List<Node> connectedNodes = new List<Node>();
    public int[] oren = new int[4] { 0, 0, 0, 1 };
}
