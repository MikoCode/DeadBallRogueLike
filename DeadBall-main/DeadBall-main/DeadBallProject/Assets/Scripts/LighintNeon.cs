using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LighintNeon : MonoBehaviour
{
    public Light2D light;
    public float min, max;
    public bool goalNeon;
    // Start is called before the first frame update
    void Start()
    {
        ChangeColor();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeColor()
    {
        if(goalNeon == false)
        {
            light.color = new Color(Random.value, Random.value, Random.value);
        }
       
       
    }
}
