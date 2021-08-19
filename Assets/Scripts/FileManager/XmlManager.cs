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
    public string playerName;
    public bool isWoman;
    public Resource[] resources;
    public Permission[] permissions;
    public Flute[] flutes;
    public Balloon[] balloons;
    public MusicalMasteryLvl[] musicalMasteryLvl;
    public MusicSheet[] musicSheets;
    public GameData gameData;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Called when a new game is created
    public void Create() {
        int gameIndex = CanCreateGame();

        if(gameIndex != 0) {
            Save(gameIndex);
        }
        else {
            Debug.Log("No");
            // Activate the "No more than 3 games" panel
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
    public void Save(int gameIndex)
    {
        string fileName = "";

        //*********************Fake********************
        playerName = "Fabian";
        isWoman = false;
        //*********************Fake********************
        
        gameData = new GameData(playerName, isWoman);

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

    public void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream xmlRead = new FileStream(CurrentDirectory + "/GameData1.xml", FileMode.Open);
        gameData = serializer.Deserialize(xmlRead) as GameData;
        Debug.Log("Nombre del jugador: " + gameData.name +
            "\nRecurso: " + gameData.resource[0].name +
            "\nGlobo: " + gameData.balloon[0].name +
            "\nFlauta: " + gameData.flute[0].name);
        xmlRead.Close();
    }

    public void Delete()
    {
        File.Delete(CurrentDirectory + "/GameData1.xml");
    }
    
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
