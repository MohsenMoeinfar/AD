using System  ; 
using System.Collections.Generic ;  
using System.Linq ; 

public class Program
{
    static List<string> cyclebaz  ;
    static void Main()
    {
        string inp = Console.ReadLine() ; 
        cyclebaz = new List<string>() ; 
        for(int i = 0 ; i < inp.Length ; i++)
        {
            string random1 =inp.Substring(i, inp.Length-i)+ inp.Substring(0 , i) ;
            cyclebaz.Add(random1) ; 
        }
        cyclebaz.Sort((a, b)=> a.CompareTo(b)) ; 
        string ans = "" ; 
        foreach(var i in cyclebaz) 
        {
            ans = ans + i.Last() ; 
        }
        Console.WriteLine(ans) ; 
    }
}