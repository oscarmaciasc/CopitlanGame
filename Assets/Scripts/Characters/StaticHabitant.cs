using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHabitant : MonoBehaviour
{

    public static StaticHabitant instance;
    public bool canInterpretatePartiture = true;
    public bool conversatinFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogActivator.instance.canActivate && conversatinFinished)
        {
            Debug.Log("Sigue la Partitura");
        }
    }
}
