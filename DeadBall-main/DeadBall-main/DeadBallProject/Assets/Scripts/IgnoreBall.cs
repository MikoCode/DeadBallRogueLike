using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBall : MonoBehaviour
{
    public BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            collision.gameObject.transform.position = collision.gameObject.transform.position;
        }
    }
}
