using System;
using System.Collections.Generic;


public class Program
{
    static List<(long, long)>[] mygraph;
    static long[] dist;
    static List<long> manfi_baza ; 
    static List<long> manfi_baza_to ; 
    static long[] beforedist;
    static long[] prev;
    static Queue<long> myqu ; 
    static bool[] visited ; 
    static void Bfs()
    {
        while(myqu.Count!= 0 )
        {
           
            long vv = myqu.Dequeue()   ; 
            if(visited[vv-1]== false)
            {
                visited[vv-1]   = true ;  
                manfi_baza_to.Add(vv)  ; 

            }
            
            foreach(var j in mygraph[vv-1])
            {
                if(visited[j.Item1-1] == false)
                {
                    myqu.Enqueue(j.Item1)  ; 
                }
                
            }
        }
    }
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
        long verticesnum = firstline[0];
        long edgesnum = firstline[1];
        mygraph = new List<(long, long)>[verticesnum];
        dist = new long[verticesnum];
        prev = new long[verticesnum];
        myqu = new Queue<long>()  ; 
        visited =  new bool[verticesnum]  ; 
        manfi_baza = new List<long>()  ; 
        manfi_baza_to = new List<long>()  ; 
        bool[] ngcy  = new bool[verticesnum]  ;  
        // bool ans = false;
        beforedist = new long[verticesnum];
        for (long i = 0; i < verticesnum; i++)
        {
            mygraph[i] = new List<(long, long)>();
        }
        for (long i = 0; i < edgesnum; i++)
        {
            long[] info = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long from = info[0];
            long to = info[1];
            long weight = info[2];
            mygraph[from - 1].Add((to, weight));
        }
        long src = long.Parse(Console.ReadLine()) ; 
        for (long i = 0; i < verticesnum; i++)
        {
            dist[i] = long.MaxValue;
            prev[i] = -1;
        }
        dist[src-1] = 0;
        for (long i = 0; i < verticesnum-1; i++)
        {
            for (long j = 0; j < verticesnum; j++)
            {
                foreach (var k in mygraph[j])
                {
                    if (dist[k.Item1 - 1] > dist[j] + k.Item2 && dist[j] != long.MaxValue)
                    {
                        dist[k.Item1 - 1] = dist[j] + k.Item2;
                        prev[k.Item1 - 1] = j;
                    }
                }

            }
        }
        for (long j = 0; j < verticesnum; j++)
        {
            foreach (var k in mygraph[j])
            {
                if (dist[k.Item1 - 1] > dist[j] + k.Item2 && dist[j] != long.MaxValue)
                {
                    dist[k.Item1 - 1] = dist[j] + k.Item2;
                    manfi_baza.Add(k.Item1) ; 
                }
            }
        }
        foreach(var i in manfi_baza)
        {
            myqu.Enqueue(i)  ; 
        }
        Bfs()  ;
        foreach(var i in manfi_baza_to)
        {
            ngcy[i-1]  =  true ;  
        }
        for(long i = 0 ; i < verticesnum ;  i++)
        {
            if(dist[i] == long.MaxValue)
            {
                Console.WriteLine("*") ;  
            }
            else if(ngcy[i] == true)
            {
                Console.WriteLine("-") ; 
            }
            else
            {
                Console.WriteLine(dist[i]) ; 
            }
        }
    }
}