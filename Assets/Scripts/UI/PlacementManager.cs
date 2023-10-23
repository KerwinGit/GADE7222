using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lap;
    [SerializeField] private TextMeshProUGUI place;
    private int playerPlace;
    [SerializeField] private GameObject playerVehicle;
    [SerializeField] private GameObject[] vehicleArr;
    [SerializeField] private Transform nextWaypoint;

    void Start()
    {
        vehicleArr = GameObject.FindGameObjectsWithTag("Car");
    }

    void Update()
    {
        lap.text = "LAP: " + playerVehicle.GetComponent<WaypointCounter>().lapCount + "";

        placeCalc();

        if (playerVehicle.GetComponent<WaypointCounter>().lapCount > 3 && playerPlace == 0)
        {
            Debug.Log("BIG WIN");
        }
    }

    private void placeCalc()
    {

        for (int i = 0; i < vehicleArr.Length; i++)
        {
            try
            {
                nextWaypoint = vehicleArr[0].GetComponent<AICar>().target;
            }
            catch
            {
                nextWaypoint = vehicleArr[1].GetComponent<AICar>().target;
            }
        }

        for (int i = 0; i < vehicleArr.Length - 1; i++)
        {
            for (int j = i + 1; j < vehicleArr.Length; j++)
            {
                if (vehicleArr[i].GetComponent<WaypointCounter>().lapCount < vehicleArr[j].GetComponent<WaypointCounter>().lapCount)
                {
                    GameObject temp = vehicleArr[i];
                    vehicleArr[i] = vehicleArr[j];
                    vehicleArr[j] = temp;
                }
                else if (vehicleArr[i].GetComponent<WaypointCounter>().lapCount == vehicleArr[j].GetComponent<WaypointCounter>().lapCount)
                {
                    if (vehicleArr[i].GetComponent<WaypointCounter>().lastPassedWaypoint < vehicleArr[j].GetComponent<WaypointCounter>().lastPassedWaypoint)
                    {
                        GameObject temp = vehicleArr[i];
                        vehicleArr[i] = vehicleArr[j];
                        vehicleArr[j] = temp;
                    }
                    else if (vehicleArr[i].GetComponent<WaypointCounter>().lastPassedWaypoint == vehicleArr[j].GetComponent<WaypointCounter>().lastPassedWaypoint)
                    {
                        if (Vector3.Distance(vehicleArr[i].transform.position, nextWaypoint.position) > Vector3.Distance(vehicleArr[j].transform.position, nextWaypoint.position))
                        {
                            GameObject temp = vehicleArr[i];
                            vehicleArr[i] = vehicleArr[j];
                            vehicleArr[j] = temp;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < vehicleArr.Length; i++)
        {
            if (vehicleArr[i].gameObject.name == "Car")
            {
                playerPlace = i;
                Debug.Log(playerPlace);
            }
        }

        place.text = "Rank: " + (playerPlace + 1) + "";
    }
}
