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
        long flights = numbers[0];
        long crews = numbers[1];
        infoedges = new long[flights+crews+2][];
        mygraph = new List<long>[flights+crews+2];
        start = 0;
        finish = flights+crews+1;
        for (long i = 0; i < mygraph.Length; i++)
        {
            mygraph[i] = new List<long>();
        }
        for (long i = 0; i < infoedges.Length; i++)
        {
            infoedges[i] = new long[flights+crews+2];
        }
        for (long j = 0; j < flights; j++)
        {
            long[] infoedge = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            for(int k = 0 ; k < crews ; k++)
            {
                infoedges[j+1][k+flights+1] = infoedge[k] ; 
            }
            // infoedges[from - 1][to - 1] += cap;
        }
        for(int i = 1 ; i <= flights ; i++)
        {
            infoedges[0][i] = 1 ;
        }
        for(int m = (int)flights+1 ; m < flights+crews + 1 ; m++ )
        {
            infoedges[m][flights+crews+1] = 1 ; 
        }
        for (long i = 0; i < flights+crews+2; i++)
        {
            for (long k = 0; k < flights+crews+2; k++)
            {
                if (infoedges[i][k] == 1)
                {
                    mygraph[i].Add(k) ; 
                    mygraph[k].Add(i)  ;
                }
            }
        }
        maxflow();
        for(int i = 1 ; i <= flights ; i++)
        {
            long tmp = -1 ; 
            foreach(var hamsaye in mygraph[i])
            {
                if(hamsaye != 0 && infoedges[i][hamsaye]==0 )
                {
                    tmp = hamsaye-flights  ; 
                    break ; 
                }
               
            }
            Console.Write(tmp + " ") ; 
        }
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
        //   Console.WriteLine(max);
    }
}