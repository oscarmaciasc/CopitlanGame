using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("gameData")]
public class GameData
{
    [XmlAttribute("name")]
    public string name { get; set; }

    [XmlAttribute("gender")]
    public bool isWoman { get; set; }

    [XmlElement("TimePlayed")]
    public TimePlayed timePlayed { get; set; }

    [XmlElement("TimeWalked")]
    public TimeWalked timeWalked { get; set; }

    [XmlElement("HappinessPercentage")]
    public HappinessPercentage happinessPercentage { get; set; }

    [XmlArray("Resources")]
    public Resource[] resource { get; set; }

    [XmlArray("Permissions")]
    public Permission[] permission { get; set; }

    [XmlArray("Flutes")]
    public Flute[] flute { get; set; }

    [XmlElement("Balloon")]
    public Balloon balloon { get; set; }

    [XmlElement("MusicalMasteryLvl")]
    public MusicalMasteryLvl musicalMasteryLvl { get; set; }

    [XmlArray("MusicSheets")]
    public MusicSheet[] musicSheet { get; set; }

    [XmlArray("AudienceResults")]
    public AudienceResult[] audienceResult { get; set; }

    [XmlArray("MineEntrances")]
    public MineEntrance[] mineEntrance { get; set; }

    [XmlArray("DirigentEntrances")]
    public DirigentEntrance[] dirigentEntrance { get; set; }

    [XmlElement("Collectables")]
    public Collectable collectable { get; set; }

    [XmlArray("HabitantResults")]
    public HabitantResult[] habitantResult { get; set; }

    [XmlArray("HabitantsInteracted")]
    public HabitantInteracted[] habitantInteracted { get; set; }


    public GameData() { }

    // Player Initialization 
    public GameData(string playerName, bool playerGender)
    {
        this.collectable = new Collectable();

        this.timePlayed = new TimePlayed();
        
        this.timeWalked = new TimeWalked();

        this.happinessPercentage = new HappinessPercentage();

        this.resource = new Resource[4];
        this.resource[0] = new Resource();
        this.resource[1] = new Resource();
        this.resource[2] = new Resource();
        this.resource[3] = new Resource();

        this.permission = new Permission[1];
        this.permission[0] = new Permission();

        this.flute = new Flute[1];
        this.flute[0] = new Flute();

        this.balloon = new Balloon();

        this.musicalMasteryLvl = new MusicalMasteryLvl();

        this.musicSheet = new MusicSheet[1];
        this.musicSheet[0] = new MusicSheet();

        this.audienceResult = new AudienceResult[4];
        this.audienceResult[0] = new AudienceResult();
        this.audienceResult[1] = new AudienceResult();
        this.audienceResult[2] = new AudienceResult();
        this.audienceResult[3] = new AudienceResult();

        this.mineEntrance = new MineEntrance[3];
        this.mineEntrance[0] = new MineEntrance();
        this.mineEntrance[1] = new MineEntrance();
        this.mineEntrance[2] = new MineEntrance();

        this.dirigentEntrance = new DirigentEntrance[3];
        this.dirigentEntrance[0] = new DirigentEntrance();
        this.dirigentEntrance[1] = new DirigentEntrance();
        this.dirigentEntrance[2] = new DirigentEntrance();

        habitantInitializer();
        habitantInteractedInitializer();

        this.name = playerName;
        this.collectable.quantity = 0;
        this.isWoman = playerGender;
        this.timePlayed.time = 0f;
        this.timeWalked.time = 0f;
        this.happinessPercentage.percentage = 0;
        this.resource[0].name = "wood";
        this.resource[0].quantity = 0;
        this.resource[1].name = "iron";
        this.resource[1].quantity = 0;
        this.resource[2].name = "gold";
        this.resource[2].quantity = 0;
        this.resource[3].name = "fuel";
        this.resource[3].quantity = 0;
        this.permission[0].name = "outterCircle";
        this.flute[0].name = "woodenFlute";
        this.flute[0].isByDefault = true;
        this.balloon.name = "balloonLvl1";
        this.musicalMasteryLvl.name = "apprentice";
        this.musicSheet[0].name = "partiture1";
        this.audienceResult[0].name = "kasakirResult";
        this.audienceResult[0].result = 0;
        this.audienceResult[1].name = "quizaniResult";
        this.audienceResult[1].result = 0;
        this.audienceResult[2].name = "naranResult";
        this.audienceResult[2].result = 0;
        this.audienceResult[3].name = "necalliResult";
        this.audienceResult[3].result = 0;
        this.mineEntrance[0].name = "tecalliEntrance";
        this.mineEntrance[0].shouldBeActive = false;
        this.mineEntrance[1].name = "acanEntrance";
        this.mineEntrance[1].shouldBeActive = false;
        this.mineEntrance[2].name = "setiEntrance";
        this.mineEntrance[2].shouldBeActive = false;
        this.dirigentEntrance[0].name = "kasakirEntrance";
        this.dirigentEntrance[0].shouldBeActive = false;
        this.dirigentEntrance[1].name = "quizaniEntrance";
        this.dirigentEntrance[1].shouldBeActive = false;
        this.dirigentEntrance[2].name = "naranEntrance";
        this.dirigentEntrance[2].shouldBeActive = false;
    }

