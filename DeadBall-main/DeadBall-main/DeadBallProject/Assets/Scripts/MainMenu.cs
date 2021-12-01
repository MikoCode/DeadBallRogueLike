using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Move[] smoothTransition;
    public Camera mainCam;
    private bool start;
    public GameManager gM;
    public GameObject highScoreObject;
    public TextMeshProUGUI highScoreText;
    private bool settingPos;
    public Image renderer;
    public Sprite[] sprite;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Sound", 1);
    }

    // Update is called once per frame
    void Update()
    {
       
        ShowHighScore();
        if (mainCam.orthographicSize <= 10)
        {
            smoothTransition[0].gameObject.SetActive(true);
            smoothTransition[1].gameObject.SetActive(true);
            for
                (int i = 0; i < 2; i++)
            {

                smoothTransition[i].timetoMove = true;
                smoothTransition[i].timetoMove = true;
            }
        }
     
    }
    private void FixedUpdate()
    {
        if (start == true)
        {
            if(mainCam.orthographicSize > 10)
            {
                mainCam.orthographicSize -= 3;
            }
           
        }
    }

    public void StartLevel1()
    {
        PlayerPrefs.SetInt("currentLevel", 1);
        PlayerPrefs.SetInt("Goals", 0);

        start = true;

    }
    private void ShowHighScore()
    {
        if(PlayerPrefs.GetInt("HighScore") >= 1)
        {
            highScoreObject.SetActive(true);
            highScoreText.text = "" + PlayerPrefs.GetInt("HighScore");
            if((PlayerPrefs.GetInt("HighScore") >= 10) && settingPos == false)
            {
                highScoreText.transform.position = new Vector2(highScoreText.transform.position.x - 1.25f, highScoreText.transform.position.y);
                settingPos = true;
            }
        }
    }


    public void CameraTV()
    {
      
    }

    public void SoundButton()
    {
        if(PlayerPrefs.GetInt("Sound") != 0)
        {
            PlayerPrefs.SetInt("Sound", 0);
            gM.soundOff = true;
            renderer.sprite = sprite[0];
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            gM.soundOff = false;
            renderer.sprite = sprite[1];
            
        }
    }
}


