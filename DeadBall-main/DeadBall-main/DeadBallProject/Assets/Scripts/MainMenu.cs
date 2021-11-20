using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Move[] smoothTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel1()
    {

        for
       (int i = 0; i < 2; i++)
        {

            smoothTransition[i].timetoMove = true;
            smoothTransition[i].timetoMove = true;
        }
       
       
;   }
}
