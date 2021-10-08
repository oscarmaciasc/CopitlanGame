using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeHouse : MonoBehaviour
{
    [SerializeField] private GameObject tradeHouseInterface;
    [SerializeField] private GameObject welcomeLayout;
    [SerializeField] private GameObject tradeLayout;
    [SerializeField] private GameObject receivedLayout;
    [SerializeField] private GameObject notEnoughResourceLayout;
    [SerializeField] private GameObject notEnoughSpaceLayout;
    [SerializeField] private GameObject[] resourceToSustractPanels = new GameObject[3];
    [SerializeField] private GameObject[] resourceToGivePanels = new GameObject[4];
    [SerializeField] private GameObject[] resourceSustractedPanels = new GameObject[3];
    [SerializeField] private GameObject[] resourceGivenPanels = new GameObject[4];

    void Start()
    {
        LoadGameData();
        ActivateWelcomeLayout();
    }

    void Update()
    {

    }

    private void LoadGameData()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();
    }

    public void DeactivateTradeHouseInterface() {
        tradeHouseInterface.SetActive(false);
    }

    public void ActivateWelcomeLayout() {
        welcomeLayout.SetActive(true);
        tradeLayout.SetActive(false);
        receivedLayout.SetActive(false);
        notEnoughResourceLayout.SetActive(false);
        notEnoughSpaceLayout.SetActive(false);
    }

    private void ActivateTradeLayout(int resourceToGiveID, int resourceToSustractID) {
        welcomeLayout.SetActive(false);
        tradeLayout.SetActive(true);
        receivedLayout.SetActive(false);
        notEnoughResourceLayout.SetActive(false);
        notEnoughSpaceLayout.SetActive(false);

        for(int i = 0; i < 3; i++) {
            if(i == resourceToSustractID) {
                resourceToSustractPanels[i].SetActive(true);
            }
            else {
                resourceToSustractPanels[i].SetActive(false);
            }
        }

        for(int i = 0; i < 4; i++) {
            if(i == resourceToGiveID) {
                resourceToGivePanels[i].SetActive(true);
            }
            else {
                resourceToGivePanels[i].SetActive(false);
            }
        }
    }

    private void ActivateReceivedLayout(int resourceSustractedID, int resourceGivenID) {
        welcomeLayout.SetActive(false);
        tradeLayout.SetActive(false);
        receivedLayout.SetActive(true);
        notEnoughResourceLayout.SetActive(false);
        notEnoughSpaceLayout.SetActive(false);

        for(int i = 0; i < 3; i++) {
            if(i == resourceSustractedID) {
                resourceSustractedPanels[i].SetActive(true);
            }
            else {
                resourceSustractedPanels[i].SetActive(false);
            }
        }

        for(int i = 0; i < 4; i++) {
            if(i == resourceGivenID) {
                resourceGivenPanels[i].SetActive(true);
            }
            else {
                resourceGivenPanels[i].SetActive(false);
            }
        }
    }

    public void CallToExchange() {
        int resourceToSustractID;
        int quantityToSustract;
        int resourceToGiveID;
        int quantityToGive;

        resourceToSustractID = GetActiveResourceToSustractPanel();
        quantityToSustract = GetResourceQuantity(resourceToSustractID);
        
        resourceToGiveID = GetActiveResourceToGivePanel();
        quantityToGive = GetResourceQuantity(resourceToGiveID);
        
        Exchange(resourceToGiveID, quantityToGive, resourceToSustractID, quantityToSustract);
    }

    private int GetActiveResourceToSustractPanel() {
        for(int i = 0; i < 3; i++) {
            if(resourceToSustractPanels[i].activeInHierarchy) {
                return i;
            }
        }
        return 1000;
    }

    private int GetActiveResourceToGivePanel() {
        for(int i = 0; i < 4; i++) {
            if(resourceToGivePanels[i].activeInHierarchy) {
                return i;
            }
        }
        return 1000;
    }

    private int GetResourceQuantity(int resourceID) {
        switch(resourceID) {
            case 0:
                return(100);
            break;
            case 1:
                return(10);
            break;
            case 2:
                return(1);
            break;
            case 3:
                return(10);
            break;
            default:
                return(10000);
            break;
        }
    }

    private void ActivateNotEnoughResourceLayout() {
        welcomeLayout.SetActive(false);
        tradeLayout.SetActive(false);
        receivedLayout.SetActive(false);
        notEnoughResourceLayout.SetActive(true);
        notEnoughSpaceLayout.SetActive(false);
    }

    private void ActivateNotEnoughSpaceLayout() {
        welcomeLayout.SetActive(false);
        tradeLayout.SetActive(false);
        receivedLayout.SetActive(false);
        notEnoughResourceLayout.SetActive(false);
        notEnoughSpaceLayout.SetActive(true);
    }

    public void OnClickIronForWood() {
        ActivateTradeLayout(0, 1);
    }

    public void OnClickGoldForWood() {
        ActivateTradeLayout(0, 2);
    }

    public void OnClickWoodForIron() {
        ActivateTradeLayout(1, 0);
    }

    public void OnClickGoldForIron() {
        ActivateTradeLayout(1, 2);
    }

    public void OnClickWoodForGold() {
        ActivateTradeLayout(2, 0);
    }

    public void OnClickIronForGold() {
        ActivateTradeLayout(2, 1);
    }

    public void OnClickWoodForFuel() {
        ActivateTradeLayout(3, 0);
    }

    public void OnClickIronForFuel() {
        ActivateTradeLayout(3, 1);
    }

    public void OnClickGoldForFuel() {
        ActivateTradeLayout(3, 2);
    }

    // Call this function on every OnClick of the trade house interface
    public void Exchange(int resourceToGiveID, int quantityToGive, int resourceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            if (XmlManager.instance.ThereIsEnoughSpace(resourceToGiveID, quantityToGive))
            {
                XmlManager.instance.IncreaseResource(resourceToGiveID, quantityToGive);
                XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract * -1);
                ActivateReceivedLayout(resourceToSustractID, resourceToGiveID);
            }
            else
            {
                ActivateNotEnoughSpaceLayout();
            }
        }
        else
        {
            ActivateNotEnoughResourceLayout();
        }
    }
}
