using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public bool isLeft, timetoMove, chosenOne, vertical, mainMenu;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        if(mainMenu == true)
        {
            speed = 25;
        }
        else
        {
            speed = 1250;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeft == true)
        {
            if(timetoMove== true)
            {
                if(vertical == false)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                }
                else
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                }
                
            }
         
        }
        else
        {
            if (timetoMove == true)
            {
                if(vertical == false)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                }
                else
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                }
              
            }
           

        }


        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Smooth") && chosenOne == true)
        {
            if(mainMenu == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
            
        }
        
    }
}
