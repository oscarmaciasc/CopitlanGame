using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public string characterSelectionScene;

    public string gameSelectionScene;

    public GameObject continueButton;

    public GameObject confirmationWindow;

    [SerializeField] private GameObject newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        if(XmlManager.instance.CanCreateGame() == 0) {
            DeactivateNewGameButton();
        }
        confirmationWindow.SetActive(false);
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
        confirmationWindow.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NoExit()
    {
        confirmationWindow.SetActive(false);
    }

    private void DeactivateNewGameButton() {
        newGameButton.gameObject.GetComponent<Button>().interactable = false;
        newGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
        newGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
    }
}
