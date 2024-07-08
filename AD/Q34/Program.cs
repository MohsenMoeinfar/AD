using System ; 
using System.Collections.Generic ; 

public class Program
{
    static int numbernodes ; 
    static List<int>[] tree ;  
    static int[] dp_val ; 
    static int[] funfactor ; 
    static void Main()
    {
        numbernodes = int.Parse(Console.ReadLine()) ; 
        dp_val = new int[numbernodes]  ; 
        tree = new List<int>[numbernodes] ; 
        funfactor  = Array.ConvertAll(Console.ReadLine().Split() , int.Parse) ; 
        for (int i = 0; i < numbernodes; i++)
        {
            tree[i] = new List<int>();
        }
        for(int i =0 ; i < numbernodes-1  ; i++)
        {
            var connection = Array.ConvertAll(Console.ReadLine().Split() , int.Parse)  ; 
            tree[connection[0]-1].Add(connection[1]-1) ; 
            tree[connection[1]-1].Add(connection[0]-1) ; 
        }
        for(int i = 0 ; i < numbernodes ; i++)
        {
            dp_val[i] = -1 ; 
        }
        var ans = funparty(0 , -1)  ;
        Console.WriteLine(ans) ; 
    }
    static int funparty(int vertex , int father)
    {
        if(dp_val[vertex] != -1)
        {
            return dp_val[vertex] ; 
        }
        int m1 = funfactor[vertex]  ; 
        int m0 = 0 ; 
        foreach(var child in tree[vertex])
        {
            if(child != father)
            {
                foreach(var childchild in tree[child])
                {
                    if(childchild != vertex)
                    {
                        m1+= funparty(childchild , child) ; 
                    }
                }
                m0+= funparty(child , vertex)  ; 
            }
        }
        dp_val[vertex] = Math.Max(m1 ,m0) ; 
        return dp_val[vertex] ;  
    }
}
