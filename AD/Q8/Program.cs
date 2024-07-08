using System;
using System.Collections.Generic;
public class vertex_dist
{
    public int dist_dist;
    public int vertex;
    public vertex_dist(int d, int v)
    {
        this.dist_dist = d;
        this.vertex = (int)v;
    }
}
public class min_heap_graph
{
    public vertex_dist[] heap;
    public int[] indexes2;
    public int size;
    public int maxSize;
    public min_heap_graph(int capacity)
    {
        maxSize = capacity;
        size = capacity;
        indexes2 = new int[capacity];
        for (int i = 0; i < capacity; i++)
        {
            indexes2[i] = i;
        }
        heap = new vertex_dist[maxSize];
        for (int i = 0; i < capacity; i++)
        {
            heap[i] = new vertex_dist(int.MaxValue / 2, i + 1);
        }
    }
    public int Parent(int i)
    {
        return (i - 1) / 2;
    }
    public int LeftChild(int i)
    {
        return 2 * i + 1;
    }
    public int RightChild(int i)
    {
        return 2 * i + 2;
    }
    public void Swap(int i, int j)
    {
        indexes2[heap[i].vertex-1] =  j ;
        indexes2[heap[j].vertex-1] = i;
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
    public void SiftUp(int i)
    {
        while (i > 0 && heap[Parent(i)].dist_dist > heap[i].dist_dist)
        {
            Swap(Parent(i), i);
            i = Parent(i);
        }
    }
    public void SiftDown(int i)
    {
        int minindex = i;
        int leftChild = LeftChild(i);
        if (leftChild < size && heap[leftChild].dist_dist < heap[minindex].dist_dist)
        {
            minindex = leftChild;
        }
        int rightChild = RightChild(i);

        if (rightChild < size && heap[rightChild].dist_dist < heap[minindex].dist_dist)
        {
            minindex = rightChild;
        }
        if (i != minindex)
        {
            Swap(i, minindex);
            SiftDown(minindex);
        }
    }
    public vertex_dist ExtractMin()
    {
        var result = heap[0];
        heap[0] = heap[size - 1];
        indexes2[heap[size-1].vertex-1] = 0  ; 
        size--;
        SiftDown(0);
        return result;
    }
    public void ChangePriority(int i, int p)
    {
        if (i < 0 || i > size)
        {
            return;
        }
        int oldP = heap[i].dist_dist;
        heap[i].dist_dist = p;

        if (p < oldP)
        {
            SiftUp(i);
        }
        else
        {
            SiftDown(i);
        }
    }
}
public class Program
{
    static List<(int, int)>[] mygraph;
    static List<int> dist;
    static List<int> prev;
    static int[] indexes;
    static void Main()
    {
        int[] firstline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        dist = new List<int>();
        prev = new List<int>();
        int verticesnumber = firstline[0];
        int edgesnumber = firstline[1];
        indexes = new int[verticesnumber];
        mygraph = new List<(int, int)>[verticesnumber];
        for (int i = 0; i < verticesnumber; i++)
        {
            mygraph[i] = new List<(int, int)>();
        }
        for (int i = 0; i < edgesnumber; i++)
        {
            int[] info_edge = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int from = info_edge[0];
            int to = info_edge[1];
            int weight = info_edge[2];
            mygraph[from - 1].Add((to, weight));
        }
        int[] lastline = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int start = lastline[0];
        int finish = lastline[1];
        for (int i = 0; i < verticesnumber; i++)
        {
            indexes[i] = i;
        }
        for (int i = 0; i < verticesnumber; i++)
        {
            dist.Add(int.MaxValue / 2);
            prev.Add(-1);
        }
        dist[(int)start - 1] = 0;
        min_heap_graph mystor = new min_heap_graph(verticesnumber);
        mystor.ChangePriority(start - 1, 0);
        while (mystor.size != 0)
        {
            var started = mystor.ExtractMin();
            foreach (var edge in mygraph[started.vertex - 1])
            {
                int to = edge.Item1;
                int weight = edge.Item2;
                if (dist[started.vertex - 1] + weight < dist[to - 1])
                {
                    dist[to - 1] = dist[started.vertex - 1] + weight;
                    prev[to - 1] = started.dist_dist;
                    var xm = mystor.indexes2[to - 1];
                    mystor.ChangePriority(xm, dist[to - 1]);
                }
            }
        }
        if (dist[finish - 1] != int.MaxValue / 2)
        {
            Console.WriteLine(dist[finish - 1]);
        }
        else
        {
            Console.WriteLine(-1);
        }
    }
}
