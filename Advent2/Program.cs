// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

    StreamReader sr = new StreamReader("Input.txt");

    Solve(sr);
    
    void Solve(StreamReader reader)
    {
        int safeCount = 0;
        while (reader.ReadLine() is { } line)
        {
            var report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            if (IsSafePart2(report))
            {
                safeCount++;
            }
        }
        Console.WriteLine(safeCount);
    }

    bool IsSafePart1(List<int> report)
    {
        bool isSafe = true;
        Console.WriteLine("new List");
        for (int i = 0; i< report.Count - 1; i++)
        {
            Console.WriteLine(report[i]);
            int diff = Math.Abs(report[i] - report[i + 1]);
        if (diff > 3 || diff < 1) { isSafe = false; break; };
        if (i != 0 && (report[i - 1] - report[i]) * (report[i] - report[i + 1]) < 0) { isSafe = false; break; } ;
        }
    Console.WriteLine(isSafe);
    return isSafe;
    }

bool IsSafePart2(List<int> report)
{
    bool isSafe = true;
    int failCount = 0;
    Console.WriteLine("new List");
    for (int i = 0; i < report.Count - 1; i++)
    {
        Console.WriteLine(report[i]);
        int diff = Math.Abs(report[i] - report[i + 1]);
        if (diff > 3 || diff < 1) failCount +=1;
        if (i != 0 && (report[i - 1] - report[i]) * (report[i] - report[i + 1]) < 0) failCount += 1;
    }
    if(failCount > 1) isSafe = false;
    Console.WriteLine(isSafe);
    return isSafe;
}
