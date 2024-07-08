using System;
using System.Collections.Generic;

class Graph
{
    public int Vertices { get; }
    private List<(int, int)>[] adjacencyList;

    public Graph(int vertices)
    {
        Vertices = vertices;
        adjacencyList = new List<(int, int)>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            adjacencyList[i] = new List<(int, int)>();
        }
    }

    public void AddEdge(int u, int v, int weight)
    {
        adjacencyList[u].Add((v, weight));
        adjacencyList[v].Add((u, weight));
    }

    public List<(int, int)> Prim()
    {
        int[] cost = new int[Vertices];
        int[] parent = new int[Vertices];
        bool[] inPriorityQueue = new bool[Vertices];
        for (int i = 0; i < Vertices; i++)
        {
            cost[i] = int.MaxValue;
            parent[i] = -1;
            inPriorityQueue[i] = true;
        }

        int u0 = 0; // Initial vertex
        cost[u0] = 0;

        PriorityQueue<int> priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(u0, cost[u0]);

        while (priorityQueue.Count > 0)
        {
            int u = priorityQueue.Dequeue();

            inPriorityQueue[u] = false;

            foreach (var edge in adjacencyList[u])
            {
                int v = edge.Item1;
                int weight = edge.Item2;

                if (inPriorityQueue[v] && cost[v] > weight)
                {
                    cost[v] = weight;
                    parent[v] = u;
                    priorityQueue.Enqueue(v, cost[v]);
                }
            }
        }

        List<(int, int)> minimumSpanningTree = new List<(int, int)>();
        for (int i = 1; i < Vertices; i++)
        {
            minimumSpanningTree.Add((parent[i], i));
        }

        return minimumSpanningTree;
    }
}

class PriorityQueue<T>
{
    private SortedDictionary<T, Queue<T>> queue = new SortedDictionary<T, Queue<T>>();

    public int Count { get; private set; }

    public void Enqueue(T item, T priority)
    {
        if (!queue.TryGetValue(priority, out Queue<T> items))
        {
            items = new Queue<T>();
            queue.Add(priority, items);
        }

        items.Enqueue(item);
        Count++;
    }

    public T Dequeue()
    {
        var first = queue.First();
        var item = first.Value.Dequeue();
        if (first.Value.Count == 0)
        {
            queue.Remove(first.Key);
        }
        Count--;
        return item;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int vertices = 5;
        Graph graph = new Graph(vertices);

        graph.AddEdge(0, 1, 2);
        graph.AddEdge(0, 3, 6);
        graph.AddEdge(1, 2, 3);
        graph.AddEdge(1, 3, 8);
        graph.AddEdge(1, 4, 5);
        graph.AddEdge(2, 4, 7);
        graph.AddEdge(3, 4, 9);

        List<(int, int)> minimumSpanningTree = graph.Prim();
        Console.WriteLine("Minimum Spanning Tree Edges:");
        foreach (var edge in minimumSpanningTree)
        {
            Console.WriteLine($"{edge.Item1} - {edge.Item2}");
        }
    }
}