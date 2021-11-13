using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerShard : MonoBehaviour
{
    public SpinningEnemy spinner;
    private GameManager gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            spinner.health -= 1;
            Instantiate(spinner.particle, transform.position, Quaternion.identity);
            Destroy(gameObject);

            if (spinner.health <= 0)
            {

                gM.enemiesKilled += 1;
                gM.enemiesAlive -= 1;
                Destroy(spinner.gameObject);
            }

        }
       else if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("RedCard")))
        {
            
                //spinner.transform.position = new Vector2(transform.position.x, transform.position.y);
            
        }
    }
}
