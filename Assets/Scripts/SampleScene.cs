using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleScene : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject confirmationWindowExit;
    [SerializeField] private Button returnArrow;
    [SerializeField] private GameObject successfulSavedPanel;
    [SerializeField] private GameObject successfulSavedExitPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject interpretatePartitureButton;
    [SerializeField] private GameObject[] partiturePanels = new GameObject[10];
    [SerializeField] private GameObject[] arrowSelectionPartiture = new GameObject[10];


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
        CheckForInputs();
    }

    private void CheckForInputs()
    {
        if (GameManager.instance.escapePressed == true) {
            exitPanel.SetActive(true);
        }
        if (GameManager.instance.vPressed == true)
        {
            ActivatePartitureSelectionPanel();
        }
    }

    public void BackPartitureSelection() {
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
        for(int i = 0; i < 10; i++) {
            arrowSelectionPartiture[i].SetActive(false);
        }
    }

    public void onClickPartiturePanel1() {
        partiturePanelPressed(0);
    }

    public void onClickPartiturePanel2() {
        partiturePanelPressed(1);
    }

    public void onClickPartiturePanel3() {
        partiturePanelPressed(2);
    }

    public void onClickPartiturePanel4() {
        partiturePanelPressed(3);
    }

    public void onClickPartiturePanel5() {
        partiturePanelPressed(4);
    }

    public void onClickPartiturePanel6() {
        partiturePanelPressed(5);
    }

    public void onClickPartiturePanel7() {
        partiturePanelPressed(6);
    }

    public void onClickPartiturePanel8() {
        partiturePanelPressed(7);
    }

    public void onClickPartiturePanel9() {
        partiturePanelPressed(8);
    }

    public void onClickPartiturePanel10() {
        partiturePanelPressed(9);
    }

    public void partiturePanelPressed(int partitureSelected) {
        interpretatePartitureButton.gameObject.GetComponent<Button>().interactable = true;
        interpretatePartitureButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(50, 30, 14, 255);

        for(int i = 0; i < 10; i++) {
            if(partitureSelected == i) {
                arrowSelectionPartiture[i].SetActive(true);
            }
            else {
                arrowSelectionPartiture[i].SetActive(false);
            }
        }
    }
}
