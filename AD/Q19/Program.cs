using System  ; 
using System.Collections.Generic ;  
using System.Linq ; 

public class Program
{
    static int Bwtmatching(int[] firstocc  , int[,] countinlast , string[] last , string pattern)
    {
        int top = 0 ; 
        int bottom = last.Length-1 ; 
        while(top <= bottom)
        {
            if(pattern.Length > 0)
            {
                char goal = pattern[pattern.Length-1]  ;
                pattern = pattern.Remove(pattern.Length-1, 1) ; 
                switch(goal)
                {
                    case 'A':
                       top = firstocc[1] + countinlast[top,1];
                       bottom = firstocc[1] + countinlast[bottom+1 , 1] -1 ; 
                       break ; 
                    case 'T':
                       top = firstocc[4] + countinlast[top,4];
                       bottom = firstocc[4] + countinlast[bottom+1 , 4] -1 ; 
                       break ; 
                    case 'C':
                       top = firstocc[2] + countinlast[top,2];
                       bottom = firstocc[2] + countinlast[bottom+1 , 2] -1 ; 
                       break ; 
                    case 'G':
                       top = firstocc[3] + countinlast[top,3];
                       bottom = firstocc[3] + countinlast[bottom+1 , 3] -1 ; 
                       break ; 
                }
            }
            else
            {
                return bottom-top+1 ; 
            }
        }
         return bottom-top+1 ; 
    }
    static List<char> Sortstr(string mystr)
    {
        List<char> mychar = new List<char>() ; 
        int[] counts = new int[5];
        foreach (char c in mystr)
        {
            switch (c)
            {
                case 'A':
                    counts[1]++;
                    break;
                case 'C':
                    counts[2]++;
                    break;
                case 'G':
                    counts[3]++;
                    break;
                case 'T':
                    counts[4]++;
                    break;
            }
        }
        mychar.Add('$');
        for (int i = 0; i < counts[1]; i++)
            mychar.Add('A');
        for (int i = 0; i < counts[2]; i++)
            mychar.Add('C');
        for (int i = 0; i < counts[3]; i++)
            mychar.Add('G');
        for (int i = 0; i < counts[4]; i++)
            mychar.Add('T');
        return mychar;
    }
    static void Main()
    {
        string inp = Console.ReadLine() ; 
        int numberofpattern   =  int.Parse(Console.ReadLine()) ;  
        var mypatterns = Console.ReadLine().Split() ; 
        int[,] countinlast = new int[inp.Length+1 , 5] ; 
        for(int i =0  ; i < inp.Length ;  i++)
        {
            for(int k = 0 ; k < 5 ; k++)
            {
                countinlast[i+1 , k] = countinlast[i , k]  ; 
            }
            switch(inp[i])
            {
                case 'A':
                   countinlast[i+1 , 1] = countinlast[i , 1] + 1  ;
                   break ;
                case 'C':
                     countinlast[i+1 ,2] = countinlast[i , 2] + 1 ;
                     break ; 
                case 'G' :
                     countinlast[i+1 , 3] = countinlast[i  , 3] + 1  ;
                     break ;
                case 'T' :
                     countinlast[i+1 , 4] = countinlast[i  , 4] + 1  ;
                     break ; 
                case '$' :
                     countinlast[i+1 , 0] =  countinlast[i , 0] + 1 ;
                     break;
            }
        }
        int[] firstocc = new int[5] ; 
        string firstclmn =  string.Join("" , Sortstr(inp));
        int[] counts = new int[5];
        Dictionary<string , string> first_to_last  = new Dictionary<string, string>()  ; 
        Dictionary<string , string> last_to_first =  new Dictionary<string, string>() ; 
        string[] mylast = new string[inp.Length]  ;  
        string[] myfirst = new string[inp.Length]  ;  
        bool Abazi = false  ; 
        bool Cbazi = false  ; 
        bool Gbazi = false  ; 
        bool Tbazi = false  ; 
        for(int i  = 0 ;  i < firstclmn.Length ; i++)
        {
              switch(firstclmn[i])
            {
                case 'A': 
                if(Abazi == false)
                {
                    firstocc[1] =  i ; 
                    Abazi = true ; 
                }
                   break  ;  
                case 'C': 
                if(Cbazi == false)
                {
                    firstocc[2] = i ; 
                    Cbazi = true ;
                }
                   break  ; 
                case 'G': 
                   if(Gbazi == false)
                   {
                    firstocc[3] = i  ;
                    Gbazi = true ; 
                   }
                   break  ; 
                case 'T': 
                   if(Tbazi == false)
                   {
                    firstocc[4] = i  ;
                    Tbazi = true ; 
                   }
                   break  ; 
                case '$':
                      firstocc[0] = i ;
                      break;
            }
        }
        for(int i = 0 ; i < inp.Length ;  i++)
        {
            switch(inp[i])
            {
                case 'A': 
                   int num = counts[1] ;
                   counts[1]++ ; 
                   mylast[i] =  'A' + num.ToString() ; 
                   break  ;  
                case 'C': 
                   int num2 = counts[2] ;
                   counts[2]++ ; 
                   mylast[i] =  'C' + num2.ToString() ; 
                   break  ; 
                case 'G': 
                   int num3 = counts[3] ;
                   counts[3]++ ; 
                   mylast[i] =  'G' + num3.ToString() ; 
                   break  ; 
                case 'T': 
                   int num4 = counts[4] ;
                   counts[4]++ ; 
                   mylast[i] =  'T' + num4.ToString() ; 
                   break  ; 
                case '$':
                      int num0 = counts[0] ; 
                      mylast[i] = '$'  +  num0.ToString();
                      break;
            }
        }
        counts = new int[5];
        for(int i = 0 ; i < inp.Length ;  i++)
        {
            switch(firstclmn[i])
            {
                case 'A': 
                   int num = counts[1] ;
                   counts[1]++ ; 
                   myfirst[i] =  'A' + num.ToString() ; 
                  
                   break  ;  
                case 'C': 
                   int num2 = counts[2] ;
                   counts[2]++ ; 
                   myfirst[i] =  'C' + num2.ToString() ; 
                   break  ; 
                case 'G': 
                   int num3 = counts[3] ;
                   counts[3]++ ; 
                   myfirst[i] =  'G' + num3.ToString() ; 
                   break  ; 
                case 'T': 
                   int num4 = counts[4] ;
                   counts[4]++ ; 
                   myfirst[i] =  'T' + num4.ToString() ; 
                   break  ; 
                case '$':
                      int num0 = counts[0] ; 
                      myfirst[i] = '$'  +  num0.ToString();
                      break;
            }
        }
        for(int i = 0 ; i< inp.Length ; i++)
        {
            first_to_last.Add(myfirst[i]  , mylast[i]) ; 
            last_to_first.Add(mylast[i] , myfirst[i])  ; 
        }
        List<int> myans = new List<int>()  ;
        foreach(var mystr in mypatterns)
        {
            int mynumber = Bwtmatching(firstocc , countinlast , mylast , mystr);
           Console.Write(mynumber + " ") ; 
        }
    }
}
