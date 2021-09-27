using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public GameObject dialogBox;

    public string[] dialogLines;

    //Keep track in wich line we are
    public int currentLine;
    public bool justStarted;
    public static DialogManager instance;

    public bool conversationIsFinished = false;

    public GameObject habitant;
    public GameObject pentagramPanel;
    public GameObject partitureSelectionPanel;


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
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        PlayerController.instance.canMove = true;

                        if (habitant.GetComponent<PartitureHabitant>() != null)
                        {
                            habitant.GetComponent<PartitureHabitant>().conversationFinished = true;
                            habitant.GetComponent<PartitureHabitant>().firstTime = true;
                        }

                        if (habitant.GetComponent<KasakirGuard>() != null)
                        {
                            habitant.GetComponent<KasakirGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<QuizaniGuard>() != null)
                        {
                            habitant.GetComponent<QuizaniGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<Mines>() != null)
                        {
                            habitant.GetComponent<Mines>().LimitPartitures(habitant);
                        }
                        else
                        {
                            // Activate the other panels.
                        }


                    }
                    else
                    {
                        dialogText.text = dialogLines[currentLine];
                        if (habitant.GetComponent<PartitureHabitant>() != null)
                        {
                            habitant.GetComponent<PartitureHabitant>().conversationFinished = false;
                        }
                    }
                }
                else
                {
                    justStarted = false;
                }

            }
        }

        if (pentagramPanel.activeInHierarchy)
        {
            // Send the habitant to PentagramManager script
            pentagramPanel.GetComponent<PentagramManager>().GetHabitant(this.habitant);
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

    public void GetHabitant(GameObject getHabitant)
    {
        // habitant is the npc im talking to
        habitant = getHabitant;
        Debug.Log("Nombre de habitante: " + getHabitant.name);
    }
}
