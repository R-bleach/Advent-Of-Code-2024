// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine("Hello, World!");

Advent14 advent14 = new Advent14();
advent14.Start();

public class Advent14
{
    StreamReader sr = new StreamReader("Input.txt");
    int gridX = 101;
    int gridY = 103;
    int[] quadrant = [0, 0, 0, 0];
    int safetyFactor = 0;
    List<Robot> robots = new List<Robot>();
    Dictionary<Robot, Vector2> robotPositions = new Dictionary<Robot, Vector2>();
    Vector2[] tree = [new Vector2(50, 2), new Vector2(48, 3), new Vector2(49, 3), new Vector2(50, 3), new Vector2(51, 3), new Vector2(52, 3), // Level 1
new Vector2(47, 4), new Vector2(48, 4), new Vector2(49, 4), new Vector2(50, 4), new Vector2(51, 4), new Vector2(52, 4), new Vector2(53, 4), // Level 2
new Vector2(46, 5), new Vector2(47, 5), new Vector2(48, 5), new Vector2(49, 5), new Vector2(50, 5), new Vector2(51, 5), new Vector2(52, 5), new Vector2(53, 5), new Vector2(54, 5), // Level 3
new Vector2(45, 6), new Vector2(46, 6), new Vector2(47, 6), new Vector2(48, 6), new Vector2(49, 6), new Vector2(50, 6), new Vector2(51, 6), new Vector2(52, 6), new Vector2(53, 6), new Vector2(54, 6), new Vector2(55, 6), // Level 4
new Vector2(44, 7), new Vector2(45, 7), new Vector2(46, 7), new Vector2(47, 7), new Vector2(48, 7), new Vector2(49, 7), new Vector2(50, 7), new Vector2(51, 7), new Vector2(52, 7), new Vector2(53, 7), new Vector2(54, 7), new Vector2(55, 7), new Vector2(56, 7), // Level 5
new Vector2(49, 8), new Vector2(50, 8), new Vector2(51, 8) ];

    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            var parts = line.Split('+', '=', ',', '\n', 'p', 'v', ' ');
            if (parts.Count() > 1)
            {
                Robot bot = new Robot(new Vector2(int.Parse(parts[2]), int.Parse(parts[3])), new Vector2(int.Parse(parts[6]), int.Parse(parts[7])), new Vector2(gridX, gridY));
                /*Console.WriteLine(bot.pos + " | " + bot.vel);*/
                robots.Add(bot);
            }
        }
        Simulate();
        Console.WriteLine($"Safety Factor: {safetyFactor}");
    }
    public void Simulate()
    {
        bool treeFound = false;
        int loopCount = 0;
        while (loopCount < 100)
        {
            loopCount++;
            Console.WriteLine(loopCount);
            int treeCheck = 0;
            foreach (var robot in robots)
            {
                robot.Move();
                if (!robotPositions.ContainsKey(robot)) robotPositions.Add(robot, robot.pos);
                else
                    robotPositions[robot] = robot.pos;
            }
        }
        foreach (var robot in robots)
        {
            if (robotPositions[robot].X < (gridX - 1) / 2 && robotPositions[robot].Y < (gridY - 1) / 2) quadrant[0]++;
            else if (robotPositions[robot].X >= (gridX + 1) / 2 && robotPositions[robot].Y < (gridY - 1) / 2) quadrant[1]++;
            else if (robotPositions[robot].X < (gridX - 1) / 2 && robotPositions[robot].Y >= (gridY + 1) / 2) quadrant[2]++;
            else if (robotPositions[robot].X >= (gridX + 1) / 2 && robotPositions[robot].Y >= (gridY + 1) / 2) quadrant[3]++;
        }
        Console.WriteLine(quadrant[0] + " " + quadrant[1] + " " + quadrant[2] + " " + quadrant[3]);
        safetyFactor = quadrant[0] * quadrant[1] * quadrant[2] * quadrant[3];
    }

}

public class Robot(Vector2 position, Vector2 vel, Vector2 grid)
{
    public Vector2 pos = position;
    public Vector2 vel = vel;
    public void Move()
    {
        pos += vel;
        if (pos.X < 0) pos.X += grid.X;
        if (pos.X >= grid.X) pos.X -= grid.X;
        if (pos.Y < 0) pos.Y += grid.Y;
        if (pos.Y >= grid.Y) pos.Y -= grid.Y;

    }
}