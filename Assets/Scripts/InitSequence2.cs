using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class InitSequence2 : MonoBehaviour
{
    public static InitSequence2 instance;
    [SerializeField] private GameObject tutorialInterface;
    [SerializeField] private GameObject pressVPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject backArrow;
    [SerializeField] private GameObject pentagramPanel;
    public string[] childReaction;
    public bool hasBeenActivated;
    public bool justStarted = false;
    public bool secondMessage = false;
    public float waitToLoad = 1f;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hasBeenActivated = false;
        justStarted = true;
        PlayerController.instance.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogManagerTutorial.instance.dialogBox.activeInHierarchy)
        {
            PlayerController.instance.canMove = false;
        }

        if (DialogManagerTutorial.instance.currentLine == 3)
        {
            DialogManagerTutorial.instance.dialogBox.SetActive(false);
            StartCoroutine(StartTutorial());
            ActivatePartiturePanel();
            if (pressVPanel.activeInHierarchy || partitureSelectionPanel.activeInHierarchy || PartitureSelectionTutorial.instance.pentagramPanel.activeInHierarchy || tutorialInterface.activeInHierarchy || DialogManagerTutorial.instance.dialogBox.activeInHierarchy)
            {
                PlayerController.instance.canMove = false;
                
                DialogActivatorTutorial.instance.canActivate = false;
            }
        }

        // This is made to avoid the second enter on the message dialog when the player finishes the partiture
        // if (DialogManagerTutorial.instance.dialogLines[0] == "Que linda cancion")
        // {
        //     DialogManagerTutorial.instance.justStarted = false;

        //     // Flag to tell we are in the second message
        //     secondMessage = true;
        //     PlayerController.instance.canMove = false;
        // }

        if (DialogManagerTutorial.instance.conversationIsFinished)
        {
            Invoke("ChangeScene", 2f);
        }
    }

    //This function is IEnumerator because we need a delay before we display the tutorial
    IEnumerator StartTutorial()
    {
        if (justStarted)
        {
            PlayerController.instance.canMove = false;
        }
        justStarted = false;
        // Delay for initSequence2 message
        yield return new WaitForSeconds(1f);
        if (!hasBeenActivated)
        {
            pressVPanel.SetActive(true);
        }
        hasBeenActivated = true;
    }

    private void ActivatePartiturePanel()
    {
        if (GameManager.instance.vPressed && hasBeenActivated)
        {
            pressVPanel.SetActive(false);
            partitureSelectionPanel.SetActive(true);
            DeactivateNoOwnedPartiturePanels();
            backArrow.SetActive(false);
        }
    }

    private void DeactivateNoOwnedPartiturePanels()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < 10; i++)
        {
            if (gameData.DoesHavePartiture("partiture" + (i + 1)))
            {
                PartitureSelectionTutorial.instance.ActivatePanel(i);
            }
            else
            {
                PartitureSelectionTutorial.instance.DeactivatePanel(i);
            }
        }
    }

    public void HasFinishedPartiture()
    {
        StartCoroutine(DeactivatePentagramPanel());
        GetPercentage();
        ChangeHabitantDialogLines();
        StartCoroutine(ShowChildDialogs());
    }

    IEnumerator DeactivatePentagramPanel()
    {
        yield return new WaitForSeconds(1);
        pentagramPanel.SetActive(false);
    }

    IEnumerator ShowChildDialogs()
    {
        DialogManagerTutorial.instance.conversationIsFinished = false;
        yield return new WaitForSeconds(2);
        DialogManagerTutorial.instance.ShowDialog(childReaction);
    }

    public void ChangeScene()
    {
        // Make a delay before loading in the new scene

        UIFade.instance.FadeToBlack();
        waitToLoad -= Time.deltaTime;
        if (waitToLoad <= 0)
        {
            SceneLoader.LoadScene("SE-Papataca");
            PlayerController.instance.areaTransitionName = "Tutorial-PapatacaSE";
        }
    }

    public void GetPercentage()
    {
        score = (PentagramManagerTutorial.instance.correctNotes * 100) / (19);
    }

    public void ChangeHabitantDialogLines()
    {
        if (score >= 0 && score <= 30)
        {
            Debug.Log("score: " + score);
            childReaction = new string[] {"A decir verdad sonó horrible", "Pero supongo que mejoraras con el tiempo"};
        } else if (score >= 40 && score <= 70)
        {
            Debug.Log("score: " + score);
            childReaction = new string[] {"Wow, ha estado bastante bien", "Nunca antes habia escuchado algo asi"};
        } else if (score >= 80 && score <= 100)
        {
            childReaction = new string[] {"Wooooooow es lo mas bello que he escuchado nunca", "me siento conmovida y profundamente feliz", "Te agradezco, suerte en tu viaje"};
        }

        secondMessage = true;
        DialogManagerTutorial.instance.justStarted = false;
        PlayerController.instance.canMove = false;
        Debug.Log(childReaction[0]);
    }
}
