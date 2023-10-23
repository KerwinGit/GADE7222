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

    // Start is called before the first frame update
    void Start()
    {
        vehicleArr = GameObject.FindGameObjectsWithTag("Car");
    }

    // Update is called once per frame
    void Update()
    {
        lap.text = "LAP: " + playerVehicle.GetComponent<WaypointCounter>().lapCount + "";
        placeCalc();

        if (playerVehicle.GetComponent<WaypointCounter>().lapCount == 3 && playerPlace == 0)
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
            catch (System.Exception e)
            {
                nextWaypoint = vehicleArr[1].GetComponent<AICar>().target;
            }
        }

        for (int i = 0; i < vehicleArr.Length - 1; i++)
        {
            for (int j = i + 1; j < vehicleArr.Length; j++)
            {
                if (vehicleArr[i].GetComponent<WaypointCounter>().waypointCount < vehicleArr[j].GetComponent<WaypointCounter>().waypointCount)
                {
                    GameObject temp = vehicleArr[i];
                    vehicleArr[i] = vehicleArr[j];
                    vehicleArr[j] = temp;
                }
            }
        }



        for (int i = 0; i < vehicleArr.Length - 1; i++)
        {
            for (int j = i + 1; j < vehicleArr.Length; j++)
            {
                if (vehicleArr[i].GetComponent<WaypointCounter>().waypointCount == vehicleArr[j].GetComponent<WaypointCounter>().waypointCount)
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

        for (int i = 0; i < vehicleArr.Length; i++)
        {
            if (vehicleArr[i].gameObject.name == "Car")
            {
                playerPlace = i;
            }
        }

        place.text = "Rank: " + (playerPlace + 1) + "";
    }
}
