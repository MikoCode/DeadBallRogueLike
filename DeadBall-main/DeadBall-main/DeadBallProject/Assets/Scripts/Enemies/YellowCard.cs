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
    public bool areSpawned;
    
    


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
        Invoke(("StartMoving"), 0.5f);
        health = 1;
        speed = Random.Range(2,5);
        Player = GameObject.FindGameObjectWithTag("Player");
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


            if (isRed == false)
            {
                if(GetInstanceID() < collision.gameObject.GetInstanceID())
                {
                    gM.enemiesKilled += 1;
                    Instantiate(redCard, collision.gameObject.transform.position, Quaternion.identity);
                }
               
               
                Destroy(gameObject, 0.1f);
                isRed = true;
                
            }
            
            

           
        }

       else  if (collision.gameObject.CompareTag("Ball")  && ( start == true) && gM.isBallReturning == false)
        {
         
            health -= 1;
            if (health <= 0)
            {
                gM.enemiesKilled += 1;
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
