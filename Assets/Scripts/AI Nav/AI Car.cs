using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = WaypointManager.Instance.firstWaypoint;
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
            target = WaypointManager.Instance.GetNextWaypoint(target).transform;
        }       
    }
}
