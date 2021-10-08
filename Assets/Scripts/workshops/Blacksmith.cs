using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call this function from the onClick of the Blacksmith interface
    public void BuyBalloon(string balloonToBuy, int resourceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resourceToSustractID) >= quantityToSustract)
        {
            // Check if the player already has the balloon
            if(gameData.DoesHaveBalloon(balloonToBuy))
            {
                // Change blacksmith dialog to say
                Debug.Log("No se realizó la compra porque ya tienes el globo");
            } else
            {
                // Sustract the balloon cost from the files
                XmlManager.instance.IncreaseResource(resourceToSustractID, quantityToSustract);

                // Add new balloon
                XmlManager.instance.AddBalloon(balloonToBuy);
            }

        }
        else
        {
            // Change blacksmith dialog to saylog
            Debug.Log("No tienes la cantidad de recursos necesarios para este intercambio");
        }
    }
}
