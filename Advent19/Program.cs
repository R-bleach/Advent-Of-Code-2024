// See https://aka.ms/new-console-template for more information
using System.Text;

Console.WriteLine("Hello, World!");

Advent19 advent19 = new Advent19();
advent19.Start();

public class Advent19
{
    StreamReader sr = new StreamReader("Input.txt");
    StreamReader reader = new StreamReader("Patterns.txt");

    List<string> wantedPatterns = new List<string>();
    List<string> availableTowels = new List<string>();
    int possible = 0;
    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            char[] sep = { ',', ' ' };
            availableTowels.AddRange(line.Split(sep).ToArray());
        }

        while (reader.ReadLine() is { } line)
        {
            wantedPatterns.Add(line);
        }

        SortTowels();
    }

    public void SortTowels()
    {
        foreach (var pattern in wantedPatterns)
        {
            if (CanFormDesign(pattern, out var usedTowels))
            {
                possible++;
                Console.WriteLine("Design: " + pattern + "can be formed with towels: "+ string.Join(", ", usedTowels));
            }
            else
            {
                Console.WriteLine("Design: "+ pattern + "cannot be formed.");
            }
        }

        Console.WriteLine("Total possible designs: " + possible);
    }

    public bool CanFormDesign(string design, out List<string> usedTowels)
    {
        int n = design.Length;
        bool[] dp = new bool[n + 1];
        dp[0] = true; 

        List<string>[] usedTowelsForPosition = new List<string>[n + 1];
        usedTowelsForPosition[0] = new List<string>(); 

        for (int i = 1; i <= n; i++)
        {
            foreach (var towel in availableTowels)
            {
                int len = towel.Length;
                if (i >= len && dp[i - len] && design.Substring(i - len, len) == towel)
                {
                    dp[i] = true;

                    
                    usedTowelsForPosition[i] = new List<string>(usedTowelsForPosition[i - len]);
                    usedTowelsForPosition[i].Add(towel);
                    break; 
                }
            }
        }


        if (dp[n])
        {
            usedTowels = usedTowelsForPosition[n];
            return true;
        }
        else
        {
            usedTowels = null;
            return false;
        }
    }
}
