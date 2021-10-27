using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D theRB;
    public Rigidbody2D theBalloonRB;
    public float moveSpeed;
    public float moveSpeedBalloon;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public Animator myAnim;
    public Animator balloonAnim;
    public string areaTransitionName;
    public int indexGame = 0;
    public bool canMove = true;
    public float stopWalked = 0f;
    public float startWalked = 0f;
    public float stopWalkedBalloon = 0f;
    public float startWalkedBallon = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // This means that there can be only one player in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // if theres another payerController with the instance set, destroy myself
            // but if the instance has been set but its me, then dont destroy me
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        // This is the real line of code we need
        //indexGame = XmlManager.instance.gameIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "InitSequence1" && SceneManager.GetActiveScene().name != "InitSequence2")
        {
            if (!InGame.instance.balloonActive)
            {
                if (canMove)
                {
                    theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
                }
                else
                {
                    theRB.velocity = Vector2.zero;
                }

                myAnim.SetFloat("moveX", theRB.velocity.x);
                myAnim.SetFloat("moveY", theRB.velocity.y);

                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    if (canMove)
                    {
                        if (startWalked == 0)
                        {
                            SetFirstWalked();
                        }

                        myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                        myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                    }
                }

                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    CheckIfIsMoving();
                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    CheckIfIsMoving();
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    CheckIfIsMoving();
                }

                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    CheckIfIsMoving();
                }
            }
            else if (InGame.instance.balloonActive)
            {
                GameData gameData = new GameData();
                gameData = XmlManager.instance.LoadGame();

                if (canMove && gameData.GetCurrentResource(3) > 0)
                {
                    theBalloonRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeedBalloon;
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

                    if (Input.GetKeyUp(KeyCode.UpArrow))
                    {
                        CheckIfIsMovingBalloon();
                    }

                    if (Input.GetKeyUp(KeyCode.DownArrow))
                    {
                        CheckIfIsMovingBalloon();
                    }

                    if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        CheckIfIsMovingBalloon();
                    }

                    if (Input.GetKeyUp(KeyCode.RightArrow))
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

        }
        else if (SceneManager.GetActiveScene().name == "InitSequence1" || SceneManager.GetActiveScene().name == "InitSequence2")
        {
            if (canMove)
            {
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
            }
            else
            {
                theRB.velocity = Vector2.zero;
            }

            myAnim.SetFloat("moveX", theRB.velocity.x);
            myAnim.SetFloat("moveY", theRB.velocity.y);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                if (canMove)
                {
                    myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                }
            }
        }


        // Keeping the player inside the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void CheckIfIsMoving()
    {
        if (theRB.velocity == Vector2.zero)
        {
            SaveTimeWalked();
        }
    }

    public void CheckIfIsMovingBalloon()
    {
        if (theBalloonRB.velocity == Vector2.zero)
        {
            SaveTimeWalkedBalloon();
        }
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        // This addition is made to avoid the sprite of the player from being choped up.
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }

    public void SetFirstWalked()
    {
        startWalked = Time.time;
    }

    public void SetFirstWalkedBalloon()
    {
        startWalkedBallon = Time.time;
    }

    public void SaveTimeWalked()
    {
        if (!InGame.instance.noFuelPanel.activeInHierarchy)
        {
            stopWalked = Time.time;
            XmlManager.instance.UpdateTimeWalked(stopWalked - startWalked);
            startWalked = 0;
            stopWalked = 0;
        }
    }

    public void SaveTimeWalkedBalloon()
    {
        if (!InGame.instance.noFuelPanel.activeInHierarchy)
        {
            stopWalkedBalloon = Time.time;
            XmlManager.instance.UpdateTimeWalkedBalloon(stopWalkedBalloon - startWalkedBallon);
            startWalkedBallon = 0;
            stopWalkedBalloon = 0;
        }
    }
}
