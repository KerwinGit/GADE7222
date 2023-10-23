using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCounter : MonoBehaviour
{
    public int lastPassedWaypoint;
    public int lapCount = 0;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            lastPassedWaypoint = WaypointManager.Instance.waypointList.IndexOf(other.gameObject);

            if (other.gameObject.name == "Waypoint")
            {
                lapCount++;
            }
        }
    }
}
