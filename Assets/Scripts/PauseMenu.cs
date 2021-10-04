using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] private GameObject MapButton;
    [SerializeField] private GameObject InventoryButton;
    [SerializeField] private GameObject BalloonButton;
    [SerializeField] private GameObject InfoButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject MapPanel;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject BalloonPanel;
    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private GameObject InvResourcesPanel;
    [SerializeField] private GameObject InvFlutesPanel;
    [SerializeField] private GameObject InvPartituresPanel;
    [SerializeField] private GameObject[] resourceQuantity = new GameObject[4];
    [SerializeField] private GameObject[] flutes = new GameObject[4];
    [SerializeField] private GameObject[] partitures = new GameObject[10];
    [SerializeField] private GameObject PauseMenuPanel;

    private GameData gameData;

    void Start()
    {
        instance = this;
        ActivatePanel();
    }

    public void ActivatePanel() {
        LoadGameData();
        ActivateMapPanel();
        PauseMenuPanel.SetActive(true);
    }

    private void LoadGameData() {
        gameData = XmlManager.instance.LoadGame();
    }

    public void ActivateMapPanel() {
        MapPanel.SetActive(true);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void ActivateInventoryPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(true);
        ActivateInvResourcesPanel();
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void ActivateBalloonPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(true);
        InfoPanel.SetActive(false);
    }

    public void ActivateInfoPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

    public void ActivateInvResourcesPanel() {
        InvResourcesPanel.SetActive(true);
        SetResourcesQuantity();
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(false);
    }

    private void SetResourcesQuantity() {
        for(int i = 0; i < 4; i++) {
            resourceQuantity[i].transform.GetComponent<Text>().text = gameData.resource[i].quantity.ToString();
        }
    }

    public void ActivateInvFlutesPanel() {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(true);
        SetFlutes();
        InvPartituresPanel.SetActive(false);
    }

    private void SetFlutes() {
        for(int i = 0; i <= gameData.flute.Length; i++) {
            flutes[i].SetActive(true);
        }

        if(gameData.flute.Length < 4) {
            for(int i = gameData.flute.Length; i < 4; i++) {
                flutes[i].SetActive(false);
            }
        }
    }

    public void ActivateInvPartituresPanel() {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(true);
        SetPartitures();
    }

    private void SetPartitures() {
        string partitureName = "";
        int partitureNum = 0;
        for(int i = 0; i < 10; i++) {
            partitureNum = i;
            partitureNum++;
            partitureName = "partiture" + partitureNum.ToString();
            
            Debug.Log(partitureName);
            if(gameData.DoesHavePartiture(partitureName)) {
                partitures[i].SetActive(true);
            }
            else {
                partitures[i].SetActive(false);
            }
        }
    }

    public void DeactivatePauseMenuPanel() {
        PauseMenuPanel.gameObject.SetActive(false);
        GameManager.instance.pPressed = false;
    }
}