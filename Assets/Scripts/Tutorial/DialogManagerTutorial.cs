using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManagerTutorial : MonoBehaviour
{

    public Text dialogText;
    public GameObject dialogBox;

    public string[] dialogLines;

    //Keep track in wich line we are
    public int currentLine;
    public bool justStarted;
    public static DialogManagerTutorial instance;

    public bool conversationIsFinished = false;
    public bool secondDialog = false;

    public GameObject habitant;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // DELETING THE SECOND ENTER ON TUTORIAL
        if(InitSequence2.instance != null)
        {
            if(InitSequence2.instance.secondMessage)
            {
                justStarted = false;
            }
        }
        // if dialog box is open and the player release the Enter key we pass to other line and update the text
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);
                        PlayerController.instance.canMove = true;

                        if(InitSequence2.instance != null && InitSequence2.instance.secondMessage)
                        {
                            conversationIsFinished = true;
                        }   
                    }
                    else
                    {
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
            DialogActivatorTutorial.instance.DeactivateTalkHud();
        }
    }

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;

        currentLine = 0;

        dialogText.text = dialogLines[0];
        dialogBox.SetActive(true);
        justStarted = true;

        if (SceneManager.GetActiveScene().name == "InitSequence1")
        {
            justStarted = false;
        }
    }
}
