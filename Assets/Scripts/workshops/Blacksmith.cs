using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    private string[] welcome = { "Buenas, bienvenido a la herreria", "Aqui podras mejorar tu globo al nivel " };
    private string[] hasBalloon = { "Ya no tengo nada mas que ofrecerte", "Ya has comprado este globo" };
    private string[] notEnoughResources = { "No tienes la cantidad de recursos necesarios para esta compra" };
    private string[] congratulations = { "Felicidades por tu nueva compra", "continua con tu viaje" };
    private string balloonName;
    public bool justStartedShouldBeFalse = false;
    public bool doOnlyOnce = true;
    public bool shouldOpenInterface = true;
    public bool once = true;
    [SerializeField] private string ballonAvailable;
    [SerializeField] private GameObject blacksmithInterface;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject blacksmithHabitant;

    // Start is called before the first frame update
    void Start()
    {
        HasBalloon();
    }

    // Update is called once per frame
    void Update()
    {
        if (blacksmithInterface.activeInHierarchy)
        {
            blacksmithHabitant.GetComponent<DialogActivator>().canActivate = false;
        }
        else
        {
            blacksmithHabitant.GetComponent<DialogActivator>().canActivate = true;
        }
    }

    // ShouldOpenInterface() is called in Dialog Manager when the conversation is finished
    public void ShouldOpenInterface()
    {
        if (shouldOpenInterface)
        {
            // Open interface
            blacksmithInterface.SetActive(true);
        }
        else if (blacksmithHabitant.GetComponent<DialogActivator>().lines == notEnoughResources)
        {
            SetWelcomeLines();
            shouldOpenInterface = true;
        }
    }

    // Call this function from the onClick of the Blacksmith interface
    public void BuyBalloon(string balloonToBuy, int resourceToSustractID, int quantityToSustract)
    {

        if (doOnlyOnce)
        {
            welcome[1] += balloonName;
            doOnlyOnce = false;
        }

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();



        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            // Sustract the balloon cost from the files
            XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract * -1);

            // Add new balloon
            XmlManager.instance.AddBalloon(balloonToBuy);

            //Destroy the previous balloon and create the new object
            // if (BalloonManager.instance != null && EssentialsLoader.instance != null)
            // {
            //     Destroy(FindObjectOfType<BalloonManager>());

            //     EssentialsLoader.instance.LoadBalloon();
            // }

            blacksmithInterface.SetActive(false);
            Debug.Log("Desactivando interfaz");

            HasBalloon();

            blacksmithHabitant.GetComponent<DialogActivator>().lines = congratulations;
            shouldOpenInterface = false;
            blacksmithHabitant.GetComponent<Blacksmith>().justStartedShouldBeFalse = true;
            DialogManager.instance.ShowDialog(congratulations);
            dialogBox.SetActive(true);
        }
        else
        {
            // Change blacksmith dialog to say
            blacksmithInterface.SetActive(false);
            Debug.Log("Desactivando interfaz");

            blacksmithHabitant.GetComponent<DialogActivator>().lines = notEnoughResources;
            shouldOpenInterface = false;
            if (once)
            {
                blacksmithHabitant.GetComponent<Blacksmith>().justStartedShouldBeFalse = true;
                once = false;
            }
            DialogManager.instance.ShowDialog(notEnoughResources);
            dialogBox.SetActive(true);

        }
    }

    public void HasBalloon()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // Check if the player already has the balloon
        if (gameData.DoesHaveBalloon(ballonAvailable))
        {
            // Change blacksmith dialog to say
            blacksmithHabitant.GetComponent<DialogActivator>().lines = hasBalloon;
            blacksmithHabitant.GetComponent<Blacksmith>().justStartedShouldBeFalse = false;
            shouldOpenInterface = false;
        }
    }

    public void SetWelcomeLines()
    {
        blacksmithHabitant.GetComponent<DialogActivator>().lines = welcome;
        blacksmithHabitant.GetComponent<Blacksmith>().justStartedShouldBeFalse = false;
        blacksmithHabitant.GetComponent<Blacksmith>().shouldOpenInterface = true;
    }

    public void BackButton()
    {
        blacksmithInterface.SetActive(false);
    }

    public void onClickWoodBlacksmith1()
    {

        balloonName = "2";
        BuyBalloon("balloonLvl2", 0, 500);
    }

    public void onClickIronBlacksmith1()
    {
        balloonName = "2";
        BuyBalloon("balloonLvl2", 1, 50);
    }

    public void onClickGoldBlacksmith1()
    {
        balloonName = "2";
        BuyBalloon("balloonLvl2", 2, 5);
    }

    public void OnClickWoodBlacksmith2()
    {
        balloonName = "3";
        BuyBalloon("balloonLvl3", 0, 1000);
    }

    public void OnClickIronBlacksmith2()
    {
        balloonName = "3";
        BuyBalloon("balloonLvl3", 1, 100);
    }

    public void OnClickGoldBlacksmith2()
    {
        balloonName = "3";
        BuyBalloon("balloonLvl3", 0, 10);
    }
}
