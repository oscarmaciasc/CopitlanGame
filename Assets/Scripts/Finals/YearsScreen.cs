using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearsScreen : MonoBehaviour
{
    public static YearsScreen instance;
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
