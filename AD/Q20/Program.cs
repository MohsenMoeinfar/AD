using System ; 
using System.Collections.Generic ; 

public class Program
{
    static void Main()
    {
        string inp = Console.ReadLine() ;  
        List<Tuple<string,int>> mysuffix  = new List<Tuple<string, int>>()  ;
        for(int i = 0 ; i< inp.Length ; i++)
        {
            string sufbaz= inp.Substring(i , inp.Length-i-1) ;
            mysuffix.Add(new Tuple<string, int>(sufbaz , i)) ; 
        }
        mysuffix.Sort((a,b)=> a.Item1.CompareTo(b.Item1)) ; 
        foreach(var k in mysuffix)
        {
            Console.Write(k.Item2 + " ") ; 
        }
    }
}
