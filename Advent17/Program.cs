// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Advent17 advent17 = new Advent17();
advent17.Run();
public class Advent17
{
    ulong A = 30886132;
    ulong B = 0;
    ulong C = 0;

    ulong[] program = new ulong[] { 2, 4, 1, 1, 7, 5, 0, 3, 1, 4, 4, 4, 5, 5, 3, 0 };
    List<ulong> answer = new List<ulong>();

    public void Run()
    {
        for (int i = 0; i < program.Length; i += 2)
        {
            switch (program[i])
            {
                case 0:
                    A = A / (ulong)MathF.Pow(2, ComboOp(program[i + 1]));
                    break;
                case 1:
                    B = B ^ program[i + 1];
                    break;
                case 2:
                    B = ComboOp(program[i + 1]) % 8;
                    break;
                case 3:
                    if (A != 0)
                    {
                        i = (int)program[i + 1] - 2;
                    }
                    break;
                case 4:
                    B = B ^ C;
                    break;
                case 5:
                    answer.Add(ComboOp(program[i + 1]) % 8);
                    break;
                case 6:
                    B = A / (ulong)MathF.Pow(2, ComboOp(program[i + 1]));
                    break;
                case 7:
                    C = A / (ulong)MathF.Pow(2, ComboOp(program[i + 1]));
                    break;
            }
        }
        for (int i = 0; i < answer.Count; i++)
        {
            Console.Write(answer[i]);
        }
    }

    public ulong ComboOp(ulong i)
    {
        if (i == 4) i = A;
        else if (i == 5) i = B;
        else if (i == 6) i = C;
        return i;
    }
}


