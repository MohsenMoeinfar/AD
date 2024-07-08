using System ; 
using System.Collections.Generic  ; 
public class Node
{
    public int number ; 
    public char ch ; 
    public List<Node> ngh; 
    public Node(int n  , char cch)
    {
        ngh = new List<Node>() ; 
        this.number = n ; 
        this.ch = cch ; 
    }
}

public class Program
{
     static void Main()
    {
        int n =  int.Parse(Console.ReadLine())  ; 
        string[] mystrs = new string[n] ; 
        int count = 0  ; 
        List<string> ans = new List<string>() ; 
        for(int i =0 ; i < n ; i++)
        {
            mystrs[i]  = Console.ReadLine()  ; 
        }
        Node root = new Node(count , 'r') ; 
        var head =  root ; 
        var cur = root ; 
        count++ ; 
        foreach(var  i in mystrs)
        {
            foreach(var  ch in i)
            {
                if(cur.ngh.Count ==0)
                {
                    Node next = new Node(count , ch)  ;
                    cur.ngh.Add(next) ; 
                    ans.Add(cur.number.ToString()+"->"+count.ToString()+":"+ch) ; 
                    cur = next ; 
                    count++ ; 
                }
                else
                {
                    Node next2 = new Node(count , ch)  ;
                    int ss = 0 ; 
                    foreach(var k in cur.ngh)
                    {
                        if(k.ch == ch)
                        {
                            cur = k;
                            ss = 1 ; 
                        }
                    }
                    if(ss == 0 )
                    {
                        cur.ngh.Add(next2);
                        ans.Add(cur.number.ToString()+"->"+count.ToString()+":"+ch) ; 
                        cur = next2 ; 
                        count++ ; 
                    }
                }
               
            }
            cur = root ; 
        }
        foreach(var i in ans)
        {
            Console.WriteLine(i)  ;  
        }
     }
}