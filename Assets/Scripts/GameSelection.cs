using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
       public GameObject ConfirmationWindowDelete;

    public GameObject[] Games;

    public string[] Names = new string[3];

    // Start is called before the first frame update
    void Start()
    {
        ConfirmationWindowDelete.SetActive(false);
        for(int i=0; i<3; i++)
        {
            if(Names[i] != "")
            {
                Games[i].gameObject.SetActive(true);
            }
            else{
                Games[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmationWindowDisplayDelete()
    {
        ConfirmationWindowDelete.SetActive(true);
    }

    public void Delete()
    {
        //Delete the selected game
    }

    public void NoDelete()
    {
        ConfirmationWindowDelete.SetActive(false);
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
}
