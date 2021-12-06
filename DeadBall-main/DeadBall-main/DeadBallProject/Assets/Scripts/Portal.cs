using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform secondPortal;
    // Start is called before the first frame update
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = new Vector2(secondPortal.position.x, secondPortal.position.y);
    }
}
