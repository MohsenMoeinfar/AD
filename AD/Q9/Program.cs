using System;
using System.Collections.Generic;

public class Program
{
    static List<(int, int)>[] mygraph;
    static int[] dist;
    static int[] beforedist;
    static int[] prev;
    static void Main()
    {
        int[] firstline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int verticesnum = firstline[0];
        int edgesnum = firstline[1];
        mygraph = new List<(int, int)>[verticesnum];
        dist = new int[verticesnum];
        prev = new int[verticesnum];
        bool ans = false;
        beforedist = new int[verticesnum];
        for (int i = 0; i < verticesnum; i++)
        {
            mygraph[i] = new List<(int, int)>();
        }
        for (int i = 0; i < edgesnum; i++)
        {
            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int from = info[0];
            int to = info[1];
            int weight = info[2];
            mygraph[from - 1].Add((to, weight));
        }
        for (int i = 0; i < verticesnum; i++)
        {
            dist[i] = int.MaxValue / 2;
            prev[i] = -1;
        }
        dist[0] = 0;
        for (int i = 0; i < verticesnum-1; i++)
        {
            for (int j = 0; j < verticesnum; j++)
            {
                foreach (var k in mygraph[j])
                {
                    if (dist[k.Item1 - 1] > dist[j] + k.Item2)
                    {
                        dist[k.Item1 - 1] = dist[j] + k.Item2;
                        prev[k.Item1 - 1] = j;
                    }
                }

            }

        }
        for (int i = 0; i < verticesnum; i++)
        {
            beforedist[i] = dist[i];
        }
        for (int j = 0; j < verticesnum; j++)
        {
            foreach (var k in mygraph[j])
            {
                if (dist[k.Item1 - 1] > dist[j] + k.Item2)
                {
                    ans = true ;  
                    break   ; 
                }
            }

        }
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