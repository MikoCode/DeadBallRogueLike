using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public GameManager gM;
    public int healthReq;
    public Image image;
    public bool checkHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(checkHealth == false)
        {
            if (gM.health >= healthReq)
            {
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
            checkHealth = true;
        }
    }
}   
