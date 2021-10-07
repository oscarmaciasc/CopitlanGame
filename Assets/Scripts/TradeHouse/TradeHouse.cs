using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeHouse : MonoBehaviour
{
    public int currentWood;
    public int currentIron;
    public int currentGold;
    public int currentFuel;


    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Call this function on every OnClick of the trade house interface
    public void Exchange(int resourceToGiveID, int quantityToGive, int resorceToSustractID, int quantityToSustract)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        // If we have the necessary amount of resource we do the trade
        if (gameData.GetCurrentResource(resorceToSustractID) >= quantityToSustract)
        {
            XmlManager.instance.IncreaseResource(resourceToGiveID, quantityToGive);
            XmlManager.instance.IncreaseResource(resorceToSustractID, quantityToSustract);
        }
        else
        {
            // Show an interface message, not a debug.log
            Debug.Log("No tienes la cantidad de recursos necesarios para este intercambio");
        }
    }
}
