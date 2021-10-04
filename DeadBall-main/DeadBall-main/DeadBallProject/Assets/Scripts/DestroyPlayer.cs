using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Card") || collision.gameObject.CompareTag("RedCard"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.1f);
        }
    }

    
}
