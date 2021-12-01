using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private int health = 1;
    private GameManager gM;
    private Transform playerCon;
    private Vector2 newPos;
    public GameObject projectile, bomb;
    public float startTimeBtwShoots;
    private bool moving, angered;
    [Range(1,2)]
    public int mode; // 1 - slow, 2 - fast, 3 - multiple;
    private float curTimeBtwShoots;
    private bool startShooting,  series;
    public float whenToShoot;
    public ParticleSystem destroyParticle;
    public float speed;
    private Rigidbody2D rb;
    public float distanceToStop;
    public bool areSpawned, bomber, isShaking, smallShooter, shooting;
    public AudioClip destroySound;
    public AudioClip shoot;
    private AudioSource source;
    public float amount;
    private Vector3 OriginalPos;
    private int bombInstantiated;
    public AudioSource ownSource;
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        if (areSpawned == true)
        {
            gameObject.SetActive(false);
        }
        Invoke("Active", Random.Range(1.7f, 3.1f));

        Invoke("StartShooting", whenToShoot);

        if(smallShooter == true)
        {
            InvokeRepeating("GetPlayerPos", 1f, Random.Range(1.5f, 2.9f));
        }
        else
        {
            InvokeRepeating("GetPlayerPos", 1f, Random.Range(4f, 7f));
        }
      
      
        playerCon = GameObject.FindGameObjectWithTag("Player").transform;
        curTimeBtwShoots = startTimeBtwShoots;
        rb = gameObject.GetComponent<Rigidbody2D>();
        shooting = true;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        gM.enemiesAlive += 1;
        if(bomber == true)
        {
            if(Random.Range(0,3) == 3)
            {
                mode = 3;
                isShaking = true;
                angered = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(bombInstantiated >= 15)
        {
            mode = 1;
            startTimeBtwShoots = 1.2f;
            angered = false;
            isShaking = false;
        }
        Shoot();
        Moving();
        if (isShaking)
        {
            Vector3 newPos =  OriginalPos + Random.insideUnitSphere * (Time.deltaTime * amount);
            
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
         
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if(bomber == true)
            {
                if(Random.Range(0,5) == 0)
                {
                    mode = 3;
                    angered = true;
                    startShooting = true;
                    shooting = true;
                    isShaking = true;
                    startTimeBtwShoots = 2.5f;
                    curTimeBtwShoots = 1.5f;
                }
            }
            health -= 1;
            startTimeBtwShoots -= 0.1f;
            StartCoroutine("Shake");
            if (health <= 0)
            {
                gM.enemiesAlive -= 1;
                source.PlayOneShot(destroySound, 1f);
                gM.enemiesKilled += 1;
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject,0.1f);
            }
        }
        else if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("RedCard")))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }


    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 3)
        {

            newPos = new Vector2(transform.position.x - Random.Range(-2, 2), transform.position.y - Random.Range(-2, 2));
            moving = true;
          
        }
        else
        {
         
            moving = false;
            if(playerCon != null)
            {
                newPos = playerCon.transform.position;
            }
          
        }
    }



    void Shoot()
    {

        
        
            if (startShooting == true && playerCon != null && shooting == true)
            {
                 if (mode == 1)

                  {
                     curTimeBtwShoots -= 1 * Time.deltaTime;

                     if (curTimeBtwShoots <= 0)
                     {
                        if(bomber == true)
                         { 
                        for(int i = 0; i < Random.Range(1,3);  i++)
                        {
                           
                            Instantiate(projectile, transform.position, Quaternion.identity);
                        }
                         }
                    else
                    {
                        Instantiate(projectile, transform.position, Quaternion.identity);
                        
                    }
                        

                          curTimeBtwShoots = startTimeBtwShoots;
                     }


                  }

                 else if(mode == 2)
                  {

                curTimeBtwShoots -= 1 * Time.deltaTime;

                if (curTimeBtwShoots <= 0)
                {
                    
                    Instantiate(projectile, new Vector2(transform.position.x + Random.Range(-0.3f,0.3f), transform.position.y + Random.Range(-0.3f,0.3f)), Quaternion.identity);

                    curTimeBtwShoots = startTimeBtwShoots;
                }


                 }
                 else if(mode == 3)
            {
                
               
               
                
              
                curTimeBtwShoots -= 1 * Time.deltaTime;

                if (curTimeBtwShoots <= 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(bomb, transform.position, Quaternion.identity);
                        bombInstantiated += 1;
                    }
                    

                    curTimeBtwShoots = startTimeBtwShoots;
                }
              
            }
            
            }

        
       

    }


    void Moving()
    {
        if ((Vector2.Distance(newPos, transform.position) >= distanceToStop ) && angered == false )
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            if(smallShooter == false)
            {
                shooting = false;
            }
           
           
        }
        else
        {
            shooting = true;
        }

        if(moving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        }
       
    }

    void GetPlayerPos()
    {
        if (playerCon != null)
        {
            newPos = new Vector2(playerCon.position.x + Random.Range(-4.1f, 4.1f), playerCon.position.y + Random.Range(-4.1f, 4.1f));
        }

        
    }


    void StartShooting()
    {
        startShooting = true;
        if(smallShooter == true)
        {
            shooting = true;
        }
    }

    void Active()
    {
        gameObject.SetActive(true);
    }

    public IEnumerator Shake()
    {
        OriginalPos = transform.position;

        if(isShaking == false)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(0.25f);
        if (angered == false)
        {
            isShaking = false;
        }
        transform.position = OriginalPos;
    }
   
    
   
}
