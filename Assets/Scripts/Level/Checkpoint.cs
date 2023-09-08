using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent onCheckpointEnter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onCheckpointEnter.Invoke();
        }
    }
}
