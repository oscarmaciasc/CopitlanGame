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
    public int gameIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Called when a game is selected in GameSelection interface;
    public void CreateTempFile(int gameIndex)
    {
        TempFile tempFile = new TempFile(gameIndex);

        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlWriter = new FileStream(CurrentDirectory + "/TempFile.xml", FileMode.Create);
        serializer.Serialize(xmlWriter, tempFile);
        xmlWriter.Close();
    }

    // Delete the temporal file
    public void DeleteTempFile()
    {
        File.Delete(CurrentDirectory + "/TempFile.xml");
    }

    // Called when a new game is created
    public bool Create(string name, bool gender)
    {
        int gameIndex = CanCreateGame();
        GameData gameData = new GameData(name, gender);

        if (gameIndex != 0)
        {
            Save(gameIndex, gameData);
            CreateTempFile(gameIndex);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Return the position in wich can create a game file or 0 in case is not posible
    public int CanCreateGame()
    {
        bool[] count = GamesCount();

        if (count[0] == false)
        {
            return 1;
        }
        else if (count[1] == false)
        {
            return 2;
        }
        else if (count[2] == false)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    // This function also allows us to create a new game, altough any object is empty.
    public void Save(int gameIndex, GameData gameData)
    {
        string fileName = "";

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));

        switch (gameIndex)
        {
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

    public void IncreaseResource(int resourceID, int quantityAdded)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        int quantityNew = gameData.resource[resourceID].quantity + quantityAdded;

        gameData.resource[resourceID].quantity = quantityNew;

        Save(gameIndex, gameData);
    }

    public void SaveAudienceResult(int audienceResultID, int result)
    {
        int gameIndex = GetGameIndex();

        GameData gamedata = LoadGame();

        gamedata.audienceResult[audienceResultID].result = result;

        Save(gameIndex, gamedata);
    }

    public void SaveMineEntranceState(int mineEntranceID, bool shouldBeActive)
    {
        int gameIndex = GetGameIndex();

        GameData gamedata = LoadGame();

        gamedata.mineEntrance[mineEntranceID].shouldBeActive = shouldBeActive;

        Save(gameIndex, gamedata);
    }

    public void SaveDirigentEntranceState(int dirigentEntranceID, bool shouldBeActive)
    {
        int gameIndex = GetGameIndex();

        GameData gamedata = LoadGame();

        gamedata.dirigentEntrance[dirigentEntranceID].shouldBeActive = shouldBeActive;

        Save(gameIndex, gamedata);
    }

    public void SaveHabitantsResults(int habitantID, int res)
    {
        Debug.Log("Si entro a la funcion");
        int gameIndex = GetGameIndex();

        GameData gamedata = LoadGame();
        
        gamedata.habitantResult[habitantID].result = res;

        Save(gameIndex, gamedata);
    }

    public void AddPermission(string NewPermission)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        int newLength = gameData.permission.Length + 1;

        Permission[] permissions = new Permission[newLength];

        for (int i = 0; i < gameData.permission.Length; i++)
        {
            permissions[i] = gameData.permission[i];
        }

        permissions[newLength - 1] = new Permission(NewPermission);

        gameData.permission = permissions;

        Save(gameIndex, gameData);
    }

    public void AddMusicalMasteryLvl(string name)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.musicalMasteryLvl.name = name;

        Save(gameIndex, gameData);
    }

    // Fill gameData array
    public GameData[] LoadAllGames()
    {
        bool[] count = GamesCount();
        GameData[] gamesData = new GameData[3];
        GameData gameData = new GameData();

        for (int i = 0; i < 3; i++)
        {
            if (count[i])
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                FileStream xmlRead = new FileStream(CurrentDirectory + "/GameData" + (i + 1) + ".xml", FileMode.Open);
                gameData = serializer.Deserialize(xmlRead) as GameData;
                xmlRead.Close();
                gamesData[i] = gameData;
            }
        }

        return gamesData;
    }

    public GameData LoadGame()
    {
        int index = GetGameIndex();
        bool[] count = GamesCount();
        GameData gameData = new GameData();

        if (count[index - 1])
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            FileStream xmlRead = new FileStream(CurrentDirectory + "/GameData" + index + ".xml", FileMode.Open);
            gameData = serializer.Deserialize(xmlRead) as GameData;
            xmlRead.Close();
        }
        else
        {
            Debug.Log("There is not a game file cabeza huevo");
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
    public bool[] GamesCount()
    {
        bool[] count = new bool[3];

        if (File.Exists(CurrentDirectory + "/GameData1.xml"))
        {
            count[0] = true;
        }
        else
        {
            count[0] = false;
        }

        if (File.Exists(CurrentDirectory + "/GameData2.xml"))
        {
            count[1] = true;
        }
        else
        {
            count[1] = false;
        }

        if (File.Exists(CurrentDirectory + "/GameData3.xml"))
        {
            count[2] = true;
        }
        else
        {
            count[2] = false;
        }

        return count;
    }

    private int GetGameIndex()
    {
        TempFile tempFile = new TempFile();

        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlRead = new FileStream(CurrentDirectory + "/TempFile.xml", FileMode.Open);
        tempFile = serializer.Deserialize(xmlRead) as TempFile;
        xmlRead.Close();

        return tempFile.gameIndex;
    }
}
