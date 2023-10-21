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

    public List<GameObject> waypointList;
    public LinkedList<GameObject> waypointLinkedList = new LinkedList<GameObject>();

    public GameObject GetNextWaypoint(int index)
    {
        return waypointLinkedList.ElementAt(index + 1);
    }

    private void InitializeWaypointList()
    {
        foreach (GameObject waypoint in waypointList)
        {
            waypointLinkedList.AddToTail(waypoint);
        }
    }

    #region unitymethods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeWaypointList();
    }
    #endregion
}
