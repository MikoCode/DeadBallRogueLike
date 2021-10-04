using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scene4 : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public PlayerController playerCon;
    public GameObject[] enemies;
    public bool didSpawn;
    public GameObject[] lights;
    public GameObject[] bigLights;
    public bool dead;
    public YellowCard[] yellowCards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ActiveEnemies();
    
        if(enemies[0] == null && enemies[1] == null && enemies[2] == null && enemies[3] == null && dead == false && GameObject.FindGameObjectsWithTag("RedCard").Length == 0)
        {
            dead = true;
            enemies[4].gameObject.SetActive(false);
        }
      
    }


    void ActiveEnemies()
    {
        
        if (playerCon.hasBall == true && didSpawn == false)
        {
            Destroy(lights[0]);
            Destroy(lights[1]);

            for (int i = 0; i <= 4; i++)
            {
                if(enemies != null)
                {
                    enemies[i].gameObject.SetActive(true);
                }
               
            }

            for (int l = 0; l <= 3; l++)
            {
                bigLights[l].gameObject.SetActive(true);
               
            }
            didSpawn = true;
        }

      
    }

   

   
}


