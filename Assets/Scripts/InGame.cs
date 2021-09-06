using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{

    public static InGame instance;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject confirmationWindowExit;
    [SerializeField] private Button returnArrow;
    [SerializeField] private GameObject successfulSavedPanel;
    [SerializeField] private GameObject successfulSavedExitPanel;
    [SerializeField] public GameObject partitureSelectionPanel;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        exitPanel.SetActive(false);
        confirmationWindowExit.SetActive(false);
        successfulSavedPanel.SetActive(false);
        successfulSavedExitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
        CheckCanMove();
    }

    private void CheckForInputs()
    {
        if (GameManager.instance.escapePressed == true)
        {
            exitPanel.SetActive(true);
        }
        if (GameManager.instance.vPressed == true)
        {
            if (partitureSelectionPanel.activeInHierarchy == false)
            {
                ActivatePartitureSelectionPanel();
            }
        }
    }


    //********************************************************************************
    private void ActivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.SetActive(true);
    }

    public void activateSavePanel()
    {
        Save();
        successfulSavedPanel.SetActive(true);
        returnArrow.interactable = false;
    }

    public void activateSaveExitPanel()
    {
        successfulSavedExitPanel.SetActive(true);
        Save();
    }

    public void Exit()
    {
        confirmationWindowExit.SetActive(true);
        returnArrow.interactable = false;
        //Opacity 255 arrow
        //returnArrow.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void Return()
    {
        exitPanel.SetActive(false);
        GameManager.instance.escapePressed = false;
        successfulSavedPanel.SetActive(false);
        successfulSavedExitPanel.SetActive(false);
        returnArrow.interactable = true;
    }

    public void SaveAndExit()
    {
        Save();
        successfulSavedExitPanel.SetActive(true);
    }

    public void ExitWithoutSaving()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

    public void Save()
    {
        Debug.Log("Saved");
    }

    //********************************************************************************
    public void CheckCanMove()
    {
        if (partitureSelectionPanel.activeInHierarchy)
            {
                PlayerController.instance.canMove = false;
            }
            else
            {
                PlayerController.instance.canMove = true;
            }
    }
}
