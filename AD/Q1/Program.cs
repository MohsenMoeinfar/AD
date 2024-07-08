using System ; 
using System.Collections.Generic  ; 
public class Program
{
    static bool[] truessss ; 
    static long[] cc  ; 
    static List<long>[] mygraph ;
    static long count = 1  ;  
    static void explore(long ver)
    {
        truessss[ver-1] = true ;
        cc[ver-1] = count ; 
        foreach(var j in mygraph[ver-1])
        {
            if(truessss[j-1] == false)
            {
                explore(j) ; 
            }
        }
    }
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        long vertices =  firstline[0] ; 
        truessss = new bool[vertices] ;
        long edges = firstline[1] ; 
        cc = new long[vertices] ; 
        mygraph = new List<long>[vertices] ; 
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
        long[] lastline = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        long vert1 = lastline[0] ; 
        long vert2 = lastline[1]  ; 
        explore(vert1); 
        if(cc[vert1-1] == cc[vert2-1])
        {
            Console.WriteLine(1) ; 
        }
        else
        {
            Console.WriteLine(0) ; 
        }
    }
}