using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public bool ready, colliding;
    private PlayerController pc;
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ready== true)
        {
            StartCoroutine("Detonation");
            ready = false;
           
        }
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colliding = true;
            
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colliding = false;


        }
    }
    IEnumerator Detonation()
    {

        yield return new WaitForSeconds(1f);

        Instantiate(explosion, transform.position, Quaternion.identity);

        if (colliding == true)
        {
           
            pc.LoseHealth();
           
            
            colliding = false;

        }
        
        
            Destroy(gameObject);
        
       

    }
}
