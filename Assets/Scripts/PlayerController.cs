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
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        // This is the real line of code we need
        //indexGame = XmlManager.instance.gameIndex;

        // Testing line
        indexGame = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
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
            if(canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        // Keeping the player inside the map
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    // public void SetBounds(Vector3 botLeft, Vector3 topRight)
    // {
    //     // This addition is made to avoid the sprite of the player from being choped up.
    //     bottomLeftLimit = botLeft + new Vector3(1f, 1f, 0f);
    //     topRightLimit = topRight + new Vector3(-1f, -1f, 0f);
    // }
}
