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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "MovingHabitant")
        {
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "MovingHabitant")
        {
            hasCollided = false;
        }
    }
}
