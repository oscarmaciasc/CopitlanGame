using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string characterSelectionScene;

    public string gameSelectionScene;

    public GameObject continueButton;

    public GameObject ConfirmationWindow;

    // Start is called before the first frame update
    void Start()
    {

        ConfirmationWindow.SetActive(false);

        /*if(PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Functions for each button of the MainMenu
    public void Continue()
    {
        SceneManager.LoadScene(gameSelectionScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(characterSelectionScene);
    }

    public void ConfirmationWindowDisplay()
    {
        ConfirmationWindow.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NoExit()
    {
        ConfirmationWindow.SetActive(false);
    }
}
