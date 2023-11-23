using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField]private GameObject spawnPrefab;

    void Awake()
    {
        AIRacerFactory factory = new AIRacerFactory();

        foreach(Transform t in spawnPoints)
        {
            AIRacer aiRacer = factory.Create<AIRacer>();

            GameObject spawnedGO = Instantiate(spawnPrefab, t.position, t.rotation);

            spawnedGO.GetComponent<NavMeshAgent>().speed = aiRacer.speed;
            spawnedGO.GetComponentInChildren<MeshFilter>().mesh = aiRacer.mesh;
            spawnedGO.GetComponentInChildren<Renderer>().material = aiRacer.material;

        }
    }
}
