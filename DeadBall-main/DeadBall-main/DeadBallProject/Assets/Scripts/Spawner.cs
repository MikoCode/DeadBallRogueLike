using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] covers;
    public GameObject goalKeeper;
    private GameObject box;
    public ParticleSystem preSpawn;
    public ParticleSystem bigPreSpawn;
    public Vector2 goalPos;
    public GameManager gM;
    public Randomizer rand;
    public Vector2 center;
    public Vector2 size;
    public float spawnTime;
    public int howManyTimes, howManyEnemies, howLong, waveType;
    public bool spawn;
    private bool checkSize;
    public int enemiesSum;
    private int howManySpinners, howManyShooters;
    private int waveCooldown;
    public bool canDestroyDoors;
    private int pitchType;
    private int whichShooter;
    private int wave1Shooters;
    public int wavesCount;
    public int waves;

    // Start is called before the first frame update
    void Start()
    {
        Difficulty();
        wavesCount = 0;

        whichShooter = 2;
        waveType = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkSize == false) //  later change this to another class
        {
            goalKeeper = GameObject.FindGameObjectWithTag("Goalkeeper");
            box = GameObject.FindGameObjectWithTag("Pen");
            GoalKeeper();
            SpawnerSize();
            CoversSpawn();
            Difficulty();
            
            checkSize = true;
        }

        EnemiesCount();

        if (spawn == true)
        {

            StartCoroutine("Wave0");


            spawn = false;
        }

        

    }


    IEnumerator Wave0()
    {
        
        SpawnerSize();
        yield return new WaitForSeconds(spawnTime);
        
        for (int i = 1; i <= waves; i++)
        {
            wavesCount += 1;
            if (waveType == 0 || waveType == 1 || waveType == 2)
            {
                for (int j = 1; j <= howManyEnemies; j++)
                {

                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[0], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color;



                   

                }


                if (waveType == 1 || waveType == 2)
                {

                    for (int k = 0; k < wave1Shooters; k++)
                    {
                        enemiesSum += 1;
                        Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                        GameObject enemy = Instantiate(Enemies[Random.Range(1,whichShooter)], pos, Quaternion.identity);
                        ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                        particle.startColor = enemy.GetComponent<SpriteRenderer>().color;
                        
                    }


                    
                }
                yield return new WaitForSeconds(4);

            }
            else if (waveType == 3 || waveType == 4)
            {
                for (int j = 1; j <= howManyShooters; j++)
                {

                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[Random.Range(1, whichShooter)], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color;

                   

                }
                yield return new WaitForSeconds(waveCooldown);
            }

            else if (waveType == 5 || waveType == 6)
            {
                for (int j = 1; j <= howManySpinners; j++)
                {
                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[5], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(bigPreSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color; /// error

                }


                yield return new WaitForSeconds(waveCooldown * 1.4f);

            }





           
            Difficulty();

            yield return new WaitForSeconds(howLong);

          


            


        }


        canDestroyDoors = true;
    }











    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);

    }


   public void SpawnerSize()
    {
        if (rand.place < 4)
        {
            pitchType = 1;
            center = new Vector2(-1.3f, -4.45f);
            size = new Vector2(12.3f, 16.5f);
        }
        else if (rand.place > 3 && rand.place < 8)
        {
            pitchType = 2;
            center = new Vector2(-5.6f, -4.76f);
            size = new Vector2(17f, 16.5f);
        }
        else 
        {
            pitchType = 3;
            center = new Vector2(-0.7f, -5.72f);
            size = new Vector2(17.12f, 15.78f);
        }
    }


    void CoversSpawn() // jesli obie sie dotkn¹ musza sie zniszczyc

    {
        if (rand.blockades == true)
        {


            Vector2 pos = center + new Vector2(Random.Range(-size.x / 2.3f, size.x / 2.3f), Random.Range(-size.y / 2.3f, size.y / 2.3f));

            for (int j = 0; j <= rand.covers; j++)
            {

                if (rand.horizontalType == true)
                {
                    Instantiate(covers[0], pos, Quaternion.identity);
                    pos += new Vector2(Random.Range(2, 6), Random.Range(2, 6));
                }

                else if (rand.horizontalType == false)
                {
                    Instantiate(covers[1], pos, Quaternion.Euler(0, 0, 90));
                    pos += new Vector2(Random.Range(2, 6), Random.Range(2, 6));
                }



            }
         }
        

        }


        void Difficulty()
        {
            if (gM.currentLevel < 2)
            {
                waveType = 0;
                waves = Random.Range(1, 3);
               

            }

            else if (gM.currentLevel >= 2 && gM.currentLevel <= 7)
            {
            
            if(gM.currentLevel < 5)
            {
                waveType = Random.Range(0, 2);
                whichShooter = 3;
            }
            else
            {
                whichShooter = 4;
                waveType = Random.Range(0, 5);
            }
                
                wave1Shooters = 1;
                howManyShooters = 2;
                waves = Random.Range(2, 4);
                waveCooldown = 6;
                howManySpinners = 1;
            
            }
            else if (gM.currentLevel > 7 && gM.currentLevel <= 12)
            {
          

            if (gM.currentLevel > 9)
            {
                waveType = Random.Range(0, 6);
            }
            else
            {
                waveType = Random.Range(0, 5);
            }

            whichShooter = 5;
            wave1Shooters = Random.Range(1, 3);
            howManySpinners = Random.Range(1, 3);
            waveCooldown = 7;
            howManyShooters = Random.Range(2, 4);
            waves = Random.Range(2, 4);






        }
            else if (gM.currentLevel > 12 && gM.currentLevel <= 17)
        {

           
            if (Random.Range(0,4) == 2)

            {
                howManySpinners = 1;
            }

            else
            {
                howManySpinners = 2;
            }
            whichShooter = 5;
            wave1Shooters = Random.Range(1, 3);
            howManyShooters = Random.Range(2, 5); 
                waveCooldown = 7;
                waves = Random.Range(3, 5);
                waveType = Random.Range(0, 6);
            }
            else if (gM.currentLevel > 17)
        {
            whichShooter = 5;
            wave1Shooters = Random.Range(2, 4);
                howManySpinners = Random.Range(2, 4);
                howManyShooters = Random.Range(3, 6);
                waveCooldown = 9;
                waves = Random.Range(3, 5);
                waveType = Random.Range(0, 6);
        }
    }







    void EnemiesCount()
    {
        if (waveType == 0)
        {


            if(gM.currentLevel < 10)
            {
                howManyEnemies = Random.Range(3, 8);
            }
            else
            {
                howManyEnemies = Random.Range(3, 8);
                
            }

            
        }
        else if (waveType == 1 || waveType == 2)
        {
            howManyEnemies = Random.Range(3, 6);
        }

    }
        
    void GoalKeeper()
    {
        if(rand.isGoalkeeper == false)
        {
            goalKeeper.gameObject.SetActive(false);
            box.gameObject.SetActive(false);
            rand.isGoalkeeper = true;
        }
    }

    void Propability()
    {
       
    }
    
}
