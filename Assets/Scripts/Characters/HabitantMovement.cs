using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantMovement : MonoBehaviour
{
    private int randomX;
    private int randomY;
    private int firstMovement;
    public Animator myAnim;
    public float moveSpeed;
    public Vector2 vector2DestinyX;
    public Vector2 vector2DestinyY;
    // public Vector2 vectorCollision;
    // public int collisionCounter = 0;
    public float currentPositionX = 0;
    public float currentPositionY = 0;
    public int counter = 0;
    public float timeToWait = 0f;
    public bool movingHabitantCanTalk = false;
    [SerializeField] private GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the first coords for the habitant
        currentPositionX = gameObject.transform.position.x;
        currentPositionY = gameObject.transform.position.y;

        GetRandomCoordTest();

    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(movingHabitantCanTalk && other.tag == "Player")
        {
            this.gameObject.GetComponent<DialogActivator>().canActivate = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.gameObject.GetComponent<DialogActivator>().canActivate = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        AvoidingCollisions();

        if (counter == 2)
        {
            // Wait 5 seconds
            timeToWait = 5f;

            // The habitant its not moving and then we can talk
            movingHabitantCanTalk = true;

            counter = 0;
            GetRandomCoordTest();
        }

        // This condition waits 5 seconds
        // The countdown its only when were not talking to the npc
        if (!dialogBox.activeInHierarchy && !this.gameObject.GetComponent<DialogActivator>().canActivate)
        {
            timeToWait -= Time.deltaTime;
        }
        if (timeToWait <= 0)
        {

            // The habitant is moving and then we cannot talk
            movingHabitantCanTalk = false;
            
            if (firstMovement == 1)
            {
                // If the habitant stop moving in x
                if (myAnim.GetFloat("moveX") == 0)
                {
                    // This assigns a value in x and let the value in y of the gameobject current position.
                    vector2DestinyX = new Vector2(transform.position.x + randomX, currentPositionY);
                }
                MoveXTest();
            }
            else
            {
                // If the habitant stop moving in y
                if (myAnim.GetFloat("moveY") == 0)
                {
                    // This assigns a value in y and let the value in x of the gameobject current position.
                    vector2DestinyY = new Vector2(currentPositionX, transform.position.y + randomY);
                }
                MoveYTest();
            }
        }

        if(dialogBox.activeInHierarchy)
        {
            myAnim.SetFloat("moveX", 0);
            myAnim.SetFloat("moveY", 0);
            vector2DestinyX = new Vector2(transform.position.x, transform.position.y);
            vector2DestinyY = new Vector2(transform.position.x, transform.position.y);
            counter = 2;

            if(currentPositionX < vector2DestinyX.x)
            {
                myAnim.SetFloat("lastMoveX", 1);
            } else if(currentPositionX > vector2DestinyX.x)
            {
                myAnim.SetFloat("lastMoveX", -1);
            } else if(currentPositionY < vector2DestinyY.y)
            {
                myAnim.SetFloat("lastMoveY", 1);
            } else if(currentPositionY > vector2DestinyY.y)
            {
                myAnim.SetFloat("lastMoveY", -1);
            }
        }
    }

    private void AvoidingCollisions()
    {

        // Identify wich habitant is colliding, avoiding usisng instances

        // Access the child of the object habitant to have the specific habitant is colliding
        if (transform.Find("collider").GetComponent<AvoidCollision>().hasCollided == true)
        {
            //vectorCollision = new Vector2(transform.position.x, transform.position.y);

            if (myAnim.GetFloat("moveX") == -1)
            {
                // move x to 5
                vector2DestinyX = new Vector2(transform.position.x + 1, currentPositionY);
                
            }
            else if (myAnim.GetFloat("moveX") == 1)
            {
                // move x to 5
                vector2DestinyX = new Vector2(transform.position.x - 1, currentPositionY);
                
            }

            if (myAnim.GetFloat("moveY") == -1)
            {
                vector2DestinyY = new Vector2(currentPositionX, transform.position.y + 1);
                
            }
            else if (myAnim.GetFloat("moveY") == 1)
            {
                vector2DestinyY = new Vector2(currentPositionX, transform.position.y - 1);
                
            }

            // if(this.transform.position.x == vectorCollision.x && this.transform.position.y == vectorCollision.y)
            // {
            //     collisionCounter ++;
            // }

            // Debug.Log("collisionCounter: " + collisionCounter);

            transform.Find("collider").GetComponent<AvoidCollision>().hasCollided = false;
        }
    }

    private void GetRandomCoordTest()
    {
        randomX = Random.Range(-5, 5);
        randomY = Random.Range(-5, 5);
        firstMovement = Random.Range(1, 2);
    }

    private void MoveXTest()
    {
        if (vector2DestinyX.x != gameObject.transform.position.x)
        {
            
            gameObject.transform.position = Vector2.MoveTowards(transform.position, vector2DestinyX, moveSpeed * Time.deltaTime);
            

            if (vector2DestinyX.x > gameObject.transform.position.x)
            {
                myAnim.SetFloat("moveX", 1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveX", 1);
            }
            else if (vector2DestinyX.x < gameObject.transform.position.x)
            {
                myAnim.SetFloat("moveX", -1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveX", -1);
            }
        }
        else
        {
            // Finish the movement
            myAnim.SetFloat("moveX", 0);

            // Assign current positionX
            currentPositionX = gameObject.transform.position.x;

            counter++;
            firstMovement = 2;
        }
    }

    private void MoveYTest()
    {
        if (vector2DestinyY.y != gameObject.transform.position.y)
        {
           
            gameObject.transform.position = Vector2.MoveTowards(transform.position, vector2DestinyY, moveSpeed * Time.deltaTime);
            

            if (vector2DestinyY.y > gameObject.transform.position.y)
            {
                myAnim.SetFloat("moveY", 1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveY", 1);
            }
            else if (vector2DestinyY.y < gameObject.transform.position.y)
            {
                myAnim.SetFloat("moveY", -1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveY", -1);
            }
        }
        else
        {
            myAnim.SetFloat("moveY", 0);

            // Assign current positionX
            currentPositionY = gameObject.transform.position.y;

            counter++;
            firstMovement = 1;
        }
    }
}
