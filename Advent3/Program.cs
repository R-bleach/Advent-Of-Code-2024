// See https://aka.ms/new-console-template for more information
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

Console.WriteLine("Hello, World!");
Advant3 advant3 = new Advant3();
advant3.Solve();
public partial class Advant3
{
    [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)")]
    public static partial Regex MulRegex();

    [GeneratedRegex("don't\\(\\)")]
    public static partial Regex DontRegex();

    [GeneratedRegex("do\\(\\)")]
    public static partial Regex DoRegex();
    StreamReader sr = new StreamReader("Input.txt");
    public void Solve()
    {
        int finalAns = 0;
        bool active = true;
        var input = sr.ReadToEnd();
        var stop = DontRegex().Matches(input);
        var start = DoRegex().Matches(input);
        var matches = MulRegex().Matches(input);
        var startIndicies = start.Select(x => x.Index).ToList();
        var stopIndicies = stop.Select(x => x.Index).ToList();
        var mergeIndicies = startIndicies.Concat(stopIndicies).OrderBy(i=>i).ToArray();
        var nextIndex = 0;
        bool isEnabled = true;
        foreach (Match match in matches)
        {
            var matchIndex = match.Index;
            if (nextIndex < mergeIndicies.Length && matchIndex > mergeIndicies[nextIndex])
            {
                var IsStart = startIndicies.Contains(mergeIndicies[nextIndex]);
                isEnabled = IsStart;
                nextIndex++;
            }
            if (isEnabled)
            {
                int a = int.Parse(match.Groups[1].Value);
                int b = int.Parse(match.Groups[2].Value);
                int ans = a * b;
                finalAns += ans;
            }
        }
        Console.WriteLine(finalAns);
    }
}
