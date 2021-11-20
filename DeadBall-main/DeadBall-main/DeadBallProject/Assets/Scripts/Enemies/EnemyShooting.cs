using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private int health = 1;
    private GameManager gM;
    private Transform playerCon;
    private Vector2 newPos;
    public GameObject projectile;
    public float startTimeBtwShoots;
    private bool moving;
    [Range(1,2)]
    public int mode; // 1 - slow, 2 - fast, 3 - multiple;
    private float curTimeBtwShoots;
    private bool startShooting, shooting, series, isShaking;
    public float whenToShoot;
    public ParticleSystem destroyParticle;
    public float speed;
    private Rigidbody2D rb;
    public float distanceToStop;
    public bool areSpawned;
    public AudioClip destroySound;
    private AudioSource source;
  
    public float amount;
    private Vector3 OriginalPos;

    // Start is called before the first frame update
    void Start()
    {
        if (areSpawned == true)
        {
            gameObject.SetActive(false);
        }
        Invoke("Active", Random.Range(1.7f, 3.1f));

        Invoke("StartShooting", whenToShoot);
        InvokeRepeating("GetPlayerPos",1f,Random.Range(4f,7f));
      
        playerCon = GameObject.FindGameObjectWithTag("Player").transform;
        curTimeBtwShoots = startTimeBtwShoots;
        rb = gameObject.GetComponent<Rigidbody2D>();
        shooting = true;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        gM.enemiesAlive += 1;
    }

    // Update is called once per frame
    void Update()
    {   
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
            newPos = playerCon.transform.position;
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
                          Instantiate(projectile, transform.position, Quaternion.identity);

                          curTimeBtwShoots = startTimeBtwShoots;
                     }


                  }

                 else if(mode == 2)
                  {

                curTimeBtwShoots -= 1 * Time.deltaTime;

                if (curTimeBtwShoots <= 0)
                {
                    Instantiate(projectile, new Vector2(transform.position.x + Random.Range(-0.3f,0.3f), transform.position.y + Random.Range(-0.3f,0.3f)), Quaternion.identity);

                    curTimeBtwShoots = 0.3f;
                }


                 }
            
            }

        
       

    }


    void Moving()
    {
        if ((Vector2.Distance(newPos, transform.position) >= distanceToStop ) )
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            shooting = false;
           
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
        isShaking = false;
        transform.position = OriginalPos;
    }
    
   
}
