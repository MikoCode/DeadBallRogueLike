using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Move[] smoothTransition;
    public Camera mainCam;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(mainCam.orthographicSize <= 10)
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
                mainCam.orthographicSize -= 2;
            }
           
        }
    }

    public void StartLevel1()
    {

        start = true;

    }


    public void CameraTV()
    {
      
    }
}


