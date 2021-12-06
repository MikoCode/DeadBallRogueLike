using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            
            collision.gameObject.transform.position = new Vector2(collision.gameObject.transform.position.x,collision.gameObject.transform.position.y);
            
        }
    }


    
}
