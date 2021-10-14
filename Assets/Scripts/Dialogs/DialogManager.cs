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
                            //habitant.GetComponent<PartitureHabitant>().GetFlute();
                        }

                        if (habitant.GetComponent<KasakirGuard>() != null)
                        {
                            habitant.GetComponent<KasakirGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<QuizaniGuard>() != null)
                        {
                            habitant.GetComponent<QuizaniGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<NaranGuard>() != null)
                        {
                            habitant.GetComponent<NaranGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<NecalliGuard>() != null)
                        {
                            habitant.GetComponent<NecalliGuard>().conversationFinished = true;
                        }

                        if (habitant.GetComponent<Tecalli>() != null)
                        {
                            if (!habitant.GetComponent<Tecalli>().hasFinished)
                            {
                                habitant.GetComponent<Tecalli>().LimitPartitures();
                            }
                            else
                            {
                                habitant.GetComponent<Tecalli>().canPass = true;
                                habitant.GetComponent<Tecalli>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Acan>() != null)
                        {
                            if (!habitant.GetComponent<Acan>().hasFinished)
                            {
                                habitant.GetComponent<Acan>().LimitPartitures();
                            }
                            else
                            {
                                habitant.GetComponent<Acan>().canPass = true;
                                habitant.GetComponent<Acan>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Seti>() != null)
                        {
                            if (!habitant.GetComponent<Seti>().hasFinished)
                            {
                                habitant.GetComponent<Seti>().LimitPartitures();
                            }
                            else
                            {
                                habitant.GetComponent<Seti>().canPass = true;
                                habitant.GetComponent<Seti>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Seti2>() != null)
                        {
                            if (!habitant.GetComponent<Seti2>().hasFinished)
                            {
                                habitant.GetComponent<Seti2>().LimitPartitures();
                            }
                            else
                            {
                                habitant.GetComponent<Seti2>().canPass = true;
                                habitant.GetComponent<Seti2>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Audience>() != null)
                        {
                            if (!habitant.GetComponent<Audience>().hasFinished)
                            {
                                habitant.GetComponent<Audience>().LimitPartitures(habitant);
                            }
                            else
                            {
                                habitant.GetComponent<Audience>().canPass = true;
                                habitant.GetComponent<Audience>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Leader>() != null)
                        {
                            if (!habitant.GetComponent<Leader>().hasFinished)
                            {
                                habitant.GetComponent<Leader>().LimitPartitures(habitant);
                            }
                            else
                            {
                                habitant.GetComponent<Leader>().canPass = true;
                                habitant.GetComponent<Leader>().finishedPartiture = true;
                            }
                        }

                        if (habitant.GetComponent<Blacksmith>() != null)
                        {
                            habitant.GetComponent<Blacksmith>().ShouldOpenInterface();
                        }

                        if (habitant.GetComponent<Workshop>() != null)
                        {
                            habitant.GetComponent<Workshop>().ShouldOpenInterface();
                        }

                        if (habitant.GetComponent<Trader>() != null)
                        {
                            habitant.GetComponent<Trader>().conversationFinished = true;
                            habitant.GetComponent<Trader>().Trade(habitant);
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

        if (SceneManager.GetActiveScene().name == "BlacksmithHouse1" && habitant.GetComponent<Blacksmith>().justStartedShouldBeFalse)
        {
            justStarted = false;
        }
        else if (SceneManager.GetActiveScene().name == "BlacksmithHouse1" && !habitant.GetComponent<Blacksmith>().justStartedShouldBeFalse)
        {
            justStarted = true;
        }

        if (SceneManager.GetActiveScene().name == "BlacksmithHouse2" && habitant.GetComponent<Blacksmith>().justStartedShouldBeFalse)
        {
            justStarted = false;
        }
        else if (SceneManager.GetActiveScene().name == "BlacksmithHouse2" && !habitant.GetComponent<Blacksmith>().justStartedShouldBeFalse)
        {
            justStarted = true;
        }

        if (SceneManager.GetActiveScene().name == "Workshop1" && habitant.GetComponent<Workshop>().justStartedShouldBeFalse)
        {
            justStarted = false;
        }
        else if (SceneManager.GetActiveScene().name == "Workshop1" && !habitant.GetComponent<Workshop>().justStartedShouldBeFalse)
        {
            justStarted = true;
        }

        if (SceneManager.GetActiveScene().name == "Workshop2" && habitant.GetComponent<Workshop>().justStartedShouldBeFalse)
        {
            justStarted = false;
        }
        else if (SceneManager.GetActiveScene().name == "Workshop2" && !habitant.GetComponent<Workshop>().justStartedShouldBeFalse)
        {
            justStarted = true;
        }
    }

    public void GetHabitant(GameObject getHabitant)
    {
        // habitant is the npc im talking to
        habitant = getHabitant;
        Debug.Log("Nombre de habitante: " + getHabitant.name);
    }
}
