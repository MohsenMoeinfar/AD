using System;
using System.Collections.Generic;

public class ShortestPathDijkstra
{
    static List<(int, int)>[] mygraph;
    static List<int> dist;
    static List<int> prev;
    static int[] indexes;

    public static void Main()
    {
        int[] firstline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int verticesnumber = firstline[0];
        int edgesnumber = firstline[1];

        dist = new List<int>(new int[verticesnumber]);
        prev = new List<int>(new int[verticesnumber]);
        indexes = new int[verticesnumber];
        mygraph = new List<(int, int)>[verticesnumber];
        for (int i = 0; i < verticesnumber; i++)
        {
            mygraph[i] = new List<(int, int)>();
            dist[i] = int.MaxValue / 2;
            prev[i] = -1;
            indexes[i] = i;
        }

        for (int i = 0; i < edgesnumber; i++)
        {
            int[] info_edge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int from = info_edge[0] - 1;
            int to = info_edge[1] - 1;
            int weight = info_edge[2];
            mygraph[from].Add((to, weight));
        }

        int[] lastline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int start = lastline[0] - 1;
        int finish = lastline[1] - 1;

        dist[start] = 0;
        PriorityQueue<(int, int)> pq = new PriorityQueue<(int, int)>();
        pq.Enqueue((0, start));

        while (pq.Count > 0)
        {
            var (dist_u, u) = pq.Dequeue();
            if (dist_u > dist[u])
                continue;

            foreach (var (v, weight) in mygraph[u])
            {
                int alt = dist[u] + weight;
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                    pq.Enqueue((alt, v));
                }
            }
        }

        if (dist[finish] != int.MaxValue / 2)
        {
            Console.WriteLine(dist[finish]);
        }
        else
        {
            Console.WriteLine(-1);
        }
    }
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> data;

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int ci = data.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (data[ci].CompareTo(data[pi]) >= 0)
                break;
            (data[ci], data[pi]) = (data[pi], data[ci]);
            ci = pi;
        }
    }

    public T Dequeue()
    {
        int li = data.Count - 1;
        T frontItem = data[0];
        data[0] = data[li];
        data.RemoveAt(li);

        --li;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li)
                break;
            int rc = ci + 1;
            if (rc <= li && data[rc].CompareTo(data[ci]) < 0)
                ci = rc;
            if (data[pi].CompareTo(data[ci]) <= 0)
                break;
            (data[pi], data[ci]) = (data[ci], data[pi]);
            pi = ci;
        }
        return frontItem;
    }

    public int Count
    {
        get { return data.Count; }
    }
}
