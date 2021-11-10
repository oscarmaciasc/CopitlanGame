using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseID : MonoBehaviour
{
    public static HouseID instance;
    public string houseID;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // if (other.tag == "Player")
    //     // {
    //     //     // This means that there can be only one player in the scene
    //     //     if (instance == null)
    //     //     {
    //     //         instance = this;
    //     //     }
    //     //     else
    //     //     {
    //     //         // if theres another payerController with the instance set, destroy myself
    //     //         // but if the instance has been set but its me, then dont destroy me
    //     //         if (instance != this)
    //     //         {
    //     //             Destroy(gameObject);
    //     //         }
    //     //     }

    //     //     DontDestroyOnLoad(gameObject);
    //     // }
    // }
}
