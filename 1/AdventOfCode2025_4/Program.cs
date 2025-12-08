using System.Collections.Generic;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines("input.txt");
//var input = File.ReadAllLines("testInput.txt");

Console.WriteLine(Solution.GetSolution1(input));
Console.ReadLine();

public static class Solution
{
    public static int GetSolution1(string[] input)
    {
        var newData = ParseData(input);

        int removedCount = 1;
        int removedSum = 0;

        while (removedCount > 0)
        {
            for (int i = 0; i < newData.Count(); i++)
            {
                List<int> newPrevRow = new();
                List<int> newThisRow = new();
                List<int> newNextRow = new();

                if (i - 1 >= 0)
                    newPrevRow = newData[i - 1];
                newThisRow = newData[i];
                if (i + 1 < newData.Count())
                    newNextRow = newData[i + 1];

                for (int j = 0; j < newThisRow.Count(); j++)
                {
                    if (newThisRow[j] > 0)
                    {
                        AddAdjacency(ref newPrevRow, ref newThisRow, ref newNextRow,
                                     j);
                    }
                }

                if (i - 1 >= 0)
                    newData[i - 1] = newPrevRow;
                newData[i] = newThisRow;
                if (i + 1 < newData.Count())
                    newData[i + 1] = newNextRow;
            }

            removedCount = newData.Sum(inner => inner.Count(x => x < 5 && x > 0));
            newData = newData.Select(row => row.Select(x => (x < 5 && x > 0) ? 0 : x).ToList()).ToList();
            newData = newData.Select(row => row.Select(x => (x > 0) ? 1 : 0).ToList()).ToList();
            removedSum += removedCount;
        }
        //foreach (var row in newData)
        //{
        //    Console.WriteLine(string.Join("", row));
        //}

        return removedSum;
    }

    public static List<List<int>> ParseData(string[] data)
    {
        List<List<int>> parsedData = new();
        foreach (string line in data)
        {
            List<int> list = new();
            foreach (char letter in line)
            {
                if (letter == '.')
                {
                    list.Add(0);
                }
                else
                {
                    list.Add(1);
                }
            }
            parsedData.Add(list);
        }
        return parsedData;
    }

    public static void AddAdjacency(ref List<int> newPreviousRow, ref List<int> newThisRow, ref List<int> newNextRow, 
                                    int index)
    {
        if (newThisRow[index] > 0)
        {
            if(newPreviousRow.Count() > 0)
            {
                if (index - 1 >= 0)
                    newPreviousRow[index - 1] = TryAdd(newPreviousRow[index - 1]);
                newPreviousRow[index] = TryAdd(newPreviousRow[index]);
                if (index + 1 < newThisRow.Count())
                    newPreviousRow[index + 1] = TryAdd(newPreviousRow[index + 1]);
            }


            if (index - 1 >= 0)
                newThisRow[index - 1] = TryAdd(newThisRow[index - 1]);
            if (index + 1 < newThisRow.Count())
                newThisRow[index + 1] = TryAdd(newThisRow[index + 1]);


            if (newNextRow.Count() > 0)
            {
                if (index - 1 >= 0)
                    newNextRow[index - 1] = TryAdd(newNextRow[index - 1]);
                newNextRow[index] = TryAdd(newNextRow[index]);
                if (index + 1 < newThisRow.Count())
                    newNextRow[index + 1] = TryAdd(newNextRow[index + 1]);
            }

        }
    }

    public static int TryAdd(int num)
    {
        if(num > 0)
        {
            return num + 1;
        }
        return 0;
    }

};