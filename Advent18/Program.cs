// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Security;
Console.WriteLine("Hello, World!");
Advent16 advent16 = new Advent16();
advent16.Start();
public class Advent16
{

    StreamReader sr = new StreamReader("Input.txt");
    StreamReader fallingBlocks = new StreamReader("BlockCords.txt");
    List<Node> nodes = new List<Node>();
    PriorityQueue<Node, int> nodeQueue = new PriorityQueue<Node, int>();
    HashSet<Node> visitedNodes = new HashSet<Node>();
    Dictionary<Vector2, Node> nodeMap = new Dictionary<Vector2, Node>();
    List<Node> blocked = new List<Node>();
    Node startNode;
    Node endNode;
    int minDist = 0;
    public void Start()
    {
        while (fallingBlocks.ReadLine() is { } blockLine)
        {
            var blockParts = blockLine.Split(',');
            Vector2 blockPos = new Vector2(int.Parse(blockParts[0]), int.Parse(blockParts[1]));
            Node blockNode = new Node(blockPos);
            blocked.Add(blockNode);
        }
        int row = 0;
        while (sr.ReadLine() is { } line)
        {
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
        for (int i = 0; i <= 2077; i++)
        {
            Console.WriteLine(blocked[i].po);
            nodeMap.Remove(blocked[i].po);
        }
        ConnectedNodes();
        FindShortestPath();
    }
    public void ConnectedNodes()
    {
        foreach (var node in nodes)
        {
            // Skip the blocked nodes and don't add neighbors for them
            if (blocked.Contains(node)) continue;

            // Connect neighbors, but skip blocked nodes
            if (nodeMap.ContainsKey(new Vector2(node.po.X, node.po.Y + 1)) && !blocked.Contains(nodeMap[new Vector2(node.po.X, node.po.Y + 1)]))
                node.connectedNodes.Add(nodeMap[new Vector2(node.po.X, node.po.Y + 1)]);

            if (nodeMap.ContainsKey(new Vector2(node.po.X, node.po.Y - 1)) && !blocked.Contains(nodeMap[new Vector2(node.po.X, node.po.Y - 1)]))
                node.connectedNodes.Add(nodeMap[new Vector2(node.po.X, node.po.Y - 1)]);

            if (nodeMap.ContainsKey(new Vector2(node.po.X + 1, node.po.Y)) && !blocked.Contains(nodeMap[new Vector2(node.po.X + 1, node.po.Y)]))
                node.connectedNodes.Add(nodeMap[new Vector2(node.po.X + 1, node.po.Y)]);

            if (nodeMap.ContainsKey(new Vector2(node.po.X - 1, node.po.Y)) && !blocked.Contains(nodeMap[new Vector2(node.po.X - 1, node.po.Y)]))
                node.connectedNodes.Add(nodeMap[new Vector2(node.po.X - 1, node.po.Y)]);

        }
    }

    public void FindShortestPath()
    {
        nodeQueue.Enqueue(startNode, 0);

        while (nodeQueue.Count > 0)
        {
            Node node = nodeQueue.Dequeue();
            
            /*Console.WriteLine(node.po);*/
            if (node == endNode) { break; }
            if (visitedNodes.Contains(node)) continue;
            visitedNodes.Add(node);
            foreach (var neighbor in node.connectedNodes)
            {

                if (visitedNodes.Contains(neighbor)) continue;
                int nodeDist = 1;
                int newDist = node.shortestDist + nodeDist;
                if (node == endNode) Console.WriteLine("Done");
                if (neighbor.shortestDist < 0 || newDist < neighbor.shortestDist)
                {
                    neighbor.shortestDist = newDist;
                    neighbor.prevNode = node;
                    nodeQueue.Enqueue(neighbor, newDist);
                }

            }
        }
        Node curNode = endNode;
        minDist = endNode.shortestDist;

        while (curNode != startNode)
        {
            /*Console.WriteLine(curNode.po);
            Console.WriteLine("prevNode");*/
            /*Console.WriteLine(curNode.prevNode.po);*/
            curNode = curNode.prevNode;
        }
        Console.WriteLine("minDist: " + (minDist));

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
