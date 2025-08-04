using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    private Dictionary<int, List<int>> adj = new();

    public void AddEdge(int u, int v)
    {
        if (!adj.ContainsKey(u)) adj[u] = new List<int>();
        if (!adj[u].Contains(v))
            adj[u].Add(v);
    }

    // Get all nodes
    public List<int> GetNodes() => adj.Keys.ToList();
    public List<int> GetAdj(int u) => adj[u].ToList<int>();

    // Remove edge from graph
    public void RemoveEdges(List<(int, int)> edges)
    {
        foreach (var (u, v) in edges)
        {
            adj[u].Remove(v);
            adj[v].Remove(u);
        }
    }

}
