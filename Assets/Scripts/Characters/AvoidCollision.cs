using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{

    public static AvoidCollision instance;
    public bool hasCollided;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Change the collider position depending on the animator movement... (maybe)
        if(transform.parent.GetComponent<Animator>().GetFloat("moveY") == 1)
        {
            //this.transform.position.y = 1.50f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        hasCollided = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasCollided = false;
    }
}
