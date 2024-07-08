using System;
using System.Collections.Generic;
public class Program
{
    static bool[] truessss;
    static long[] cc;
    static List<long>[] mygraph;
    static long count = 0;
    static Stack<long> mm;
    static bool ans;
    static void newexplore(long ver)
    {

        mm.Push(ver);
        truessss[ver - 1] = true;
        cc[ver - 1] = count;
        foreach (var j in mygraph[ver - 1])
        {
            if (truessss[j - 1] == false)
            {
                foreach (var ia in mm)
                {
                    foreach (var z in mygraph[j - 1])
                    {

                        if (ia == z)
                        {
                            ans = true;

                        }

                    }
                }
            }
            if (truessss[j - 1] == false)
            {
                newexplore(j);
            }
        }
    }
    static void dfs()
    {
        for (int i = 1; i <= mygraph.Length; i++)
        {
            if (truessss[i - 1] == false)
            {
                mm = new Stack<long>();
                newexplore(i);
            }

        }
    }
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
        long vertices = firstline[0];
        truessss = new bool[vertices];
        long edges = firstline[1];
        cc = new long[vertices];
        mygraph = new List<long>[vertices];
        for (long i = 0; i < vertices; i++)
        {
            mygraph[i] = new List<long>();
        }
        for (long i = 0; i < edges; i++)
        {
            long[] line = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            mygraph[line[0] - 1].Add(line[1]);
        }
        dfs();
        if (ans == true)
        {
            Console.WriteLine(1);
        }
        else
        {
            Console.WriteLine(0);
        }
    }
}