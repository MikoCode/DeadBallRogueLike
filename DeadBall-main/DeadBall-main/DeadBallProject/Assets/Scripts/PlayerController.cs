using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ball;
    public GameObject ballSprite, destroyer;
    public Transform ballPoint;
    public Camera cam;
    public ParticleSystem[] loseHealth;
    public ParticleSystem startingParticle, ballShootParticle;
    public bool hasBall = true, refreshGoalKeeper;
    public SpriteRenderer sprite;
    public bool up, down, right, left;
    private bool isMoving;
    public float speed;
    public GameManager gM;
    public Spawner spawner;
    private Vector2 movement;
    public AudioClip shootSound, damageSound;
    public AudioSource source;
   
    private Vector2 mousePos;
    private Ball ballScript;
    public float ballSpeed;
    [SerializeField] float time = 0.2f;
    private bool camera;
   

    // Start is called before the first frame update
    void Start()
    {
       
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine("PlayerStart");
        
    }

    // Update is called once per frame
    void Update()
    {

       
        if(camera == false)
        {
           
            cam = GameObject.FindGameObjectWithTag("Pitch").GetComponentInChildren<Camera>();
            camera = true;
        }
        if(isMoving == true)
        {
            ballSpeed = speed * 2f;
        }
        else
        {
            ballSpeed = 12f;
        }
       
        movement.y = Input.GetAxisRaw("Horizontal");
        movement.x = -Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
       
    }

    private void FixedUpdate()
    {
        if(gM.gameOn == true)
        {
            Moving();
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
      
      


    }


    void Moving()
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D))))) // Simple way to make acceleration for the player, if he is moving forward.
        {
            isMoving = true;
            time -= 1 * Time.deltaTime;
            if (speed <= 10 && time <= 0)
            {
                speed += 0.9f;
                time = 0.2f;

            }
        }
        else
        {
            isMoving = false;
            speed = 5;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); // actual movement with rigidbody

    }



    void Shoot()
    {

        if (hasBall == true ) // naprawiæ, dzia³¹ tylko gdy jest bramkarz

        {
            
            ballSprite.gameObject.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.PlayOneShot(shootSound, 0.4f);
                ballShootParticle.Play();
                GameObject projectile =  Instantiate(ball, ballPoint.position, ballPoint.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(ballPoint.up * ballSpeed, ForceMode2D.Impulse);
                hasBall = false;
                refreshGoalKeeper = true;

               
            }
        }

        else if(hasBall == false )
        {
            ballSprite.gameObject.SetActive(false);
            
          
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedCard"))
        {
            LoseHealth();

            PlayerPrefs.SetInt("GameHealth", gM.health);
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            LoseHealth();

            PlayerPrefs.SetInt("GameHealth", gM.health);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            LoseHealth();
         
            PlayerPrefs.SetInt("GameHealth", gM.health);
            gM.enemiesAlive -= 1;
            gM.enemiesKilled += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {


            LoseHealth();
            
                PlayerPrefs.SetInt("GameHealth", gM.health);
                gM.enemiesKilled += 1;
                Destroy(collision.gameObject);
            
        }
    }





    void LoseHealth()
    {
        source.PlayOneShot(damageSound, 0.3f);
        if(gM.health == 3)
        {
           gM.hearth[2].enabled = false;
            LoseHealthImage();
            Instantiate(loseHealth[0], transform.position, Quaternion.identity);

        }
        else if(gM.health == 2)
        {
            gM.hearth[1].enabled = false;
            LoseHealthImage();
            Instantiate(loseHealth[0], transform.position, Quaternion.identity);

        }

        else if(gM.health == 1)
        {
            gM.hearth[0].enabled = false;

            
            gM.GameOver();
            Instantiate(loseHealth[1], transform.position, Quaternion.identity);
            Instantiate(destroyer, transform.position, Quaternion.identity);
            Destroy(gameObject);
            

        }

        gM.health -= 1;
        
    }
  void LoseHealthImage()
    {
        gM.hearth[3].gameObject.SetActive(true);
        gM.hearth[3].GetComponent<loseHealthImage>().transparency = 0.2f;
    }


    IEnumerator PlayerStart()
    {
       
        yield return new WaitForSeconds(1f);
        
        sprite.enabled = true;
    }

}
