using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    public static BalloonManager instance;
    public string balloonName;
    public int fuelLimit;
    public float velocity;
    public int currentFuel;
    public bool canMove = true;
    public Vector2 initialPosition;
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        // We call the SetBalloonInfo here with the name of the ballon (from files) as a parameter
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        SetBalloonInfo(gameData.GetBalloonName());

        // Fill tank with file info
        currentFuel = gameData.GetCurrentResource(3);
    }

    // Update is called once per frame
    void Update()
    {
        fuelDecrease();
    }



    public void fuelDecrease()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.resource[3].quantity <= 0)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            if (this.gameObject.transform.position.x >= initialPosition.x + 10 || this.gameObject.transform.position.x <= initialPosition.x - 10 || this.gameObject.transform.position.y >= initialPosition.y + 10 || this.gameObject.transform.position.y <= initialPosition.y - 10)
            {
                Debug.Log("Entering IF");
                initialPosition = this.gameObject.transform.position;
                currentFuel--;
                XmlManager.instance.IncreaseResource(3, -1);
            }
        }
        else
        {
            gameData.SetFuelToZero();
            Debug.Log("Te has acabado el combustible, compra");
            InGame.instance.ActivateNoFuelPanel();
            InGame.instance.DeactivateBalloon();
            myAnim.SetFloat("moveX", 0);
            myAnim.SetFloat("moveY", 0);
        }
    }

    public void SetBalloonInfo(string balloonName)
    {
        this.balloonName = balloonName;

        if (balloonName == "balloonLvl1")
        {
            this.fuelLimit = 20;
            this.velocity = 5;
        }

        if (balloonName == "balloonLvl2")
        {
            this.fuelLimit = 30;
            this.velocity = 6f;
        }

        if (balloonName == "balloonLvl3")
        {
            this.fuelLimit = 40;
            this.velocity = 7f;
        }
    }

    
}
