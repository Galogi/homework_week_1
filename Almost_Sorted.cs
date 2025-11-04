using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'almostSorted' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void almostSorted(List<int> arr)
    {
        int n = arr.Count;
        List<int> sorted = new List<int>(arr);
        sorted.Sort();

        if (arr.SequenceEqual(sorted))
        {
            Console.WriteLine("yes");
            return;
        }

        List<int> diff = new List<int>();
        for (int i = 0; i < n; i++)
            if (arr[i] != sorted[i])
                diff.Add(i);

        if (diff.Count == 2)
        {
            Console.WriteLine("yes");
            Console.WriteLine($"swap {diff[0] + 1} {diff[1] + 1}");
            return;
        }

        int l = diff.First();
        int r = diff.Last();
        arr.Reverse(l, r - l + 1);

        if (arr.SequenceEqual(sorted))
        {
            Console.WriteLine("yes");
            Console.WriteLine($"reverse {l + 1} {r + 1}");
        }
        else
        {
            Console.WriteLine("no");
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.almostSorted(arr);
    }
}
