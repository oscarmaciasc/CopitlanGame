using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{

    public static AvoidCollision instance;
    public bool hasCollided;
    public float habitantHeight;

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
            this.transform.position = new Vector2(this.transform.parent.position.x, this.transform.parent.position.y + habitantHeight);
        } else if(transform.parent.GetComponent<Animator>().GetFloat("moveY") == -1)
        {
            this.transform.position = new Vector2(this.transform.parent.position.x, this.transform.parent.position.y);
        }
        
        if(transform.parent.GetComponent<Animator>().GetFloat("moveX") == 1)
        {
            this.transform.position = new Vector2(this.transform.parent.position.x + 0.65f, this.transform.parent.position.y + 0.4f);
        } else if(transform.parent.GetComponent<Animator>().GetFloat("moveX") == -1)
        {
            this.transform.position = new Vector2(this.transform.parent.position.x - 0.65f, this.transform.parent.position.y + 0.4f);
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
