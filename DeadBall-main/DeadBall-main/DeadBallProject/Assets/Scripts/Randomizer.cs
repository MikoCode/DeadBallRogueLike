using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{

    
    public int place, covers, enemies, waves, goalkeeperLevel;
    public bool blockades;
    public bool isGoalkeeper, horizontalType;
    public GameObject[] pitch;
    public Spawner spawner;
    public GameManager gM;
   
    // Start is called before the first frame update
    void Start()
    {

        place = Random.Range(0, 12);

        if (place == 2 || place == 3 || place == 4 || place == 5 || place == 9 || place == 10)
        {
            horizontalType = true;
        }
        else
        {
            horizontalType = false;
        }
        


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

   
   
}
