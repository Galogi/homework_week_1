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
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     */
    public static List<string> bomberMan(int n, List<string> grid)
    {
       
        if (n == 1) return grid;

      
        if (n % 2 == 0)
        {
            return Enumerable.Repeat(new string('O', grid[0].Length), grid.Count).ToList();
        }

        
        var firstExplosion = Detonate(grid);
        var secondExplosion = Detonate(firstExplosion);

        if ((n - 3) % 4 == 0)
            return firstExplosion;
        else
            return secondExplosion;
    }


    private static List<string> Detonate(List<string> grid)
    {
        int rows = grid.Count;
        int cols = grid[0].Length;
        char[,] result = new char[rows, cols];

        
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = 'O';

        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };

       
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 'O')
                {
                    result[i, j] = '.';
                    for (int d = 0; d < 4; d++)
                    {
                        int ni = i + dx[d];
                        int nj = j + dy[d];
                        if (ni >= 0 && ni < rows && nj >= 0 && nj < cols)
                            result[ni, nj] = '.';
                    }
                }
            }
        }

       
        List<string> next = new List<string>();
        for (int i = 0; i < rows; i++)
        {
            char[] row = new char[cols];
            for (int j = 0; j < cols; j++)
                row[j] = result[i, j];
            next.Add(new string(row));
        }
        return next;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}