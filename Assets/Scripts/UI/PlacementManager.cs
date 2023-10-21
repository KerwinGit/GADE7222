using TMPro;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lap;
    [SerializeField] private TextMeshProUGUI place;
    private int playerPlace;
    [SerializeField] private GameObject playerVehicle;
    [SerializeField] private GameObject[] vehicleArr;

    // Start is called before the first frame update
    void Start()
    {

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
