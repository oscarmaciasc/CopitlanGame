using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public static BalloonManager instance;
    public string balloonName;
    public int fuelLimit;
    public float velocity;
    public int[] cost;
    public int currentFuel;
    public bool canMove = true;
    public Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        // We call the SetBalloonInfo here with the name of the ballon (from files) as a parameter
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        SetBalloonInfo(gameData.GetBalloonName());

        initialPosition = this.gameObject.transform.position;

        // Fill tank with file info
        currentFuel = gameData.GetCurrentResource(3);

        if (currentFuel != 0)
        {
            canMove = true;
        }

        //currentFuel = this.fuelLimit;
    }

    // Update is called once per frame
    void Update()
    {
        fuelDecrease();
    }

    public void fuelDecrease()
    {
        if (canMove)
        {
            if (this.gameObject.transform.position.x >= initialPosition.x + 10 || this.gameObject.transform.position.x <= initialPosition.x - 10 || this.gameObject.transform.position.y >= initialPosition.y + 10 || this.gameObject.transform.position.y <= initialPosition.y - 10)
            {
                Debug.Log("Entering IF");
                initialPosition = this.gameObject.transform.position;
                currentFuel--;
                XmlManager.instance.IncreaseResource(3, -1);

                if (currentFuel <= 0)
                {
                    canMove = false;
                }
            }
        } else 
        {
            Debug.Log("Te has acabado el combustible, compra puerco");
        }
    }

    public void SetBalloonInfo(string balloonName)
    {
        this.balloonName = balloonName;

        if (balloonName == "balloonLvl1")
        {
            this.fuelLimit = 20;
            this.velocity = 3;
        }

        if (balloonName == "balloonLvl2")
        {
            this.fuelLimit = 30;
            this.velocity = 3.5f;
            this.cost[0] = 500;
            this.cost[1] = 50;
            this.cost[2] = 5;
        }

        if (balloonName == "balloonLvl3")
        {
            this.fuelLimit = 40;
            this.velocity = 4;
            this.cost[0] = 1000;
            this.cost[1] = 100;
            this.cost[2] = 10;
        }
    }
}
