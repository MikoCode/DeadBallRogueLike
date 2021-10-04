using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCard : MonoBehaviour
{
    private float speed;
    private GameObject Player;
    private GameManager gM;
    [SerializeField] private int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        speed = Random.Range(2,4);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), speed * Time.deltaTime);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            health -= 1;
            if (health <= 0)
            {
                gM.enemiesKilled += 1;
                Destroy(gameObject);
            }
        }
       
        else if (collision.gameObject.CompareTag("RedCard"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }




    }

    
}
