using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{

    [SerializeField]private Transform target;
    private NavMeshAgent agent;
    private int currentTargetWaypoint = 0;
    

    void Start()
    {
        WaypointManager instance = WaypointManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        target = WaypointManager.Instance.GetNextWaypoint(currentTargetWaypoint).transform;
    }

    void Update()
    {
        agent.destination = target.position;

        Quaternion targetRotation = Quaternion.LookRotation(agent.steeringTarget - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, agent.angularSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            target = WaypointManager.Instance.GetNextWaypoint(currentTargetWaypoint).transform;

            Debug.Log(currentTargetWaypoint);
            
        }

        currentTargetWaypoint++;

        if (currentTargetWaypoint >= WaypointManager.Instance.waypointLinkedList.size)
        {
            currentTargetWaypoint = 0;
        }
    }
}
