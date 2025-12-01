using System.Net.Sockets;

Console.WriteLine("Hello, World!");

int startingPos = 50;
var allMoves = File.ReadAllLines("input.txt");

int zeroCount = 0;

Console.WriteLine(Solution.GetSolution(ref zeroCount, ref startingPos, allMoves));
Console.ReadLine();

public static class Solution
{
    public static int GetSolution(ref int zeroCount, ref int pos, string[] moves)
    {
        foreach (string move in moves)
        {
            MoveDial(ref zeroCount, ref pos, move);
        }
        return zeroCount;
    }

    public static int MoveDial(ref int zeroCount, ref int startingPos, string numCode)
    {
        char code = numCode[0];
        int num = 0;
        int.TryParse(numCode.Remove(0, 1), out num);

        if (code == 'R')
        {
            startingPos += num;
            while (startingPos > 99)
            {
                startingPos -= 100;
                if(startingPos != 0)
                {
                    zeroCount++;
                }
            }
        }
        else if (code == 'L')
        {
            if (startingPos == 0) {
                zeroCount--;
            }

            startingPos -= num;
            while (startingPos < 0)
            {
                startingPos += 100;
                zeroCount++;
            }
        }
        else
        {
            throw new Exception("aaaa");
        }

        if (startingPos == 0)
        {
            zeroCount++;
        }

        return startingPos;
    }
};