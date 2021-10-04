using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] covers;
    public float spawnTime;
    public int howManyTimes, howManyEnemies, howLong;
    public bool spawn;
    private bool checkSize;
  
    public Vector2 center;
    public Vector2 size;
    public Randomizer rand;
    public int enemiesSum;
    
    
    public ParticleSystem preSpawn;
   
    
    // Start is called before the first frame update
    void Start()
    {
        enemiesSum = howManyTimes * howManyEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkSize == false)
        {
            SpawnerSize();
            CoversSpawn();
            checkSize = true;
        }



        if (spawn == true)
        {
            
            StartCoroutine("EnemiesSpawn");
            spawn = false;
        }
       

    }


    IEnumerator EnemiesSpawn()
    {
        SpawnerSize();
        yield return new WaitForSeconds(spawnTime);

        for (int i = 1; i <= howManyTimes; i ++)
        {
            for (int j = 1; j <= howManyEnemies; j++)
            {
                Vector2 pos = center + new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
                Instantiate(preSpawn, pos, Quaternion.identity);
                Instantiate(Enemies[0], pos, Quaternion.identity);
                
            }
           
            yield return new WaitForSeconds(howLong);
            
            yield return new WaitForSeconds(spawnTime);
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size) ;
       
    }


    void SpawnerSize()
    {
        if(rand.place == 0)
        {
             center = new Vector2(-1.3f, -4.45f);
            size = new Vector2(12.3f, 16.5f);
        }
        else if(rand.place == 1)
        {
            center = new Vector2(-5.6f, -4.76f);
            size = new Vector2(17f, 16.5f);
        }
    }


    void CoversSpawn() // jesli obie sie dotkn¹ musza sie zniszczyc

    {
        if(rand.covers == 1)
        {
            for (int j = 1; j <= 2; j++)
            {
                Vector2 pos = center + new Vector2(Random.Range(-size.x / 2.3f, size.x / 2.3f), Random.Range(-size.y / 2.3f, size.y / 2.3f));
               
                Instantiate(covers[0], pos, Quaternion.identity);
                pos += new Vector2(Random.Range(2, 6), Random.Range(2, 6));


            }
        }
    }


}
