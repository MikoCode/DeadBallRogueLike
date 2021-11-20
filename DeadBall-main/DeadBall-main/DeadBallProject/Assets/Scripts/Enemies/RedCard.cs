using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCard : MonoBehaviour
{
    private float speed;
    private GameObject Player;
    private GameManager gM;
    public ParticleSystem destroyParticle;
    [SerializeField] private int health = 3;
    public AudioClip destroySound;
    private AudioSource source;
    public float amount;
    private Vector3 OriginalPos;
    private bool isShaking;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        speed = Random.Range(2,4);
        Player = GameObject.FindGameObjectWithTag("Player");
        gM.enemiesAlive += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), speed * Time.deltaTime);
        }

        if (isShaking)
        {
            Vector3 newPos = OriginalPos + Random.insideUnitSphere * (Time.deltaTime * amount);

            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            health -= 1;
            speed += 0.7f;
            StartCoroutine("Shake");
            if (health <= 0)
            {

                source.PlayOneShot(destroySound, 1f);
                gM.enemiesAlive -= 1;
                gM.enemiesKilled += 2;
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
       
        else if (collision.gameObject.CompareTag("RedCard"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }




    }

    public IEnumerator Shake()
    {
        OriginalPos = transform.position;

        if (isShaking == false)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(0.25f);
        isShaking = false;
        transform.position = OriginalPos;
    }
}
