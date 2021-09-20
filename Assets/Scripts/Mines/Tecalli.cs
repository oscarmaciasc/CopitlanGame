using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tecalli : MonoBehaviour
{

    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    private Vector2 destiny;

    // Start is called before the first frame update
    void Start()
    {
        destiny = new Vector2(transform.position.x + 3, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // if partiture is finished activate gameobject areaEntrance and walk away

        if (canPass)
        {
            if (destiny.x != gameObject.transform.position.x)
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
                myAnim.SetFloat("moveX", 1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveX", 1);
            }
            else
            {
                // Finish the movement
                myAnim.SetFloat("moveX", 0);
                
                Destroy(this.gameObject);
                theEntrance.SetActive(true);
            }
        }
    }
}
