using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    #region Singleton
    private static WaypointManager instance;

    public static WaypointManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WaypointManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("WaypointManager");
                    instance = managerObject.AddComponent<WaypointManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    public WaypointGraphDataSO waypointGraphSO;
    private DirectedGraph<Transform> waypointGraph;

    public Transform firstWaypoint;

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        List<Transform> neighbors = waypointGraph.GetNeighbors(currentWaypoint);

        if (neighbors.Count > 1)
        {
            // Randomly choose one of the neighbors as the next waypoint
            int randomIndex = Random.Range(0, neighbors.Count);

            return neighbors[randomIndex];
        }
        else
        {
            return neighbors[0];
        }
    }

    private void InitializeWaypointGraph() //uses waypoint names to find waypoint transforms within the scene to populate the graph
    {
        waypointGraph = new DirectedGraph<Transform>();

        foreach (var waypointData in waypointGraphSO.waypointsData)
        {
            waypointGraph.AddVertex(GameObject.Find(waypointData.waypointName).transform);

            int count = 0;
            foreach (var connection in waypointData.connectedWaypointNames)
            {
                waypointGraph.AddEdge(GameObject.Find(waypointData.waypointName).transform, GameObject.Find(waypointData.connectedWaypointNames[count]).transform);
                count++;
            }
        }
    }

    //old logic which uses linked list
    //public List<GameObject> waypointList;
    //public LinkedList<GameObject> waypointLinkedList = new LinkedList<GameObject>();

    //public GameObject GetNextWaypoint(int index)
    //{
    //    return waypointLinkedList.ElementAt(index + 1);
    //}

    //private void InitializeWaypointList()
    //{
    //    foreach (GameObject waypoint in waypointList)
    //    {
    //        waypointLinkedList.AddToTail(waypoint);
    //    }
    //}

    #region unitymethods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeWaypointGraph();
        //InitializeWaypointList();
    }
    #endregion
}
