using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public bool ready;
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
           // Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);
        }
    }


    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(ready == true)
            {
              //  Instantiate(explosion, transform.position, Quaternion.identity);
                pc.LoseHealth();
                Destroy(gameObject);
            }
            
        }
    }
}
