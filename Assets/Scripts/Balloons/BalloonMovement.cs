using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    private int randomX;
    private int randomY;
    private int firstMovement;
    public Animator myAnim;
    public float moveSpeed;
    public Vector2 vector2DestinyX;
    public Vector2 vector2DestinyY;
    public float currentPositionX = 0;
    public float currentPositionY = 0;
    public int counter = 0;
    public float timeToWait = 0f;
    [SerializeField] GameObject dialogBox;
    public bool balloonCanTalk = false;

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
        if(balloonCanTalk && other.tag == "Player")
        {
            this.gameObject.GetComponent<DialogActivator>().canActivate = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.gameObject.GetComponent<DialogActivator>().canActivate = false;
            Debug.Log("BalloonMovement1 pone canActivate en: " +  this.gameObject.GetComponent<DialogActivator>().canActivate);
        }
    }

    // Update is called once per frame
    void Update()
    {

        AvoidingCollisions();

        if (counter == 2)
        {
            // Wait 20 seconds
            timeToWait = 20f;

            // The balloon its not moving and then we can talk
            balloonCanTalk = true;

            counter = 0;
            GetRandomCoordTest();
        } else
        {
             this.gameObject.GetComponent<DialogActivator>().canActivate = false;
            // Debug.Log("BalloonMovement2 pone canActivate en: " +  this.gameObject.GetComponent<DialogActivator>().canActivate);
        }

        // This condition waits 20 seconds
        // The countdown its only when were not talking to the npc
        if (!dialogBox.activeInHierarchy && !this.gameObject.GetComponent<DialogActivator>().canActivate)
        {
            timeToWait -= Time.deltaTime;
        }
        if (timeToWait <= 0)
        {
            // The balloon is moving and then we cannot talk
            //this.gameObject.GetComponent<DialogActivator>().canActivate = false;
            balloonCanTalk = false;

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


        // // Keeping the npc inside the map
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, theMap.localBounds.min.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    private void AvoidingCollisions()
    {

        // Identify wich habitant is colliding, avoiding usisng instances

        // Access the child of the object habitant to have the specific habitant is colliding
        if (transform.Find("collider").GetComponent<AvoidCollision>().hasCollided == true)
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

            transform.Find("collider").GetComponent<AvoidCollision>().hasCollided = false;
        }
    }

    private void GetRandomCoordTest()
    {
        randomX = Random.Range(-10, 10);
        randomY = Random.Range(-10, 10);
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
