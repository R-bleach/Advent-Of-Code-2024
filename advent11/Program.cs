// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Numerics;

Console.WriteLine("Hello, World!");

Advent11 advent11 = new Advent11();
advent11.Start();
public class Advent11
{
    int timesBlinked = 25;
    int totalStones = 0;
    List<ulong> stones = new List<ulong>();
    Dictionary<(ulong, ulong), ulong> cache = new Dictionary<(ulong, ulong), ulong>();
    StreamReader reader = new StreamReader("Input.txt");
    public void Start()
    {
        while (reader.ReadLine() is { } line)
        {
            char[] sep = { ' ' };
            var report = line.Split(sep, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();
            stones = report;
        }
        Console.WriteLine(Blink(stones, timesBlinked));
    }

    public ulong Blink(List<ulong> stone, int timesToBlink)
    {
        if (timesToBlink == 0) return (ulong)stone.Count;

        ulong count = 0;

        for (int i = 0; i < stone.Count; i++)
        {
            ulong stoneResult = Blink(ProcessBlink(stone[i]), timesToBlink-1);
            count += stoneResult;
        }
        return count;
    }

    public List<ulong> ProcessBlink(ulong stone)
    {
        var newStones = new List<ulong>();
        newStones.Add(stone);
        if (stone == 0)
        {
            newStones[0] = 1;
        }
        else if (stone.ToString().ToCharArray().Length % 2 == 0)
        {
            char[] chars = stone.ToString().ToCharArray();
            string firstStone = "";
            string secondStone = "";
            for (int j = 0; j < chars.Length; j++)
            {
                if (j < chars.Length / 2)
                {
                    firstStone = firstStone + chars[j];
                }
                else
                {
                    secondStone = secondStone + chars[j];
                }
            }
            newStones[0] = (ulong.Parse(firstStone));
            ulong newStone = ulong.Parse(secondStone);
            newStones.Add(newStone);
        }
        else
        {
            newStones[0] = stone * 2024;
        }
        return newStones;
    }
}