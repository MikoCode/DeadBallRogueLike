using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI goal;
    public TextMeshProUGUI healthText;
    public int currentLevel;
    public bool gameOn;
    public int startTime;
    public bool goalSkip;
    public int enemiesKilled;
    public bool multipleQuestLevel;
    public int questsDone;
    public int requieredQuests;
    public Transform doors;
    public bool isBallReturning;
    public Spawner spawner;
    public bool spawnLate;
    public int health;
    public int enemiesRequiered;
   
    // Start is called before the first frame update
    void Start()
    {
       
        currentLevel = PlayerPrefs.GetInt("currentLevel");

        if(currentLevel == 0)
        {
            health = 3;
            PlayerPrefs.SetInt("GameHealth", health);
        }
        else
        {
            
            health = PlayerPrefs.GetInt("GameHealth");
        }
        enemiesKilled = 0;
        if(goalSkip == false)
        {
            StartCoroutine("GoalText");
        }
        else
        {
            gameOn = true;
        }
        healthText.text = "" + health;
    }

    // Update is called once per frame
    void Update()
    {
        doors = GameObject.FindGameObjectWithTag("Pitch").gameObject.transform.Find("Doors");
        if (health <= 0)
        {
            currentLevel = 0;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if(doors != null && multipleQuestLevel == true)
        {
            if(questsDone == requieredQuests)
            {
                Destroy(doors);
            }
        }

        if(spawnLate == true && spawner!= null)
        {
            if(enemiesKilled >= enemiesRequiered)
            {
                spawner.spawn = true;
                spawnLate = false;
            }
        }


        if (enemiesKilled >= spawner.enemiesSum)
        {
            doors.gameObject.SetActive(false);
        }
      
    }



    IEnumerator GoalText()
    {
        yield return new WaitForSeconds(startTime);
        goal.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        goal.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        goal.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        goal.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        goal.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        goal.gameObject.SetActive(false);
        gameOn = true;


    }
}
