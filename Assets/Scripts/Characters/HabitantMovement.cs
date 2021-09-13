using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantMovement : MonoBehaviour
{

    private int randomX;
    private int randomY;
    private int firstMovement;
    private int destinyCoordX;
    private int destinyCoordY;
    public bool canMove;
    public Animator myAnim;
    public float moveSpeed;
    public Vector2 vector2DestinyX;
    public Vector2 vector2DestinyY;
    public float currentPositionX = 0;
    public float currentPositionY = 0;
    public int counter = 0;
    public float timeToWait = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the first coords for the habitant
        currentPositionX = gameObject.transform.position.x;
        currentPositionY = gameObject.transform.position.y;

        GetRandomCoordTest();

    }

    // Update is called once per frame
    void Update()
    {
        AvoidingCollisions();

        if (counter == 2)
        {
            // Wait 5 seconds
            timeToWait = 5f;

            counter = 0;
            GetRandomCoordTest();
        }

        // This condition waits 5 seconds
        timeToWait -= Time.deltaTime;
        if (timeToWait <= 0)
        {
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
    }

    private void AvoidingCollisions()
    {

        // The only problem here is the posibility of a loop when having to colliders very close.
        if (AvoidCollision.instance.hasCollided)
        {
            if (myAnim.GetFloat("moveX") == -1)
            {
                // move x to 5
                vector2DestinyX = new Vector2(transform.position.x + 3, currentPositionY);
            }
            else if (myAnim.GetFloat("moveX") == 1)
            {
                // move x to 5
                vector2DestinyX = new Vector2(transform.position.x - 3, currentPositionY);
            }

            if (myAnim.GetFloat("moveY") == -1)
            {
                vector2DestinyY = new Vector2(currentPositionX, transform.position.y + 3);
            }
            else if (myAnim.GetFloat("moveY") == 1)
            {
                vector2DestinyY = new Vector2(currentPositionX, transform.position.y - 3);
            }

            AvoidCollision.instance.hasCollided = false;
        }
    }

    private void GetRandomCoordTest()
    {
        randomX = Random.Range(-5, 5);
        randomY = Random.Range(-5, 5);
        firstMovement = Random.Range(1, 2);
        Debug.Log("Random X: " + randomX);
        Debug.Log("Random Y: " + randomY);
        Debug.Log("First Movement: " + firstMovement);
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


    //***************************************************************************************************************************

}
