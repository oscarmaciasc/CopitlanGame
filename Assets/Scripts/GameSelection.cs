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

    [SerializeField] private GameObject arrowGame1;
    [SerializeField] private GameObject arrowGame2;
    [SerializeField] private GameObject arrowGame3;

    public string[] names = new string[3];

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

        for (int i = 0; i < 3; i++)
        {
            if (names[i] != "")
            {
                games[i].gameObject.SetActive(true);
            }
            else
            {
                games[i].gameObject.SetActive(false);
            }
        }
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

    public void ConfirmationWindowDisplayDelete()
    {
        confirmationWindowDelete.SetActive(true);
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

    public void CharacterSelection()
    {
        if(XmlManager.instance.CanCreateGame() != 0)
        {
            SceneManager.LoadScene("CharacterSelection");
        }
        else
        {
            Debug.Log("You already have 3 games");
            // Deactivate NewGame button
            // also implement this on MainMenu newGame
        }
    }
}
