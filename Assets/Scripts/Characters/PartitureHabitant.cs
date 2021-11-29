using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitureHabitant : MonoBehaviour
{

    // Partiture Habitants

    public static PartitureHabitant instance;
    public bool conversationFinished = false;
    public bool canActivate;
    public bool partitureFinished = false;
    public bool firstTime = true;
    public bool canShowPartitures = true;
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


        if (this.canActivate && this.conversationFinished && firstTime && canShowPartitures)
        {
            InGame.instance.ActivatePartitureSelectionPanel();
            firstTime = false;
        }

        if (partitureFinished && this.gameObject.GetComponent<Tecalli>() == null && this.gameObject.GetComponent<Acan>() == null && this.gameObject.GetComponent<Seti>() == null && this.gameObject.GetComponent<Seti2>() == null && this.gameObject.GetComponent<Audience>() == null && this.gameObject.GetComponent<Leader>() == null)
        {
            canShowPartitures = false;
            // change lines
            //this.gameObject.GetComponent<DialogActivator>().lines = newLines;
        }
        else if (partitureFinished && ((this.gameObject.GetComponent<Tecalli>() != null && this.gameObject.GetComponent<Tecalli>().canPass) || (this.gameObject.GetComponent<Acan>() != null && this.gameObject.GetComponent<Acan>().canPass) || (this.gameObject.GetComponent<Seti>() != null && this.gameObject.GetComponent<Seti>().canPass) || (this.gameObject.GetComponent<Seti2>() != null && this.gameObject.GetComponent<Seti2>().canPass) || (this.gameObject.GetComponent<Audience>() != null && this.gameObject.GetComponent<Audience>().canPass) || (this.gameObject.GetComponent<Leader>() != null && this.gameObject.GetComponent<Leader>().canPass)))
        {
            canShowPartitures = false;
            GameManager.instance.vPressed = false;
        }

    }

    public bool HasPartituresFilter()
    {
        if (habitant.GetComponent<Tecalli>() != null || habitant.GetComponent<Acan>() != null || habitant.GetComponent<Seti>() != null || habitant.GetComponent<Seti2>() != null || habitant.GetComponent<Audience>() != null || habitant.GetComponent<Leader>() != null)
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
    }
}
