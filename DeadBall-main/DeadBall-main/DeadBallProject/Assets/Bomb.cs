using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject DetonationSpot;
    public Spawner spawner;
    private Rigidbody2D rb;
    public float speed;
    private Vector2 pos;
    private Vector2 randomPos;
    private Vector2 startPos;
    public Detonator detonator;
    public float distance;
    private PlayerController Player;
    public bool isShaking;
    public float amount;
    public float newDistance;
    private Vector3 OriginalPos;
    private bool definedDetonator;
    public AudioSource source;
    public AudioClip clip;
    public bool randomMode;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPos = new Vector2(transform.position.x + Random.Range(-2.1f, 2f), transform.position.y + Random.Range(-2.1f, 2f));
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Spawner>();
        randomPos = spawner.center + new Vector2(Random.Range(-spawner.size.x / 2, spawner.size.x / 2), Random.Range(-spawner.size.y / 2, spawner.size.y / 2));
        rb = GetComponent<Rigidbody2D>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        pos = new Vector2(Player.transform.position.x + Random.Range(-2.1f,2f), Player.transform.position.y + +Random.Range(-2.1f, 2f));       
       
       
    }

    // Update is called once per frame
    void Update()

        
    {

        if(definedDetonator == false)
        {
            if(randomMode == true)
            {

                detonator = Instantiate(DetonationSpot, randomPos, Quaternion.identity).GetComponent<Detonator>();
                distance = Vector2.Distance(startPos, randomPos);
            }
            else
            {
                detonator = Instantiate(DetonationSpot, pos, Quaternion.identity).GetComponent<Detonator>();
                distance = Vector2.Distance(startPos, pos);
            }
            definedDetonator = true;
        }


        if (isShaking)
        {
            Vector3 newPos = OriginalPos + Random.insideUnitSphere * (Time.deltaTime * amount);

            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        

        if(randomMode == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPos, speed * Time.deltaTime);
            newDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), randomPos);
            if (transform.position.x == randomPos.x && transform.position.y == randomPos.y)
            {
                detonator.ready = true;
                StartCoroutine("Shake");

            }
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
            newDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), pos);
            if (transform.position.x == pos.x && transform.position.y == pos.y)
            {
                detonator.ready = true;
                StartCoroutine("Shake");

            }
        }

       

       
        
    }
    private void FixedUpdate()
    {
        if (newDistance >= (distance / 2))
        {
            if (gameObject.transform.localScale.x < 0.9f && gameObject.transform.localScale.y < 0.9f && gameObject.transform.localScale.z < 0.9f)
            {
                gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }

        }
        else
        {
            if (gameObject.transform.localScale.x > 0.6f && gameObject.transform.localScale.y > 0.6f && gameObject.transform.localScale.z > 0.6f)
            {
                gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }


        }
    }
    public IEnumerator Shake()
    {
        OriginalPos = transform.position;

        if (isShaking == false)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(1f);
        source.PlayOneShot(clip,0.3f);
        Destroy(gameObject);
        
    }



}
