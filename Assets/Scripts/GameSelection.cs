using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
    public GameObject confirmationWindowDelete;
    public GameObject deleteGameButton;
    public GameObject loadGameButton;
    public GameObject[] gamePanels;
    private GameData[] gamesData = new GameData[3];
    private int gamesNumber = 0;
    private int index = 0;

    [SerializeField] private GameObject createGameButton;
    [SerializeField] private GameObject arrowGame1;
    [SerializeField] private GameObject arrowGame2;
    [SerializeField] private GameObject arrowGame3;
    [SerializeField] private GameObject[] nameGames = new GameObject[3];
    [SerializeField] private GameObject[] permitsOutCir = new GameObject[3];
    [SerializeField] private GameObject[] permitsTrian = new GameObject[3];
    [SerializeField] private GameObject[] permitsInCir = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        arrowGame1.SetActive(false);
        arrowGame2.SetActive(false);
        arrowGame3.SetActive(false);
        confirmationWindowDelete.SetActive(false);
        //deleteGameButton.SetActive(false);
        //loadGameButton.SetActive(false);
        
        DeactivateButtons();

        //Changin color and outline color when no game is selected
        if (deleteGameButton.GetComponent<Button>().interactable == false || loadGameButton.GetComponent<Button>().interactable == false)
        {
            loadGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
            loadGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);

            deleteGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
            deleteGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
        }

        // Activate and filling game panels with files info
        ActivateGamePanels();
    }

    // Update is called once per frame
    void Update()
    {
        CanCreateGame();
        //Changin color and outline color when a game is selected
        if (deleteGameButton.GetComponent<Button>().interactable == true || loadGameButton.GetComponent<Button>().interactable == true)
        {
            loadGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 255);
            loadGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 255);

            deleteGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 255);
            deleteGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 255);
        }
    }

    public void ActivateGamePanels() {
        // Activates and fills game panels with files info
        gamesData = XmlManager.instance.LoadAllGames();
        
        for(int i = 0; i < 3; i++) {
            if(gamesData[i] != null) {
                gamesNumber++;
            }
        }

        if(gamesNumber > 0) {
            for (int i = 0; i < 3; i++)
            {
                if (gamesData[i] != null)
                {
                    Debug.Log(gamesData[i].name);
                    gamePanels[i].gameObject.SetActive(true);
                    FillGamePanel(i);
                }
                else
                {
                    gamePanels[i].gameObject.SetActive(false);
                }
            }
        }
        else {
            for (int i = 0; i < 3; i++)
            {
                gamePanels[i].gameObject.SetActive(false);
            }
        }
    }

    public void FillGamePanel(int i) {
        // Fills game panel info with files info
        nameGames[i].gameObject.GetComponent<Text>().text = gamesData[i].name;
        if(gamesData[i].DoesHavePermit("innerCircle")) {
            permitsOutCir[i].gameObject.SetActive(true);
            permitsTrian[i].gameObject.SetActive(true);
            permitsInCir[i].gameObject.SetActive(true);
        }
        else if (gamesData[i].DoesHavePermit("triangle")) {
            permitsOutCir[i].gameObject.SetActive(true);
            permitsTrian[i].gameObject.SetActive(true);
            permitsInCir[i].gameObject.SetActive(false);
        }
        else if (gamesData[i].DoesHavePermit("outterCircle")) {
            permitsOutCir[i].gameObject.SetActive(true);
            permitsTrian[i].gameObject.SetActive(false);
            permitsInCir[i].gameObject.SetActive(false);
        }
        
    }

    private void CanCreateGame() {
        // Deactivate the create game button if there are three games already and viceversa loko
        if(XmlManager.instance.CanCreateGame() == 0)
        {
            DeactivateCreateGameButton();
        }
        else
        {
            ActivateCreateGameButton();
        }
    }

    public void ConfirmationWindowDisplayDelete()
    {
        confirmationWindowDelete.SetActive(true);
    }

    public void Create() {
        if(XmlManager.instance.CanCreateGame() != 0) {
            SceneManager.LoadScene("CharacterSelection");
        }
    }

    public void Load()
    {
        XmlManager.instance.CreateTempFile(index);
    }

    public void Delete()
    {
        XmlManager.instance.Delete(index);
        DeactivateWindowDelete();
        DeactivateButtons();
        SceneManager.LoadScene("GameSelection");
    }

    public void ActivateDeletePanel()
    {
        ConfirmationWindowDisplayDelete();
    }

    public void DeactivateWindowDelete()
    {
        confirmationWindowDelete.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameSelected()
    {
        ButtonActivator();
    }

    private void ButtonActivator()
    {
        //deleteGameButton.SetActive(true);
        deleteGameButton.GetComponent<Button>().interactable = true;
        //loadGameButton.SetActive(true);
        loadGameButton.GetComponent<Button>().interactable = true;
        //return in the function of the id or name of the game we want to delete
    }

    public void Game1()
    {
        arrowGame2.SetActive(false);
        arrowGame3.SetActive(false);
        arrowGame1.SetActive(true);
        index = 1;
    }

    public void Game2()
    {
        arrowGame1.SetActive(false);
        arrowGame3.SetActive(false);
        arrowGame2.SetActive(true);
        index = 2;
    }

    public void Game3()
    {
        arrowGame1.SetActive(false);
        arrowGame2.SetActive(false);
        arrowGame3.SetActive(true);
        index = 3;
    }

    public void DeactivateButtons()
    {
        deleteGameButton.GetComponent<Button>().interactable = false;
        loadGameButton.GetComponent<Button>().interactable = false;
        // Condition: if game 1 deactivate arrow 1, if game 2 deactivate game 2, etc...
    }

    private void DeactivateCreateGameButton()
    {
        createGameButton.gameObject.GetComponent<Button>().interactable = false;
        createGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
        createGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
    }

    private void ActivateCreateGameButton()
    {
        createGameButton.gameObject.GetComponent<Button>().interactable = true;
        createGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 255);
        createGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 255);
    }
}
