// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine("Hello, World!");

/*Day10 day = new Day10();
day.A(System.IO.File.ReadAllText("Input.txt"));*/

Advent10 advent10 = new Advent10();
advent10.Start();


public class Advent10
{
    int score = 0;
    StreamReader sr = new StreamReader("Input.txt");
    List<int[]> points = new List<int[]>();
    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            var report = line.ToCharArray();
            points.Add((Array.ConvertAll(report, c => (int)Char.GetNumericValue(c))).ToArray());
        }
        FindStartingPoint(points);
        Console.WriteLine(score);
    }
    public void FindStartingPoint(List<int[]> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = 0; j < points[i].Length; j++)
            {
                Console.WriteLine(points[i][j]);
                if (points[i][j] == 0)
                {
                    Console.WriteLine("StartFinding");
                    PathFinding(points, j, i, 0, new List<Vector2>());
                    Console.WriteLine("done: " + score);
                    Console.WriteLine(i + " " + j);
                }
            }
        }
    }
    public void PathFinding(List<int[]> points, int X, int Y, int curHeight, List<Vector2> Traverserd)
    {
        /*Console.WriteLine(curHeight + " " + X + " " + Y);*/
        /*if (Traverserd.Contains(new Vector2(X, Y)))
        {
            return;
        }*/
        Traverserd.Add(new Vector2(X, Y));
        if (curHeight == 9)
        {
            score++;
        }
        else
        {
            if (Y + 1 < points.Count && points[Y + 1][X] == curHeight + 1)
            {
                PathFinding(points, X, Y + 1, curHeight + 1, Traverserd);
            }
            if (X + 1 < points[Y].Length && points[Y][X + 1] == curHeight + 1)
            {
                PathFinding(points, X + 1, Y, curHeight + 1, Traverserd);
            }
            if (Y - 1 >= 0 && points[Y - 1][X] == curHeight + 1)
            {
                PathFinding(points, X, Y - 1, curHeight + 1, Traverserd);
            }
            if (X - 1 >= 0 && points[Y][X - 1] == curHeight + 1)
            {
                PathFinding(points, X - 1, Y, curHeight + 1, Traverserd);

            }

        }
    }
}