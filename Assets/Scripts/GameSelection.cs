using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
    public GameObject confirmationWindowDelete;
    public GameObject deleteGameButton;
    public GameObject[] games;

    public string[] names = new string[3];

    // Start is called before the first frame update
    void Start()
    {
        confirmationWindowDelete.SetActive(false);
        deleteGameButton.SetActive(false);

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
       
    }

    public void ConfirmationWindowDisplayDelete()
    {
        confirmationWindowDelete.SetActive(true);
    }

    public void Delete()
    {
        //Delete the selected game
    }

    public void NoDelete()
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
        deleteGameButton.SetActive(true);
        //return in the function the id or name of the game we want to delete
    }
}
