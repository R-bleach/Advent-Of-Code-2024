// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine("Hello, World!");

Advent7 advent7 = new Advent7();
advent7.calculate();
public class Advent7
{
    StreamReader sr = new StreamReader("Input.txt");
    ulong total = 0;
    public void calculate()
    {
        int score = 0;
        while (sr.ReadLine() is { } line)
        {
            char[] sep = { ':', ',', ' ' };
            var report = line.Split(sep, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList(); ;
            if (doEquation(report))
            {
                total += report[0];
                Console.WriteLine(total);
            }
        }
    }

    bool doEquation(List<ulong> eq)
    {
        return Solve(eq[1],eq[0],2,eq);
    }
    bool Solve(ulong cur, ulong ans,int index,List<ulong> eq)
    {
        if(cur > ans) return false;

        if(index == eq.Count)
        {
            return cur == ans;
        }

        if (Solve(cur + eq[index],ans, index +1,eq))
        {
            return true;
        }

        if (Solve(cur * eq[index], ans, index + 1, eq))
        {
            return true;
        }

        if (Solve(ulong.Parse((cur.ToString() + eq[index].ToString())), ans, index + 1, eq))
        {
            return true;
        }


        return false;
    }
}