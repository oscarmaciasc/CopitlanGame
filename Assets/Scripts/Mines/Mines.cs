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
    public string[] goodLines;
    public string[] badLines;


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
                canPass = true;
                this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            }
            else
            {
                this.gameObject.GetComponent<DialogActivator>().lines = badLines;
                canPass = false;
                finishedPartiture = false;
            }
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

                // Save that this entrance has to be active in files 

                GameData gameData = new GameData();
                gameData = XmlManager.instance.LoadGame();

                if (this.gameObject.name == "Tecalli0")
                {
                    XmlManager.instance.SaveMineEntranceState(0, true);
                }
                else if (this.gameObject.name == "Acan0")
                {
                    XmlManager.instance.SaveMineEntranceState(0, true);
                }
                else if (this.gameObject.name == "Seti0")
                {
                    XmlManager.instance.SaveMineEntranceState(0, true);
                }

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
            if (habitant.name == "Tecalli0")
            {
                // Deactivate everything but easyPartiture 1
                PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture1");
            }
            else if (habitant.name == "Acan0")
            {
                // Deactivate everything but easyPartiture 2
                PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture2");
            }
            else if (habitant.name == "Seti0")
            {
                // Deactivate everything but mediumPartitures 4 & 5
                PartitureSelection.instance.DeactivateMinePartitures2("PanelPartiture4", "PanelPartiture5");
            }
        }
    }
}
