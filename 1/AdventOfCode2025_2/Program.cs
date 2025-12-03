using System.Net.Sockets;

Console.WriteLine("Hello, World!");
var input = File.ReadAllLines("input.txt");
//var input = File.ReadAllLines("testInput.txt");
string[] idRanges = input[0].Split(",");

Console.WriteLine(Solution.GetSolution(idRanges));

Console.ReadLine();

public static class Solution
{
    public static long GetSolution(string[] idRanges)
    {
        long sum = 0;
        foreach (var range in idRanges)
        {
            var idSpan = GetIdSpan(range);
            for (long i = idSpan[0]; i <= idSpan[1]; i++)
            {
                if (WindowCheck(i))
                {
                    //Console.WriteLine(i + " is goofy !");
                    sum += i;
                }
            }
        }
        return sum;
    }

    public static List<long> GetIdSpan(string idRange)
    {
        //"111-222"
        string[] idMinMaxString = idRange.Split("-");
        long Min = 0;
        long Max = 1;
        long.TryParse(idMinMaxString[0], out Min);
        long.TryParse(idMinMaxString[1], out Max);
        return new List<long> { Min, Max };
    }

    public static bool WindowCheck(long num)
    {
        //"121212"
        string stringNum = num.ToString();
        for (int i = 1; i <= stringNum.Length / 2; i++)
        {
            int windowLength = i;
            string startingWindow = stringNum.Substring(0, windowLength);
            while (stringNum.Length > windowLength)
            {
                if(windowLength + i > stringNum.Length)
                {
                    break;
                }

                string newWindow = stringNum.Substring(windowLength, i);
                
                if (startingWindow != newWindow)
                {
                    break;
                }

                windowLength += i;

                if(stringNum.Length <= windowLength)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public static bool MirrorCheck(long num)
    {
        long delimiter = 1;
        while (num > delimiter)
        {
            long leftNum = num / delimiter;
            long rightNum = num % delimiter;
            if (leftNum == rightNum && StringMirrorCheck(num))
            {
                return true;
            }
            delimiter *= 10;
        }
        return false;
    }

    public static bool StringMirrorCheck(long num)
    {
        string stringNum = num.ToString();
        if (stringNum.Length % 2 == 0)
        {
            string lhs = stringNum.Substring(0, stringNum.Length / 2);
            string rhs = stringNum.Substring(stringNum.Length / 2);
            if(lhs == rhs)
            {
                return true;
            }
        }
        return false;
    }

};