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
            Instantiate(spinner.particleSmall, transform.position, Quaternion.identity);
            

            if (spinner.health <= 0)
            {

                gM.enemiesKilled += 1;
                gM.enemiesAlive -= 1;
                spinner.source.PlayOneShot(spinner.destroySpinner, 1f);
                Instantiate(spinner.particleBig, transform.position, Quaternion.identity);
                Destroy(spinner.gameObject);
            }
            else
            {
                spinner.source.PlayOneShot(spinner.destroyShard, 0.4f);
            }

            Destroy(gameObject);

        }
       else if (collision.gameObject.CompareTag("Player") || (collision.gameObject.CompareTag("RedCard")))
        {
            
                //spinner.transform.position = new Vector2(transform.position.x, transform.position.y);
            
        }
    }
}
