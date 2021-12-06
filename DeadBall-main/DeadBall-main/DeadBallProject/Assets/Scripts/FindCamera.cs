using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{
    public Canvas canvas;
    private bool foundCamera;
   
    // Update is called once per frame
    void Update()
    {
        if(foundCamera == false)
        {
            canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            foundCamera = true;
        }
    }
}
