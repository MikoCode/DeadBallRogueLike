using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class MovingNeon : MonoBehaviour
{
    public bool isStarting;
    public string whereToMove;
    private Vector2 startPos;
    public Light2D light;
    private bool stopLight;
    private GameManager gM;
    private int speed;
    public ParticleSystem destroyParticle;
    public bool mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (mainMenu)
        {
            speed = 200;
        }
        else
        {
            speed = 5;
        }

        startPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if(isStarting == true)
        {
            if(stopLight == false)
            {
                light.enabled = true;
            }
           
            if(whereToMove == "Left")
            {
                transform.Translate(Vector2.left * Time.deltaTime * speed);
            }
            else if(whereToMove == "Right")
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else if(whereToMove == "Up")
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
            else if(whereToMove == "Down")
            {
                transform.Translate(Vector2.down * Time.deltaTime * speed);
            }
           
        }
        else if(isStarting == false)
        {
            light.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MovingNeon"))
        {
            transform.position = startPos;

            if (isStarting == true)
            {
                isStarting = false;
            }
            else if (isStarting == false)
            {
                isStarting = true;
            }
        }
        else if (collision.gameObject.CompareTag("StopLight"))
        {
            stopLight = true;
            light.enabled = false;
        }
        else if (collision.gameObject.CompareTag("StartLight"))
        {
            stopLight = false;
            light.enabled = true;
        }

        else if (collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }
   


}
