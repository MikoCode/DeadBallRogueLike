using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinnerShard : MonoBehaviour
{
    public SpinningEnemy spinner;
    private GameManager gM;
    public SpriteRenderer sprite;
    public GameObject particle;
    public CircleCollider2D cc;
    public GameObject light2D;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            sprite.enabled = false;
            cc.enabled = false;
            Destroy(light2D);
            spinner.health -= 1;
            Instantiate(spinner.particleSmall, transform.position, Quaternion.identity);
            

            if (spinner.health <= 0)
            {

                gM.enemiesKilled += 1;
                gM.enemiesAlive -= 1;
                spinner.StartCoroutine("Dying");

            }
            else
            {
                spinner.source.PlayOneShot(spinner.destroyShard, 0.4f);

            }

            particle.gameObject.SetActive(true);

        }
    }
}
