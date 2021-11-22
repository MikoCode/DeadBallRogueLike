using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightColors : MonoBehaviour
{
    public Light2D light1, light2;
    public Color[] colors;
    public int c1, c2;
    // Start is called before the first frame update
    void Start()
    {
        c1 = Random.Range(0, 5);
        c2 = Random.Range(0, 5);
        if(c2 == c1)
        {
            c2 += 1;
        }

        light1.color = new Color(colors[c1].r, colors[c1].g, colors[c1].b);
        light2.color = new Color(colors[c2].r, colors[c2].g, colors[c2].b);
        light1.intensity = 1.5f;
        light2.intensity = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
       

    }
}
