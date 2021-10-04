using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject ball;
    public bool refreshGoalKeeper;
    private GameManager gM;
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
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && didScore == false && isQuest == false)
        {
            refreshGoalKeeper = true;
            didScore = true;
            goalLight.gameObject.SetActive(true);
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
            refreshGoalKeeper = true;
            didScore = true;
            goalLight.gameObject.SetActive(true);
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
            
            gM.currentLevel += 1;
            PlayerPrefs.SetInt("currentLevel", gM.currentLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
