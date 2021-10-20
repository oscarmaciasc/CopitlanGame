using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitant80Partiture3 : MonoBehaviour
{
    public static Habitant80Partiture3 instance;
    public bool conversationFinished = false;
    public bool canCheck = true;
    private string[] lines = { "Buen dia, espero te haya servido el regalo" };
    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.DoesHavePartiture("partiture3"))
        {
            this.gameObject.GetComponent<DialogActivator>().lines = lines;
            canCheck = false;
        }
        else
        {
            canCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();
        if (gameData.DoesHavePartiture("partiture3"))
        {
            this.gameObject.GetComponent<DialogActivator>().lines = lines;
        }
    }

    public void AddPartiture3()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (conversationFinished && !gameData.DoesHavePartiture("partiture3"))
        {
            Debug.Log("Regalando Partitura3");
            XmlManager.instance.AddPartiture3();
        }
    }
}
