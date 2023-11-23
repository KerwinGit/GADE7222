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

            //MeshFilter meshFilter = spawnedGO.GetComponent<MeshFilter>();

            //if (meshFilter == null)
            //{
            //    meshFilter = spawnedGO.AddComponent<MeshFilter>();
            //}

            //MeshRenderer meshRenderer = spawnedGO.GetComponent<MeshRenderer>();

            //if (meshRenderer == null)
            //{
            //    meshRenderer = spawnedGO.AddComponent<MeshRenderer>();
            //}

            spawnedGO.GetComponent<NavMeshAgent>().speed = aiRacer.speed;
            spawnedGO.GetComponent<Renderer>().material = aiRacer.material;
            //meshFilter.mesh = aiRacer.model;
            //meshRenderer.material = aiRacer.material;
            
            
            
        }
    }
}
