using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{
    
    private AudioSource audioSource;
    private GameManager gM;
    private float time, startTime;
    
    // Start is called before the first frame update
    private void Awake()
    {
        startTime = 0.1f;
        time = startTime;
        audioSource = GetComponent<AudioSource>();
      

        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(gM == null)
        {
            gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); 
        }

        if(gM.gameOver == true)
        {
            
            time -= 1 * Time.deltaTime;
            
            if (time < 0 && audioSource.pitch > 0 )
            {
                if(audioSource.volume > 0.1f)
                {
                    audioSource.volume -= 0.1f;
                }
              
                audioSource.pitch -= 0.07f;
                time = startTime;
            }
        }


        if(gM.musicQuieter == true)
        {

            time -= 1 * Time.deltaTime;
            if (time < 0 && audioSource.volume > 0.2f)
            {
                
                
                    audioSource.volume -= 0.04f;
                

               
                time = startTime;
            }
        }

        if(gM.musicLouder == true)
        {
            time -= 1 * Time.deltaTime;
            if (time < 0 )
            {
                if (audioSource.volume < 0.3f)
                {
                    audioSource.volume += 0.04f;
                    time = startTime;
                }
                else
                {
                    gM.musicLouder = false;
                }

            }
            
        }
      
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
}
