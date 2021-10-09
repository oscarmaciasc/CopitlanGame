using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{

    public bool conversationFinished = true;
    private string[] hasBalloon = {"No se realizo la compra porque ya tienes el globo"};
    [SerializeField] private string ballonAvailable;
    [SerializeField] private GameObject blacksmithInterface;
    [SerializeField] private GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    // HasBalloon() is called in Dialog Manager when the conversation is finished
    public void HasBalloon(GameObject habitant)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();
        
        // Check if the player already has the balloon
        if (gameData.DoesHaveBalloon(ballonAvailable))
        {
            // Change blacksmith dialog to say
            habitant.GetComponent<DialogActivator>().lines = hasBalloon;
        }
        else
        {
            if(conversationFinished)
            {
                // Open interface
                blacksmithInterface.SetActive(true);

                Debug.Log("Activando la interfaz de herreria");

                // We cant talk in this point
            }
        }
    }

    // Call this function from the onClick of the Blacksmith interface
    public void BuyBalloon(string balloonToBuy, int resourceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            // Sustract the balloon cost from the files
            XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract);

            // Add new balloon
            XmlManager.instance.AddBalloon(balloonToBuy);
        }
        else
        {
            // Change blacksmith dialog to saylog
            Debug.Log("No tienes la cantidad de recursos necesarios para este intercambio");
        }
    }
}
