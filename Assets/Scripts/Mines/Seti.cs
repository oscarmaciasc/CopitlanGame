using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seti : MonoBehaviour
{
    public static Seti instance;
    private Vector2 destiny;
    public string[] goodLines = { "Wow ha sido algo impresionante, estoy feliz" };
    public string[] badLines = { "No me gusto nada..." };
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] noFluteDialog = { "Tu flauta actual no puede interpretar esta partitura, intenta mejorando tu flauta" };
    private string[] setiNormalLines = {"Estamos acampando", "Escucha los sonidos de la naturaleza", "*se escucha una rana*"};

    public bool hasFinished = false;
    public bool finishedPartiture = false;
    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    [SerializeField] private GameObject partitureSelectionPanel;
    public bool canActivatePartiturePanel = true;
    public bool notFound = false;
    public bool notFoundFlutes = false;
    public int percentageToPass = 80;

    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        destiny = new Vector2(transform.position.x - 2, transform.position.y);

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.mineEntrance[2].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (!hasFinished)
        // {
        //     GetPercentage();
        // }
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
        if (canPass && finishedPartiture && Seti2.instance.canPass)
        {
            if (destiny.x != gameObject.transform.position.x)
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
                myAnim.SetFloat("moveX", -1);

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveY", -1);
            }
            else
            {
                // Finish the movement
                myAnim.SetFloat("moveX", 0);

                theEntrance.SetActive(true);

                // Save that this entrance has to be active in files 
                if (finishedPartiture && canPass)
                {
                    XmlManager.instance.SaveMineEntranceState(2, true);
                }
            }
        }
    }


    public void LimitPartitures()
    {
        if (this.gameObject.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture4", "PanelPartiture5", "PanelPartiture6", this.gameObject);
            Debug.Log("Filter Partitures");
        }

        if (notFound)
        {
            partitureSelectionPanel.SetActive(false);
            this.gameObject.GetComponent<DialogActivator>().lines = noPartituresDialog;
            canActivatePartiturePanel = false;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }

        if (notFoundFlutes)
        {
            partitureSelectionPanel.SetActive(false);
            this.gameObject.GetComponent<DialogActivator>().lines = noFluteDialog;
            canActivatePartiturePanel = false;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }

        
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }

    public bool NotFoundFlutes()
    {
        notFoundFlutes = true;
        return notFoundFlutes;
    }

    public void SetFound()
    {
        notFoundFlutes = false;
        notFound = false;
    }

    public void SetiNormalLines(GameObject habitant)
    {
        habitant.gameObject.GetComponent<DialogActivator>().lines = setiNormalLines;
    }
   
}