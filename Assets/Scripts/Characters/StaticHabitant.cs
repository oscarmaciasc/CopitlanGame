using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHabitant : MonoBehaviour
{

    // Partiture Habitants

    public static StaticHabitant instance;
    public bool canInterpretatePartiture = true;
    public bool conversationFinished = false;
    public bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // We recive the canActive variable from DialogActivator from the especific gameObject were talking to.
        canActivate = this.gameObject.GetComponent<DialogActivator>().CanActive();

        if(canActivate && conversationFinished)
        {
            DialogActivator.instance.canActivate = false;
            InGame.instance.ActivatePartitureSelectionPanel();
        }
    }
}
