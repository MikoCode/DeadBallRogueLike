using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{

    public GameObject[] pitch;
    public int place, covers, enemies, waves;
    public bool blockades;
    public bool isGoalkeeper;
    public Spawner spawner;
    public GameManager gM;
    public int goalkeeperLevel;
    // Start is called before the first frame update
    void Start()
    {
            place = Random.Range(0, 9);


        if(gM.currentLevel == 1)
        {
            PlayerPrefs.SetInt("GoalKeeperLevel", gM.currentLevel);
        }


        goalkeeperLevel = PlayerPrefs.GetInt("GoalKeeperLevel");

        if(Random.Range(0,4) >= 2 && gM.health < 3 && gM.currentLevel >= (goalkeeperLevel +=3))
        {
            isGoalkeeper = true;
            PlayerPrefs.SetInt("GoalKeeperLevel", gM.currentLevel);
        }
      
        if(Random.Range(0,2) == 0)
        {
            blockades = true;
            covers = Random.Range(0, 3);
        }

        enemies = Random.Range(3, 6);
        waves = Random.Range(2, 4);
        

        for(int i = 0; i < 1; i++)
        {
            Instantiate(pitch[place], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
