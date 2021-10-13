using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acan : MonoBehaviour
{

    public static Acan instance;
    private Vector2 destiny;
    public string[] goodLines = { "Estoy agradecida, me has alegrado el dia" };
    public string[] badLines = { "No me terminas de convencer, intentalo de nuevo" };
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };

    public bool hasFinished = false;
    public bool finishedPartiture = false;
    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    [SerializeField] private GameObject partitureSelectionPanel;
    public bool canActivatePartiturePanel = true;
    public bool notFound = false;
    public int percentageToPass = 65;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        destiny = new Vector2(transform.position.x + 2, transform.position.y);

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.mineEntrance[1].shouldBeActive)
        {
            hasFinished = true;
            this.gameObject.GetComponent<DialogActivator>().lines = goodLines;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinished)
        {
            GetPercentage();
        }
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
        if (canPass && finishedPartiture)
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
                if (finishedPartiture && canPass)
                {
                    XmlManager.instance.SaveMineEntranceState(1, true);
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
            PartitureSelection.instance.DeactivateMinePartitures("PanelPartiture2", this.gameObject);
            Debug.Log("Filter Partitures");
        }

        if (notFound)
        {
            partitureSelectionPanel.SetActive(false);
            this.gameObject.GetComponent<DialogActivator>().lines = noPartituresDialog;
            canActivatePartiturePanel = false;
            this.gameObject.GetComponent<PartitureHabitant>().canShowPartitures = false;
        }
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }
}
