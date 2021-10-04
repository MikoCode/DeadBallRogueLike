using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ball;
    public GameObject ballSprite;
    public Transform ballPoint;
    public Camera cam;
    public bool hasBall = true, refreshGoalKeeper;
    public bool up, down, right, left;
    private bool isMoving;
    public float speed;
    public GameManager gM;
    private Vector2 movement;
    private Vector2 mousePos;
    private Ball ballScript;
    public float ballSpeed;
    [SerializeField] float time = 0.2f;
    private bool camera;
   

    // Start is called before the first frame update
    void Start()
    {

        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if(gM.health <= 0)
        {
            Destroy(gameObject);
        }
        if(camera == false)
        {
            camera = true;
            cam = GameObject.FindGameObjectWithTag("Pitch").GetComponentInChildren<Camera>();
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
            gM.health -= 1;
            gM.healthText.text = "" + gM.health;
            PlayerPrefs.SetInt("GameHealth", gM.health);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            gM.health -= 1;
            gM.healthText.text = "" + gM.health;
            PlayerPrefs.SetInt("GameHealth", gM.health);
            gM.enemiesKilled += 1;
            Destroy(collision.gameObject);
        }
    }








}
