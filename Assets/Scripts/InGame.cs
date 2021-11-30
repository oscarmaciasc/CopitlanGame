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
    [SerializeField] private bool canActivateBalloon;
    private GameObject player;
    public GameObject balloon;
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

        if(!XmlManager.instance.LoadGame().WasLoadedAlready())
        {
            Debug.Log("Loading position");
            FindObjectOfType<PlayerController>().gameObject.transform.position = new Vector3(XmlManager.instance.LoadGame().lastSaved.coordX, XmlManager.instance.LoadGame().lastSaved.coordY, 0f);
            XmlManager.instance.WasLoadedAlready(true);
        }

        if (SceneManager.GetActiveScene().name != "TradeHouse1") //or tradehouse2, etc
        {
            
            // Reference to the balloon
            balloon = FindObjectOfType<BalloonPlayerController>().gameObject;

            balloon.GetComponent<BalloonPlayerController>().enabled = false;

            // Reference to the player
            player = FindObjectOfType<PlayerController>().gameObject;

            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            if (SceneManager.GetActiveScene().name == "SW-Papataca")
            {
                if (gameData.mineEntrance[0].shouldBeActive)
                {
                    tecalliEntrance.SetActive(true);
                }
            }

            if (SceneManager.GetActiveScene().name == "NW-Papataca")
            {
                if (gameData.mineEntrance[1].shouldBeActive)
                {
                    acanEntrance.SetActive(true);
                }
                if (gameData.mineEntrance[2].shouldBeActive)
                {
                    setiEntrance.SetActive(true);
                }
            }

            if (SceneManager.GetActiveScene().name == "S-OutterCircle")
            {
                Debug.Log("Entro a S-OutterCircle");
                DontDestroyOnLoad(balloon);

                if (gameData.dirigentEntrance[0].shouldBeActive)
                {
                    kasakirEntrance.SetActive(true);
                }
            }

            if (SceneManager.GetActiveScene().name == "Triangle")
            {
                if (gameData.dirigentEntrance[1].shouldBeActive)
                {
                    quizaniEntrance.SetActive(true);
                }
            }

            if (SceneManager.GetActiveScene().name == "InnerCircle")
            {
                if (gameData.dirigentEntrance[2].shouldBeActive)
                {
                    naranEntrance.SetActive(true);
                }
            }
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

        // canActivateBalloon is set directly in the editor
        if (GameManager.instance.fPressed && !balloonActive && canActivateBalloon)
        {
            ActivateBalloon();
        }
        else if (GameManager.instance.fPressed && balloonActive && canActivateBalloon)
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

    public void ActivateBalloon()
    {
        balloon.GetComponent<BalloonPlayerController>().enabled = true;
        balloon.GetComponent<BalloonManager>().enabled = true;
        balloon.GetComponent<SpriteRenderer>().enabled = true;
        balloon.GetComponent<CircleCollider2D>().enabled = true;

        player.SetActive(false);
        balloonActive = true;
        GameManager.instance.fPressed = false;
    }

    public void DeactivateBalloon()
    {
        //balloon.SetActive(false);
        balloon.GetComponent<BalloonPlayerController>().enabled = false;
        balloon.GetComponent<BalloonManager>().enabled = false;
        balloon.GetComponent<SpriteRenderer>().enabled = false;
        balloon.GetComponent<CircleCollider2D>().enabled = false;

        player.transform.position = balloon.transform.position;
        player.SetActive(true);
        balloonActive = false;

        // We set the velocity to 0 to reassign the velocity for possible upgrades.
        //balloon.GetComponent<BalloonPlayerController>().moveSpeedBalloon = 0;
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
        XmlManager.instance.UpdateLastSaved(SceneManager.GetActiveScene().name, PlayerController.instance.gameObject.transform.position.x, PlayerController.instance.gameObject.transform.position.y);
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
        Debug.Log("******** Bye ********");
        XmlManager.instance.WasLoadedAlready(false);
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
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
            if (BalloonPlayerController.instance != null)
            {
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
        // Restoring the dimension of the array to avoid indexOutOfRange Exceptions.
        Partitures.instance.numberNotes = new string[10];
        pentagramPanel.SetActive(false);
        pentagramActive = false;
    }
}
