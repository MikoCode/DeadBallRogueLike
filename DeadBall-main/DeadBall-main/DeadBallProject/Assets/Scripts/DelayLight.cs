using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DelayLight : MonoBehaviour
{
    public Light2D light;
    public float lightIntensity;
    public int t;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnOn", t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void TurnOn()
    {
        light.intensity = lightIntensity;
    }
}
