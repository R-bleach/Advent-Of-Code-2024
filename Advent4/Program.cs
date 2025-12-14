// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");
Advent4 advent = new Advent4();
advent.Find();
public class Advent4()
{
    StreamReader sr = new StreamReader("Input.txt");
    List<char[]> lines = new List<char[]>();
    public void Find()
    {
        while (sr.ReadLine() is { } block)
        {
            var report = block.ToCharArray();
            lines.Add(report);
        }
        IsSafePart1(lines);
    }

    int merry = 0;
    void IsSafePart1(List<char[]> crossWord)
    {
        for (int i = 0; i < crossWord.Count; i++)
        {
            for (int j = 0; j < crossWord[i].Length; j++)
            {
                //Console.WriteLine(crossWord[i][j]);
                /*if (crossWord[i][j] == 'X')
                    checkXMAS(i, j);*/
                if (crossWord[i][j] == 'A')
                    checkCrossMAS(i, j);
            }
        }
        Console.WriteLine(merry);
        Console.WriteLine("done");
    }

    /*void checkDiagonals(int row, int collum)
    {
        bool downCheck = row - 3 >= 0;
        bool upCheck = row + 3 < lines.Count;
        bool leftCheck = collum - 3 >= 0;
        bool rightCheck = collum + 3 < lines[row].Length;

        if (downCheck)
        {
            if (leftCheck)
            {
                if (lines[row - 1][collum - 1] == 'M' && lines[row - 2][collum - 2] == 'A' && lines[row - 3][collum - 3] == 'S')
                {
                    merry++;
                    Console.WriteLine("Merry Xmas");
                }
            }
            if (rightCheck)
            {
                if (lines[row - 1][collum + 1] == 'M' && lines[row - 2][collum + 2] == 'A' && lines[row - 3][collum + 3] == 'S')
                {
                    merry++;
                    Console.WriteLine("Merry Xmas");
                }
            }
        }

        if (upCheck)
        {
            if (rightCheck)
            {
                if (lines[row + 1][collum + 1] == 'M' && lines[row + 2][collum + 2] == 'A' && lines[row + 3][collum + 3] == 'S')
                {
                    merry++;
                    Console.WriteLine("Merry Xmas");
                }
            }
            if (leftCheck)
            {
                if (lines[row + 1][collum - 1] == 'M' && lines[row + 2][collum - 2] == 'A' && lines[row + 3][collum - 3] == 'S')
                {
                    merry++;
                    Console.WriteLine("Merry Xmas");
                }
            }
        }

    }*/

    void checkXMAS(int row, int collum)
    {
        var directions = new (int rowShift, int colShift)[]
        {
            (-1, -1), (-1, 1), (1, 1), (1, -1),(-1, 0), (0, 1), (1, 0), (0, -1)
        };

        foreach (var (rowShift, colShift) in directions)
        {
            if (IsValidDirection(row, collum, rowShift, colShift))
            {
                if (lines[row + rowShift][collum + colShift] == 'M' &&
                    lines[row + 2 * rowShift][collum + 2 * colShift] == 'A' &&
                    lines[row + 3 * rowShift][collum + 3 * colShift] == 'S')
                {
                    merry++;
                    Console.WriteLine("Merry Xmas");
                }
            }
        }
    }
    bool IsValidDirection(int row, int collum, int rowShift, int colShift)
    {
        return row + 3 * rowShift >= 0 && row + 3 * rowShift < lines.Count &&
               collum + 3 * colShift >= 0 && collum + 3 * colShift < lines[row].Length;
    }
    void checkCrossMAS(int row, int collum)
    {
        int cross = 0;
        var directions = new (int rowShift, int colShift)[]
        {
            (-1, -1), (-1, 1), (1, 1), (1, -1)
        };

        foreach (var (rowShift, colShift) in directions)
        {
            if (IsValidCross(row, collum, rowShift, colShift))
            {
                Console.WriteLine("valid");
                if (lines[row + rowShift][collum + colShift] == 'M' &&
                    lines[row - rowShift][collum - colShift] == 'S')
                {
                    cross++;
                    if (cross == 2)
                    {
                        merry++;
                        Console.WriteLine("Merry Xmas");
                    }
                }
            }
        }
        bool IsValidCross(int row, int collum, int rowShift, int colShift)
        {
            bool check1 = row + rowShift >= 0 && row + rowShift < lines.Count &&
                   collum + colShift >= 0 && collum + colShift < lines[row].Length;

            bool check2 = row - rowShift >= 0 && row - rowShift < lines.Count &&
                   collum - colShift >= 0 && collum - colShift < lines[row].Length;
            return check1 && check2;
        }
    }
}