using System ; 
using System.Collections.Generic  ; 
public class Program
{
    static bool[] truessss ; 
    static long[] cc  ; 
    static List<long>[] revmygraph ;
    static List<long>[] mygraph ; 
    static long[ , ] preandpost ; 
    static long count = 1  ;  
    static long count2 = 0 ; 
    static Stack<long> ans ; 
    static void newexplore(long ver)
    {
            truessss[ver-1] = true ;
            preandpost[ver-1 , 0] = count ; 
            count++ ; 
            foreach(var j in revmygraph[ver-1])
             {
               if(truessss[j-1] == false)
               {
                  newexplore(j) ; 
               }
             }
            preandpost[ver-1 , 1] = count  ;
            count++ ;  
    }
    static void newexplore2(long ver)
    {
            truessss[ver-1] = true ;
            foreach(var j in mygraph[ver-1])
             {
               if(truessss[j-1] == false)
              {
                  newexplore2(j) ; 
              }
             }
            
    }
    static void dfs()
    {
        for(int i = 1 ; i<= revmygraph.Length ; i++)
        {
            if(truessss[i-1] == false)
            {
               newexplore(i)  ; 
            }
        }
    }
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        ans = new Stack<long>() ; 
        long vertices =  firstline[0] ; 
        preandpost =  new long[vertices, 2]; 
        truessss = new bool[vertices] ;
        long edges = firstline[1] ; 
        cc = new long[vertices] ; 
        revmygraph = new List<long>[vertices] ; 
        mygraph = new List<long>[vertices] ; 
        Tuple<long , long>[] myposts = new Tuple<long , long>[vertices]   ; 
        for (long i = 0; i < vertices; i++)
        {
           revmygraph[i] = new List<long>();
        }
        for (long i = 0; i < vertices; i++)
        {
           mygraph[i] = new List<long>();
        }
        for(long i = 0  ; i < edges ; i++)
        {
            long[] line = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;  
            revmygraph[line[1]-1].Add(line[0]) ; 
            mygraph[line[0]-1].Add(line[1]) ; 
        }
        dfs() ; 
        for(int i = 0 ;  i< myposts.Length ; i++)
        {
            myposts[i]  = new Tuple<long, long>(i , preandpost[i , 1])  ;  
        }
        Array.Sort(myposts , (y,x) => x.Item2.CompareTo(y.Item2)) ; 
        // foreach(var k in myposts)
        // {
        //     Console.WriteLine(k) ; 
        // }
        truessss = new bool[vertices] ;
        for(int i = 0 ; i< myposts.Length ; i++)
        {
            if(truessss[myposts[i].Item1] == false)
            {
                newexplore2(myposts[i].Item1+1)  ; 
                count2++ ; 
            }
        }
        Console.WriteLine(count2)  ; 
    }
}
