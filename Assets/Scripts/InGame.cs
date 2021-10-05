using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{

    public static InGame instance;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject confirmationWindowExit;
    [SerializeField] private Button returnArrow;
    [SerializeField] private GameObject successfulSavedPanel;
    [SerializeField] private GameObject successfulSavedExitPanel;
    [SerializeField] public GameObject partitureSelectionPanel;
    [SerializeField] public GameObject pentagramPanel;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject tecalliEntrance;
    [SerializeField] private GameObject acanEntrance;
    [SerializeField] private GameObject setiEntrance;
    [SerializeField] private GameObject kasakirEntrance;
    [SerializeField] private GameObject quizaniEntrance;
    [SerializeField] private GameObject naranEntrance;
    [SerializeField] private GameObject PauseMenuPanel;
    private bool pauseMenuHasBeenStarted = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        exitPanel.SetActive(false);
        confirmationWindowExit.SetActive(false);
        successfulSavedPanel.SetActive(false);
        successfulSavedExitPanel.SetActive(false);
        UIFade.instance.FadeFromBlack();

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();


            if (gameData.mineEntrance[0].shouldBeActive)
            {
                tecalliEntrance.SetActive(true);
            }
            if (gameData.mineEntrance[1].shouldBeActive)
            {
                acanEntrance.SetActive(true);
            }
            if (gameData.mineEntrance[2].shouldBeActive)
            {
                setiEntrance.SetActive(true);
            }

            if(gameData.dirigentEntrance[0].shouldBeActive)
            {
                kasakirEntrance.SetActive(true);
            }
            if(gameData.dirigentEntrance[1].shouldBeActive)
            {
                quizaniEntrance.SetActive(true);
            }
            if(gameData.dirigentEntrance[2].shouldBeActive)
            {
                naranEntrance.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
        CheckCanMove();
    }

    private void CheckForInputs()
    {
        if (GameManager.instance.escapePressed)
        {
            exitPanel.SetActive(true);
        }
        if (GameManager.instance.vPressed)
        {
            if (partitureSelectionPanel.activeInHierarchy == false)
            {
                ActivatePartitureSelectionPanelFreely();
            }
        }
        if (GameManager.instance.pPressed)
        {
            if (PauseMenuPanel.activeInHierarchy == false)
            {
                ActivatePauseMenuPanel();
            }
        }
    }

    public void ActivatePauseMenuPanel()
    {
        if(!this.pauseMenuHasBeenStarted) {
            PauseMenuPanel.SetActive(true);
            pauseMenuHasBeenStarted = true;
        }
        else {
            PauseMenu.instance.ActivatePanel();
        }
    }

    public void DeactivatePauseMenuPanel()
    {
        PauseMenuPanel.SetActive(false);
    }


    //********************************************************************************
    public void ActivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.SetActive(true);
        PartitureSelection.instance.DeativateArrowsPartitureSelection();
        if (!PartitureHabitant.instance.HasPartituresFilter())
        {
            Debug.Log(PartitureHabitant.instance.HasPartituresFilter());
            PartitureSelection.instance.DeactivateNoOwnedPartiturePanels();
            PartitureSelection.instance.DeativateArrowsPartitureSelection();
        }
    }

    public void ActivatePartitureSelectionPanelFreely()
    {
        partitureSelectionPanel.SetActive(true);
        PartitureSelection.instance.DeactivateNoOwnedPartiturePanels();
        PartitureSelection.instance.DeativateArrowsPartitureSelection();
    }

    public void DeactivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.SetActive(false);
    }

    public void activateSavePanel()
    {
        Save();
        successfulSavedPanel.SetActive(true);
        returnArrow.interactable = false;
    }

    public void activateSaveExitPanel()
    {
        successfulSavedExitPanel.SetActive(true);
        Save();
    }

    public void Exit()
    {
        confirmationWindowExit.SetActive(true);
        returnArrow.interactable = false;
    }

    public void Return()
    {
        exitPanel.SetActive(false);
        GameManager.instance.escapePressed = false;
        successfulSavedPanel.SetActive(false);
        successfulSavedExitPanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void SaveAndExit()
    {
        Save();
        successfulSavedExitPanel.SetActive(true);
    }

    public void ExitWithoutSaving()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

    public void Save()
    {
        Debug.Log("Saved");
    }

    //********************************************************************************
    public void CheckCanMove()
    {
        if (partitureSelectionPanel.activeInHierarchy || dialogBox.activeInHierarchy || pentagramPanel.activeInHierarchy)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public void HasFinishedPartiture()
    {
        StartCoroutine(DeactivatePentagramPanel());
    }

    IEnumerator DeactivatePentagramPanel()
    {
        yield return new WaitForSeconds(1);
        pentagramPanel.SetActive(false);
    }
}
