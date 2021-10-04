using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{

    public GameObject[] pitch;
    public int place, covers;
    // Start is called before the first frame update
    void Start()
    {
        place = Random.Range(0, 2);
        covers = 1;

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
