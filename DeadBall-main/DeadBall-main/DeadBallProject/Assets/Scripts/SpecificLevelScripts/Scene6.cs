using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene6 : MonoBehaviour
{

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.enemiesKilled >= 8)
        {
            gm.questsDone += 1;
        }
    }
}
