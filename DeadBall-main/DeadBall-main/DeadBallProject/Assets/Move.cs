using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public bool isLeft, timetoMove, chosenOne;
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
                transform.Translate(Vector2.left * Time.deltaTime * 1100);
            }
         
        }
        else
        {
            if (timetoMove == true)
            {
                transform.Translate(Vector2.right * Time.deltaTime * 1100);
            }
            
        }


        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Smooth") && chosenOne == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}