using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCard : MonoBehaviour
{
    private float speed;
    private GameObject Player;
    public Rigidbody2D rb;
    public GameManager gM;
    public int health;
    public SpriteRenderer sprite;
    public BoxCollider2D bc;
    private bool isRed;
    public GameObject redCard;
    public bool start;
    public ParticleSystem explosion;
    public bool areSpawned;
    public AudioClip destroySound;
    private AudioSource source;



    // Start is called before the first frame update
    void Start()
    { 
        
        if(areSpawned == true)
        {
            gameObject.SetActive(false);
        }
        Invoke("StartMoving", 1f);
        Invoke("SetActive", Random.Range(1.7f,3.1f));
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        Invoke(("StartMoving"), 0.5f);
        health = 1;
        speed = Random.Range(2,5);
        Player = GameObject.FindGameObjectWithTag("Player");
        gM.enemiesAlive += 1;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
      
            
    }


    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Card"))
        {

            gM.enemiesAlive -= 1;

            if (isRed == false)
            {
                if(GetInstanceID() < collision.gameObject.GetInstanceID())
                {
                    
                    Instantiate(redCard, collision.gameObject.transform.position, Quaternion.identity);
                }

                isRed = true;
                Destroy(gameObject);
               
                
            }
            
            

           
        }

       else  if (collision.gameObject.CompareTag("Ball")  && ( start == true) && gM.isBallReturning == false)
        {
            source.PlayOneShot(destroySound, 1f);
            health -= 1;
            if (health <= 0)
            {
                gM.enemiesAlive -= 1;
                gM.enemiesKilled += 1;
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        

    }


    void Moving()
    {
        if (Player != null && start == true) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), speed * Time.deltaTime);
        }
    }

    void StartMoving()
    {
        start = true;
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
    

}
