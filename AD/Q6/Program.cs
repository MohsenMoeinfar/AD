using System ; 
using System.Collections.Generic  ; 
public class Program
{
    static bool[] truessss ; 
    static long[] dist ; 
    static Queue<long> myqu ; 
    static List<long>[] mygraph ; 
    
    static void Bfs(long from)
    {
        dist[from-1] = 0  ; 
        myqu.Enqueue(from)  ;  
        while(myqu.Count!=0 )
        {
            long tmp = myqu.Dequeue() ; 
            foreach(var j in mygraph[tmp-1])
            {
                if(dist[j-1] == long.MaxValue )
                {
                    myqu.Enqueue(j) ; 
                    dist[j-1] = dist[tmp-1] + 1  ; 
                }
            }
        }




    }
    // static void dfs()
    // {
    //     for(int i = 1 ; i<= mygraph.Length ; i++)
    //     {
    //         if(truessss[i-1] == false)
    //         {
    //            newexplore(i)  ; 
        
    //         }
    //     }
    // }
   
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        long vertices =  firstline[0] ; 
        long edges = firstline[1] ; 
        truessss = new bool[vertices] ;
        mygraph = new List<long>[vertices] ; 
        dist = new long[vertices]  ; 
        for(int i = 0 ; i< dist.Length ; i++)
        {
            dist[i] = long.MaxValue ; 
        }
        for (long i = 0; i < vertices; i++)
        {
           mygraph[i] = new List<long>();
        }
        for(long i = 0  ; i < edges ; i++)
        {
            long[] line = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;  
            mygraph[line[0]-1].Add(line[1]) ; 
            mygraph[line[1]-1].Add(line[0]) ; 
        }
        myqu = new Queue<long>() ;  
        long[] lastline =  Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        long from =  lastline[0] ;  
        long to = lastline[1]  ; 
        Bfs(from) ; 
        if(dist[to-1]== long.MaxValue)
        {
            Console.WriteLine(-1)  ;
        }
        else
        {
            Console.WriteLine(dist[to-1]) ; 
        }
    }
}
