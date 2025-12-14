// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");
Advent8 advent8 = new Advent8();
advent8.Find();
public class Advent8
{
    StreamReader sr = new StreamReader("Input.txt");
    Dictionary<char, List<Vector2>> letterPos = new Dictionary<char, List<Vector2>>();
    List<Vector2> occupied = new List<Vector2>();
    List<char[]> lines = new List<char[]>();
    int signalCount = 0;
    public void Find()
    {
        while (sr.ReadLine() is { } block)
        {
            var report = block.ToCharArray();
            lines.Add(report);
        }
        Check(lines);
        Console.WriteLine(signalCount);
    }
    public void Check(List<char[]> rows)
    {
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[i].Length; j++)
            {
                if (rows[i][j] != '.')
                {
                    FindSignals(rows[i][j], i, j);
                    if (!occupied.Contains(new Vector2(j, i)))
                    {
                        occupied.Add(new Vector2(j, i));
                        Console.WriteLine("Signal created at tower: " + new Vector2(j, i));
                        signalCount++;
                    }
                }
            }
        }
    }

    public void FindSignals(char letter, int Y, int X)
    {
        if (letterPos.ContainsKey(letter))
        {

            for (int i = 0; i < letterPos[letter].Count; i++)
            {
                Console.WriteLine(i + " " + letter + " " + letterPos[letter][i]);
                Vector2 pos1 = new Vector2(X, Y);
                Vector2 pos = letterPos[letter][i];
                Vector2 distance1 = new Vector2(X - pos.X, Y - pos.Y);
                Vector2 distance2 = new Vector2(pos.X - X, pos.Y - Y);

                Vector2 signal1 = pos1 + distance1;
                Vector2 signal2 = pos + distance2;

                while (signal1.X >= 0 && signal1.Y >= 0 && signal1.Y < lines.Count && signal1.X < lines[Y].Length )
                {
                    if(!occupied.Contains(signal1))
                    {
                        occupied.Add(signal1);
                        signalCount++;
                    }
                    Console.WriteLine("Signal1 created at: " + signal1);
                    signal1 += distance1;
                }
                while (signal2.X >= 0 && signal2.Y >= 0 && signal2.Y < lines.Count && signal2.X < lines[Y].Length)
                {
                    if (!occupied.Contains(signal2))
                    {
                        occupied.Add(signal2);
                        signalCount++;
                    }
                    Console.WriteLine("Signal2 created at: " + signal2);
                    signal2 += distance2;

                }
            }
        }
        else
        {
            List<Vector2> positions = new List<Vector2>();
            letterPos.Add(letter, positions);
            if (!occupied.Contains(new Vector2(X, Y)))
            {
                occupied.Add(new Vector2(X, Y));
                signalCount++;
            }
        }
        letterPos[letter].Add(new Vector2(X, Y));
    }
}