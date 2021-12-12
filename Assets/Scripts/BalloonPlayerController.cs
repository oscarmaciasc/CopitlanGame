using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonPlayerController : MonoBehaviour
{
    public static BalloonPlayerController instance;
    public Rigidbody2D theBalloonRB;
    public float moveSpeedBalloon;
    public Animator balloonAnim;
    public bool canMove = true;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public float stopWalkedBalloon = 0f;
    public float startWalkedBallon = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // This means that there can be only one balloon in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // if theres another balloonController with the instance set, destroy myself
            // but if the instance has been set but its me, then dont destroy me
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        Debug.Log("Starting Balloon");
    }

    void OnEnable()
    {
        this.transform.position = FindObjectOfType<PlayerController>(true).transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpeedBalloon == 0)
        {
            moveSpeedBalloon = this.gameObject.GetComponent<BalloonManager>().velocity;
        }

        if (SceneManager.GetActiveScene().name != "InitSequence1" && SceneManager.GetActiveScene().name != "InitSequence2")
        {

            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            if (canMove && gameData.GetCurrentResource(3) > 0)
            {
                if (canMove)
                {
                    theBalloonRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeedBalloon;
                }
                else
                {
                    theBalloonRB.velocity = Vector2.zero;
                }

                balloonAnim.SetFloat("moveX", theBalloonRB.velocity.x);
                balloonAnim.SetFloat("moveY", theBalloonRB.velocity.y);

                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    if (canMove)
                    {
                        if (startWalkedBallon == 0)
                        {
                            SetFirstWalkedBalloon();
                        }

                        balloonAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                        balloonAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                    }
                }

                if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && startWalkedBallon != 0)
                {
                    CheckIfIsMovingBalloon();
                }

                if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && startWalkedBallon != 0)
                {
                    CheckIfIsMovingBalloon();
                }

                if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) && startWalkedBallon != 0)
                {
                    CheckIfIsMovingBalloon();
                }

                if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) && startWalkedBallon != 0)
                {
                    CheckIfIsMovingBalloon();
                }
            }
            else
            {
                theBalloonRB.velocity = Vector2.zero;
                balloonAnim.SetFloat("moveX", 0);
                balloonAnim.SetFloat("moveY", 0);
            }
        }

        // if(SceneManager.GetActiveScene().name == "MainMenu")
        // {
        //     Destroy(gameObject);
        // }
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        // This addition is made to avoid the sprite of the player from being choped up.
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }

    public void CheckIfIsMovingBalloon()
    {
        if (theBalloonRB.velocity == Vector2.zero)
        {
            SaveTimeWalkedBalloon();
        }
    }
    public void SetFirstWalkedBalloon()
    {
        startWalkedBallon = Time.time;
        Debug.Log("[Balloon] startWalked: " + startWalkedBallon);
    }

    public void SaveTimeWalkedBalloon()
    {
        if (!InGame.instance.noFuelPanel.activeInHierarchy)
        {
            stopWalkedBalloon = Time.time;
            Debug.Log("[Balloon] stopWalked: " + stopWalkedBalloon);
            XmlManager.instance.UpdateTimeWalkedBalloon(stopWalkedBalloon - startWalkedBallon);
            Debug.Log("[Balloon] Resta stopWalked - startWalked: " + (stopWalkedBalloon - startWalkedBallon));
            Debug.Log("[Balloon] TimeBalloon: " + XmlManager.instance.LoadGame().timeBalloon);
            startWalkedBallon = 0;
            stopWalkedBalloon = 0;
        }
    }
}
