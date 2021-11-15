using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinalCamera : MonoBehaviour
{
    public static FinalCamera instance;
    public bool animationEnd = false;

    void Start()
    {
        instance = this;
    }

    public void setAnimationEnd()
    {
        animationEnd = true;
    }
}
