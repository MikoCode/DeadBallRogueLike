using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LighintNeon : MonoBehaviour
{
    public Light2D light;
    public float min, max;
    // Start is called before the first frame update
    void Start()
    {
        light.color = new Color(Random.value, Random.value, Random.value);
        InvokeRepeating("ChangeColor", Random.Range(0.4f, 3.1f), Random.Range(3.8f, 5.6f));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeColor()
    {
        light.color = new Color(Random.value, Random.value, Random.value);
    }
}
