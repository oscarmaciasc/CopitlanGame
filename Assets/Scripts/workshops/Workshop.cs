using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    [SerializeField] private GameObject workshopInterface;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private string fluteAvailable;
    [SerializeField] private GameObject workshopHabitant;
    [SerializeField] private GameObject confirmationWood;
    [SerializeField] private GameObject confirmationIron;
    [SerializeField] private GameObject confirmationGold;
    private string[] hasFlute = { "Ya no tengo nada mas que ofrecerte", "Ya has comprado esta flauta" };
    private string[] notEnoughResources = { "No tienes la cantidad de recursos necesarios para esta compra" };
    private string[] welcome = { "Buenas, bienvenido al taller", "Aqui podras mejorar tu flauta" };
    private string[] congratulations = { "Felicidades por tu nueva compra", "continua con tu viaje" };
    private string[] goldenFluteReward = { "Vaya, veo que has conseguido los 20 coleccionables", "Felicidades, te obsequio con la flauta de oro", "Una flauta especial y muy fina" };
    public bool shouldOpenInterface = true;
    public bool justStartedShouldBeFalse = false;
    public bool conversationFinished = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (workshopInterface.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }

        GetGoldenFlute();
        HasFlute();
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
            Debug.Log("Si tiene todos los coleccionables");
            // This is to avoid having many goldenFlutes saved and do it only once
            if (!gameData.DoesHaveFlute("goldenFlute"))
            {
                Debug.Log("No se tiene la de oro");
                // Change workshop habitant dialog lines to say the goldenFluteReward lines
                workshopHabitant.GetComponent<DialogActivator>().lines = goldenFluteReward;
                if (conversationFinished)
                {
                    XmlManager.instance.AddFlute("goldenFlute");
                    shouldOpenInterface = false;
                    workshopInterface.SetActive(false);
                }
            }
        }
    }

    public void HasFlute()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();


        if (gameData.DoesHaveFlute("goldenFlute"))
        {

            workshopHabitant.GetComponent<DialogActivator>().lines = welcome;
            workshopHabitant.GetComponent<Workshop>().justStartedShouldBeFalse = false;
        }


        // Check if the player already has the balloon
        if (gameData.DoesHaveFlute(fluteAvailable))
        {
            // Change blacksmith dialog to say
            workshopHabitant.GetComponent<DialogActivator>().lines = hasFlute;
            workshopHabitant.GetComponent<Workshop>().justStartedShouldBeFalse = false;
            shouldOpenInterface = false;
        }
        else if (!gameData.DoesHaveFlute(fluteAvailable))
        {
            shouldOpenInterface = true;
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

    // ************************************************

    public void OnClickConfirmationWoodWorkshop1()
    {
        confirmationWood.SetActive(false);
        BuyFlute("woodenIronFlute", 0, 500);
    }
    public void OnClickWoodWorkshop1()
    {
        confirmationWood.SetActive(true);
        confirmationIron.SetActive(false);
        confirmationGold.SetActive(false);
    }

    public void OnClickConfirmationIronWorkshop1()
    {
        confirmationIron.SetActive(false);
        BuyFlute("woodenIronFlute", 1, 50);
    }

    public void OnClickIronWorkshop1()
    {
        confirmationIron.SetActive(true);
        confirmationWood.SetActive(false);
        confirmationGold.SetActive(false);
    }

    public void OnClickConfirmationGoldWorkshop1()
    {
        confirmationGold.SetActive(false);
        BuyFlute("woodenIronFlute", 2, 5);
    }

    public void OnClickGoldWorkshop1()
    {
        confirmationGold.SetActive(true);
        confirmationWood.SetActive(false);
        confirmationIron.SetActive(false);
    }

    // ************************************************

    public void OnClickConfirmationWoodWorkshop2()
    {
        BuyFlute("ironFlute", 0, 4000);
    }

    public void OnClickConfirmationIronWorkshop2()
    {
        BuyFlute("ironFlute", 1, 400);
    }

    public void OnClickConfirmationGoldWorkshop2()
    {
        BuyFlute("ironFlute", 2, 40);
    }
}
