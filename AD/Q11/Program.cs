using System  ;
using System.Collections.Generic  ;
public class Program
{
    static List<((int  , int) , double)> mygraph ; 
    static List<(int , int)> inp  ; 
    static long Find(long x , long[] parents)
    {
        if (x != parents[x])
            parents[x] = Find(parents[x] , parents);
        return parents[x];
    }
    static double Cal(int x1 , int x2 , int y1 , int y2)
    {
        double hehe1 = Math.Pow(x1-x2 , 2) ;  
        double hehe2 = Math.Pow(y1-y2 , 2) ;  
        double ans = Math.Sqrt(hehe1+hehe2)  ; 
        return ans  ; 

    }
    static void Merge(long destination, long source , long[] rank , long[] parents)
    {
        long rootofdest = Find(destination ,parents);
        long rootofsource = Find(source , parents);
        if (rank[rootofdest] < rank[rootofsource])
        {
            parents[rootofdest] = rootofsource;
        }
        else
        {
            parents[rootofsource] = rootofdest;
            if (rank[rootofdest] == rank[rootofsource])
            {
                rank[rootofdest]++;
            }
             
        }
    }
    static void Main()
    {
        int num =  int.Parse(Console.ReadLine()) ;  
        long[] parents = new long[num];
        long[] rank = new long[num];
        for (long i = 0; i < num; i++)
        {
            rank[i] = 0;
            parents[i] = i;
        }
        mygraph =  new List<((int, int), double)> () ; 
        double ans = 0  ; 
        inp = new List<(int, int)>()  ; 
        for(int i  = 0 ;  i< num  ; i++)
        {
            var lineinfo =  Array.ConvertAll(Console.ReadLine().Split() ,  int.Parse) ;  
            int x = lineinfo[0]  ; 
            int y  = lineinfo[1]  ; 
            inp.Add((x,  y)) ; 
        }
        for(int i  =  0 ; i< num   ;  i++)
        {
            for(int j = i+1 ;  j < num ; j++)
            {
                int x1 = inp[i].Item1  ;
                int y1 = inp[i].Item2 ;  
                int x2 = inp[j].Item1  ; 
                int y2  = inp[j].Item2   ;
                double dist= Cal(x1 , x2 , y1 , y2) ; 
                mygraph.Add(((i,j) , dist)) ; 
            }
        }
        mygraph.Sort((x,y)=> x.Item2.CompareTo(y.Item2)) ; 
        foreach(var i in mygraph)
        {
            int V1  = i.Item1.Item1  ; 
            int V2 =  i.Item1.Item2  ;  
            if(Find(V1 , parents) != Find(V2 , parents))
            {
                ans   = ans + i.Item2 ;  
                Merge(V1 , V2  , rank , parents)  ; 
            } 
        }
        Console.WriteLine(ans.ToString("F9"))  ;  
    }
}