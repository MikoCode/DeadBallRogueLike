using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject ball;
    public bool refreshGoalKeeper;
    private GameManager gM;
    public AudioClip goalSound;
    public AudioClip playerTeleport;
    private AudioSource source;
    public GameObject goalKeeper;
    public ParticleSystem goalExplosion;
    public GameObject[] posts;
    public GameObject goalKeeperPrefab;
    public GameObject Player;
    public GameObject goalLight;
    public Transform startPlayer;
    public bool didScore;
    public GameObject box;
    public bool isQuest;
    // Start is called before the first frame update
    void Start()
    {
       
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        source = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && didScore == false && isQuest == false)
        {
            gM.goalsCount += 1;
            PlayerPrefs.SetInt("Goals", gM.goalsCount);
            gM.level.text = "Goals: " + gM.goalsCount;
            if (gM.goalsCount > gM.highScore)
            {
                PlayerPrefs.SetInt("HighScore", gM.goalsCount);
            }
            refreshGoalKeeper = true;
            didScore = true;
            goalLight.gameObject.SetActive(true);
            source.PlayOneShot(goalSound, 0.5f);
            Instantiate(goalExplosion, transform.position, Quaternion.identity);
            for (int i = 0; i < posts.Length; i++)
            {
                Destroy(posts[i].gameObject);
            }
            Destroy(collision.gameObject);
            if (box != null) 
            { 
                box.gameObject.SetActive(false);
            }
              
           
           



        }

        else if (collision.gameObject.CompareTag("Ball") && didScore == false && isQuest == true)
        {
            gM.goalsCount += 1;
            PlayerPrefs.SetInt("Goals", gM.goalsCount);
            gM.level.text = "Goals: " + gM.goalsCount;
            refreshGoalKeeper = true;
            didScore = true;
            goalLight.gameObject.SetActive(true);
            source.PlayOneShot(goalSound, 0.5f);
            Instantiate(goalExplosion, transform.position, Quaternion.identity);
            for (int i = 0; i < posts.Length; i++)
            {
                Destroy(posts[i].gameObject);
            }
          
            if (box != null)
            {
                box.gameObject.SetActive(false);
            }
        }

        
        

        
        else if (collision.gameObject.CompareTag("Player") && didScore == true && isQuest == false)

        {
            source.PlayOneShot(playerTeleport, 0.5f);
            gM.currentLevel += 1;
            PlayerPrefs.SetInt("currentLevel", gM.currentLevel);
            Destroy(collision.gameObject);
            gM.NextScene();
        }


    }
}
