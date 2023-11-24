using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent onCheckpointEnter;

    private void OnTriggerEnter(Collider other)//fires off a unity event when checkpoint is entered
    {
        if (other.CompareTag("Player"))
        {
            onCheckpointEnter.Invoke();
            SFXManager.Instance.PlayAudio("Fruit collect 1");
        }
    }
}
