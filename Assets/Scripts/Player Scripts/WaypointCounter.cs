using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCounter : MonoBehaviour
{
    public int waypointCount = 0;
    public int lapCount = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            waypointCount++;
            if (other.gameObject.name == "Waypoint (34)")
            {
                lapCount++;
            }
        }

    }
}
