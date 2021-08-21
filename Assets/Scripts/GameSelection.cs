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
    public GameObject[] games;
    private GameData[] gamesData = new GameData[3];
    private int gamesNumber = 0;

    [SerializeField] private GameObject arrowGame1;
    [SerializeField] private GameObject arrowGame2;
    [SerializeField] private GameObject arrowGame3;
    [SerializeField] private GameObject nameGame1;
    [SerializeField] private GameObject nameGame2;
    [SerializeField] private GameObject nameGame3;


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

        ActivateGamePanels();
    }

    // Update is called once per frame
    void Update()
    {
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
        gamesData = XmlManager.instance.Load();
        
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
                    games[i].gameObject.SetActive(true);
                    FillGamePanel(i);
                }
                else
                {
                    games[i].gameObject.SetActive(false);
                }
            }
        }
        else {
            for (int i = 0; i < 3; i++)
            {
                games[i].gameObject.SetActive(false);
            }
        }
    }

    public void FillGamePanel(int i) {


        switch(i) {
            case 0:
                nameGame1.gameObject.GetComponent<Text>().text = gamesData[i].name;
            break;
            case 1:
                nameGame2.gameObject.GetComponent<Text>().text = gamesData[i].name;
            break;
            case 2:
                nameGame3.gameObject.GetComponent<Text>().text = gamesData[i].name;
            break;
        }
    }

    public void ConfirmationWindowDisplayDelete()
    {
        confirmationWindowDelete.SetActive(true);
    }

    public void Create() {
        if(XmlManager.instance.Create()) {
            SceneManager.LoadScene("CharacterSelection");
        }
        else {
            Debug.Log("No xD");
            // Activate the "No more than 3 games" panel
        }
    }
    public void Delete()
    {
        //Delete the selected game
    }

    public void DeactivateWindowDelete()
    {
        confirmationWindowDelete.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartSequence()
    {

    }

    public void LoadGame()
    {

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
    }

    public void Game2()
    {
        arrowGame1.SetActive(false);
        arrowGame3.SetActive(false);
        arrowGame2.SetActive(true);
    }

    public void Game3()
    {
        arrowGame1.SetActive(false);
        arrowGame2.SetActive(false);
        arrowGame3.SetActive(true);
    }

    public void DeactivateButtons()
    {
        deleteGameButton.GetComponent<Button>().interactable = false;
        loadGameButton.GetComponent<Button>().interactable = false;
        // Condition: if game 1 deactivate arrow 1, if game 2 deactivate game 2, etc...
    }
}
