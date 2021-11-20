using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public bool isLeft, timetoMove, chosenOne, vertical, mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
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
                    transform.Translate(Vector2.left * Time.deltaTime * 1250);
                }
                else
                {
                    transform.Translate(Vector2.up * Time.deltaTime * 1250);
                }
                
            }
         
        }
        else
        {
            if (timetoMove == true)
            {
                if(vertical == false)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * 1250);
                }
                else
                {
                    transform.Translate(Vector2.down * Time.deltaTime * 1250);
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
