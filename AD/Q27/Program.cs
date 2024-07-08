using System;
using System.Collections;
using System.Collections.Generic;
public class Program
{
    static List<long>[] mygraph;
    static long[][] information;
    static long[][] infoedges ; 
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
        long n = numbers[0];
        long k = numbers[1];
        information = new long[n][] ; 
        for(int i = 0 ;  i < n  ; i++)
        {
            information[i] = new long[k] ; 
        }
        for(int i = 0 ; i < n ; i++)
        {
            var line = Array.ConvertAll(Console.ReadLine().Split() , long.Parse) ; 
            for(int j = 0 ; j < k ; j++)
            {
                information[i][j] = line[j] ; 
            }
        }
        long[][] moshtarakat = new long[n][] ; 
        for(int i = 0  ; i < n ; i++)
        {
            moshtarakat[i] = new long[n] ;  
        }
        for(int z= 0 ; z < n ; z++)
        {
            for(int s = 0 ; s < n ; s++)
            {
                moshtarakat[z][s] = 1 ; 
            }
        }
        for(int stock1 = 0 ; stock1 < information.Length ; stock1++)
        {
            for(int stock2 = 0 ; stock2 < information.Length ; stock2++)
            {
                for(int point = 0 ; point < k ; point++)
                {
                    if(information[stock2][point] <= information[stock1][point])
                    {
                        moshtarakat[stock1][stock2] = 0 ; 
                    }
                }
            }
        }
        infoedges = new long[(2*n)+2][];
        mygraph = new List<long>[(2*n)+2];
        start = 0;
        finish = 2*n+1;
        for (long i = 0; i < mygraph.Length; i++)
        {
            mygraph[i] = new List<long>();
        }
        for (long i = 0; i < infoedges.Length; i++)
        {
            infoedges[i] = new long[(2*n)+2];
        }
        for (long j = 0; j < n; j++)
        {
            for(int b = 0 ; b < n ; b++)
            {
                if(moshtarakat[j][b]==1)
                {
                    infoedges[j+1][b+n+1] =1 ; 
                }
            }
            // infoedges[from - 1][to - 1] += cap;
        }
        for(int i = 1 ; i <= n ; i++)
        {
            infoedges[0][i] = 1 ;
        }
        for(int m = (int)n+1 ; m < 2*n+ 1 ; m++ )
        {
            infoedges[m][2*n+1] = 1 ; 
        }
        for (long i = 0; i < 2*n+2; i++)
        {
            for (long z = 0; z < 2*n+2; z++)
            {
                if (infoedges[i][z] == 1)
                {
                    mygraph[i].Add(z) ; 
                    mygraph[z].Add(i)  ;
                }
            }
        }
        var max = maxflow();
        Console.WriteLine(n-max) ; 
    }
    static long maxflow()
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
        return max ; 
    }
}