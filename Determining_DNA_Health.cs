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



class Node
{
    public int[] Next = new int[26];   // transitions
    public int Link = 0;               // failure link
    public int OutputLink = -1;        // next node with output on failure chain
    public List<int> Indices = new List<int>();     // gene indices
    public List<long> PrefixHealth = new List<long>(); // prefix sums of health

    public Node()
    {
        for (int i = 0; i < 26; i++) Next[i] = -1;
    }
}

class Solution
{
    static List<Node> nodes = new List<Node>();

    static int LowerBound(List<int> a, int value)
    {
        int l = 0, r = a.Count;
        while (l < r)
        {
            int m = (l + r) / 2;
            if (a[m] < value) l = m + 1;
            else r = m;
        }
        return l;
    }

    static int UpperBound(List<int> a, int value)
    {
        int l = 0, r = a.Count;
        while (l < r)
        {
            int m = (l + r) / 2;
            if (a[m] <= value) l = m + 1;
            else r = m;
        }
        return l;
    }

    static long RangeSum(Node node, int start, int end)
    {
        if (node.Indices.Count == 0) return 0L;

        List<int> idx = node.Indices;
        List<long> pref = node.PrefixHealth;

        int leftPos = LowerBound(idx, start);
        int rightPos = UpperBound(idx, end) - 1;

        if (leftPos > rightPos || rightPos < 0) return 0L;

        long res = pref[rightPos];
        if (leftPos > 0) res -= pref[leftPos - 1];
        return res;
    }

    static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> genes = Console.ReadLine().TrimEnd().Split(' ').ToList();
        List<int> health = Console.ReadLine().TrimEnd().Split(' ')
                            .Select(int.Parse).ToList();

        // 1. Build trie
        nodes.Add(new Node()); // root at index 0

        for (int i = 0; i < n; i++)
        {
            string g = genes[i];
            int cur = 0;

            foreach (char ch in g)
            {
                int c = ch - 'a';
                if (nodes[cur].Next[c] == -1)
                {
                    nodes[cur].Next[c] = nodes.Count;
                    nodes.Add(new Node());
                }
                cur = nodes[cur].Next[c];
            }

            // end of gene g at node cur
            Node node = nodes[cur];
            node.Indices.Add(i);

            long last = node.PrefixHealth.Count > 0 ? node.PrefixHealth[node.PrefixHealth.Count - 1] : 0L;
            node.PrefixHealth.Add(last + health[i]);
        }

        // 2. Build failure links (Aho-Corasick) and fill missing transitions
        Queue<int> q = new Queue<int>();

        // root links
        for (int c = 0; c < 26; c++)
        {
            int nxt = nodes[0].Next[c];
            if (nxt != -1)
            {
                nodes[nxt].Link = 0;
                nodes[nxt].OutputLink = nodes[0].Indices.Count > 0 ? 0 : -1;
                q.Enqueue(nxt);
            }
            else
            {
                nodes[0].Next[c] = 0;
            }
        }
        nodes[0].Link = 0;
        nodes[0].OutputLink = -1;

        
        while (q.Count > 0)
        {
            int v = q.Dequeue();
            for (int c = 0; c < 26; c++)
            {
                int nxt = nodes[v].Next[c];
                if (nxt != -1)
                {
                    int fail = nodes[v].Link;
                    nodes[nxt].Link = nodes[fail].Next[c];

                   
                    if (nodes[nodes[nxt].Link].Indices.Count > 0)
                        nodes[nxt].OutputLink = nodes[nxt].Link;
                    else
                        nodes[nxt].OutputLink = nodes[nodes[nxt].Link].OutputLink;

                    q.Enqueue(nxt);
                }
                else
                {
                    nodes[v].Next[c] = nodes[nodes[v].Link].Next[c];
                }
            }
        }

        int s = Convert.ToInt32(Console.ReadLine().Trim());

        long minHealth = long.MaxValue;
        long maxHealth = long.MinValue;

        for (int si = 0; si < s; si++)
        {
            string[] line = Console.ReadLine().TrimEnd().Split(' ');
            int start = int.Parse(line[0]);
            int end = int.Parse(line[1]);
            string d = line[2];

            long total = 0L;
            int state = 0;

            foreach (char ch in d)
            {
                int c = ch - 'a';
                state = nodes[state].Next[c];

              
                int u = state;
                while (u != -1)
                {
                    if (nodes[u].Indices.Count > 0)
                        total += RangeSum(nodes[u], start, end);
                    u = nodes[u].OutputLink;
                }
            }

            if (total < minHealth) minHealth = total;
            if (total > maxHealth) maxHealth = total;
        }

        Console.WriteLine(minHealth + " " + maxHealth);
    }
}