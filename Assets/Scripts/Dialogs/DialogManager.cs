using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public GameObject dialogBox;

    public string[] dialogLines;

    //Keep track in wich line we are
    public int currentLine;

    public static DialogManager instance;

    private bool justStarted;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // if dialog box is open and the player release the Enter key we pass to other line and update the text
        if (dialogBox.activeInHierarchy)
        {
            if(Input.GetKeyUp(KeyCode.Return))
            {
                if(!justStarted)
                {
                    currentLine++;

                    if(currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        PlayerController.instance.canMove = true;
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
        }
    }

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;

        currentLine = 0;

        dialogText.text = dialogLines[0];
        dialogBox.SetActive(true);

        justStarted = true;

        PlayerController.instance.canMove = false;
    }
}
