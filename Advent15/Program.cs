// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");
Advent15 advent = new Advent15();
advent.Start();
public class Advent15
{
    public Dictionary<Vector2, char> grid = new Dictionary<Vector2, char>();
    List<char> directions = new List<char>();
    StreamReader sr = new StreamReader("Input.txt");
    StreamReader dir = new StreamReader("Directions.txt");
    int gpsCoords = 0;

    Vector2 pos = new Vector2();

    public void Start()
    {
        int curY = 0;
        while (sr.ReadLine() is { } line)
        {
            var report = line.ToCharArray();
            for (int i = 0; i < report.Length; i++)
            {
                if (report[i] == '@') pos = new Vector2(i, curY);
                grid.Add(new Vector2(i, curY), report[i]);
            }
            curY += 1;
        }
        while (dir.ReadLine() is { } line)
        {
            var moves = line.ToCharArray();
            foreach (var move in moves)
            {
                directions.Add(move);
            }
        }

        MoveRobot();
        foreach (var key in grid.Keys)
        {
            if (grid[key] == 'O')
            {
                Console.WriteLine(key);
                gpsCoords += (100 * (int)(Math.Abs(key.Y)) + (int)Math.Abs(key.X));
            }
        }
        Console.WriteLine(gpsCoords);
    }

