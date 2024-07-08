using System;
using System.Collections.Generic;
using System.Linq ; 
public class SuffixTreeNode
{
    public SuffixTreeNode Parent;
    public Dictionary<char, SuffixTreeNode> Children;
    public int Depth;
    public int Start;
    public int End;
    public SuffixTreeNode(int start, int end, int depth, SuffixTreeNode parent)
    {
        this.Start = start;
        this.End = end;
        this.Depth = depth;
        this.Children = new Dictionary<char, SuffixTreeNode>();
        this.Parent = parent;
    }
}
public class Program
{
    static string mystr;
    static SuffixTreeNode root;
    static int[] suffixarr;
    static int[] lcparr;
    static void Buildsuffixtree()
    {
        root = new SuffixTreeNode(-1, -1, 0, null);
        int Lcp_prev = 0;
        SuffixTreeNode curnode = root;
        for (int i = 0; i < mystr.Length; i++)
        {
            int suffix = suffixarr[i];
            while (curnode.Depth > Lcp_prev)
            {
                curnode = curnode.Parent;
            }
            if (curnode.Depth == Lcp_prev)
            {
                curnode = create_new_leaf(curnode, suffix);
            }
            else
            {
                int edgestart = suffixarr[i - 1] + curnode.Depth;
                int offset = Lcp_prev - curnode.Depth;
                SuffixTreeNode midnode = BreakEdge(curnode, edgestart, offset);
                curnode = create_new_leaf(midnode, suffix);
            }
            if (i < mystr.Length - 1)
            {
                Lcp_prev = lcparr[i];
            }
        }
    }
    static SuffixTreeNode BreakEdge(SuffixTreeNode node, int start, int offset)
    {
        int end = start + offset;
        int depth = node.Depth + offset;
        char startchar = mystr[start];
        char midchar = mystr[end];
        SuffixTreeNode midnode = new SuffixTreeNode(start, end, depth, node);
        midnode.Children[midchar] = node.Children[startchar];
        node.Children[startchar].Parent = midnode;
        node.Children[startchar].Start += offset;
        node.Children[startchar] = midnode;
        return midnode;
    }
    static SuffixTreeNode create_new_leaf(SuffixTreeNode node, int suffix)
    {
        int start = suffix + node.Depth;
        int end = mystr.Length;
        int depth = mystr.Length - suffix;
        SuffixTreeNode leaf = new SuffixTreeNode(start, end, depth, node);
        node.Children[mystr[start]] = leaf;
        return leaf;
    }
    static void Print(SuffixTreeNode root)
    {
        if (root == null)
        {
            return;
        }
        Stack<SuffixTreeNode> stack = new Stack<SuffixTreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            SuffixTreeNode cur = stack.Pop();
            if (cur.Children.Count > 0)
            {
                foreach (var child in cur.Children.Values.Reverse())
                {
                    stack.Push(child);
                }
            }
            if (cur.Start != -1 && cur.End != -1)
            {
                Console.WriteLine(cur.Start + " " + cur.End);
            }
        }
    }
    static void Main()
    {
        mystr = Console.ReadLine();
        suffixarr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        lcparr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        Console.WriteLine(mystr);
        Buildsuffixtree();
        Print(root);
    }
}