using System  ; 
using System.Collections.Generic ; 
public class Program
{
    static int[] computecharclass(string txt ,  int[] order)
    {
        int[] cls  = new int[txt.Length]  ; 
        int  tmp  =  0 ; 
        cls[order[0]] =  tmp ; 
        for(int i = 1 ; i < txt.Length ; i++)
        {
            if(txt[order[i]] == txt[order[i-1]])
            {
                cls[order[i]]  =  tmp   ; 
            }
            else
            {
                tmp++ ; 
                cls[order[i]] = tmp  ; 
            }
        }
        return  cls  ; 
    }
    static int[] sortt(string txt)
    {
        int[] order  = new int[txt.Length] ; 
        int[] count = new int[5] ;  
        for(int i = 0 ; i < txt.Length ; i++)
        {
            switch(txt[i])
            {
                case '$' : 
                     count[0] ++ ; 
                     break ; 
                case 'A' : 
                      count[1] ++  ; 
                    break ; 
                case 'C' : 
                      count[2] ++ ;  
                      break ;  
                case 'G' : 
                      count[3] ++ ;  
                      break ;  
                case 'T' :
                      count[4] ++  ;  
                      break  ;
            }
        }
        for(int i =1 ; i < count.Length ; i++)
        {
            count[i] = count[i] + count[i-1]  ; 
        }
        for(int i = txt.Length-1 ;  i>=0 ; i--)
        {
              switch(txt[i])
            {
                case '$' : 
                     count[0] -- ; 
                     order[count[0]]  = i ; 
                     break ; 
                case 'A' : 
                      count[1] -- ; 
                    order[count[1]]  = i ; 
                    break ; 
                case 'C' : 
                      count[2] -- ;  
                        order[count[2]]  = i ; 
                      break ;  
                case 'G' : 
                      count[3] -- ;  
                      order[count[3]]  = i ; 
                      break ;  
                case 'T' :
                      count[4] -- ;  
                      order[count[4]]  = i ; 
                      break  ;
            }
        }
        return order ;  
    }
    static int[] sortdouble(string txt  , int L  , int[] order  , int[] cls)
    {
        int[] count  = new int[txt.Length]  ; 
        int[] neworder  = new int[txt.Length]  ; 
        for(int i = 0 ; i < txt.Length ; i++)
        {
            count[cls[i]] ++ ; 
        }
        for(int i =1  ; i < txt.Length ; i++)
        {
            count[i] = count[i] + count[i-1] ;
        }
        for(int i = txt.Length-1 ; i>= 0  ; i--)
        {
            int start = (order[i] - L + txt.Length) % txt.Length  ;
            int cl = cls[start] ; 
            count[cl] = count[cl] -1 ; 
            neworder[count[cl]] = start  ; 
        }
        return neworder  ;
    }
    static int[] updatecls(int[] order  , int[] cls , int L)
    {
        int[] newcls = new int[order.Length]  ;  
        int tmp = 0 ; 
        newcls[order[0]] = tmp ; 
        for(int i = 1 ; i < order.Length ; i++)
        {
            int cur = order[i]   ; 
            int prev = order[i-1]  ; 
            int mid = (order[i]+ L) % order.Length ; 
            int prevmid =  (order[i-1]+ L) % order.Length ; 
            if(cls[cur] != cls[prev] || cls[mid] != cls[prevmid])
            {
                tmp++  ;
                newcls[order[i]]  = tmp  ; 
            }
            else
            {
                newcls[order[i]]  =  tmp  ; 

            }
        }
        return newcls ; 
    }
    static int[] BuildSuffixArray(string txt)
    {
        int[] order = sortt(txt)  ; 
        int[] cls = computecharclass(txt  , order)  ; 
        int L = 1 ; 
        while(L < txt.Length)
        {
            order  = sortdouble(txt , L , order , cls)  ;
            cls =  updatecls(order , cls , L)  ; 
            L = 2 * L  ; 
        }
        return  order  ; 
    }
    static void Main()
    {
        string txt  =  Console.ReadLine()  ;  
        var mysuffixarray  =  BuildSuffixArray(txt)  ; 
        foreach(var i in mysuffixarray)
        {
            Console.Write(i + " ") ;  
        }
    }
}