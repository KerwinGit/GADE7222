using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Graph<T> //generic implementation of graph
{
    protected Dictionary<T, List<T>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<T, List<T>>(); //vertex: T; list of adjacent members: List<T>
    }

    public void AddVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex)) //checks vertex doesn't already exist
        {
            adjacencyList[vertex] = new List<T>(); //initialises empty list of neighbours to vertex in dictionary
        }
    }

    public List<T> GetNeighbors(T vertex) //returns a list of connected vertices
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            return adjacencyList[vertex];
        }

        return new List<T>();
    }

    public abstract void AddEdge(T source, T destination);
}

public class UndirectedGraph<T> : Graph<T> 
{
    public override void AddEdge(T source, T destination)
    {
        AddVertex(source);
        AddVertex(destination);

        adjacencyList[source].Add(destination); //adds the destination vertex to the adjacency list of the source vertex
        adjacencyList[destination].Add(source); //adds the source vertex to the adjacency list of the destination vertex
    }
}

public class DirectedGraph<T> : Graph<T> 
{
    public override void AddEdge(T source, T destination)
    {
        AddVertex(source);
        AddVertex(destination);

        adjacencyList[source].Add(destination); //only the destination vertex is added to the source's list as the flow goes in one direction only
    }
}
