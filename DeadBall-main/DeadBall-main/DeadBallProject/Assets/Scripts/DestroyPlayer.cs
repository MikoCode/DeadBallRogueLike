using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
  
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
