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
    public GameObject habitant;

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

        Debug.Log("Entrando a Seti");
    }

    // Update is called once per frame
    void Update()
    {
        if (habitant != null)
        {
            if (!hasFinished)
            {
                GetPercentage();
            }
            CheckIfCanPass();


        // Debug.Log("notFound se setea a: " + notFound);
    }

    public void GetPercentage()
    {
        //  Debug.Log("Get Percentage notFound se setea a: " + notFound);
        if (finishedPartiture)
        {
            Debug.Log("correctNotes: " + PentagramManager.instance.correctNotes);
            Debug.Log("totalNotes: " + PentagramManager.instance.TotalNotes());
            Debug.Log("percentageToPass: " + percentageToPass);
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                habitant.GetComponent<Seti>().canPass = true;
                habitant.GetComponent<DialogActivator>().lines = goodLines;
            }
            else
            {
                Debug.Log("no debo");
                habitant.GetComponent<DialogActivator>().lines = badLines;
                habitant.GetComponent<Seti>().finishedPartiture = false;
                habitant.GetComponent<Seti>().canPass = false;


                // if (habitant.GetComponent<Seti>().canPass)
                // {
                //     habitant.GetComponent<Seti>().finishedPartiture = true;
                // }
            }


        }
    }

    public void CheckIfCanPass()
    {
        //  Debug.Log("CheckIfCanPass notFound se setea a: " + notFound);
        if (canPass && finishedPartiture && bothFinished)
        {
            if (destiny.x != gameObject.transform.position.x)
            {
                gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
                if (habitant.name == "Seti0")
                {
                    myAnim.SetFloat("moveX", -1);
                }
                else if (habitant.name == "Seti1")
                {
                    myAnim.SetFloat("moveX", 1);
                }

                // Making the player Idle in the last direction
                myAnim.SetFloat("lastMoveY", -1);
            }
            else
            {
                // Finish the movement
                myAnim.SetFloat("moveX", 0);

                theEntrance.SetActive(true);

                // Save that this entrance has to be active in files 
                if (habitant.GetComponent<Seti>().finishedPartiture && habitant.GetComponent<Seti>().canPass && habitantOne.GetComponent<Seti>().bothFinished && habitantTwo.GetComponent<Seti>().bothFinished)
                {
                    XmlManager.instance.SaveMineEntranceState(2, true);
                }
            }
        }
    }

    public void LimitPartitures()
    {
        if (this.gameObject.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass && canActivatePartiturePanel)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            // Deactivate everything but easyPartiture 2
            Debug.Log("AAAAAAAAAAANTES");
            PartitureSelection.instance.DeactivateMinePartitures2("PanelPartiture4", "PanelPartiture5");
            Debug.Log("SI INTENTO HACERLO22222");
        }

        Debug.Log("notFound" + notFound);
        if (notFound)
        {
            Debug.Log("Mamarrachada");
            partitureSelectionPanel.SetActive(false);
            this.gameObject.GetComponent<DialogActivator>().lines = noPartituresDialog;
            canActivatePartiturePanel = false;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
    }

    public bool NotFoundPartitures()
    {
        Debug.Log("SETEO");
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
                if (habitantOne.GetComponent<Seti>().canPass)
                {
                    habitantTwo.GetComponent<DialogActivator>().lines = missingSister;
                }
                if (habitantTwo.GetComponent<Seti>().canPass)
                {
                    habitantOne.GetComponent<DialogActivator>().lines = missingSister;
                }
                habitantOne.GetComponent<Seti>().bothFinished = false;
                habitantTwo.GetComponent<Seti>().bothFinished = false;
            }
        }
    }

    public void GetHabitant(GameObject getHabitant)
    {
        // habitant is the npc im talking to
        habitant = getHabitant;
    }
}