using System ; 
using System.Collections.Generic  ; 
public class Program
{
    static bool[] truessss ; 
    static long[] cc  ; 
    static List<long>[] mygraph ;
    static long count = 0  ;  
    static void newexplore(long ver)
    {
            truessss[ver-1] = true ;
            cc[ver-1] = count ; 
             foreach(var j in mygraph[ver-1])
             {
               if(truessss[j-1] == false)
              {
                  newexplore(j) ; 
              }
          
            }
    }
    static void dfs()
    {
        for(int i = 1 ; i<= mygraph.Length ; i++)
        {
            if(truessss[i-1] == false)
            {
               newexplore(i)  ; 
               count++ ;
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
        dfs() ; 
        Array.Sort(cc) ; 
        long tmp  = -1 ;
        long count = 0 ; 
        foreach(var ii in cc)
        {
            if(tmp != ii)
            {
                tmp = ii  ;
                count++ ; 

            }
        }
        Console.WriteLine(count) ; 
       
    }
}