using System;
using System.Collections.Generic;
using System.Net.Sockets;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines("input.txt");
//var input = File.ReadAllLines("testInput.txt");


Console.WriteLine(Solution.GetSolution1(input));
Console.ReadLine();

public static class Solution
{
    public static long GetSolution1(string[] input)
    {
        long sum = 0;
        int lineCount = 1;
        string[] finalOutput = new string[input.Length];
        string line = input[lineCount];
        string prevLine = input[lineCount - 1];

        while(input.Length - 1 > lineCount)
        {
            var newState = IterateState(prevLine, line);
            line = newState.newLine;
            finalOutput[lineCount] = line;
            prevLine = line;
            lineCount++;
            line = input[lineCount];
            sum += newState.splitCount;
        }
        //foreach (var item in finalOutput)
        //{
        //    Console.WriteLine(item);
        //}

        return sum;
    }

    public static (string newLine, int splitCount) IterateState(string prevLine, string thisLine)
    {
        string changedThisLine = "";
        int splitCount = 0;
        for (int letterIndex = 0; letterIndex < prevLine.Length; letterIndex++)
        {
            switch (prevLine[letterIndex])
            {
                case 'S':
                    if (thisLine[letterIndex] == '.')
                    {
                        changedThisLine += '|';
                    }
                    else
                    {
                        changedThisLine += '.';
                    }
                    break;

                case '|':
                    if (thisLine[letterIndex] == '.')
                    {
                        changedThisLine += '|';
                    }
                    else if(thisLine[letterIndex] == '^')
                    {
                        changedThisLine = changedThisLine.Remove(changedThisLine.Length - 1);
                        changedThisLine += "|^|";
                        splitCount++;
                        letterIndex++;
                    }
                    break;

                default:
                    changedThisLine += thisLine[letterIndex];
                    break;
            }
        }
        return (changedThisLine, splitCount);
    }
};