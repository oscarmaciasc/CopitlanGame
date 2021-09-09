using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHabitant : MonoBehaviour
{

    public static StaticHabitant instance;
    public bool canInterpretatePartiture = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
