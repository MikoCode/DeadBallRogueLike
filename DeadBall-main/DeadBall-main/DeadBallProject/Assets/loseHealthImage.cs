using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseHealthImage : MonoBehaviour
{
    public Image image;
    public float transparency;
    // Start is called before the first frame update
    void Start()
    {
        transparency = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(1, 0, 0, transparency);
        transparency -= 0.1f * Time.deltaTime;
    }
}
