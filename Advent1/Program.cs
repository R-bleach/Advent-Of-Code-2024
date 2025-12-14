// See https://aka.ms/new-console-template for more informatio
using System.Runtime.CompilerServices;

StreamReader sr = new StreamReader("Input.txt");
List<int> allIds = new List<int>();

List<int> locations1 = new List<int>();

List<int> locations2 = new List<int>();
int distance = 0;

while(sr.ReadLine() is string Line)
{
    var parts = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if(int.TryParse(parts[0], out var number))
    {
        locations1.Add(number);
    }
    if(int.TryParse(parts[1], out number))
    {
        locations2.Add(number);
    }
}

part2();

//Part 1://
void part1()
{

    /*for (int i = 0; i < allIds.Count; i++)
    {
        if (i % 2 == 0)
        {
            locations1.Add(allIds[i]);
        }
        else
        {
            locations2.Add(allIds[i]);
        }
    }*/

    /*Console.WriteLine("before sort: ");
    for (int i = 0; i < locations1.Count; i++)
    {
        Console.WriteLine(locations1[i]);
    }

    bool swapped;
    for (int j = 0; j < locations1.Count - 1; j++)
    {
        swapped = false;
        for (int i = 0; i < locations1.Count - j - 1; i++)
        {
            if (locations1[i] > locations1[i + 1])
            {
                int numb1 = locations1[i];
                int numb2 = locations1[i + 1];
                locations1[i] = numb2;
                locations1[i + 1] = numb1;
                swapped = true;
            }
        }
        if (!swapped)
            break;
    }

    for (int j = 0; j < locations2.Count - 1; j++)
    {
        swapped = false;
        for (int i = 0; i < locations2.Count - j - 1; i++)
        {
            if (locations2[i] > locations2[i + 1])
            {
                int numb1 = locations2[i];
                int numb2 = locations2[i + 1];
                locations2[i] = numb2;
                locations2[i + 1] = numb1;
                swapped = true;
            }
        }
        if (!swapped)
            break;
    }
    *//*
    Console.WriteLine("after sort: ");
    for (int i = 0; i < locations1.Count; i++)
    {
        Console.WriteLine(locations1[i] + "   " + locations2[i]);
    }


    locations1.Sort();
    locations2.Sort();

    *//*
    for (int i = 0; i < locations1.Count; i++)
    {
        if (locations1[i] > locations2[i])
        {
            Console.WriteLine(locations1[i] + " - " + locations2[i] + "=" + (locations1[i] - locations2[i]));
            distance += locations1[i] - locations2[i];
            Console.WriteLine(distance);
        }
        else
        {
            Console.WriteLine(locations2[i] + " - " + locations1[i] + "=" + (locations2[i] - locations1[i]));
            distance += locations2[i] - locations1[i];
            Console.WriteLine(distance);
        }
    }
    *//*
    for (int i = 0; i < locations1.Count; i++)
    {
        distance += Math.Abs(locations1[i] - locations2[i]);
    }
    Console.WriteLine(distance);*/
}

//Part 2://

void part2()
{
    int simScore = 0;
    for (int i = 0; i < locations1.Count; i++)
    {
        int timesSim = 0;
        for (int j = 0; j < locations2.Count; j++)
        {
            if (locations1[i] == locations2[j]) timesSim++;
        }
        simScore += locations1[i] * timesSim;
    }
    Console.WriteLine(simScore);
}
Console.ReadKey();


