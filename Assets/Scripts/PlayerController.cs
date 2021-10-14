using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        Debug.Log("startWalked: " + startWalked);
                    }

                    myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                    myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                }
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Debug.Log("ReleaseUpArrow");
                CheckIfIsMoving();
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Debug.Log("ReleaseDownArrow");
                CheckIfIsMoving();
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Debug.Log("ReleaseLeftArrow");
                CheckIfIsMoving();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Debug.Log("ReleaseRightArrow");
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
                            Debug.Log("startWalkedBalloon: " + startWalkedBallon);
                        }

                        balloonAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                        balloonAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                    }
                }

                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    Debug.Log("ReleaseUpArrow");
                    CheckIfIsMovingBalloon();
                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    Debug.Log("ReleaseDownArrow");
                    CheckIfIsMovingBalloon();
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    Debug.Log("ReleaseLeftArrow");
                    CheckIfIsMovingBalloon();
                }

                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    Debug.Log("ReleaseRightArrow");
                    CheckIfIsMovingBalloon();
                }
            }
            else
            {
                theBalloonRB.velocity = Vector2.zero;
                balloonAnim.SetFloat("moveX", 0);
                balloonAnim.SetFloat("moveY", 0);
                Debug.Log("No puedes moverte hasta que compres combustible");
            }

        }


        // Keeping the player inside the map
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void CheckIfIsMoving()
    {
        if (theRB.velocity == Vector2.zero)
        {
            Debug.Log("Not Moving");
            SaveTimeWalked();
        }
        else
        {
            Debug.Log("Moving");
        }
    }

    public void CheckIfIsMovingBalloon()
    {
        if (theBalloonRB.velocity == Vector2.zero)
        {
            Debug.Log("Not Moving Balloon");
            SaveTimeWalkedBalloon();
        }
        else
        {
            Debug.Log("Moving Balloon");
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
        stopWalked = Time.time;
        Debug.Log("stopWalked: " + stopWalked);
        XmlManager.instance.UpdateTimeWalked(stopWalked - startWalked);
        startWalked = 0;
        stopWalked = 0;
    }

    public void SaveTimeWalkedBalloon()
    {
        stopWalkedBalloon = Time.time;
        Debug.Log("stopWalkedBalloon: " + stopWalkedBalloon);
        XmlManager.instance.UpdateTimeWalkedBalloon(stopWalkedBalloon - startWalkedBallon);
        startWalkedBallon = 0;
        stopWalkedBalloon = 0;
    }
}
