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

    void OnAplicationQuit()
    {
        WasLoadedAlready(false);
    }

    // Called when a game is selected in GameSelection interface;
    public void CreateTempFile(int gameIndex)
    {
        TempFile tempFile = new TempFile(gameIndex, 1000);

        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlWriter = new FileStream(Application.persistentDataPath + "/TempFile.xml", FileMode.Create);
        serializer.Serialize(xmlWriter, tempFile);
        xmlWriter.Close();
    }

    public void SaveTempFile(TempFile tempFile)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlWriter = new FileStream(Application.persistentDataPath + "/TempFile.xml", FileMode.Create);
        serializer.Serialize(xmlWriter, tempFile);
        xmlWriter.Close();
    }

    public void HouseIDTempFile(int houseID)
    {
        TempFile tempFile = new TempFile();

        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlRead = new FileStream(Application.persistentDataPath + "/TempFile.xml", FileMode.Open);
        tempFile = serializer.Deserialize(xmlRead) as TempFile;
        xmlRead.Close();

        tempFile.houseID = houseID;

        SaveTempFile(tempFile);
    }

    // Delete the temporal file
    public void DeleteTempFile()
    {
        File.Delete(Application.persistentDataPath + "/TempFile.xml");
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

        FileStream xmlWriter = new FileStream(Application.persistentDataPath + fileName, FileMode.Create);
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

    public void IncreaseCollectable(int collectableID)
    {
        int newLength;

        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        Collectable[] collectables;

        if (gameData.collectable != null)
        {
            newLength = gameData.collectable.Length + 1;
            collectables = new Collectable[gameData.collectable.Length + 1];

            // Filling the new array with the previous array values
            for (int i = 0; i < gameData.collectable.Length; i++)
            {
                collectables[i] = gameData.collectable[i];
            }
        }
        else
        {
            newLength = 1;
            collectables = new Collectable[1];
        }

        collectables[newLength - 1] = new Collectable(collectableID, true);

        gameData.collectable = collectables;

        Save(gameIndex, gameData);
    }

    public void AddBalloon(string balloonName)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.balloon.name = balloonName;

        Save(gameIndex, gameData);
    }

    public bool ThereIsEnoughSpace(int resourceID, int quantityAdded)
    {
        int gameIndex = GetGameIndex();
        int maxQuantity = 0;

        GameData gameData = LoadGame();

        switch (resourceID)
        {
            case 0:
                maxQuantity = 5000;
                break;
            case 1:
                maxQuantity = 500;
                break;
            case 2:
                maxQuantity = 50;
                break;
            case 3:
                maxQuantity = gameData.GetBalloonCapacity();
                break;
        }

        if ((gameData.resource[resourceID].quantity + quantityAdded) > maxQuantity)
        {
            return false;
        }

        return true;
    }


    public void SetDefaultFlute(string fluteName)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        for (int i = 0; i < gameData.flute.Length; i++)
        {
            if (i == GetFluteIndex(fluteName))
            {
                gameData.flute[i].isByDefault = true;
            }
            else
            {
                gameData.flute[i].isByDefault = false;
            }
        }

        Save(gameIndex, gameData);
    }

    private int GetFluteIndex(string fluteName)
    {
        if (fluteName == "woodenFlute")
        {
            return 0;
        }
        else if (fluteName == "woodenIronFlute")
        {
            return 1;
        }
        else if (fluteName == "ironFlute")
        {
            return 2;
        }
        else if (fluteName == "goldenFlute")
        {
            return 3;
        }
        return 1000;
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

    public void SaveInteractedHabitantState(int index)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.habitantInteracted[index].interacted = true;

        Save(gameIndex, gameData);
    }

    public bool InteractedTrue(int index)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        if (gameData.habitantInteracted[index].interacted)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public void UpdateHappinessPercentage(int percentage)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.happinessPercentage.percentage = percentage;

        Save(gameIndex, gameData);
    }

    public void AddPermission(string newPermission)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        int newLength = gameData.permission.Length + 1;

        Permission[] permissions = new Permission[newLength];

        for (int i = 0; i < gameData.permission.Length; i++)
        {
            permissions[i] = gameData.permission[i];
        }

        permissions[newLength - 1] = new Permission(newPermission);

        gameData.permission = permissions;

        Save(gameIndex, gameData);
    }

    public void AddPartiture(string partitureName)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        int newLength = gameData.musicSheet.Length + 1;

        MusicSheet[] musicSheets = new MusicSheet[newLength];

        for (int i = 0; i < gameData.musicSheet.Length; i++)
        {
            musicSheets[i] = gameData.musicSheet[i];
        }

        musicSheets[newLength - 1] = new MusicSheet(partitureName);

        gameData.musicSheet = musicSheets;

        Save(gameIndex, gameData);
    }

    public void AddFlute(string newFlute)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        int newLength = gameData.flute.Length + 1;

        Flute[] flutes = new Flute[newLength];

        for (int i = 0; i < gameData.flute.Length; i++)
        {
            flutes[i] = gameData.flute[i];
        }

        // We set the new flute name and default state to false
        flutes[newLength - 1] = new Flute(newFlute, false);

        gameData.flute = flutes;

        Save(gameIndex, gameData);
    }

    public void AddGoldenFlute()
    {
        int gameIndex = GetGameIndex();
        GameData gameData = LoadGame();

        Flute[] flutes = new Flute[3];

        flutes[gameData.flute.Length + 1] = new Flute("goldenFlute", false);

        gameData.flute = flutes;

        Save(gameIndex, gameData);
    }

    public void AddMusicalMasteryLvl(string name)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.musicalMasteryLvl.name = name;

        Save(gameIndex, gameData);
    }

    public void UpdateTimePlayed(float timePlayed)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.timePlayed.time += timePlayed;

        Save(gameIndex, gameData);
    }

    public void UpdateTimeWalked(float timeWalked)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.timeWalked.time += timeWalked;

        Save(gameIndex, gameData);
    }

    public void UpdateTimeWalkedBalloon(float timeWalkedBalloon)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.timeBalloon.time += timeWalkedBalloon;

        Save(gameIndex, gameData);
    }

    public void UpdateLastSaved(string scene, float coordX, float coordY)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.lastSaved.scene = scene;

        gameData.lastSaved.coordX = coordX;

        gameData.lastSaved.coordY = coordY;

        Save(gameIndex, gameData);
    }

    public void WasLoadedAlready(bool wasLoadedSalready)
    {
        int gameIndex = GetGameIndex();

        GameData gameData = LoadGame();

        gameData.lastSaved.wasLoadedAlready = wasLoadedSalready;

        Save(gameIndex, gameData);
    }

    // Fill gameData array
    public GameData[] LoadAllGames()
    {
        bool[] count = GamesCount();
        GameData[] gamesData = new GameData[3];
        GameData gameData = new GameData();

        Debug.Log("Path: " + Application.persistentDataPath);

        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }

        for (int i = 0; i < 3; i++)
        {
            if (count[i])
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameData));
                FileStream xmlRead = new FileStream(Application.persistentDataPath + "/GameData" + (i + 1) + ".xml", FileMode.Open);
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
            FileStream xmlRead = new FileStream(Application.persistentDataPath + "/GameData" + index + ".xml", FileMode.Open);
            gameData = serializer.Deserialize(xmlRead) as GameData;
            xmlRead.Close();
        }
        else
        {
            Debug.Log("There is not a game file");
        }

        return gameData;
    }

    // Delete a file
    public void Delete(int index)
    {
        string filePath = "/GameData" + index + ".xml";
        File.Delete(Application.persistentDataPath + filePath);
    }

    // Return an bool array in wich are the positions of existent games
    public bool[] GamesCount()
    {
        bool[] count = new bool[3];

        if (File.Exists(Application.persistentDataPath + "/GameData1.xml"))
        {
            count[0] = true;
        }
        else
        {
            count[0] = false;
        }

        if (File.Exists(Application.persistentDataPath + "/GameData2.xml"))
        {
            count[1] = true;
        }
        else
        {
            count[1] = false;
        }

        if (File.Exists(Application.persistentDataPath + "/GameData3.xml"))
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
        FileStream xmlRead = new FileStream(Application.persistentDataPath + "/TempFile.xml", FileMode.Open);
        tempFile = serializer.Deserialize(xmlRead) as TempFile;
        xmlRead.Close();

        return tempFile.gameIndex;
    }

    public int GetHouseId()
    {
        TempFile tempFile = new TempFile();

        XmlSerializer serializer = new XmlSerializer(typeof(TempFile));
        FileStream xmlRead = new FileStream(Application.persistentDataPath + "/TempFile.xml", FileMode.Open);
        tempFile = serializer.Deserialize(xmlRead) as TempFile;
        xmlRead.Close();

        return tempFile.houseID;
    }
}
