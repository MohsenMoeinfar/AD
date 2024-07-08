using System;
using System.Collections;
using System.Collections.Generic;
public class Program
{
    static List<long>[] mygraph;
    static long[][] infoedges;
    static long start;
    static long finish;
    static long[] findpath()
    {
        bool[] visitd = new bool[mygraph.Length];
        long[] saver = new long[mygraph.Length];
        for(int i = 0 ; i< mygraph.Length ; i++)
        {
            saver[i] = -1 ; 
        }
        // Array.Fill(saver, -1);
        Queue<long> ququ = new Queue<long>();
        ququ.Enqueue(start);
        long curnode = start;
        visitd[start] = true;
        while (curnode != finish && ququ.Count > 0)
        {
            curnode = ququ.Dequeue();
            foreach (var item in mygraph[curnode])
            {
                if (visitd[item] == false && infoedges[curnode][item] != 0)
                {
                    ququ.Enqueue(item);
                    saver[item] = curnode;
                    visitd[item] = true;
                }
            }
        }
        return saver;
    }
    static void Main()
    {
        long[] numbers = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
        long numcity = numbers[0];
        long numroads = numbers[1];
        infoedges = new long[numcity][];
        mygraph = new List<long>[numcity];
        start = 0;
        finish = numcity - 1;
        for (long i = 0; i < numcity; i++)
        {
            mygraph[i] = new List<long>();
        }
        for (long i = 0; i < numcity; i++)
        {
            infoedges[i] = new long[numcity];
        }
        for (long j = 0; j < numroads; j++)
        {
            long[] infoedge = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long from = infoedge[0];
            long to = infoedge[1];
            long cap = infoedge[2];
            if (from != to)
            {
                infoedges[from - 1][to - 1] += cap;
            }
        }
        for (long i = 0; i < numcity; i++)
        {
            for (long k = 0; k < numcity; k++)
            {
                if (infoedges[i][k] != 0)
                {
                    mygraph[i].Add(k);
                    if (infoedges[k][i] == 0)
                    {
                        mygraph[k].Add(i);
                    }
                }
            }
        }
        maxflow();
    }
    static void maxflow()
    {
        long max = 0;
        bool br = false;
        while (br == false)
        {
            var pathfather = findpath();
            if (pathfather[finish] == -1)
            {
                br = true;
            }
            if (br == false)
            {
                List<long> respath = new List<long>();
                long cur = finish;
                while (start != cur)
                {
                    respath.Add(cur);
                    cur = pathfather[cur];
                }
                respath.Add(start);
                respath.Reverse();
                long min = long.MaxValue;
                for (int i = 0; i < respath.Count - 1; i++)
                {
                    long tmp = infoedges[respath[i]][respath[i + 1]];
                    if (tmp <= min)
                    {
                     min = tmp;
                    }
                }
                for (int i = 0; i < respath.Count - 1; i++)
                {
                    infoedges[respath[i]][respath[i + 1]] -= min;
                    infoedges[respath[i + 1]][respath[i]] += min;
                }
                 max += min;
            }
        }
          Console.WriteLine(max);
    }
}