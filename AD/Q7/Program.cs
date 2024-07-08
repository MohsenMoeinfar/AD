using System ; 
using System.Collections.Generic  ; 
public class Program
{
    static bool[] truessss ; 
    static long[] dist ; 
    static Queue<long> myqu ; 
    static List<long>[] mygraph ; 
    static bool ans = true ; 
    static string[] color ;
    static void Bfs(long from)
    {
        truessss[from-1] = true ; 
        dist[from-1] = 0  ; 
        color[from-1] = "B" ; 
        myqu.Enqueue(from)  ;  
        while(myqu.Count!=0 )
        {
            long tmp = myqu.Dequeue() ; 
            foreach(var j in mygraph[tmp-1])
            {
                if(dist[j-1] == long.MaxValue)
                {
                    myqu.Enqueue(j) ; 
                    truessss[j-1] = true ;  
                    dist[j-1] = dist[tmp-1] + 1  ; 
                    string curcol = color[tmp-1] ; 
                    if(curcol == "B")
                    {
                        color[j-1]  = "W" ; 
                    }
                    else
                    {
                        color[j-1] = "B" ;
                    }
                }
                else
                {
                    if(color[tmp-1] ==  color[j-1])
                    {
                        ans = false ; 
                        return ; 

                    }
                }
            }
        }
    }
    static void Main()
    {
        long[] firstline = Array.ConvertAll(Console.ReadLine().Split(),long.Parse) ;
        long vertices =  firstline[0] ; 
        long edges = firstline[1] ; 
        truessss = new bool[vertices] ;
        mygraph = new List<long>[vertices] ; 
        dist = new long[vertices]  ; 
        color = new string[vertices] ; 
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
        for(int i = 1 ; i<= vertices ; i++)
        {
            if(truessss[i-1]== false)
            {
                Bfs(i) ; 
            }
        }
        if(ans == false)
        {
            Console.WriteLine(0) ; 
        }
        else
        {
            Console.WriteLine(1) ; 
        }
        }
    }

