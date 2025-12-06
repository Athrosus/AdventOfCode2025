using System.Net.Sockets;

Console.WriteLine("Hello, World!");
var input = File.ReadAllLines("input.txt");
//var input = File.ReadAllLines("testInput.txt");

Console.WriteLine(Solution.GetSolution2(input));

Console.ReadLine();

public static class Solution
{
    public static long GetSolution2(string[] stringBanks)
    {
        long sum = 0;
        foreach (string stringBank in stringBanks)
        {
            int[] bank = StringToIntArray(stringBank);
            int[] joltages = new int[12];

            int lastJoltageIndex = 0;
            for (int i = 0; i < joltages.Length; i++) 
            {
                int[] possibleJoltages = bank[lastJoltageIndex..(bank.Length - 11 + i)];
                int thisJoltagesLastJoltageIndex = lastJoltageIndex;

                for (int j = 0; j < possibleJoltages.Length; j++)
                {
                    if (joltages[i] < possibleJoltages[j])
                    {
                        joltages[i] = possibleJoltages[j];
                        lastJoltageIndex = j + thisJoltagesLastJoltageIndex ;
                    }
                }


                lastJoltageIndex += 1;
            }


            sum += StringArrayAddition(joltages);
        }
        return sum;
    }

    public static long StringArrayAddition(int[] array)
    {
        string stringSum = "";
        foreach (int num in array)
        {
            stringSum += num.ToString();
        }
        long sum = 0;
        long.TryParse(stringSum, out sum);
        return sum;
    }


    public static long GetSolution1(string[] stringBanks)
    {
        long sum = 0;
        foreach (string stringBank in stringBanks)
        {
            int[] bank = StringToIntArray(stringBank);
            int[] joltages = new int[12];
            long leftMax = bank[0];
            long rightMax = bank[1];

            for (int i = 0; i < bank.Length - 1; i++)
            {
                if (bank[i] > leftMax)
                {
                    leftMax = bank[i];
                    rightMax = bank[i + 1];
                }
                else if (bank[i + 1] > rightMax)
                {
                    rightMax = bank[i + 1];
                }
            }
            sum += StringAddition(leftMax, rightMax);
        }
        return sum;
    }

    public static int[] StringToIntArray(string input)
    {
        int[] longs = new int[input.Length];
        for(int i = 0; i < longs.Length; i++)
        {
            string a = input[i].ToString();
            int b = 0;
            int.TryParse(a, out b);
            longs[i] = b;
        }
        return longs;
    }

    public static long StringAddition(long a, long b)
    {
        string stringSum = a.ToString() + b.ToString();
        long sum = 0;
        long.TryParse(stringSum, out sum);
        return sum;
    }
};