using System  ; 
using System.Collections.Generic ;  
using System.Linq ; 

public class Program
{
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
        string firstclmn =  string.Join("" , Sortstr(inp));
        int[] counts = new int[5];
        Dictionary<string , string> first_to_last  = new Dictionary<string, string>()  ; 
        Dictionary<string , string> last_to_first =  new Dictionary<string, string>() ; 
        string[] mylast = new string[inp.Length]  ;  
        string[] myfirst = new string[inp.Length]  ;  
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
        List<char> mychar = new List<char>()  ;
        mychar.Add('$') ; 
        string firstattack = first_to_last["$0"];
        while(firstattack != "$0")
        {
            char first = firstattack[0];
            mychar.Add(first) ; 
            firstattack = first_to_last[firstattack];
        }
        mychar.Reverse() ; 
        Console.WriteLine(string.Join("" , mychar)) ; 
    }
}
