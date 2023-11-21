using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            SFXManager.Instance.PlayAudio("Bump");
        }
        catch
        {
            Debug.Log("i hate this stupid f!@#$%^ error leave me alone");
        }
    }
}
