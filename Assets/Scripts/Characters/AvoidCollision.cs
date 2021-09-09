using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{

    public static AvoidCollision instance;
    public Vector3 rightPosition;
    public Vector3 leftPosition;
    public Vector3 topPosition;
    public Vector3 bottomPosition;
    public bool hasCollided;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        rightPosition = new Vector3(0.65f, 0.30f, 0f);
        leftPosition = new Vector3(-0.65f, 0.30f, 0f);
        topPosition = new Vector3(-0.03f, 1.6f, 0f);
        bottomPosition = new Vector3(-0.01f, 0.02f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Change the collider position depending on the animator movement... (maybe)
        if(transform.parent.GetComponent<Animator>().GetFloat("moveY") == 1)
        {
            Debug.Log("Collider Top");
            this.transform.localPosition += topPosition;
        } else if(transform.parent.GetComponent<Animator>().GetFloat("moveY") == -1)
        {
            Debug.Log("Collider Bottom");
            this.transform.localPosition += bottomPosition;
        }
        
        if(transform.parent.GetComponent<Animator>().GetFloat("moveX") == 1)
        {
            Debug.Log("Collider Right");
            this.transform.localPosition += rightPosition;
        } else if(transform.parent.GetComponent<Animator>().GetFloat("moveX") == -1)
        {
            Debug.Log("Collider Left");
            this.transform.localPosition += leftPosition;
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