    public void MoveRobot()
    {
        foreach (var dir in directions)
        {
            switch (dir)
            {
                case '^':
                    Console.WriteLine("Moving North");
                    switch (grid[new Vector2(pos.X, pos.Y - 1)])
                    {
                        case '#':
                            Console.WriteLine("Hit a wall!");
                            break;
                        case '.':
                            grid[new Vector2(pos.X, pos.Y)] = '.';
                            grid[new Vector2(pos.X, pos.Y - 1)] = '@';
                            pos.Y -= 1;
                            break;
                        case 'O':
                            if (grid[new Vector2(pos.X, pos.Y - 2)] == '#')
                            {
                                Console.WriteLine("Hit a wall with a box!");
                                break;
                            }
                            else if (grid[new Vector2(pos.X, pos.Y - 2)] == 'O')
                            {
                                int boxes = 0;
                                bool blocked = false;
                                bool canPush = false;
                                while (!blocked && !canPush)
                                {
                                    boxes++;
                                    if (grid[new Vector2(pos.X, pos.Y - 2 - boxes)] == '#')
                                    {
                                        blocked = true;
                                    }
                                    else if (grid[new Vector2(pos.X, pos.Y - 2 - boxes)] == '.')
                                    {
                                        canPush = true;
                                    }
                                }
                                if (blocked)
                                {
                                    Console.WriteLine("Hit a wall with a box!");
                                    break;
                                }
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X, pos.Y - 1)] = '@';
                                grid[new Vector2(pos.X, pos.Y - 2)] = 'O';
                                for (int i = 0; i < boxes; i++)
                                {
                                    grid[new Vector2(pos.X, pos.Y - 2 - boxes)] = 'O';
                                }
                                pos.Y -= 1;
                                break;
                            }
                            else
                            {
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X, pos.Y-1)] = '@';
                                grid[new Vector2(pos.X , pos.Y-2)] = 'O';
                                pos.Y -= 1;
                                Console.WriteLine("Pushed a box to!: " + grid[new Vector2(pos.X, pos.Y - 2)]);
                                break;
                            }
                    }
                    Console.WriteLine("Found the oxygen system!");
                    break;
                case 'v':
                    Console.WriteLine("Moving South");
                    switch (grid[new Vector2(pos.X, pos.Y + 1)])
                    {
                        case '#':
                            Console.WriteLine("Hit a wall!");
                            break;
                        case '.':
                            grid[new Vector2(pos.X, pos.Y)] = '.';
                            grid[new Vector2(pos.X, pos.Y + 1)] = '@';
                            pos.Y += 1;
                            break;
                        case 'O':
                            if (grid[new Vector2(pos.X, pos.Y + 2)] == '#')
                            {
                                Console.WriteLine("Hit a wall with a box!");
                                break;
                            }
                            else if (grid[new Vector2(pos.X, pos.Y + 2)] == 'O')
                            {
                                int boxes = 0;
                                bool blocked = false;
                                bool canPush = false;
                                while (!blocked && !canPush)
                                {
                                    boxes++;
                                    if (grid[new Vector2(pos.X, pos.Y + 2 + boxes)] == '#')
                                    {
                                        blocked = true;
                                    }
                                    else if (grid[new Vector2(pos.X, pos.Y + 2 + boxes)] == '.')
                                    {
                                        canPush = true;
                                    }
                                }
                                if (blocked)
                                {
                                    Console.WriteLine("Hit a wall with a box!");
                                    break;
                                }
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X, pos.Y + 1)] = '@';
                                grid[new Vector2(pos.X, pos.Y + 2)] = 'O';
                                for (int i = 0; i < boxes; i++)
                                {
                                    grid[new Vector2(pos.X, pos.Y + 2 + boxes)] = 'O';
                                }
                                pos.Y += 1;
                                break;
                            }
                            else
                            {
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X, pos.Y + 1)] = '@';
                                grid[new Vector2(pos.X, pos.Y + 2)] = 'O';
                                pos.Y += 1;
                                Console.WriteLine("Pushed a box to!: " + grid[new Vector2(pos.X, pos.Y+2)]);
                                break;
                            }
                    }
                    break;
                case '<':
                    Console.WriteLine("Moving West");
                    switch (grid[new Vector2(pos.X - 1, pos.Y)])
                    {
                        case '#':
                            Console.WriteLine("Hit a wall!");
                            break;
                        case '.':
                            grid[new Vector2(pos.X, pos.Y)] = '.';
                            grid[new Vector2(pos.X - 1, pos.Y)] = '@';
                            pos.X -= 1;
                            break;
                        case 'O':
                            Console.WriteLine(grid[new Vector2(pos.X - 1, pos.Y)]);
                            if (grid[new Vector2(pos.X - 2, pos.Y)] == '#')
                            {
                                Console.WriteLine("Hit a wall with a box!");
                                break;
                            }
                            else if (grid[new Vector2(pos.X - 2, pos.Y)] == 'O')
                            {
                                int boxes = 0;
                                bool blocked = false;
                                bool canPush = false;
                                while (!blocked && !canPush)
                                {
                                    boxes++;
                                    if (grid[new Vector2(pos.X - 2 - boxes, pos.Y)] == '#')
                                    {
                                        blocked = true;
                                    }
                                    else if (grid[new Vector2(pos.X - 2 - boxes, pos.Y)] == '.')
                                    {
                                        canPush = true;
                                    }
                                }
                                if (blocked)
                                {
                                    Console.WriteLine("Hit a wall with a box!");
                                    break;
                                }
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X - 1, pos.Y)] = '@';
                                grid[new Vector2(pos.X - 2, pos.Y)] = 'O';
                                for (int i = 0; i < boxes; i++)
                                {
                                    grid[new Vector2(pos.X - 2 - boxes, pos.Y)] = 'O';
                                }
                                pos.X -= 1;
                                break;
                            }
                            else
                            {
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X - 1, pos.Y)] = '@';
                                grid[new Vector2(pos.X - 2, pos.Y)] = 'O';
                                pos.X -= 1;
                                break;
                            }
                    }
                    break;
                case '>':
                    Console.WriteLine("Moving East");
                    switch (grid[new Vector2(pos.X + 1, pos.Y)])
                    {
                        case '#':
                            Console.WriteLine("Hit a wall!");
                            break;
                        case '.':
                            grid[new Vector2(pos.X, pos.Y)] = '.';
                            grid[new Vector2(pos.X + 1, pos.Y)] = '@';
                            pos.X += 1;
                            break;
                        case 'O':
                            if (grid[new Vector2(pos.X + 2, pos.Y)] == '#')
                            {
                                Console.WriteLine("Hit a wall with a box!");
                                break;
                            }
                            else if (grid[new Vector2(pos.X + 2, pos.Y)] == 'O')
                            {
                                int boxes = 0;
                                bool blocked = false;
                                bool canPush = false;
                                while (!blocked && !canPush)
                                {
                                    boxes++;
                                    if (grid[new Vector2(pos.X + 2 + boxes, pos.Y)] == '#')
                                    {
                                        blocked = true;
                                    }
                                    else if (grid[new Vector2(pos.X + 2 + boxes, pos.Y)] == '.')
                                    {
                                        canPush = true;
                                    }
                                }
                                if (blocked)
                                {
                                    Console.WriteLine("Hit a wall with a box!");
                                    break;
                                }
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X + 1, pos.Y)] = '@';
                                grid[new Vector2(pos.X + 2, pos.Y)] = 'O';
                                for (int i = 0; i < boxes; i++)
                                {
                                    grid[new Vector2(pos.X + 2 + boxes, pos.Y)] = 'O';
                                }
                                pos.X += 1;
                                break;
                            }
                            else
                            {
                                grid[new Vector2(pos.X, pos.Y)] = '.';
                                grid[new Vector2(pos.X + 1, pos.Y)] = '@';
                                grid[new Vector2(pos.X + 2, pos.Y)] = 'O';
                                pos.X += 1;
                                Console.WriteLine("Found the oxygen system!");
                                break;
                            }
                    }
                    break;
            }
        }
    }
}
