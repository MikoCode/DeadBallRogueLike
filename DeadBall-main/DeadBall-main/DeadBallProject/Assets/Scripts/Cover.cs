using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    private Vector3 OriginalPos;
    private bool isShaking;
    public ParticleSystem explosion;
   [SerializeField] private int hp;
    public float amount;
    public SpriteRenderer[] color;
    public BoxCollider2D[] box;
    private AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        hp = 15;
        // transform.rotation = Quaternion.Euler(0, 0, 90);
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            Vector3 newPos = OriginalPos + Random.insideUnitSphere * (Time.deltaTime * amount);

            newPos.z = transform.position.z;
            transform.position = newPos;
        }

        if(hp < 14 && hp > 8)
        {
            for(int i = 0; i < 4; i++)
            {
                color[i].color = new Color(0.6792f, 0.6792f, 0.6972f, 0.7f);
            }
        }
        else if(hp < 8 && hp > 3)
        {
            for (int i = 0; i < 4; i++)
            {
                color[i].color = new Color(0.6792f, 0.6792f, 0.6972f, 0.5f);
            }
        }
        
        else if(hp <= 3)
        {
            StartCoroutine("DisableCol");
            for (int i = 0; i < 4; i++)
            {
                color[i].color = new Color(0.6792f, 0.6792f, 0.6972f, 0.25f);
                
            }
        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("upBorder") || collision.gameObject.CompareTag("rightBorder") || collision.gameObject.CompareTag("leftBorder") || collision.gameObject.CompareTag("downBorder"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") )
        {
            
            if (collision.gameObject.GetComponent<Ball>().returning == false)
            {
                StartCoroutine("Shake");
                hp -= 4;
                if (hp <= 0)
                {
                   
                    Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                    source.PlayOneShot(clip, 0.5f);
                    Destroy(gameObject);
                }
            }


        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine("Shake");
            Destroy(collision.gameObject);
            hp -= 2;
            if (hp <= 0)
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
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

    private IEnumerator DisableCol()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < 4; i++)
        {

            box[i].enabled = false;
        }
    }
}
