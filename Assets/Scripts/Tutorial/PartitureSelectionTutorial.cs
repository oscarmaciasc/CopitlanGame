using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartitureSelectionTutorial : MonoBehaviour
{

    public static PartitureSelectionTutorial instance;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject confirmationWindowExit;
    [SerializeField] private Button returnArrow;
    [SerializeField] private GameObject successfulSavedPanel;
    [SerializeField] private GameObject successfulSavedExitPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject interpretatePartitureButton;
    [SerializeField] private GameObject[] partiturePanels = new GameObject[10];
    [SerializeField] private GameObject[] arrowSelectionPartiture = new GameObject[10];
    [SerializeField] public GameObject pentagramPanel;
    [SerializeField] private GameObject tutorialInterface;
    public string panelPartitureName;

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
        DeactivatePartitureSelectionPanel();
    }

    // Update is called once per frame
    void Update()
    {
        // This allows us to activate the partitureSelectionPanel only when the vPressed message is display.
        if (!InitSequence2.instance.hasBeenActivated)
        {
            GameManager.instance.vPressed = false;
        }

        
    }

    public void BackPartitureSelection()
    {
        DeactivatePartitureSelectionPanel();
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
        Application.Quit();
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

    private void DeactivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.gameObject.SetActive(false);
        GameManager.instance.vPressed = false;
        DeativateArrowsPartitureSelection();

        interpretatePartitureButton.gameObject.GetComponent<Button>().interactable = false;
        interpretatePartitureButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(50, 30, 14, 140);
    }

    private void ActivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.SetActive(true);
    }

    private void DeativateArrowsPartitureSelection()
    {
        for (int i = 0; i < 10; i++)
        {
            arrowSelectionPartiture[i].SetActive(false);
        }
    }

    public void onClickPartiturePanel1()
    {
        partiturePanelPressed(0);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[0].gameObject.transform.Find("InfoLayout1").gameObject.transform.Find("HorizontalLayout1").gameObject.transform.Find("Name1").gameObject.GetComponent<Text>().text;
    }

    public void onClickPartiturePanel2()
    {
        partiturePanelPressed(1);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[1].gameObject.transform.Find("InfoLayout2").gameObject.transform.Find("HorizontalLayout2").gameObject.transform.Find("Name2").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel3()
    {
        partiturePanelPressed(2);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[2].gameObject.transform.Find("InfoLayout3").gameObject.transform.Find("HorizontalLayout3").gameObject.transform.Find("Name3").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel4()
    {
        partiturePanelPressed(3);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[3].gameObject.transform.Find("InfoLayout4").gameObject.transform.Find("HorizontalLayout4").gameObject.transform.Find("Name4").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel5()
    {
        partiturePanelPressed(4);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[4].gameObject.transform.Find("InfoLayout5").gameObject.transform.Find("HorizontalLayout5").gameObject.transform.Find("Name5").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel6()
    {
        partiturePanelPressed(5);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[5].gameObject.transform.Find("InfoLayout6").gameObject.transform.Find("HorizontalLayout6").gameObject.transform.Find("Name6").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel7()
    {
        partiturePanelPressed(6);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[6].gameObject.transform.Find("InfoLayout7").gameObject.transform.Find("HorizontalLayout7").gameObject.transform.Find("Name7").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel8()
    {
        partiturePanelPressed(7);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[7].gameObject.transform.Find("InfoLayout8").gameObject.transform.Find("HorizontalLayout8").gameObject.transform.Find("Name8").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel9()
    {
        partiturePanelPressed(8);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[8].gameObject.transform.Find("InfoLayout9").gameObject.transform.Find("HorizontalLayout9").gameObject.transform.Find("Name9").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel10()
    {
        partiturePanelPressed(9);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[9].gameObject.transform.Find("InfoLayout10").gameObject.transform.Find("HorizontalLayout10").gameObject.transform.Find("Name10").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void ActivatePanel(int index) {
        partiturePanels[index].SetActive(true);
    }

    public void DeactivatePanel(int index) {
        partiturePanels[index].SetActive(false);
    }

    public void partiturePanelPressed(int partitureSelected)
    {
        interpretatePartitureButton.gameObject.GetComponent<Button>().interactable = true;
        interpretatePartitureButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(50, 30, 14, 255);

        for (int i = 0; i < 10; i++)
        {
            if (partitureSelected == i)
            {
                arrowSelectionPartiture[i].SetActive(true);
            }
            else
            {
                arrowSelectionPartiture[i].SetActive(false);
            }
        }
    }

    public void onPartitureSelected()
    {
        DeactivatePartitureSelectionPanel();
        //open tutorial
        tutorialInterface.SetActive(true);
    }
}
