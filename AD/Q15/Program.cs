using System;
using System.Collections.Generic;
public class Node
{
    public int number;
    public char ch;
    public List<Node> ngh;
    public Node(int n, char cch)
    {
        ngh = new List<Node>();
        this.number = n;
        this.ch = cch;
    }
}
public class Program
{
    static void Main()
    {
        string txt = Console.ReadLine() ;
        int n = int.Parse(Console.ReadLine());
        string[] mystrs = new string[n];
        int count = 0;
        List<string> ans = new List<string>();
        for (int i = 0; i < n; i++)
        {
            mystrs[i] = Console.ReadLine() + '$';
        }
        Node root = new Node(count, 'r');
        var head = root;
        var cur = root;
        count++;
        foreach (var i in mystrs)
        {
            foreach (var ch in i)
            {
                if (cur.ngh.Count == 0)
                {
                    Node next = new Node(count, ch);
                    cur.ngh.Add(next);
                    ans.Add(cur.number.ToString() + "->" + count.ToString() + ":" + ch);
                    cur = next;
                    count++;
                }
                else
                {
                    Node next2 = new Node(count, ch);
                    int ss = 0;
                    foreach (var k in cur.ngh)
                    {
                        if (k.ch == ch)
                        {
                            cur = k;
                            ss = 1;
                        }
                    }
                    if (ss == 0)
                    {
                        cur.ngh.Add(next2);
                        ans.Add(cur.number.ToString() + "->" + count.ToString() + ":" + ch);
                        cur = next2;
                        count++;
                    }
                }
            }
            cur = root;
        }
        bool goal = false ; 
        bool random =  false  ; 
        List<int> ans2 =  new List<int>()  ; 
        for (int i = 0; i < txt.Length; i++)
        {
            string sufstr = txt.Substring(i);
            foreach (var tt in sufstr)
            {
                foreach (var ng in cur.ngh)
                {
                    
                    if (tt == ng.ch )
                    {
                        cur = ng;
                        goal = true  ; 
                        break;
                    }
                    else
                    {
                        goal  = false  ;  
                    }
                }
                foreach (var ng in cur.ngh)
                {
                    if(ng.ch == '$')
                    {
                        random =  true ; 
                        ans2.Add(i)  ; 
                        break ; 
                    }

                }
                if(random == true)
                {
                    break ; 
                }
                if(goal== false)
                {
                    break  ; 
                }
                if(cur.ngh.Count == 0)
                {
                    ans2.Add(i) ; 
                    break ;  

                }
            }
            random = false ; 
            cur = root ; 
        }
        foreach(var  i in ans2)
        {
            Console.Write(i + " ")  ;  
        }
    }
}