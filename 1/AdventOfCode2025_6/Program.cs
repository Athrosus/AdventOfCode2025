using System;
using System.Collections.Generic;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines("input.txt");
//var input = File.ReadAllLines("testInput.txt");


Console.WriteLine(Solution.GetSolution2(input));
Console.ReadLine();

public static class Solution
{

    public static long GetSolution2(string[] input)
    {
        long sum = 0;
        // 51
        //387
        //215
        //*
        //175 * 581 * 32 = 3253600

        //64 
        //23 
        //314
        //+
        //4 + 431 + 623 = 1058

        // Numbers go drom top to bottom

        // First you do parseIntoLists, 
        var numbers = parseIntoLists(input);
        var trueNumbers = new List<List<long>>();
        var digitCounts = new List<int>();
        //then you see what's the biggest number in each column and get the number of digits in that column for that.
        foreach (var number in numbers)
        {
            digitCounts.Add(GetNumberOfDigitsPerColumn(number));
        }

        // Then you go character by character through the lines of input, making new strings based on the number of digits in each column.
        //      Remembering to ignore the space after a compleat number string.
        var stringColumns = parseIntoStringColumns(input, digitCounts);


        // Then you need to make an array where these digit strings with spaces are combined into arraies depending on their column.

        // Then you make the actual numbers by lexographically adding the digits and skipping whenever you see a " ".
        // Each digit from top to bottom would multiply the previous number by 10 and then add the digit itself.
        // This is to handle when the first few digits are a " " but also to add the digits lexographically into an actual number.
        foreach (var stringColumn in stringColumns)
        {
            trueNumbers.Add(parsIntoHumanNumbers(stringColumn).ToList());
        }

        // Then just applay the operation to the newly gotten numbers.

        var operations = parseIntoOperations(input);

        for (int i = 0; i < trueNumbers.Count(); i++)
        {
            long localSum = 0;
            for (int j = 0; j < trueNumbers[i].Count(); j++)
            {
                if (operations[i] == "+")
                {
                    localSum += trueNumbers[i][j];
                }
                else if (operations[i] == "*")
                {
                    if (localSum < 1)
                    {
                        localSum = 1;
                    }
                    localSum *= trueNumbers[i][j];
                }
            }
            sum += localSum;
        }


        return sum;
    }

    public static long[] parsIntoHumanNumbers(List<string> stringColumns)
    {
        var numbers = new long[stringColumns[0].Length];

        for (int i = 0; i < stringColumns.Count(); i++)
        {
            for(int j = 0; j < stringColumns[i].Length; j++)
            {
                if (stringColumns[i][j] != ' ')
                {
                    numbers[j] *= 10;
                    numbers[j] += long.Parse(stringColumns[i][j].ToString());
                }
            }
        }

        return numbers;
    }

    public static List<List<string>> parseIntoStringColumns(string[] input, List<int> digitCounts)
    {
        var numberStarting = input.Take(input.Length - 1);
        var list = new List<List<string>>();

        foreach (var line in numberStarting)
        {
            string digitString = "";
            int numberInLine = 0;

            for(int i = 0; i < line.Length; i++)
            {
                digitString += line[i];
                if(digitString.Length == digitCounts[numberInLine])
                {
                    if (list.Count() <= numberInLine)
                    {
                        list.Add(new() { digitString });
                    }
                    else
                    {
                        list[numberInLine].Add(digitString);
                    }
                    numberInLine++;
                    digitString = "";
                    i++;
                }
            }
        }
        return list;
    }

    public static int GetNumberOfDigitsPerColumn( List<long> list )
    {
        int count = 0;
        var maxNum = list.Max();
        int delimiter = 1;
        while(maxNum > delimiter)
        {
            count++;
            delimiter *= 10;
        }
        return count;
    }

    public static long GetSolution1(string[] input)
    {
        long sum = 0;
        var numbers = parseIntoLists(input);
        var operations = parseIntoOperations(input);

        for (int i = 0; i < numbers.Count(); i++)
        {
            long localSum = 0;
            for(int j = 0; j < numbers[i].Count(); j++)
            {
                if (operations[i] == "+")
                {
                    localSum += numbers[i][j];
                }
                else if (operations[i] == "*")
                {
                    if(localSum < 1)
                    {
                        localSum = 1;
                    }
                    localSum *= numbers[i][j];
                }
            }
            sum += localSum;
        }

        return sum;
    }

    public static List<List<long>> parseIntoLists(string[] input)
    {
        var numberStarting = input.Take(input.Length - 1);
        var list = new List<List<long>>();

        foreach (var line in numberStarting)
        {
            var strings = line.Split(' ');
            strings = strings.Where( x => x != "" ).ToArray();

            for (int i = 0; i < strings.Length; i++)
            {
                if (list.Count() <= i)
                {
                    list.Add( new() { long.Parse(strings[i]) });
                }
                else
                {
                    list[i].Add(long.Parse(strings[i]));
                }
            }
        }
        return list;
    }
    public static List<string> parseIntoOperations(string[] input)
    {
        string[] operationStarting = input.Skip(input.Length - 1).ToArray();
        var operations = new List<string>();


        var strings = operationStarting[0].Split(' ');
        operations = strings.Where(x => x != "").ToList();

        return operations;
    }

};