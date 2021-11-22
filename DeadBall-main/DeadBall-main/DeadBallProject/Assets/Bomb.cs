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
    private Vector2 startPos;
    public Detonator detonator;
    public float distance;
    private PlayerController Player;
    public float newDistance;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPos = new Vector2(transform.position.x, transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Spawner>();
        pos = Player.transform.position;               //spawner.center + new Vector2(Random.Range(-spawner.size.x / 2, spawner.size.x / 2), Random.Range(-spawner.size.y / 2, spawner.size.y / 2));
        detonator = Instantiate(DetonationSpot, pos, Quaternion.identity).GetComponent<Detonator>();
        distance = Vector2.Distance(startPos, pos);
    }

    // Update is called once per frame
    void Update()


    { 
        
        newDistance  = Vector2.Distance(new Vector2(transform.position.x,transform.position.y), pos); 

       

        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);

        if(transform.position.x == pos.x && transform.position.y == pos.y)
        {
            detonator.ready = true;
            Destroy(gameObject);
        }
        
    }
    private void FixedUpdate()
    {
        if (newDistance >= (distance / 2))
        {
            if (gameObject.transform.localScale.x < 1.2f && gameObject.transform.localScale.y < 1.2f && gameObject.transform.localScale.z < 1.2f)
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




}
