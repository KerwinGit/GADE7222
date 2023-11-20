using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaypointGraphDataSO", menuName = "Custom/Waypoint Graph Data Scriptable Object")]
public class WaypointGraphDataSO : ScriptableObject
{
    [System.Serializable]
    public class WaypointData
    {
        public string waypointName;
        public List<string> connectedWaypointNames;
    }

    public List<WaypointData> waypointsData;
}
