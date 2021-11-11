using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGame : MonoBehaviour
{

    public static InGame instance;
    [SerializeField] private GameObject exitMenu;
    [SerializeField] private GameObject savePanel;
    [SerializeField] private GameObject overwriteSavePanel;
    [SerializeField] private GameObject succesfullySavePanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject saveOnExitPanel;
    [SerializeField] private GameObject overwriteExitPanel;
    [SerializeField] private GameObject successfullyExitPanel;
    [SerializeField] private Button returnArrow;
    [SerializeField] public GameObject partitureSelectionPanel;
    [SerializeField] public GameObject pentagramPanel;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject tecalliEntrance;
    [SerializeField] private GameObject acanEntrance;
    [SerializeField] private GameObject setiEntrance;
    [SerializeField] private GameObject kasakirEntrance;
    [SerializeField] private GameObject quizaniEntrance;
    [SerializeField] private GameObject naranEntrance;
    [SerializeField] private GameObject pauseMenuPanel;
    private GameObject player;
    private GameObject balloon;
    public bool pentagramActive = true;
    public GameObject noFuelPanel;
    public bool balloonActive = false;
    private bool pauseMenuHasBeenStarted = false;
    public float lastSaved = 0f;

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
        DeactivateAllExitMenuPanels();

        UIFade.instance.FadeFromBlack();

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {

            // Reference to the balloon
            balloon = FindObjectOfType<BalloonPlayerController>().gameObject;
            balloon.SetActive(false);

            // Reference to the player
            player = FindObjectOfType<PlayerController>().gameObject;

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

            if (gameData.dirigentEntrance[0].shouldBeActive)
            {
                kasakirEntrance.SetActive(true);
            }
            if (gameData.dirigentEntrance[1].shouldBeActive)
            {
                quizaniEntrance.SetActive(true);
            }
            if (gameData.dirigentEntrance[2].shouldBeActive)
            {
                naranEntrance.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            // Reference to the balloon
            balloon = FindObjectOfType<BalloonPlayerController>().gameObject;
            balloon.SetActive(false);

            // Reference to the player
            player = FindObjectOfType<PlayerController>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
        CheckCanMove();
        NoFuelPanel();

    }

    private void CheckForInputs()
    {
        if (GameManager.instance.escapePressed && !exitMenu.activeInHierarchy)
        {
            ActivateExitMenu();
            GameManager.instance.escapePressed = false;
        }
        else if (GameManager.instance.escapePressed && exitMenu.activeInHierarchy)
        {
            Return();
            GameManager.instance.escapePressed = false;
        }

        if (GameManager.instance.vPressed && !partitureSelectionPanel.activeInHierarchy)
        {
            ActivatePartitureSelectionPanelFreely();
            GameManager.instance.vPressed = false;
        }
        else if (GameManager.instance.vPressed && partitureSelectionPanel.activeInHierarchy)
        {
            DeactivatePartitureSelectionPanel();
            GameManager.instance.vPressed = false;
        }

        if (GameManager.instance.pPressed && !pauseMenuPanel.activeInHierarchy)
        {
            ActivatePauseMenuPanel();
            GameManager.instance.pPressed = false;
        }
        else if (GameManager.instance.pPressed && pauseMenuPanel.activeInHierarchy)
        {
            DeactivatePauseMenuPanel();
            GameManager.instance.pPressed = false;
        }

        if (GameManager.instance.fPressed && !balloonActive && SceneManager.GetActiveScene().name == "SampleScene")
        {
            balloon.SetActive(true);
            player.SetActive(false);
            balloonActive = true;
            GameManager.instance.fPressed = false;
        }
        else if (GameManager.instance.fPressed && balloonActive && SceneManager.GetActiveScene().name == "SampleScene")
        {
            DeactivateBalloon();
        }
    }

    private void DeactivateAllExitMenuPanels()
    {
        exitMenu.SetActive(false);
        savePanel.SetActive(false);
        overwriteSavePanel.SetActive(false);
        succesfullySavePanel.SetActive(false);
        exitPanel.SetActive(false);
        saveOnExitPanel.SetActive(false);
        overwriteExitPanel.SetActive(false);
        successfullyExitPanel.SetActive(false);
    }

    private void ActivateExitMenu()
    {
        if (partitureSelectionPanel.activeInHierarchy || pentagramPanel.activeInHierarchy)
        {
            GameManager.instance.escapePressed = false;
        }
        else
        {
            exitMenu.SetActive(true);
        }
    }

    public void ActivatePauseMenuPanel()
    {
        if (!this.pauseMenuHasBeenStarted)
        {
            pauseMenuPanel.SetActive(true);
            pauseMenuHasBeenStarted = true;
        }
        else
        {
            PauseMenu.instance.ActivatePanel();
        }
    }

    public void DeactivatePauseMenuPanel()
    {
        pauseMenuPanel.SetActive(false);
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
            PartitureSelection.instance.FluteFilter();
            PartitureSelection.instance.DeativateArrowsPartitureSelection();
        }


    }

    public void DeactivateBalloon()
    {
        balloon.SetActive(false);
        player.transform.position = balloon.transform.position;
        player.SetActive(true);
        balloonActive = false;

        // We set the velocity to 0 to reassign the velocity for possible upgrades.
        balloon.GetComponent<BalloonPlayerController>().moveSpeedBalloon = 0;
        GameManager.instance.fPressed = false;
    }

    public void ActivatePartitureSelectionPanelFreely()
    {
        partitureSelectionPanel.SetActive(true);
        PartitureSelection.instance.DeactivateNoOwnedPartiturePanels();
        PartitureSelection.instance.FluteFilter();
        PartitureSelection.instance.DeativateArrowsPartitureSelection();
    }

    public void DeactivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.SetActive(false);
    }

    public void ActivateSavePanel()
    {
        savePanel.SetActive(true);
        returnArrow.interactable = false;
    }

    public void DeactivateSavePanel()
    {
        savePanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void ActivateSaveOverwritePanel()
    {
        overwriteSavePanel.SetActive(true);
        savePanel.SetActive(false);
    }

    public void DeactivateSaveOverwritePanel()
    {
        overwriteSavePanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void ActivateSaveSuccessfullyPanel()
    {
        succesfullySavePanel.SetActive(true);
        overwriteSavePanel.SetActive(false);
    }

    public void Return()
    {
        exitMenu.SetActive(false);
        GameManager.instance.escapePressed = false;
        succesfullySavePanel.SetActive(false);
        successfullyExitPanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void Save()
    {
        XmlManager.instance.UpdateTimePlayed(Time.time - lastSaved);
        lastSaved = Time.time;
    }

    public void ActivateExitPanel()
    {
        exitPanel.SetActive(true);
        returnArrow.interactable = false;
    }

    public void DeactivateExitPanel()
    {
        exitPanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void ActivateSaveOnExitPanel()
    {
        saveOnExitPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void ActivateExitOverwritePanel()
    {
        overwriteExitPanel.SetActive(true);
        saveOnExitPanel.SetActive(false);
    }

    public void DeactivateExitOverwritePanel()
    {
        overwriteExitPanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void ActivateExitSuccessfullyPanel()
    {
        successfullyExitPanel.SetActive(true);
        overwriteExitPanel.SetActive(false);
    }

    public void ActivateNoFuelPanel()
    {
        noFuelPanel.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

    private void NoFuelPanel()
    {
        if (noFuelPanel.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                noFuelPanel.SetActive(false);
            }
        }
    }

    //********************************************************************************
    public void CheckCanMove()
    {
        if (partitureSelectionPanel.activeInHierarchy || dialogBox.activeInHierarchy || pentagramPanel.activeInHierarchy || noFuelPanel.activeInHierarchy)
        {
            PlayerController.instance.canMove = false;
            BalloonPlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
            if(BalloonPlayerController.instance != null) {
                BalloonPlayerController.instance.canMove = true;
            }
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
        pentagramActive = false;
    }
}