    public bool DoesHavePermit(string permitType)
    {
        for (int i = 0; i < permission.Length; i++)
        {
            if (permitType == permission[i].name)
            {
                return true;
            }
        }

        return false;
    }

    public bool DoesHavePartiture(string partitureName)
    {
        for (int i = 0; i < musicSheet.Length; i++)
        {
            if (partitureName == musicSheet[i].name)
            {
                return true;
            }
        }

        return false;
    }
    public bool DoesHaveBalloon(string balloonName)
    {
        if (balloon.name == balloonName)
        {
            return true;
        }

        return false;
    }

    public bool DoesHaveFlute(string fluteName)
    {
        for (int i = 0; i < flute.Length; i++)
        {
            if (flute[i].name == fluteName)
            {
                return true;
            }
        }

        return false;
    }

    public bool DoesHaveAllCollectables()
    {
        if(collectable.quantity == 20)
        {
            return true;
        }

        return false;
    }

    public bool DoesHaveMusicalMasteryLevel(string nombre)
    {
        if (musicalMasteryLvl.name == nombre)
        {
            return true;
        }

        return false;
    }

    public int GetAudienceResult(string audienceName)
    {
        for (int i = 0; i < 4; i++)
        {
            if (audienceResult[i].name == audienceName)
            {
                return audienceResult[i].result;
            }
        }
        return 1000;
    }

    public string GetMusicalMasteryLevel()
    {
        if (musicalMasteryLvl.name == "apprentice")
        {
            return "Aprendiz";
        }
        else if (musicalMasteryLvl.name == "experienced")
        {
            return "Diestro";
        }
        else if (musicalMasteryLvl.name == "master")
        {
            return "Maestro";
        }
        else if (musicalMasteryLvl.name == "legend")
        {
            return "Leyenda";
        }

        return "none";
    }

    public int GetCurrentResource(int id)
    {
        for (int i=0; i < resource.Length; i++)
        {
            if(i == id)
            {
                return resource[i].quantity;
            }
        }

        return 1000;
    }

    public string GetBalloonName()
    {
        return balloon.name;
    }

    public int GetBalloonCapacity() {
        string balloonName;
        
        for(int i = 0; i < 3; i++) {
            balloonName = "balloonLvl";
            balloonName += (i + 1).ToString();
            if(GetBalloonName() == balloonName) {
                return 10 * (i + 2);
            }
        }
        
        return 1000;
    }

    public void habitantInitializer()
    {
        this.habitantResult = new HabitantResult[80];

        for (int i = 0; i < 80; i++)
        {
            this.habitantResult[i] = new HabitantResult();
            this.habitantResult[i].id = i;
            this.habitantResult[i].result = 0;
        }
    }

    public void habitantInteractedInitializer()
    {
        this.habitantInteracted = new HabitantInteracted[186];

         for (int i = 0; i < 186; i++)
        {
            this.habitantInteracted[i] = new HabitantInteracted();
            this.habitantInteracted[i].id = i;
            this.habitantInteracted[i].interacted = false;
        }
    }

    public int GetHabitantHappinesPercentage()
    {
        int habitantHappinessPercentage = 0;

        for (int i = 0; i < this.habitantResult.Length; i++)
        {
            habitantHappinessPercentage += habitantResult[i].result;
        }

        habitantHappinessPercentage /= 80;

        return habitantHappinessPercentage;
    }

    public int GetAndSaveHappinesPercentage()
    {
        double necalliPercentage = GetAudienceResult("necalliResult") * (0.8);
        double habitantPercentage = GetHabitantHappinesPercentage() * (0.2);
        int res = (int)necalliPercentage + (int)habitantPercentage;

        XmlManager.instance.UpdateHappinessPercentage(res);

        return res;
    }
}
