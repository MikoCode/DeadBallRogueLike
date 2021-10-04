using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene7 : MonoBehaviour
{
    public GameObject light;
    public GameObject enemies;
    public GameObject blockade;
    public GameObject mainHallLight;

    public Goal goal;
    public bool isFight;
    public bool activatedEvent;
    private bool finishedEvent;
    public GameManager gM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
            if(gM.enemiesKilled >= 5 && finishedEvent == false && isFight == true)
            {
                
                blockade.gameObject.SetActive(false);
                finishedEvent = true;
                gM.questsDone += 1;
            }

        if(goal != null && goal.didScore == true && finishedEvent == false)
        {
            blockade.gameObject.SetActive(false);
            finishedEvent = true;
            gM.questsDone += 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && finishedEvent == false)
        {
            

            if (isFight)
            {
            
                light.gameObject.SetActive(true);
                enemies.gameObject.SetActive(true);
                blockade.gameObject.SetActive(true);
                

            }
            else
            {
                blockade.gameObject.SetActive(true);
                light.gameObject.SetActive(true);
               

            }
           
        }

        



    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            mainHallLight.gameObject.SetActive(false);

            if (finishedEvent == true)
            {
                light.gameObject.SetActive(true);
            }
            
        }
       
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(finishedEvent == true)
            {
                light.gameObject.SetActive(false);
            }
            
            mainHallLight.gameObject.SetActive(true);
        }
    }
}
