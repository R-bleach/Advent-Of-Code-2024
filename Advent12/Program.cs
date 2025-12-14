// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine("Hello, World!");

Advent12 advent12 = new Advent12();
advent12.Start();


public class Advent12
{
    int fenceCount = 0;
    int shapes = 0;
    Dictionary<int, List<Vector2>> fences = new Dictionary<int, List<Vector2>>();
    StreamReader sr = new StreamReader("Input.txt");
    List<char[]> points = new List<char[]>();
    Dictionary<char, List<Vector2>> visited = new Dictionary<char, List<Vector2>>();
    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            var report = line.ToCharArray();
            points.Add(report);
        }
        FindStartingPoint(points);
        Console.WriteLine(fenceCount);
    }
    public void FindStartingPoint(List<char[]> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < points[i].Length; j++)
            {
                Console.WriteLine(points[i][j]);
                if (!visited.ContainsKey(points[i][j]))
                {
                    visited.Add(points[i][j], new List<Vector2>());
                }
                if (!visited[points[i][j]].Contains(new Vector2(i, j)))
                {
                    fences.Add(shapes, new List<Vector2>());
                    Vector2 path = PathFinding(points, j, i, 0, 0);
                    shapes++;
                    Console.WriteLine(path.X + " * " + path.Y);
                    fenceCount += (int)(path.X * path.Y);
                }

            }
        }
        checkSameFence();
    }


    public Vector2 PathFinding(List<char[]> points, int X, int Y, int numberOfPlots, int numbOfFences)
    {
        if (!visited[points[Y][X]].Contains(new Vector2(Y, X)))
        {
            visited[points[Y][X]].Add(new Vector2(Y, X));
        }
        else return new Vector2(0, 0);
        numberOfPlots++;

        Vector2 plots = new Vector2(0, 0);
        if (Y + 1 >= points.Count || points[Y + 1][X] != points[Y][X])
        {
            fences[shapes].Add(new Vector2(Y+1, X));
            numbOfFences++;
        }
        else plots += PathFinding(points, X, Y + 1, 0, 0);
        if (X + 1 >= points[Y].Length || points[Y][X + 1] != points[Y][X])
        {
            fences[shapes].Add(new Vector2(Y, X+1));
            numbOfFences++;
        }
        else plots += PathFinding(points, X + 1, Y, 0, 0);
        if (Y - 1 < 0 || points[Y - 1][X] != points[Y][X])
        {
            fences[shapes].Add(new Vector2(Y-1, X));
            numbOfFences++;
        }
        else plots += PathFinding(points, X, Y - 1, 0, 0);
        if (X - 1 < 0 || points[Y][X - 1] != points[Y][X])
        {
            fences[shapes].Add(new Vector2(Y, X-1));
            numbOfFences++;
        }
        else plots += PathFinding(points, X - 1, Y, 0, 0);
        Vector2 returnValue = new Vector2(numberOfPlots + plots.X, numbOfFences + plots.Y);

        return returnValue;
    }

    void checkSameFence()
    {
        for (int i = 0; i < shapes; i++)
        {
            for (int j = 0; j < fences[i].Count - 1; j++)
            {
                if (fences[i][j] == new Vector2(fences[i][j + 1].X, fences[i][j + 1].Y + 1))
                { Console.WriteLine("Removed Fence: " + fences[i][j]); fenceCount -= 1; }
                if (fences[i][j] == new Vector2(fences[i][j + 1].X + 1, fences[i][j + 1].Y))
                { Console.WriteLine("Removed Fence: " + fences[i][j]); fenceCount -= 1; }

            }
        }
        Console.WriteLine(shapes);
    }
}