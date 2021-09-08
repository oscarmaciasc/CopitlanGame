using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.Threading;
using UnityEngine.SceneManagement;
using System;
using static System.Environment;


[XmlRoot("XmlManager")]
public class XmlManager : MonoBehaviour
{

    public static XmlManager instance;
    private string playerName;
    private bool isWoman;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Called when a new game is created
    public bool Create(string name, bool gender) {
        int gameIndex = CanCreateGame();
        GameData gameData = new GameData(name, gender);

        if(gameIndex != 0) {
            Save(gameIndex, gameData);
            return true;
        }
        else {
            return false;
        }
    }

    // Return the position in wich can create a game file or 0 in case is not posible
    public int CanCreateGame() {
        bool[] count = GamesCount();
        
        if(count[0] == false) {
            return 1;
        }
        else if (count[1] == false) {
            return 2;
        }
        else if (count[2] == false) {
            return 3;
        }
        else {
            return 0;
        }
    }

    // This function also allows us to create a new game, altough any object is empty.
    public void Save(int gameIndex, GameData gameData)
    {
        string fileName = "";

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        switch (gameIndex) {
            case 1:
                fileName = "/GameData1.xml";
            break;
            case 2:
                fileName = "/GameData2.xml";
            break;
            case 3:
                fileName = "/GameData3.xml";
            break;
        }

        FileStream xmlWriter = new FileStream(CurrentDirectory + fileName, FileMode.Create);
        serializer.Serialize(xmlWriter, gameData);
        xmlWriter.Close();
    }
    
    public void IncreaseResource(int gameIndex, int resourceID, int quantityAdded)
    {
        GameData gameData = LoadGame(gameIndex);
        
        Debug.Log(gameData.name);
        Debug.Log(gameData.resource[resourceID].quantity + " + " + quantityAdded);
        gameData.name += "a";

        int quantityNew = gameData.resource[resourceID].quantity + quantityAdded;

        gameData.resource[resourceID].quantity = quantityNew;

        Save(gameIndex, gameData);
    }

    // Fill gameData array
    public GameData[] LoadAllGames()
    {
        bool[] count = GamesCount();
        GameData[] gamesData = new GameData[3];
        GameData gameData = new GameData();

        for(int i = 0; i < 3; i++) {
            if(count[i]) {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                FileStream xmlRead = new FileStream(CurrentDirectory + "/GameData" + (i + 1) + ".xml", FileMode.Open);
                gameData = serializer.Deserialize(xmlRead) as GameData;
                xmlRead.Close();
                gamesData[i] = gameData;
            }
        }

        return gamesData;
    }

    public GameData LoadGame(int index)
    {
        bool[] count = GamesCount();
        GameData gameData = new GameData();

        if(count[index]) {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream xmlRead = new FileStream(CurrentDirectory + "/GameData" + (index + 1) + ".xml", FileMode.Open);
            gameData = serializer.Deserialize(xmlRead) as GameData;
            xmlRead.Close();
        }
        else {
            Debug.Log("There is not an file game cabeza huevo");
        }

        return gameData;
    }

    // Delete a file
    public void Delete(int index)
    {
        string filePath = "/GameData" + index + ".xml";
        File.Delete(CurrentDirectory + filePath);
    }
    
    // Return an bool array in wich are the positions of existent games
    public bool[] GamesCount() {
        bool[] count = new bool[3];

        if (File.Exists(CurrentDirectory + "/GameData1.xml")) {
            count[0] = true;
        }
        else {
            count[0] = false;
        }

        if (File.Exists(CurrentDirectory + "/GameData2.xml")) {
            count[1] = true;
        }
        else {
            count[1] = false;
        }

        if (File.Exists(CurrentDirectory + "/GameData3.xml")) {
            count[2] = true;
        }
        else {
            count[2] = false;
        }

        return count;
    }
}
