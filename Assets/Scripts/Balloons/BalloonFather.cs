using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFather : MonoBehaviour
{
    public static BalloonFather instance;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
