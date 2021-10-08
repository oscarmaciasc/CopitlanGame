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
    [SerializeField] private GameObject balloon1Panel;
    [SerializeField] private GameObject balloon2Panel;
    [SerializeField] private GameObject balloon3Panel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject musicalMasteryLevel;
    [SerializeField] private GameObject happinessPercentage;
    [SerializeField] private GameObject minPlayed;
    [SerializeField] private GameObject minWalked;
    [SerializeField] private GameObject minBalloon;
    [SerializeField] private GameObject interactedHabitants;
    [SerializeField] private GameObject collectables;
    [SerializeField] private GameObject outterCirclePermission;
    [SerializeField] private GameObject trianglePermission;
    [SerializeField] private GameObject innerCirclePermission;
    [SerializeField] private GameObject femalePanel;
    [SerializeField] private GameObject malePanel;


    private GameData gameData;

    void Start()
    {
        instance = this;
        ActivatePanel();
    }

    public void ActivatePanel() {
        LoadGameData();
        ActivateMapPanel();
        pauseMenuPanel.SetActive(true);
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
        SetBallon();
        SetSlider();
        InfoPanel.SetActive(false);
    }

    private void SetBallon() {
        string balloonName = "";

        for(int i = 0; i < 3; i++) {
            balloonName = "balloonLvl" + (i + 1).ToString();
            if(gameData.DoesHaveBalloon(balloonName)) {
                switch(i) {
                    case 0:
                        balloon1Panel.SetActive(true);
                        balloon2Panel.SetActive(false);
                        balloon3Panel.SetActive(false);
                    break;
                    case 1:
                        balloon1Panel.SetActive(false);
                        balloon2Panel.SetActive(true);
                        balloon3Panel.SetActive(false);
                    break;
                    case 2:
                        balloon1Panel.SetActive(false);
                        balloon2Panel.SetActive(false);
                        balloon3Panel.SetActive(true);
                    break;
                }
            }
        }
    }

    public void SetSlider()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        BalloonPanel.transform.Find("FuelLayout").Find("FuelLevel").GetComponent<Slider>().maxValue = gameData.GetBalloonCapacity();
        BalloonPanel.transform.Find("FuelLayout").Find("FuelLevel").GetComponent<Slider>().value = gameData.GetCurrentResource(3);
    }

    public void ActivateInfoPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(true);
        SetInfo();
    }

    private void SetInfo() {
        musicalMasteryLevel.transform.GetComponent<Text>().text = gameData.GetMusicalMasteryLevel();

        // *************************** KINDA FAKE ***************************
        Debug.Log("Necalli: " + gameData.GetAudienceResult("necalliResult"));
        happinessPercentage.transform.GetComponent<Text>().text = gameData.happinessPercentage.percentage.ToString();
        // *************************** KINDA FAKE ***************************

        minPlayed.transform.GetComponent<Text>().text = FormatTime(gameData.timePlayed.time);

        if(gameData.DoesHavePermit("outterCircle")) {
            outterCirclePermission.SetActive(true);
        }
        else {
            outterCirclePermission.SetActive(false);
        }

        if(gameData.DoesHavePermit("triangle")) {
            trianglePermission.SetActive(true);
        }
        else {
            trianglePermission.SetActive(false);
        }

        if(gameData.DoesHavePermit("innerCircle")) {
            innerCirclePermission.SetActive(true);
        }
        else {
            innerCirclePermission.SetActive(false);
        }

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

        for(int i = 0; i < 10; i++) {
            partitureName = "partiture" + (i + 1).ToString();
            
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
        pauseMenuPanel.gameObject.SetActive(false);
        GameManager.instance.pPressed = false;
    }

    private string FormatTime(float time) {
        int minutes = (int) time / 60;
        int seconds = (int) (time - (60 * minutes));
        return string.Format("{0:00}:{1:00}", minutes, seconds );
    }
}
