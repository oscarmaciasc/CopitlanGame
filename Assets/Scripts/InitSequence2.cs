using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class InitSequence2 : MonoBehaviour
{
    public static InitSequence2 instance;
    [SerializeField] private GameObject tutorialInterface;
    [SerializeField] private GameObject startPlayingPanel;
    [SerializeField] private GameObject pressVPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject backArrow;
    [SerializeField] private GameObject pentagramPanel;
    [SerializeField] private GameObject dialogBox;
    public string[] childReaction;
    public bool hasBeenActivated;
    public bool hasfinishedDialogs = false;
    public bool justStarted = false;
    public bool secondMessage = false;
    public bool shouldCountDown = false;
    public float waitToLoad = 1f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hasBeenActivated = false;
        justStarted = true;
        PlayerController.instance.canMove = true;

        Debug.Log("STARTING TUTORIAL");
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogManager.instance.dialogBox.activeInHierarchy)
        {
            PlayerController.instance.canMove = false;
        }

        if (DialogManager.instance.currentLine == 3)
        {
            DialogManager.instance.dialogBox.SetActive(false);
            StartCoroutine(StartTutorial());
            ActivatePartiturePanel();
            if (pressVPanel.activeInHierarchy || partitureSelectionPanel.activeInHierarchy || PartitureSelectionTutorial.instance.pentagramPanel.activeInHierarchy || tutorialInterface.activeInHierarchy || DialogManager.instance.dialogBox.activeInHierarchy)
            {
                PlayerController.instance.canMove = false;
                
                DialogActivator.instance.canActivate = false;
            }
        }

        // This is made to avoid the second enter on the message dialog when the player finishes the partiture
        if (DialogManager.instance.dialogLines[0] == "Que linda cancion")
        {
            DialogManager.instance.justStarted = false;

            // Flag to tell we are in the second message
            secondMessage = true;
            PlayerController.instance.canMove = false;
        }

        if (secondMessage && DialogManager.instance.currentLine >= 1)
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
        Debug.Log("Deactivating no owned partiture panels");
        // ******************************FAKE*****************************
        int index = 1;
        // ******************************FAKE*****************************
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame(index);

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

        // Change npc0 dialogs

        // IF %notes is high {} fill a new array with the dialogs


        // if the conversation is finished passed to the other scene

        StartCoroutine(ShowChildDialogs());

    }

    IEnumerator DeactivatePentagramPanel()
    {
        yield return new WaitForSeconds(1);
        pentagramPanel.SetActive(false);
    }

    IEnumerator ShowChildDialogs()
    {
        DialogManager.instance.conversationIsFinished = false;
        yield return new WaitForSeconds(2);
        DialogManager.instance.ShowDialog(childReaction);
    }

    public void ChangeScene()
    {
        // Make a delay before loading in the new scene

        UIFade.instance.FadeToBlack();
        waitToLoad -= Time.deltaTime;
        if (waitToLoad <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
