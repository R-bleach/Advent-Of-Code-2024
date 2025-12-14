// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Numerics;

Console.WriteLine("Hello, World!");
Advent13 advent = new Advent13();
advent.Start();

public class Advent13
{
    List<Vector2> combis = new List<Vector2>();
    StreamReader sr = new StreamReader("Input.txt");
    ulong coinsSpent = 0;
    Dictionary<Vector2, int> memo = new Dictionary<Vector2, int>();
    Queue<Vector2> nextPos = new Queue<Vector2>();
    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            var parts = line.Split('+', '=', ',', '\n');
            if (parts.Count() > 1)
                combis.Add(new Vector2(ulong.Parse(parts[1]), ulong.Parse(parts[3])));
        }
        for (int i = 0; i < combis.Count; i += 3)
        {
            Console.WriteLine(i);
            findCheapestPath(combis[i + 2], new Vector2(combis[i + 2].X + 10000000000000, combis[i + 2].Y + 10000000000000), new Vector2(1, 1), combis[i], combis[i + 1], 0);
            Console.WriteLine(coinsSpent);
        }
    }

    bool FindCheapestPath(Vector2 curPos, Vector2 finalPos, Vector2 ButtonPressed, Vector2 buttonA, Vector2 buttonB, ulong minCoinsSpent)
    {
        

        curPos += ButtonPressed;
        Console.WriteLine(curPos);
        if (ButtonPressed == buttonA) minCoinsSpent += 3;
        else if (ButtonPressed == buttonB) minCoinsSpent += 1;
        if (finalPos.X < curPos.X || finalPos.Y < curPos.Y)
        {
            return false;
        }
        if (curPos == finalPos) { coinsSpent += minCoinsSpent; return true; }

        if (FindCheapestPath(curPos, finalPos, buttonB, buttonA, buttonB, minCoinsSpent)) return true;
        if (FindCheapestPath(curPos, finalPos, buttonA, buttonA, buttonB, minCoinsSpent)) return true;

        return false;
    }

    void findCheapestPath(Vector2 curPos, Vector2 finalPos, Vector2 ButtonPressed, Vector2 buttonA, Vector2 buttonB, ulong minCoinsSpent)
    {
        for(ulong i = 0; Math.Min(buttonA.X,buttonB.X)*i < (finalPos.X) ; i++)
        {
            for(ulong j = 0; Math.Min(buttonA.Y, buttonB.Y) * j < (finalPos.Y); j++)
            {
                ulong X = (ulong)buttonA.X * i + (ulong)buttonB.X * j;
                ulong Y = (ulong)buttonA.Y * i + (ulong)buttonB.Y * j;
                if (finalPos == new Vector2(X, Y))
                {
                    Console.WriteLine("winner");
                    ulong newCoinsSpent = (3 * i) + (1 * j);
                    if(minCoinsSpent > newCoinsSpent || minCoinsSpent == 0) minCoinsSpent = newCoinsSpent;
                }
            }
            
        }
        coinsSpent += minCoinsSpent;
        Console.WriteLine(coinsSpent);
    }
}