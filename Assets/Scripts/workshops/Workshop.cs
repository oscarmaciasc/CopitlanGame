using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] private GameObject workshopInterface;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private string fluteAvailable;
    [SerializeField] private GameObject workshopHabitant;
    private string[] hasFlute = { "Ya no tengo nada mas que ofrecerte", "Ya has comprado esta flauta" };
    private string[] notEnoughResources = { "No tienes la cantidad de recursos necesarios para esta compra" };
    private string[] welcome = { "Buenas, bienvenido al taller", "Aqui podras mejorar tu flauta" };
    private string[] congratulations = { "Felicidades por tu nueva compra", "continua con tu viaje" };
    public bool shouldOpenInterface = true;
    public bool justStartedShouldBeFalse = false;

    // Start is called before the first frame update
    void Start()
    {
        HasFlute();
    }

    // Update is called once per frame
    void Update()
    {
        if (workshopInterface.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
    }

    public void BuyFlute(string fluteToBuy, int resourceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            // Sustract the flute cost from the files
            XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract * -1);

            // Add new balloon
            XmlManager.instance.AddFlute(fluteToBuy);

            workshopInterface.SetActive(false);

            HasFlute();

            workshopHabitant.GetComponent<DialogActivator>().lines = congratulations;
            shouldOpenInterface = false;
            workshopHabitant.GetComponent<Workshop>().justStartedShouldBeFalse = true;
            DialogManager.instance.ShowDialog(congratulations);
            dialogBox.SetActive(true);


        }
        else
        {
            // Change artisan dialog to saylog
            workshopInterface.SetActive(false);

            workshopHabitant.GetComponent<DialogActivator>().lines = notEnoughResources;
            shouldOpenInterface = false;
            workshopHabitant.GetComponent<Workshop>().justStartedShouldBeFalse = true;
            DialogManager.instance.ShowDialog(notEnoughResources);
            dialogBox.SetActive(true);
        }
    }


    public void GetGoldenFlute()
    {
        // Read from files if collectables array has 19 elements, if its true then add the goldenFlute to the player files when talking to any workshop comerciant.
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.DoesHaveAllCollectables())
        {
            // This is to avoid having many goldenFlutes saved and do it only once
            if (!gameData.DoesHaveFlute("goldenFlute"))
            {
                // Change workshop habitant dialog lines to say the goldenFluteReward lines
                XmlManager.instance.AddFlute("goldenFlute");
            }
        }
    }

    public void HasFlute()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // Check if the player already has the balloon
        if (gameData.DoesHaveFlute(fluteAvailable))
        {
            // Change blacksmith dialog to say
            workshopHabitant.GetComponent<DialogActivator>().lines = hasFlute;
            shouldOpenInterface = false;
        }
    }

    // ShouldOpenInterface() is called in Dialog Manager when the conversation is finished
    public void ShouldOpenInterface()
    {
        if (shouldOpenInterface)
        {
            // Open interface
            workshopInterface.SetActive(true);
        }
        else if (workshopHabitant.GetComponent<DialogActivator>().lines == notEnoughResources)
        {
            SetWelcomeLines();
            shouldOpenInterface = true;
        }
    }

    public void SetWelcomeLines()
    {
        workshopHabitant.GetComponent<DialogActivator>().lines = welcome;
        workshopHabitant.GetComponent<Workshop>().justStartedShouldBeFalse = false;
    }

    public void BackButton()
    {
        workshopInterface.SetActive(false);
    }

    public void OnClickWoodWorkshop1()
    {
        BuyFlute("woodenIronFlute", 0, 500);
    }

    public void OnClickIronWorkshop1()
    {
        BuyFlute("woodenIronFlute", 1, 50);
    }

    public void OnClickGoldWorkshop1()
    {
        BuyFlute("woodenIronFlute", 2, 5);
    }

    public void OnClickWoodWorkshop2()
    {
        BuyFlute("ironFlute", 0, 4000);
    }

    public void OnClickIronWorkshop2()
    {
        BuyFlute("ironFlute", 1, 400);
    }

    public void OnClickGoldWorkshop2()
    {
        BuyFlute("ironFlute", 2, 40);
    }
}
