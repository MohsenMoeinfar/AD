using System ; 
using System.Collections.Generic  ;

public class Program
{
    static void Main()
    {
        var firstline = Array.ConvertAll(Console.ReadLine().Split() , int.Parse) ; 
        int vertex = firstline[0] ;  
        int edge = firstline[1] ;  
        int[,] infoedges = new int[edge , 2] ; 
        for(int i = 0 ; i < edge ; i++)
        {
            var line = Array.ConvertAll(Console.ReadLine().Split() , int.Parse) ; 
            infoedges[i, 0] = line[0] ; 
            infoedges[i,1] = line[1] ;  
        }
        List<string> all_res = new List<string>() ; 
        int[] cols = new int[3] ; 
        for(int i = 1 ; i <=3 ; i++)
        {
            cols[i-1] = i ;
        }
        for(int i = 0 ; i < vertex ; i++)
        {
            int[] vars = new int[3] ; 
            for(int j = 0 ; j < cols.Length ; j++)
            {
                vars[j] = 3*i +  cols[j]  ; 
            }
            all_res.Add(string.Join(" " , vars) + " 0");
            for(int j = 0 ; j < vars.Length ;j++)
            {
                for(int k = j+ 1 ; k  < vars.Length ; k++)
                {
                     all_res.Add("-" + vars[j] + " -" + vars[k] + " 0");
                }
            }
        }
        for(int i = 0 ;  i < edge ; i++)
        {
            for(int j = 0 ; j < cols.Length ; j++)
            {
                int from = infoedges[i , 0] -1 ; 
                int to = infoedges[i,1] -1 ; 
                int varfrom = 3*from  + cols[j] ; 
                int varto = 3*to + cols[j] ; 
                all_res.Add("-" + varfrom + " -" + varto + " 0");
                // all_res.Add($"-{varfrom} -{varto} 0");
            }
        }
        Console.WriteLine(all_res.Count + " " +(vertex*3)) ; 
        for(int i = 0 ; i < all_res.Count ; i++)
        {
            Console.WriteLine(all_res[i]) ; 
        }
    }
}