using System;
using System.Collections.Generic;
public class Node
{
    public char ch;
    public Tuple<int , int> stafin ; 
    public List<Node> ngh;
    public Node(Tuple<int , int> mytp, char cch)
    {
        stafin = mytp ; 
        ngh = new List<Node>();
        this.ch = cch;
    }
}
public class Program
{
    static Node saver  ;  
    static void print(Node node , string text)
    {
        if(node.stafin.Item1 != -1 && node.stafin.Item2!= -1)
        {
            Console.WriteLine(text.Substring(node.stafin.Item1 , node.stafin.Item2-node.stafin.Item1+1)) ; 
        }
        foreach(var chichild in node.ngh)
        {
                print(chichild , text)  ; 
        }
    }
    static void Main()
    {
        string txt = Console.ReadLine();
        bool find ; 
        Node root = new Node(new Tuple<int, int>(-1, -1), 'r');
        var head = root;
        bool ss = false  ; 
        var cur = root;
        for (int i = 0; i < txt.Length; i++)
        {
             cur = root;
             find = false ; 
             for(int k = i  ; k < txt.Length ;  k++)
             {
                char curchar = txt[k] ;  
                foreach(var bache in cur.ngh)
                {
                    if(bache.ch ==curchar)
                    {
                        find  = true ;  
                        saver = bache ; 
                        break  ; 
                    }
                }
                if(find == false)
                {
                    Node newnew  = new Node(new Tuple<int, int>(k , txt.Length-1) , curchar) ; 
                    cur.ngh.Add(newnew)  ; 
                    break ; 
                }
                else
                {
                    int m = saver.stafin.Item1  ; 
                    while(k < txt.Length && m <= saver.stafin.Item2)
                    {
                        if(txt[k] != txt[m])
                        {
                            int aa = saver.stafin.Item2 ;
                            
                            Node shekaft = new Node(new Tuple<int, int>(saver.stafin.Item1 , m-1) , curchar) ; 
                            Node newbranch1 = new Node(new Tuple<int, int>(k , txt.Length-1)  , txt[k]) ;  
                            cur.ngh.Remove(saver)  ;
                            cur.ngh.Add(shekaft) ; 
                            shekaft.ngh.Add(saver)  ; 
                            shekaft.ngh.Add(newbranch1) ; 
                            saver.stafin = new Tuple<int, int>(m , aa)  ; 
                            saver.ch = txt[m]   ;
                            ss = true  ; 
                            break  ; 
                        }
                        k++ ; 
                        m++ ; 
                    }
                    if(ss == true)
                    {
                        ss = false  ; 
                        break  ; 
                    }
                    if(m > saver.stafin.Item2)
                    {
                        find = false ;
                        cur = saver ; 
                        k-- ; 
                    }
                }
             }
        }
        cur = root ; 
        print(cur , txt)  ; 
    }
}