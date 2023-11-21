using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lap;
    [SerializeField] private TextMeshProUGUI place;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private TextMeshProUGUI finishText;
    private int playerPlace;
    private int winCount = 0;
    [SerializeField] private GameObject playerVehicle;
    [SerializeField] private GameObject[] vehicleArr;
    [SerializeField] private Transform nextWaypoint;

    void Start()
    {
        vehicleArr = GameObject.FindGameObjectsWithTag("Car");
    }

    void Update()
    {
        lap.text = "LAP: " + playerVehicle.GetComponent<WaypointCounter>().lapCount + "/3";
        

        placeCalc();

        if (playerVehicle.GetComponent<WaypointCounter>().lapCount > 3)
        {
            Finish();
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
                    if (vehicleArr[i].GetComponent<WaypointCounter>().lastPassedWaypoint < vehicleArr[j].GetComponent<WaypointCounter>().lastPassedWaypoint && vehicleArr[j].GetComponent<WaypointCounter>().lastPassedWaypoint != WaypointManager.Instance.waypointGraphSO.waypointsData.Count-1)
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
            }
        }        

        place.text = "Rank: " + (playerPlace + 1) + "";
    }

    private void Finish()
    {
        if(winCount<1)
        {
            finishText.text = "Your Position: " + (playerPlace + 1);
            finishPanel.SetActive(true);
            winCount++;
        }
    }
}
