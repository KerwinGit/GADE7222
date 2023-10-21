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

    public LinkedList<GameObject> waypointList = new Stack<GameObject>();

    public GameObject GetNextWaypoint(int index)
    {
        if (index >= 0)
        {
            return waypointList.ElementAt(index + 1);
        }

        return null; // Index out of bounds or no next waypoint.
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
    }
    #endregion
}
