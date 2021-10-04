using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableDoors : MonoBehaviour
{
    private int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if(hp > 1)
            {
                hp -= 1;
            }
            else
            {
                Destroy(gameObject);
            }


        }
    }
}
