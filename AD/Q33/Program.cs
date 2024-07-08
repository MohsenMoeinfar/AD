using System;
using System.Collections.Generic;
using System.Threading ; 

public class Program
{
    static List<int>[] mygraph;
    static List<int>[] revmygraph;
    static List<int> postorders;
    static int clock;
    static List<List<int>> sccs;
    static List<Tuple<int, int>> mylines;
    static bool[] visited;
    static int vv;
    static int cc;
    static void Main()
    {
        Thread  mythread = new Thread(startthread , int.MaxValue) ; 
        mythread.Start() ; 
    }
    static void startthread()
    {
         mylines = new List<Tuple<int, int>>();
        var firstline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int v = firstline[0];
        int c = firstline[1];
        vv = v;
        cc = c;
        mygraph = new List<int>[2 * vv];
        revmygraph = new List<int>[2 * vv];
        for (int i = 0; i < 2 * vv; i++)
        {
            mygraph[i] = new List<int>();
            revmygraph[i] = new List<int>();
        }
        for (int i = 0; i < c; i++)
        {
            var line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int a = line[0];
            int b = line[1];
            mylines.Add(new Tuple<int, int>(a, b));
        }
        findscc();
        var goal = satprob();
        if (!goal)
        {
            Console.WriteLine("UNSATISFIABLE");
        }

    }
    static void previsit(int v)
    {
        clock++;
    }

    static void postvisit(int v)
    {
        postorders.Add(v);
        clock++;
    }
    static void Explore1(int v)
    {
        visited[v] = true;
        previsit(v);
        foreach (var u in mygraph[v])
        {
            if (!visited[u])
            {
                Explore1(u);
            }
        }
        postvisit(v);
    }

    static void Explore2(int v, List<int> scc)
    {
        visited[v] = true;
        scc.Add(v);
        previsit(v);
        foreach (var u in revmygraph[v])
        {
            if (!visited[u])
            {
                Explore2(u, scc);
            }
        }
        postvisit(v);
    }
    static void findscc()
    {
        foreach (var cl in mylines)
        {
            int from1 = -cl.Item1;
            int to1 = cl.Item2;
            int from2 = -cl.Item2;
            int to2 = cl.Item1;
            if (from1 > 0)
            {
                from1 = from1 - 1;
            }
            else
            {
                from1 = vv + (-from1 - 1);
            }
            if (from2 > 0)
            {
                from2 = from2 - 1;
            }
            else
            {
                from2 = vv + (-from2 - 1);
            }
            if (to1 > 0)
            {
                to1 = to1 - 1;
            }
            else
            {
                to1 = vv + (-to1 - 1);
            }
            if (to2 > 0)
            {
                to2 = to2 - 1;
            }
            else
            {
                to2 = vv + (-to2 - 1);
            }
            mygraph[from1].Add(to1);
            revmygraph[to1].Add(from1);
            mygraph[from2].Add(to2);
            revmygraph[to2].Add(from2);
        }
    }
    static bool satprob()
    {
        visited = new bool[2 * vv];
        sccs = new List<List<int>>();
        postorders = new List<int>();
        int[] cmp = new int[2 * vv];
        clock = 0;
        int[] results = new int[vv];
        bool[] checker = new bool[2 * vv];
        for (int i = 0; i < 2 * vv; i++)
        {
            if (!visited[i])
            {
                Explore1(i);
            }
        }
        visited = new bool[2 * vv];
        for (int i = postorders.Count - 1; i >= 0; i--)
        {
            int v = postorders[i];
            if (!visited[v])
            {
                var scc = new List<int>();
                Explore2(v, scc);
                sccs.Add(scc);
            }
        }
        for (int i = 0; i < sccs.Count; i++)
        {
            foreach (var node in sccs[i])
            {
                cmp[node] = i;
            }
        }
        for (int i = 0; i < vv; i++)
        {
            if (cmp[i] == cmp[i + vv])
            {
                return false;
            }
        }
        for (int i = sccs.Count - 1; i >= 0; i--)
        {
            foreach (var node in sccs[i])
            {
                int var;
                bool ispos;
                if (node < vv)
                {
                    var = node;
                    ispos = true;
                }
                else
                {
                    var = node - vv;
                    ispos = false;
                }
                int oppnode;
                if (node < vv)
                {
                    oppnode = node + vv;
                }
                else
                {
                    oppnode = node - vv;
                }
                if (!checker[node] && !checker[oppnode])
                {
                    if (ispos)
                    {
                        results[var] = 1;
                    }
                    else
                    {
                        results[var] = -1;
                    }
                    checker[node] = true;
                    checker[oppnode] = true;
                }
            }
        }
        Console.WriteLine("SATISFIABLE");
        for (int i = 0; i < vv; i++)
        {
            if (results[i] > 0)
            {
                Console.Write((i + 1) + " ");
            }
            else
            {
                Console.Write(-(i + 1) + " ");
            }
        }
        Console.WriteLine();
        return true;
    }
}