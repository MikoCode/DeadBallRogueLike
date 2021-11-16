using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEnemy : MonoBehaviour
{
    private Transform playerCon;
    private Vector2 newPos;
    private GameManager gM;
    public ParticleSystem particleSmall;
    public ParticleSystem particleBig;
    public float health;
    private bool start;
    public float speed;
    public bool areSpawned;
    public AudioSource source;
    public AudioClip destroyShard;
    public AudioClip destroySpinner;
    // Start is called before the first frame update
    void Start()
    {
        if (areSpawned == true)
        {
            gameObject.SetActive(false);
        }
        health = 3;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        gM.enemiesAlive += 1;
        InvokeRepeating("GetPlayerPos", 1f, Random.Range(3, 6));
        playerCon = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("StartMoving", 2f);

    }

    // Update is called once per frame
    void Update()
    {
        
            Moving();
       
       
        
    }

    void Moving()
    {
        if (start == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        }
        transform.Rotate(new Vector3(0,0,5), 150 * Time.deltaTime);


        if (gameObject.transform.position.x == newPos.x && gameObject.transform.position.y == gameObject.transform.position.y)
        {
            GetPlayerPos();
        }
    }

    void GetPlayerPos()
    {
        if (playerCon != null)
        {
            newPos = new Vector2(playerCon.position.x + Random.Range(-1, 2), playerCon.position.y + Random.Range(-1, 2));
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("RedCard")))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }


    }

    void StartMoving()
    {
        start = true;
        gameObject.SetActive(true);
    }
}