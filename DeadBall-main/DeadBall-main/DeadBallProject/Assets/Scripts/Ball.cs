using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Ball : MonoBehaviour
{
    public  Rigidbody2D rb;
    public  PlayerController playerCon;
    private CircleCollider2D box;
    public  ParticleSystem wallTouch;
    public ParticleSystem enemyTouch;
    private GameManager gM;
    public  float ballPower;
    public  int hz, vt;
    public  bool first;
    public  bool destroyable;
    public  bool startDynamicly;
    public bool returning, canReturn;
    private bool didSavePos;
    private float posY;
    private float savedPosY;
    private float time;
    private float posX;
    private float savedPosX;
    public AudioClip hitSound;
    public AudioSource source;
    public Light2D light;
    private SpriteRenderer sprite;





    // Start is called before the first frame update
    void Start()
    {
        returning = false;
        canReturn = false;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        box = gameObject.GetComponent<CircleCollider2D>();
        gM.isBallReturning = false;
        ballPower = 600; 
        time = 0.4f;
        Invoke("IsDestroyble", 0.05f);


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

        BallComeback();

        if(startDynamicly == true)
        {
            DynamicMove();
        }

        SlowingDown();
       
       
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
            source.PlayOneShot(hitSound, 0.45f);
            canReturn = true;
            Instantiate(wallTouch, transform.position, Quaternion.identity);
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
            source.PlayOneShot(hitSound, 0.45f);
            canReturn = true;
            Instantiate(wallTouch, transform.position, Quaternion.identity);
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
            source.PlayOneShot(hitSound, 0.45f);
            canReturn = true;
            Instantiate(wallTouch, transform.position, Quaternion.identity);
            if (savedPosY <= posY)
            {
                rb.AddForce(new Vector2(1, 0.45f) * ballPower);
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
            source.PlayOneShot(hitSound, 0.45f);
            canReturn = true;
            Instantiate(wallTouch, transform.position, Quaternion.identity);
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
        else if (collision.gameObject.CompareTag("RedCard"))
        {
            source.PlayOneShot(hitSound, 0.8f);
            ParticleSystem touch=  Instantiate(enemyTouch, collision.gameObject.transform.position, Quaternion.identity);
            canReturn = true;
            touch.startColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
        }

        else if (collision.gameObject.CompareTag("Goalkeeper"))
        {
            source.PlayOneShot(hitSound, 0.6f);
            ParticleSystem touch = Instantiate(enemyTouch, collision.gameObject.transform.position, Quaternion.identity);
            touch.startColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            canReturn = true;
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



    

    private void OnTriggerStay2D(Collider2D collision)
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


    void BallComeback()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canReturn == true || Input.GetKeyDown(KeyCode.Mouse1) && rb.velocity.magnitude <= 4)

        {
            returning = true;
            gM.isBallReturning = true;
        }


        if (playerCon.hasBall == false && returning == true)
        {

            if(playerCon != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerCon.transform.position, 25f * Time.deltaTime);
            }
            light.color = new Color(1, 0, 0.125f, 0.27f);
            sprite.color = new Color(1, 0, 0.125f, 0.27f);



        }


        if (returning == true) // activating trigger while ball is coming back, so it could go through walls and enemies
        {
            box.isTrigger = true;
        }
    }
   
}
