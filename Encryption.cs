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
     * Complete the 'encryption' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string encryption(string s)
{
   
    var tBuilder = new StringBuilder(s.Length);
    foreach (char ch in s)
        if (ch != ' ')
            tBuilder.Append(ch);

    string t = tBuilder.ToString();
    int L = t.Length;
    if (L == 0) return "";

  
    double root = Math.Sqrt(L);
    int rows = (int)Math.Floor(root);
    int cols = (int)Math.Ceiling(root);
    if (rows * cols < L) rows++;  


    var outBuilder = new StringBuilder(L + cols - 1);
    for (int c = 0; c < cols; c++)
    {
        for (int r = 0; r < rows; r++)
        {
            int idx = r * cols + c; 
            if (idx < L) outBuilder.Append(t[idx]);
        }
        if (c < cols - 1) outBuilder.Append(' ');
    }
    return outBuilder.ToString();
}

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.encryption(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
