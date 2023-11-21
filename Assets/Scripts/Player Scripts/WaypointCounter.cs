using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCounter : MonoBehaviour
{
    public int lastPassedWaypoint;
    public int lapCount = 1;
    private bool canLap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            string taggedWaypointName = other.name;
            for(int i = 0; i < WaypointManager.Instance.waypointGraphSO.waypointsData.Count; i++)
            {
                if (taggedWaypointName.Equals(WaypointManager.Instance.waypointGraphSO.waypointsData[i].waypointName))
                {
                    lastPassedWaypoint = i; break;
                }
            }
        }
        if (other.gameObject.name.Equals("LapActivator"))
        {
            canLap = true;
        }
        if (other.gameObject.name.Equals("LapCounter") && canLap)
        {
            lapCount++;
            canLap = false;
        }
    }
}
