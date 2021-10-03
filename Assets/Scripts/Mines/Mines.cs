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
    public bool hasFinished = false;
    public bool notFound = false;
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    public bool canActivatePartiturePanel = true;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        destiny = new Vector2(transform.position.x + 2, transform.position.y);

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.mineEntrance[0].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
        else if (gameData.mineEntrance[1].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
        else if (gameData.mineEntrance[2].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
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
                Move();
            }
            else
            {
                // Finish the movement
                myAnim.SetFloat("moveX", 0);

                theEntrance.SetActive(true);

                // Save that this entrance has to be active in files 

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
        if (habitant.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass  && canActivatePartiturePanel)
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

            if (notFound)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noPartituresDialog;
                canActivatePartiturePanel = false;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }

    public void Move()
    {
        gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
        myAnim.SetFloat("moveX", 1);

        // Making the player Idle in the last direction
        myAnim.SetFloat("lastMoveY", -1);
    }
}
