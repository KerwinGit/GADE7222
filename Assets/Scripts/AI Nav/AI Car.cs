using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;
    private int currentTargetWaypoint = 0;

    void Start()
    {
        WaypointManager instance = WaypointManager.Instance;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("waypoint"))
        {
            GetNextWaypoint(currentTargetWaypoint);
        }

        currentTargetWaypoint++;

        if(currentTargetWaypoint > WaypointManager.Instance.waypointList.size)
        {
            currentTargetWaypoint = 0;
        }
    }

    private void GetNextWaypoint(int currentWaypointID)
    {
        WaypointManager.Instance.waypointList.ElementAt(currentWaypointID);
    }
}
