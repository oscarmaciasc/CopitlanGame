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
    public bool canShowPartitures = true;
    public string[] newLines;
    public GameObject habitant;

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

        // If its not an audience streak = 0
        if(this.gameObject.GetComponent<Audience>() == null)
        {
            PentagramManager.streak = 0;
        }

        if (this.canActivate && this.conversationFinished && firstTime && canShowPartitures)
        {
            InGame.instance.ActivatePartitureSelectionPanel();
            firstTime = false;
        }
        
        if (partitureFinished && this.gameObject.GetComponent<Mines>() == null)
        {  
            // canActivate = this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();
            canShowPartitures = false;
            // change lines
            this.gameObject.GetComponent<DialogActivator>().lines = newLines;
        }

    }

    public void HasFinishedPartiture()
    {
        partitureFinished = true;
    }

    public bool HasPartituresFilter()
    {
        Debug.Log("Nombre del Objeto" + this.gameObject.name);
        if(habitant.GetComponent<Mines>() != null || habitant.GetComponent<Audience>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetHabitant(GameObject getHabitant)
    {
        // habitant is the npc im talking to
        habitant = getHabitant;
        Debug.Log("Nombre de habitante: " + getHabitant.name);
    }
}
