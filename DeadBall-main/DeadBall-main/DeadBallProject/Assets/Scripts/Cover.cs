using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public ParticleSystem explosion;
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 15;
       // transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("upBorder") || collision.gameObject.CompareTag("rightBorder") || collision.gameObject.CompareTag("leftBorder") || collision.gameObject.CompareTag("downBorder"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") )
        {
            if (collision.gameObject.GetComponent<Ball>().returning == false)
            {
                hp -= 5;
                if (hp <= 0)
                {
                    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }


        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            hp -= 2;
            if (hp <= 0)
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }
}
