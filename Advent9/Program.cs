// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Advent9 advent9 = new Advent9();
advent9.Start();
public class Advent9
{
    List<string> dots = new List<string>();
    List<string> ordered = new List<string>();
    Dictionary<ulong, List<ulong>> positions = new Dictionary<ulong, List<ulong>>();
    Dictionary<ulong, List<ulong>> dotPositions = new Dictionary<ulong, List<ulong>>();
    StreamReader sr = new StreamReader("Input.txt");
    ulong sum = 0;

    public void Start()
    {
        while (sr.ReadLine() is { } line)
        {
            var report = line.ToCharArray();
            List<ulong> Aint = (Array.ConvertAll(report, c => (ulong)Char.GetNumericValue(c))).ToList();
            Solve(Aint);
        }
        Console.WriteLine(sum);
    }

    public void Solve(List<ulong> numbers)
    {
        Console.WriteLine(numbers.Count);
        for (int i = 0; i < numbers.Count; i++)
        {
            if(i%2 == 0) positions.Add((ulong)i/2, new List<ulong>());
            else dotPositions.Add((ulong)i, new List<ulong>());
            for (ulong j = 0; j < numbers[i]; j++)
                {
                    if (i % 2 != 0)
                    {
                        dotPositions[(ulong)i].Add(j);
                        dots.Add(".");
                    }
                    else
                    {
                        positions[(ulong)i/2].Add(j);
                        int noDot = i / 2;
                        string s = noDot.ToString();
                        dots.Add(s);
                    }
                }
        }
        Order();
    }

    public void Order()
    {
        Console.WriteLine("ordering");
        Console.WriteLine(dots.Count);
        for (int i = 0; i < dots.Count; i++)
        {
            if (dots[i] == ".")
            {
                for (int j = dots.Count - 1; j > i; j--)
                {
                    if (dots[j] != ".")
                    {

                        dots[i] = dots[j];
                        dots[j] = ".";
                        break;
                    }
                }
            }
            ordered.Add(dots[i]);
            Console.WriteLine(ordered[i]);
        }
        Sum();
    }

    private void Sum()
    {
        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] == ".") break;
            sum += UInt64.Parse(ordered[i]) * (ulong)i;
        }
    }
}
