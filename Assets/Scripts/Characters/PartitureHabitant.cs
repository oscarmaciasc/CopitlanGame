using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitureHabitant : MonoBehaviour
{

    // Partiture Habitants

    public static PartitureHabitant instance;
    public bool canInterpretatePartiture = true;
    public bool conversationFinished = false;
    public bool canActivate;
    public bool partitureFinished = false;
    public bool firstTime = true;

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

        conversationFinished = DialogManager.instance.conversationIsFinished;

        PentagramManager.streak = 0;

        if (this.canActivate && this.conversationFinished && firstTime)
        {
            InGame.instance.ActivatePartitureSelectionPanel();
            firstTime = false;
        }
        if (partitureFinished)
        {
            canActivate = this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();
        }

    }

    public void HasFinishedPartiture()
    {
        partitureFinished = true;
    }
}
