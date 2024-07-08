using System ; 
using System.Collections.Generic  ;

public class Program
{
    static void Main()
    {
        var firstine = Array.ConvertAll(Console.ReadLine().Split() , int.Parse)  ; 
        int vertex = firstine[0] ;  
        int edge = firstine[1]  ; 
        int[ ,] vars = new int[vertex , vertex]  ; 
        int[ , ] saver = new int[edge  , 2]  ; 
        for(int i = 0 ;  i < edge ; i++)
        {
            var line = Array.ConvertAll(Console.ReadLine().Split() , int.Parse)  ; 
            saver[i , 0] = line[0] ;  
            saver[i ,1]  = line[1]  ;  
        }
        List<string> all_res =  new List<string>() ; 
        int[ , ] graph = new int[ vertex, vertex ] ; 
        for(int i = 0 ; i < edge ; i++)
        {
            int v1 = saver[i , 0]-1 ; 
            int v2 = saver[i,1] -1 ; 
            graph[v1 , v2] = 1 ; 
            graph[v2 , v1] = 1  ; 
        }
        // List<List<int>> nodes = new List<List<int>>();
        int varcounter = 1 ; 
        for(int i=0 ; i < vertex ; i++)
        {
            for(int j = 0 ; j < vertex ; j++)
            {
                vars[i,j] = varcounter ; 
                varcounter++ ; 
            }
        }
        for(int i = 0 ; i < vertex ; i++)
        {
            int[] nodes = new int[vertex ] ; 
            for(int z = 0 ;z < vertex ; z++)
            {
                nodes[z] = vars[z,i] ; 
            }
            all_res.Add(string.Join(" " , nodes) + " 0") ; 
            for(int j = 0 ; j < vertex ; j++)
            {
                for(int k = j+1 ; k < vertex ; k++ )
                {
                      all_res.Add("-" + nodes[j] + " -" + nodes[k] + " 0");
                }
            }
        }
        for(int i = 0 ; i < vertex ; i++)
        {
            int[] nodes = new int[vertex ] ; 
            for(int z = 0 ;z < vertex ; z++)
            {
                nodes[z] = vars[i ,z] ; 
            }
            all_res.Add(string.Join(" " , nodes) + " 0") ; 
            for(int j = 0 ; j < vertex ; j++)
            {
                for(int k = j+1 ; k < vertex ; k++ )
                {
                      all_res.Add("-" + vars[i , j] + " -" + vars[i ,k] + " 0");
                }
            }
        }
        for(int i = 0 ; i < vertex ; i++)
        {
            for(int j = i+1 ; j < vertex ; j++)
            {
                if(graph[i ,j ]== 0)
                {
                    for(int z = 0 ; z < vertex-1 ; z++)
                    {
                        all_res.Add("-" + vars[i , z] + " -" + vars[j ,z+1] + " 0");
                        all_res.Add("-" + vars[j , z] + " -" + vars[i ,z+1] + " 0");
                    }
                }
            }
        }
       Console.WriteLine(all_res.Count + " " + vertex*vertex) ; 
       foreach(var line in all_res)
       {
        Console.WriteLine(line) ;
       }
    }
}