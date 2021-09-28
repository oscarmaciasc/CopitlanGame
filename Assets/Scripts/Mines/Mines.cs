using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{

    public static Mines instance;
    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    private Vector2 destiny;
    public bool finishedPartiture = false;
    public int percentageToPass;
    public int correctNotes = 0;
    [SerializeField] private GameObject pentagramPanel;
    [SerializeField] private GameObject partitureSelectionPanel;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        destiny = new Vector2(transform.position.x + 2, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        GetPercentage();
        CheckIfCanPass();
    }

    public void GetPercentage()
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                Debug.Log("Enhorabuena, has pasado");
                this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();
                canPass = true;
            }
            else
            {
                Debug.Log("No me terminas de convencer, intentalo de nuevo");
                canPass = false;
                finishedPartiture = false;
            }
            Debug.Log("Correct Notes: " + PentagramManager.instance.correctNotes);
            Debug.Log("Total Notes: " + PentagramManager.instance.TotalNotes());
        }
    }

    public void CheckIfCanPass()
    {
        if (canPass && !pentagramPanel.activeInHierarchy)
        {
            if (destiny.x != gameObject.transform.position.x)
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
                myAnim.SetFloat("moveX", 1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveY", -1);
            }
            else
            {
                // Finish the movement
                myAnim.SetFloat("moveX", 0);

                theEntrance.SetActive(true);
            }
        }
        else
        {
            // Interpretate Partiture again
        }
    }

    public void LimitPartitures(GameObject habitant)
    {
        if(habitant.GetComponent<PartitureHabitant>().conversationFinished == true)
        {
            partitureSelectionPanel.SetActive(true);
        }
        
        if (partitureSelectionPanel.activeInHierarchy)
        {
             Debug.Log("Habitant name: " + habitant);

            if (habitant.name == "Tecalli0")
            {
                // Deactivate everything but easyPartiture 1
                Debug.Log("tecalli está loko");
                PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture1");
            }
            else if (habitant.name == "Acan0")
            {
                // Deactivate everything but easyPartiture 2
                PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture2");
                Debug.Log("Acan esta mamadisimo");
            }
            else if (habitant.name == "Seti0")
            {
                // Deactivate everything but mediumPartitures 4 & 5
                PartitureSelection.instance.DeactivateMinePartitures2("PanelPartiture4", "PanelPartiture5");
            }
        }
    }
}
