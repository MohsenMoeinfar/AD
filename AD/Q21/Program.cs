using System ;  
using System.Collections.Generic  ;


public class Program
{
    static int[] computeprefix(string patt)
    {
        int[] myarr = new int[patt.Length]  ; 
        myarr[0] = 0  ; 
        int border =  0  ; 
        for(int i = 1 ;  i < patt.Length ; i++)
        {
            while(border> 0  && patt[i] != patt[border])
            {
                border = myarr[border-1]  ; 
            }
            if(patt[i] == patt[border])
            {
                border = border+1 ; 
            }
            else
            {
                border =  0 ; 
            }
            myarr[i] = border  ; 
        }
        return myarr ; 
    }
    static void Main()
    {
        string pattern = Console.ReadLine()  ;
        string txt = Console.ReadLine()  ; 
        string ptdtxt = pattern + '$' + txt  ; 
        List<long> myans = new List<long>()  ; 
        var prefixbaz = computeprefix(ptdtxt)  ; 
        for(int i = pattern.Length+1 ; i < ptdtxt.Length ; i++)
        {
            if(prefixbaz[i]== pattern.Length)
            {
                myans.Add(i-2*pattern.Length)  ; 
            }
        }
        foreach(var i in myans)
        {
            Console.Write(i + " ") ; 
        }

    }
}