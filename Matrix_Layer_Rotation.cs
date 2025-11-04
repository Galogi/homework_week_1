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
     * Complete the 'matrixRotation' function below.
     *
     * The function accepts following parameters:
     *  1. 2D_INTEGER_ARRAY matrix
     *  2. INTEGER r
     */

    public static void matrixRotation(List<List<int>> matrix, int r)
{
    int m = matrix.Count;          
    int n = matrix[0].Count;      
    int layers = Math.Min(m, n) / 2;

    for (int layer = 0; layer < layers; layer++)
    {
        int top = layer;
        int left = layer;
        int bottom = m - 1 - layer;
        int right = n - 1 - layer;

        
        List<int> elems = new List<int>();

        
        for (int i = top; i <= bottom; i++)
            elems.Add(matrix[i][left]);

        
        for (int j = left + 1; j <= right; j++)
            elems.Add(matrix[bottom][j]);

     
        for (int i = bottom - 1; i >= top; i--)
            elems.Add(matrix[i][right]);

        
        for (int j = right - 1; j >= left + 1; j--)
            elems.Add(matrix[top][j]);

        int len = elems.Count;
        int shift = r % len;   

        
        if (shift != 0)
        {
            List<int> rotated = new List<int>(new int[len]);
            
            for (int i = 0; i < len; i++)
            {
                rotated[i] = elems[(i + len - shift) % len];
            }
            elems = rotated;
        }

        int idx = 0;

        
        for (int i = top; i <= bottom; i++)
            matrix[i][left] = elems[idx++];

        
        for (int j = left + 1; j <= right; j++)
            matrix[bottom][j] = elems[idx++];

        
        for (int i = bottom - 1; i >= top; i--)
            matrix[i][right] = elems[idx++];

     
        for (int j = right - 1; j >= left + 1; j--)
            matrix[top][j] = elems[idx++];
    }

    
    for (int i = 0; i < m; i++)
    {
        Console.WriteLine(string.Join(" ", matrix[i]));
    }
}


}

class Solution
{
    public static void Main(string[] args)
    {
        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int m = Convert.ToInt32(firstMultipleInput[0]);

        int n = Convert.ToInt32(firstMultipleInput[1]);

        int r = Convert.ToInt32(firstMultipleInput[2]);

        List<List<int>> matrix = new List<List<int>>();

        for (int i = 0; i < m; i++)
        {
            matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
        }

        Result.matrixRotation(matrix, r);
    }
}
