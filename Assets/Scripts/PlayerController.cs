using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public Animator myAnim;
    public string areaTransitionName;
    public int indexGame = 0;
    public bool canMove = true;
    public float stopWalked = 0f;
    public float startWalked = 0f;

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

    public void SaveTimeWalked()
    {
        stopWalked = Time.time;
        Debug.Log("stopWalked: " + stopWalked);
        XmlManager.instance.UpdateTimeWalked(stopWalked - startWalked);
        startWalked = 0;
        stopWalked = 0;
    }
}
