using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{


    private float startPosX, startPosY, offset;
    private bool goingBack;
    public bool isVertical;
    private bool goalComponents;
    public int difficulty; // 1- Easy 2- Normal 3 - Hard
    [SerializeField] private int direction;
    [SerializeField] private float speed;
    public Transform Ball;
    public PlayerController playerCon;
    public Goal goal;
    public GameObject hearth;
   

    public AudioClip defend;
    // Start is called before the first frame update
    void Start()
    {
       
            
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        direction = 3;
      
        
        speed = Random.Range(6, 12);
        offset = Random.Range(-0.7f, 0.7f);
        
      
    }

    // Update is called once per frame
    void Update()
    {
       if(goalComponents == false)
        {
            if(Ball != null)
            {
                Ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
            }
          
            playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            goalComponents = true;
        }



        
        if (isVertical == true)
        {
            VerticalKeeper();
        }
        else
        {
            HorizontalKeeper();
        }
     
    }


    void HorizontalKeeper()
    {

        if (goal.didScore == true)
        {
            Goal();
           
        }

        else if (goal.didScore == false && goal.refreshGoalKeeper == true || playerCon.refreshGoalKeeper == true)

        {
            if (difficulty == 1)
            {
                speed = Random.Range(1.3f, 4f);
            }
            else if (difficulty == 2)
            {
                speed = Random.Range(2.5f, 6.1f);
            }
            else
            {
                speed = Random.Range(5.6f, 8.3f);
            }

            Ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
            direction = Random.Range(0, 15);
           
            goal.refreshGoalKeeper = false;
            playerCon.refreshGoalKeeper = false;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosX +  offset, transform.position.y), 2 * Time.deltaTime);
            offset = Random.Range(-0.7f, 0.7f);
        }



        if (transform.position.x <=  startPosX + 2.1f && transform.position.x >=  startPosX -2.1f && playerCon.hasBall == false && goingBack == false && goal.didScore == false)
        {
            if (direction == 0)
            {


                if (Vector2.Distance(new Vector2(Ball.transform.position.x, Ball.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 4 && Ball != null)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(-Ball.transform.position.x, transform.position.y), speed * Time.deltaTime);
                }
            }
            else if (direction == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosX + offset, transform.position.y), speed * Time.deltaTime);
            }
            else
            {
                if(Ball != null)
                {
                    if (Vector2.Distance(new Vector2(Ball.transform.position.x, Ball.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 4)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Ball.transform.position.x, transform.position.y), speed * Time.deltaTime);
                    }
                }

                
             
            }

        }
        else
        {
            goingBack = true;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosX, transform.position.y), 2 * Time.deltaTime);

            if (transform.position.x == startPosX)
            {
                goingBack = false;
               
            }


        }


        if (playerCon.hasBall == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosX +offset, transform.position.y), 3 * Time.deltaTime);
            
        }
    }

    void VerticalKeeper()
    {
        if (goal.didScore == true)
        {
            Goal();
        }

        else if (goal.didScore == false && goal.refreshGoalKeeper == true || playerCon.refreshGoalKeeper == true)

        {
            if (difficulty == 1)
            {
                speed = Random.Range(1.3f, 4f);
            }
            else if (difficulty == 2)
            {
                speed = Random.Range(2.5f, 6.1f);
            }
            else
            {
                speed = Random.Range(5.6f, 8.3f);
            }

            Ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
            direction = Random.Range(0, 15);
         
            goal.refreshGoalKeeper = false;
            playerCon.refreshGoalKeeper = false;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startPosY + offset), 2 * Time.deltaTime);
            offset = Random.Range(-0.7f, 0.7f);
        }



        if (transform.position.y <= startPosY + 1.1f && transform.position.y >= startPosY - 2.1f && playerCon.hasBall == false && goingBack == false && goal.didScore == false)
        {
            if (direction == 0)
            {


                if ( Ball != null && Vector2.Distance(new Vector2(Ball.transform.position.x, Ball.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 4 )
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -Ball.transform.position.y), speed * Time.deltaTime);
                }
            }
            else if (direction == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startPosY + offset), speed * Time.deltaTime);
            }
            else
            {
                if (Ball != null && Vector2.Distance(new Vector2(Ball.transform.position.x, Ball.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < 4)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, Ball.transform.position.y), speed * Time.deltaTime) ;
                }
            }

        }
        else
        {
            goingBack = true;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startPosY), 2 * Time.deltaTime);

            if (transform.position.y == startPosY)
            {
                goingBack = false;
                
            }


        }


        if (playerCon.hasBall == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, startPosY + offset), 3 * Time.deltaTime);
           
        }
    }

   



    void Goal() 
    {
        Instantiate(hearth, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}


