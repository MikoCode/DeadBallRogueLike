using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController playerCon;
    private CircleCollider2D box;
    public ParticleSystem particle;
    private GameManager gM;
    public float ballPower;
    public bool first;
    private float time;
    private bool didSavePos;
    private float posY;
    private float savedPosY;
    public bool startDynamicly;
    private float posX;
    private float savedPosX;
    private bool returning;
    public int hz, vt;
    


    public bool destroyable;
    // Start is called before the first frame update
    void Start()
    {
        returning = false;
        
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gM.isBallReturning = false;
        Invoke("IsDestroyble", 0.4f);
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ballPower = 600; 
        time = 0.4f;
        box = gameObject.GetComponent<CircleCollider2D>();
      

    }

    // Update is called once per frame
    void Update()
    {
      
       
        posX = transform.position.x;
        posY = transform.position.y;
        if(didSavePos == false)
        {
            savedPosY = posY;
            savedPosX = posX;

            didSavePos = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude < 4)
        {
            returning = true;
            gM.isBallReturning = true;
        }


        if(playerCon.hasBall == false && returning == true )
        {
           
            transform.position = Vector2.MoveTowards(transform.position, playerCon.transform.position, 20f * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.125f, 0.27f);
                
            
           
        }


        if(startDynamicly == true)
        {
            DynamicMove();
        }

        SlowingDown();
       
        if(returning == true)
        {
            
            box.isTrigger = true;
        }
    }

    private void FixedUpdate()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision) // this happens after ball hits pitch bound
    {
        startDynamicly = false;
        ballPower -= (rb.velocity.magnitude * 5);
        if (collision.gameObject.CompareTag("upBorder"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            if (savedPosX <= posX)
            {
                rb.AddForce(new Vector2(0.2f, -1) * ballPower);
            }
            else
            {
                rb.AddForce(new Vector2(-0.2f, -1) * ballPower);
            }
            didSavePos = false;
           
           
        }
        else if (collision.gameObject.CompareTag("downBorder"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            if (savedPosX <=  posX)
            {
                rb.AddForce(new Vector2(0.2f, 1) * ballPower);
            }
            else
            {
                rb.AddForce(new Vector2(-0.2f, 1) * ballPower);
            }
            didSavePos = false;
            if (ballPower > 70)
            {
                ballPower -= 150;
            }
        }
        else if (collision.gameObject.CompareTag("leftBorder"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            if (savedPosY <= posY)
            {
                rb.AddForce(new Vector2(1, 0.2f) * ballPower);
            }
           else
            {
                rb.AddForce(new Vector2(1, -0.2f) * ballPower);
            }
            didSavePos = false;
            if (ballPower > 70)
            {
                ballPower -= 150;
            }
        }
        else if (collision.gameObject.CompareTag("rightBorder"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            if (savedPosY <= posY)
            {
                rb.AddForce(new Vector2(-1, 0.2f) * ballPower);
            }
            else
            {
                rb.AddForce(new Vector2(-1, -0.2f) * ballPower);
            }
            didSavePos = false;
            if (ballPower > 70)
            {
                ballPower -= 150;
            }
        }
        if (collision.gameObject.CompareTag("Player") && destroyable == true)
        {
            playerCon.hasBall = true;
            
            Destroy(gameObject);
            if (ballPower > 70)
            {
                ballPower -= 150;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && destroyable == true)
        {
            playerCon.hasBall = true;

            Destroy(gameObject);
            if (ballPower > 70)
            {
                ballPower -= 150;
            }
        }
    }


    void IsDestroyble()
    {
        destroyable = true;
    }



    void SlowingDown()
    {

        if(ballPower > 70)
        {
            time -= 1 * Time.deltaTime;

            if (time <= 0)
            {
                rb.velocity *= 0.96f;
                time = 0.2f;
            }
        }
        else
        {
            time -=  Time.deltaTime;

            if (time <= 0)
            {
                rb.velocity *= 0.8f;
                time = 0.1f;
            }
        }
    

        
    }

    void DynamicMove()
    {
        transform.Translate(new Vector2(hz,vt) * 10f * Time.deltaTime);
    }
}
