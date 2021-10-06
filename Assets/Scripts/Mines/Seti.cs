using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seti : MonoBehaviour
{

    public static Seti instance;
    private Vector2 destiny;
    public string[] goodLines = { "Estoy agradecida, me has alegrado el dia" };
    public string[] badLines = { "No me terminas de convencer, intentalo de nuevo" };
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] missingSister = { "Mi hermana se ve muy convencida, pero yo no he escuchado nada..." };

    public bool hasFinished = false;
    public bool finishedPartiture = false;
    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    [SerializeField] private GameObject pentagramPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    public bool canActivatePartiturePanel = true;
    public bool notFound = false;
    public int percentageToPass = 80;
    public bool bothFinished = false;
    public GameObject habitantOne;
    public GameObject habitantTwo;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (this.gameObject.name == "Seti0")
        {
            destiny = new Vector2(transform.position.x - 2, transform.position.y);
        }
        else if (this.gameObject.name == "Seti1")
        {
            destiny = new Vector2(transform.position.x + 2, transform.position.y);
        }

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.mineEntrance[2].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }

        habitantOne = GameObject.Find("Seti0");
        habitantTwo = GameObject.Find("Seti1");
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckIfCanPass();
    }

    public void GetPercentage()
    {
        if (finishedPartiture)
        {
            Debug.Log("correctNotes: " + PentagramManager.instance.correctNotes);
            Debug.Log("totalNotes: " + PentagramManager.instance.TotalNotes());
            Debug.Log("percentageToPass: " + percentageToPass);
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                canPass = true;
                this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            }
            else
            {
                Debug.Log("no debo");
                this.gameObject.GetComponent<DialogActivator>().lines = badLines;
                 canPass = false;
                finishedPartiture = false;
            }
        }
    }

    public void CheckIfCanPass()
    {
        if (canPass && finishedPartiture && bothFinished)
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
                if (finishedPartiture && canPass && bothFinished)
                {
                    XmlManager.instance.SaveMineEntranceState(2, true);
                }
            }
        }
    }

    public void LimitPartitures(GameObject habitant)
    {
        if (this.gameObject.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass && canActivatePartiturePanel)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            // Deactivate everything but easyPartiture 2
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

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }

    public void BothFinished(GameObject habitant)
    {
        if (finishedPartiture)
        {
            if (habitantOne.GetComponent<Seti>().canPass && habitantTwo.GetComponent<Seti>().canPass)
            {
                habitantOne.GetComponent<Seti>().bothFinished = true;
                habitantTwo.GetComponent<Seti>().bothFinished = true;
            }
            else
            {
                habitant.GetComponent<DialogActivator>().lines = missingSister;
                habitantOne.GetComponent<Seti>().bothFinished = false;
                habitantTwo.GetComponent<Seti>().bothFinished = false;
            }
        }
    }
}