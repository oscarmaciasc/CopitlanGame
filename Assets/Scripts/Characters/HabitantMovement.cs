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

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        destinyCoordX = 0;
        destinyCoordY = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && destinyCoordX == 0 && destinyCoordY == 0)
        {
            GetRandomCoord();
            StartCoroutine(Wait5Secs());
            StepToDestinyCoord();
            HasReachedDestiny();
        }
        else if (canMove)
        {
            StartCoroutine(Wait5Secs());
            StepToDestinyCoord();
            HasReachedDestiny();
        }

        StartCoroutine(Wait5Secs());

        /*
        if (AvoidCollision.instance.hasCollided != true)
        {
            MoveRandomPosition();

            // if the character get to the coords, then wait 5 seconds
            if (Time.time == 5f)
            {
                // Repit the algorithm
                MoveRandomPosition();
            }
        }
        else
        {
            MoveRandomPosition();
        }
        */

    }

    private void HasReachedDestiny()
    {
        Debug.Log("Position X: " + gameObject.transform.position.x);
        Debug.Log("Position Y: " + gameObject.transform.position.y);
        if (gameObject.transform.position.x == destinyCoordX && gameObject.transform.position.y == destinyCoordY)
        {
            canMove = false;
            destinyCoordX = 0;
            destinyCoordY = 0;
        }
    }

    private void StepToDestinyCoord()
    {
        if (firstMovement == 1f)
        {
            MoveX();
            MoveY();
        }
        else
        {
            MoveY();
            MoveX();
        }

    }

    private void MoveX()
    {
        //Move X
        if (gameObject.transform.position.x < destinyCoordX)
        {
            gameObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
        }
        else if (gameObject.transform.position.x > destinyCoordX)
        {
            gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y, 0);
        }
    }

    private void MoveY()
    {
        // Move Y
        if (gameObject.transform.position.y < destinyCoordY)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
        }
        else if (gameObject.transform.position.y > destinyCoordY)
        {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
        }
    }

    public void GetRandomCoord()
    {
        randomX = Random.Range(-5, 5);
        randomY = Random.Range(-5, 5);
        firstMovement = Random.Range(1, 2);

        destinyCoordX = (int)gameObject.transform.position.x + randomX;
        Debug.Log("DestinyCoordX(" + destinyCoordX + ") = " + "PositionX(" + (int)gameObject.transform.position.x + ") + RandomX(" + randomX + ")");
        destinyCoordY = (int)gameObject.transform.position.y + randomY;
        Debug.Log("DestinyCoordY(" + destinyCoordY + ") = " + "PositionY(" + (int)gameObject.transform.position.y + ") + RandomY(" + randomY + ")");

        Debug.Log("Destino X: " + destinyCoordX);
        Debug.Log("Destino Y: " + destinyCoordY);
    }

    IEnumerator Wait5Secs()
    {
        Debug.Log("Time: " + Time.time);
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Time: " + Time.time);
    }

}
