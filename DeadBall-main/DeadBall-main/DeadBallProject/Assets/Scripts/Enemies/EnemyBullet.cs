using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PlayerController Player;
    private Vector3 moveDirection;
    public bool isMultiple;
    private float posX, posY;
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        moveDirection = ((Player.transform.position  - transform.position)).normalized * speed;
        if(isMultiple == false)
        {
            rb.velocity = new Vector3(moveDirection.x + Random.Range(-1.1f, 1.1f), moveDirection.y + Random.Range(-1.1f, 1.1f));
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x , moveDirection.y );
        }
      
        Destroy(gameObject,5f);
     
     
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }


}
