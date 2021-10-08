using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyFlute(string fluteToBuy, int resourceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            // Check if the player already has the balloon
            if (gameData.DoesHaveFlute(fluteToBuy))
            {
                // Change blacksmith dialog to say
                Debug.Log("No se realizó la compra porque ya tienes la flauta");
            }
            else
            {
                // Sustract the flute cost from the files
                XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract);

                // Add new balloon
                XmlManager.instance.AddFlute(fluteToBuy);
            }

        }
        else
        {
            // Change blacksmith dialog to saylog
            Debug.Log("No tienes la cantidad de recursos necesarios para esta compra");
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
}
