using System.Collections.Generic;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

//var input = File.ReadAllLines("input.txt");
var input = File.ReadAllLines("testInput.txt");


Console.WriteLine(Solution.GetSolution2(input));
Console.ReadLine();

public static class Solution
{

    public static long GetSolution2(string[] input)
    {
        var parsedInput = ParseData(input);
        List<List<long>> spans = parsedInput.spans;
        List<long> foods = parsedInput.foods;
        long sum = 0;

        foreach (var span in spans)
        {
            // this doesnt work because there are overlaping spans.
            // first you sort the spans by their mins.
            // I think I need to make something that will goe throu the spans,
            // colaps the spans together combining spans if their min fals under the currantly being made span and take the new soon to be collapsed spans max
            // this stops when the next spans min isnt inside the collapsed span and a new collapsed span is created
            // do this untill you run out of spans.
            long count = span[1] - span[0];
            sum += count;
        }

        return sum;
    }

    public static long GetSolution1(string[] input)
    {
        var parsedInput = ParseData(input);
        List<List<long>> spans = parsedInput.spans;
        List<long> foods = parsedInput.foods;
        int counter = 0;

        foreach (var food in foods)
        {
            foreach (var span in spans)
            {
                if(IsInSpan(span, food))
                {
                    //Console.WriteLine("The food of Id: " + food + " is fresh because it falls in the span: " + String.Join("-", span));
                    counter++;
                    break;
                }
            }
        }

        return counter;
    }

    public static bool IsInSpan(List<long> foodSpan, long food)
    {
        if(food >= foodSpan[0] && food <= foodSpan[1])
        {
            return true;
        }
        return false;
    }

    public static (List<List<long>> spans, List<long> foods) ParseData(string[] input) 
    {
        List<List<long>> listOfSpans = new();
        List<long> listOfFood = new();
        long listIndex = 0;
        string lastString = input[listIndex];
        while(lastString != "")
        {
            List<long> newRange = new();
            var splitString2 = lastString.Split('-');
            foreach (var line in splitString2)
            {
                long intParse = 0;
                long.TryParse(line, out intParse);
                newRange.Add(intParse);
            }
            listOfSpans.Add(newRange);
            listIndex++;
            lastString = input[listIndex];
        }

        listIndex++;

        while (listIndex < input.Length)
        {
            lastString = input[listIndex];
            long intParse = 0;
            long.TryParse(lastString, out intParse);
            listOfFood.Add(intParse);
            listIndex++;
        }

        return (listOfSpans, listOfFood); 
    }

};