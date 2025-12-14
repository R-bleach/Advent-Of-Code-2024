// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

Advent6 advent6 = new Advent6();
advent6.Find();
public class Advent6
{
    StreamReader sr = new StreamReader("Input.txt");

    List<char[]> lines = new List<char[]>();
    int pathsWalked = 0;
    public void Find()
    {
        while (sr.ReadLine() is { } block)
        {
            var report = block.ToCharArray();
            lines.Add(report);
        }
        FindPath(lines);

    }

    Vector2 curPos = new Vector2();
    void FindPath(List<char[]> path)
    {
        bool done = false;
        for (int i = 0; i < path.Count; i++)
        {
            for (int j = 0; j < path[i].Length; j++)
            {
                if (path[i][j] == '^')
                {
                    WalkPath(i, j);
                    done = true;
                    break;
                }
            }
            if (done == true) break;
        }
    }

    void WalkPath(int row, int collum)
    {
        curPos = new Vector2(row, collum);
        List<Vector2> visited1 = new List<Vector2>();
        bool stillThere = true;
        Console.WriteLine(curPos);
        int loopCount = 0;
        List<char[]> saveLine = new List<char[]>();
        saveLine = lines.Select(arr => arr.ToArray()).ToList();
        while (stillThere)
        {
            if (lines[(int)curPos.X][(int)curPos.Y] == '^' && curPos.X - 1 >= 0)
            {
                if (lines[(int)curPos.X - 1][(int)curPos.Y] == '#')
                {
                    lines[(int)curPos.X][(int)curPos.Y] = '>';

                }
                else
                {
                    if (!visited1.Contains(curPos))
                    {
                        visited1.Add(curPos);
                    }
                    lines[(int)curPos.X][(int)curPos.Y] = '.';
                    curPos.X--;
                    lines[(int)curPos.X][(int)curPos.Y] = '^';
                }
            }
            else if (lines[(int)curPos.X][(int)curPos.Y] == 'v' && curPos.X + 1 < lines.Count)
            {
                if (lines[(int)curPos.X + 1][(int)curPos.Y] == '#')
                {
                    lines[(int)curPos.X][(int)curPos.Y] = '<';
                }
                else
                {
                    if (!visited1.Contains(curPos))
                    {
                        visited1.Add(curPos);
                    }
                    lines[(int)curPos.X][(int)curPos.Y] = '.';
                    curPos.X++;
                    lines[(int)curPos.X][(int)curPos.Y] = 'v';
                }

            }
            else if (lines[(int)curPos.X][(int)curPos.Y] == '>' && curPos.Y + 1 < lines[(int)curPos.X].Length)
            {
                if (lines[(int)curPos.X][(int)curPos.Y + 1] == '#')
                {
                    lines[(int)curPos.X][(int)curPos.Y] = 'v';
                }
                else
                {
                    if (!visited1.Contains(curPos))
                    {
                        visited1.Add(curPos);
                    }
                    lines[(int)curPos.X][(int)curPos.Y] = '.';
                    curPos.Y++;
                    lines[(int)curPos.X][(int)curPos.Y] = '>';
                }

            }
            else if (lines[(int)curPos.X][(int)curPos.Y] == '<' && curPos.Y - 1 >= 0)
            {
                if (lines[(int)curPos.X][(int)curPos.Y - 1] == '#')
                {
                    lines[(int)curPos.X][(int)curPos.Y] = '^';
                }
                else
                {
                    if (!visited1.Contains(curPos))
                    {
                        visited1.Add(curPos);
                    }
                    lines[(int)curPos.X][(int)curPos.Y] = '.';
                    curPos.Y--;
                    lines[(int)curPos.X][(int)curPos.Y] = '<';
                }
            }
            else
            {
                if (!visited1.Contains(curPos))
                {
                    visited1.Add(curPos);
                }
                stillThere = false;
            }
        }

        for (int i = visited1.Count - 1; i > 0; i--)
        {
            curPos = new Vector2(row, collum);
            List<Vector2> visited2 = new List<Vector2>();
            stillThere = true;
            List<char[]> obsCheck = new List<char[]>();
            obsCheck = saveLine.Select(arr => arr.ToArray()).ToList();
            obsCheck[(int)visited1[i].X][(int)visited1[i].Y] = '#';
            Console.WriteLine(i);
            Console.WriteLine("obstacle places at: " + (int)visited1[i].X + " "+ (int)visited1[i].Y);
            int sameStep = 0;
            while (stillThere)
            {
                if(sameStep > 130)
                {
                    Console.WriteLine("LoopDetected");
                    loopCount++;
                    stillThere = false;
                }
                else if (obsCheck[(int)curPos.X][(int)curPos.Y] == '^' && curPos.X - 1 >= 0)
                {
                    if (obsCheck[(int)curPos.X - 1][(int)curPos.Y] == '#')
                    {
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '>';

                    }
                    else
                    {
                        if (!visited2.Contains(curPos))
                        {
                            visited2.Add(curPos);
                            sameStep = 0;
                        }
                        else { sameStep++; }
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '.';
                        curPos.X--;
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '^';
                    }
                }
                else if (obsCheck[(int)curPos.X][(int)curPos.Y] == 'v' && curPos.X + 1 < obsCheck.Count)
                {
                    if (obsCheck[(int)curPos.X + 1][(int)curPos.Y] == '#')
                    {
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '<';
                    }
                    else
                    {
                        if (!visited2.Contains(curPos))
                        {
                            visited2.Add(curPos);
                            sameStep = 0;
                        }
                        else { sameStep++; }
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '.';
                        curPos.X++;
                        obsCheck[(int)curPos.X][(int)curPos.Y] = 'v';
                    }

                }
                else if (obsCheck[(int)curPos.X][(int)curPos.Y] == '>' && curPos.Y + 1 < obsCheck[(int)curPos.X].Length)
                {
                    if (obsCheck[(int)curPos.X][(int)curPos.Y + 1] == '#')
                    {
                        obsCheck[(int)curPos.X][(int)curPos.Y] = 'v';
                    }
                    else
                    {
                        if (!visited2.Contains(curPos))
                        {
                            visited2.Add(curPos);
                            sameStep = 0;
                        }
                        else { sameStep++; }
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '.';
                        curPos.Y++;
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '>';
                    }

                }
                else if (obsCheck[(int)curPos.X][(int)curPos.Y] == '<' && curPos.Y - 1 >= 0)
                {
                    if (obsCheck[(int)curPos.X][(int)curPos.Y - 1] == '#')
                    {
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '^';
                    }
                    else
                    {
                        if (!visited2.Contains(curPos))
                        {
                            visited2.Add(curPos);
                            sameStep = 0;
                        }
                        else { sameStep++; }
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '.';
                        curPos.Y--;
                        obsCheck[(int)curPos.X][(int)curPos.Y] = '<';
                    }
                }
                else
                {
                    if (!visited2.Contains(curPos))
                    {
                        visited2.Add(curPos);
                        sameStep = 0;
                    }
                    stillThere = false;
                }
            }
        }
        Console.WriteLine("DoneWalking");
        Console.WriteLine(visited1.Count);
        Console.WriteLine(loopCount);
    }
}