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

    public string playerName;
    public Resource[] resources;
    public Permission[] permissions;
    public Flute[] flutes;
    public Balloon[] balloons;
    public GameData gameData;

    // This function also allows us to create a new game, altough any object is empty.
    public void Save()
    {
        // Filling the object arrays

        playerName = "Fabian";

        resources = new Resource[]{
            new Resource { name = "gold", quantity=10},
            new Resource { name = "iron", quantity=5},
            new Resource { name = "wood", quantity=30}
        };

        permissions = new Permission[]{
            new Permission { name = "outterCircle"},
            new Permission { name = "innerCircle"},
            new Permission { name = "triangle"}
        };

        flutes = new Flute[]{
            new Flute { name = "woodenFlute"},
            new Flute { name = "wooden-ironFlute"},
            new Flute { name = "ironFlute"},
            new Flute { name = "goldenFlute"}
        };

        balloons = new Balloon[]{
            new Balloon { name = "balloonLvl1"},
            new Balloon { name = "balloonLvl2"},
            new Balloon { name = "balloonLvl3"}
        };

        gameData = new GameData(playerName, resources, permissions, flutes, balloons);

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream xmlWriter = new FileStream(CurrentDirectory + "/xmlTest.xml", FileMode.Create);
        serializer.Serialize(xmlWriter, gameData);
        xmlWriter.Close();
    }

    public void Load()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream xmlRead = new FileStream(CurrentDirectory + "/xmlTest.xml", FileMode.Open);
        gameData = serializer.Deserialize(xmlRead) as GameData;
        Debug.Log("Nombre del jugador: " + gameData.name +
            "\nRecurso: " + gameData.resource[0].name +
            "\nGlobo: " + gameData.balloon[0].name +
            "\nFlauta: " + gameData.flute[0].name);
        xmlRead.Close();
    }

    public void Delete()
    {
        File.Delete(CurrentDirectory + "/xmlTest.xml");
    }
}
