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

        // First you do parseIntoLists, then you see what's the biggest number in each column and get the number of digits in that column for that.

        // Then you go character by character through the lines of input, making new strings based on the number of digits in each column.
        //      Remembering to ignore the space after a compleat number string.

        // Then you need to make an array where these digit strings with spaces are combined into arraies depending on their column.

        // Then you make the actual numbers by lexographically adding the digits and skipping whenever you see a " ".
        // Each digit from top to bottom would multiply the previous number by 10 and then add the digit itself.
        // This is to handle when the first few digits are a " " but also to add the digits lexographically into an actual number.

        // Then just applay the operation to the newly gotten numbers.


        return 0;
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