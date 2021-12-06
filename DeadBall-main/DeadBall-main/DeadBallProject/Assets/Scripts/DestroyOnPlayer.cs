using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayer : MonoBehaviour
{
    public GameObject cover;

    
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("RedCard"))
        {
            Destroy(cover);
        }
    }
}
