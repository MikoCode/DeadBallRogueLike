using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI goal;
    public TextMeshProUGUI[] YouDied;
    public Button RestartButton;
    public TextMeshProUGUI level;
    public Transform doors;
    public Spawner spawner;
    public Image[] hearth;
    public int currentLevel;
    public int startTime;
    public int questsDone;
    public int requieredQuests;
    public int enemiesKilled;
    public int health;
    public int enemiesRequiered;
    public bool gameOn;
    public bool goalSkip;
    public bool multipleQuestLevel;
    public bool find;
    public bool spawnLate;
    public bool isBallReturning;
    public bool MainMenu;
    public bool gameOver;
    public Move[] smoothTransition;
    public GameObject audioObject, newAudioObject;
    public int enemiesAlive;
    public bool soundOff;
    public bool musicQuieter;
    public bool musicLouder;
    private int goals;
    public int highScore;
    public Image pauseImage, musicImage;
    public TextMeshProUGUI pauseText;
    public bool paused;
    public Sprite[] music;
    public Button musicButton;
    public int goalsCount;
    
    


    // Start is called before the first frame update
    void Start()
    {
        

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 160;
        musicLouder = true;

        highScore = PlayerPrefs.GetInt("HighScore");

        goalsCount = PlayerPrefs.GetInt("Goals");

        currentLevel = PlayerPrefs.GetInt("currentLevel");
      
        if (currentLevel == 1 && MainMenu == false)
        {
            health = 3;
            PlayerPrefs.SetInt("GameHealth", health);

            newAudioObject = Instantiate(audioObject, transform.position, Quaternion.identity);
            
        }
        else
        {
            newAudioObject = GameObject.FindGameObjectWithTag("Music");
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

        goals = currentLevel - 1;
        if(MainMenu == false)
        {
            level.text = "Goals: " + goalsCount;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (MainMenu == false)
        {
            

            if(paused == false)
            {
                Gameplay(); 

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
                {
                    PauseOn();
                    if (PlayerPrefs.GetInt("Sound") == 0)
                    {
                        musicImage.sprite = music[0];
                    }
                    else
                    {
                        musicImage.sprite = music[1];
                    }
                   
                    
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
                {
                    PauseOff();

                }
            }
        }


        
    }



  
    

    public void Gameplay()
    {
        if (find == false) // finding doors to another level. Cant be used at start, because it is spawned at the first frame. Function is disabled immidiatly by making find boolean true.
        {
            doors = GameObject.FindGameObjectWithTag("Pitch").gameObject.transform.Find("Doors");
         
            find = true;
        }

        if (health <= 0) // making current level at 0 after player dies.
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
        }

        






        if (enemiesAlive <= 0 && spawner.wavesCount >= spawner.waves)
        {
            
            musicQuieter = true;
            spawner.StopAllCoroutines();
            doors.gameObject.SetActive(false); // opening doors to another level after clearing level
        }
    }

   public  void GameOver()
    {
        if (goalsCount > highScore)
        {
            PlayerPrefs.SetInt("HighScore", goalsCount);
        }
        gameOver = true;
        spawner.StopAllCoroutines();
        StartCoroutine("EndText");

    }


    IEnumerator EndText()
    {
       
        YouDied[0].gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1);
      
        YouDied[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
       
        
        RestartButton.gameObject.SetActive(true);

    }


    public void Restart()
    {
        Destroy(newAudioObject);
        NextScene();
        currentLevel = 1;
        PlayerPrefs.SetInt("Goals", 0);
       

    }

    public void Healing()
    {


        if(health == 2)
        {
            hearth[2].enabled = true;
        }
        else if(health == 1)
        {
            hearth[1].enabled = true;
        }
        health += 1;
        PlayerPrefs.SetInt("GameHealth", health);
    }

    public void NextScene()
    {
        if (goalsCount > highScore)
        {
            PlayerPrefs.SetInt("HighScore", goalsCount);
        }  

        for
        (int i = 0; i < 2; i++)
        {
            
            smoothTransition[i].timetoMove = true;
            smoothTransition[i].timetoMove = true;
        }
        
       
      
    }

    private void PauseOn()
    {
        paused = true;
        Time.timeScale = 0;
        pauseImage.gameObject.SetActive(true);
        pauseText.gameObject.SetActive(true);
        musicImage.gameObject.SetActive(true);
        musicButton.gameObject.SetActive(true);
    }
    private void PauseOff()
    {
        paused = false;
        Time.timeScale = 1;
        pauseImage.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
        musicImage.gameObject.SetActive(false);
        musicButton.gameObject.SetActive(true);
    }

    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("Sound") != 0)
        {
            PlayerPrefs.SetInt("Sound", 0);
            soundOff = true;
            musicImage.sprite = music[0];
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundOff = false;
            musicImage.sprite = music[1];
            musicLouder = true;
        }
    }
}
