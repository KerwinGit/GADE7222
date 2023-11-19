using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource engine;
    [SerializeField] private AnimationCurve pitch;
    [SerializeField] private GameObject tire;


    private void Awake()
    {
        engine.Play();
    }
    void Start()
    {
        
         
    }

    // Update is called once per frame
    void Update()
    {
        engine.pitch = pitch.Evaluate(tire.GetComponent<Tire>().getVelocityPercentage());
    }
}
