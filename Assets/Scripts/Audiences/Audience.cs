using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public static Audience instance;
    public bool finishedPartiture = false;
    public int percentageToPass;
    public int res;
    public bool canPass = false;
    [SerializeField] private GameObject partitureSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetPercentage();
    }

    public void GetPercentage()
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                canPass = true;
                this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();

                res = (60) + (((PentagramManager.maxStreak) * (40)) / (PentagramManager.instance.TotalNotes()));

                // Sent this to stats and do the following math in the game
            }
            else
            {
                canPass = false;
                finishedPartiture = false;
            }
        }
    }

    public void LimitPartitures(GameObject habitant)
    {
        if (habitant.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            if (habitant.name == "Kasakir")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture1", "PanelPartiture2", "PanelPartiture3");
            }
            else if (habitant.name == "Quizani")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture4", "PanelPartiture5", "PanelPartiture6");
            }
            else if (habitant.name == "Naran")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture7", "PanelPartiture8", "PanelPartiture9");
            }

            
        }
    }
}
