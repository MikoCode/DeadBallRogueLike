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
    [SerializeField] private int waves;

    // Start is called before the first frame update
    void Start()
    {
      
        
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

            if (waveType == 0 || waveType == 1)
            {
                for (int j = 1; j <= howManyEnemies; j++)
                {

                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[0], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color;





                }


                if (waveType == 1)
                {

                    for (int k = 1; k <= 1; k++)
                    {
                        enemiesSum += 1;
                        Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                        GameObject enemy = Instantiate(Enemies[1], pos, Quaternion.identity);
                        ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                        particle.startColor = enemy.GetComponent<SpriteRenderer>().color;

                    }


                    yield return new WaitForSeconds(3);
                }
            }
            else if (waveType == 2 || waveType == 3)
            {
                for (int j = 1; j <= howManyShooters; j++)
                {

                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[Random.Range(1, 3)], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(preSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color;




                }
                yield return new WaitForSeconds(waveCooldown);
            }

            else if (waveType == 4 || waveType == 5)
            {
                for (int j = 1; j <= howManySpinners; j++)
                {
                    enemiesSum += 1;
                    Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                    GameObject enemy = Instantiate(Enemies[Random.Range(3, 4)], pos, Quaternion.identity);
                    ParticleSystem particle = Instantiate(bigPreSpawn, pos, Quaternion.identity);
                    particle.startColor = enemy.GetComponent<SpriteRenderer>().color; /// error

                }


                yield return new WaitForSeconds(waveCooldown * 2);

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


    void SpawnerSize()
    {
        if (rand.place == 0)
        {
            center = new Vector2(-1.3f, -4.45f);
            size = new Vector2(12.3f, 16.5f);
        }
        else if (rand.place == 1)
        {
            center = new Vector2(-5.6f, -4.76f);
            size = new Vector2(17f, 16.5f);
        }
        else if (rand.place == 2)
        {
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

                if (rand.place == 0 || rand.place == 1)
                {
                    Instantiate(covers[0], pos, Quaternion.identity);
                    pos += new Vector2(Random.Range(2, 6), Random.Range(2, 6));
                }

                else
                {
                    Instantiate(covers[1], pos, Quaternion.Euler(0, 0, 90));
                    pos += new Vector2(Random.Range(2, 6), Random.Range(2, 6));
                }



            }
         }
        

        }


        void Difficulty()
        {
            if (gM.currentLevel < 3)
            {
                waveType = 0;
                waves = Random.Range(1, 3);

            }

            else if (gM.currentLevel >= 3 && gM.currentLevel <= 7)
            {
                waveType = Random.Range(0, 4);
                howManyShooters = 2;
                waves = Random.Range(2, 4);
                waveCooldown = 4;
                howManySpinners = 1;
            }
            else if (gM.currentLevel > 7)
            {
                howManySpinners = 1;
                waves = Random.Range(2, 4);
                howManyShooters = Random.Range(2,4);
                waveCooldown = 4;
                waveType = Random.Range(0, 5);
            }
            else if (gM.currentLevel > 15)
            {
                howManySpinners = Random.Range(1,3);
                howManyShooters = Random.Range(2, 5); 
                waveCooldown = 5;
                waves = Random.Range(3, 5);
                waveType = Random.Range(0, 5);
            }
        }




        void EnemiesCount()
        {
            if (waveType == 0 || waveType == 1)
            {
                howManyEnemies = Random.Range(3, 8);
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
    
}
