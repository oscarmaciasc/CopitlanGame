using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("gameData")]
public class GameData
{
    [XmlAttribute ("name")]
    public string name { get; set; }

    [XmlAttribute ("gender")]
    public bool isWoman { get; set; }

    [XmlArray ("Resources")]
    public Resource[] resource { get; set; }
    
    [XmlArray ("Permissions")]
    public Permission[] permission { get; set; }
    
    [XmlArray ("Flutes")]
    public Flute[] flute { get; set; }

    [XmlArray ("Balloons")]
    public Balloon[] balloon { get; set; }

    [XmlArray ("MusicalMasteryLvl")]
    public MusicalMasteryLvl[] musicalMasteryLvl { get; set; }

    [XmlArray ("MusicSheets")]
    public MusicSheet[] musicSheet { get; set; }

    [XmlArray ("AudienceResults")]
    public AudienceResult[] audienceResult { get; set; }

    public GameData(){}

    // Player Initialization 
    public GameData(string playerName, bool playerGender) 
    {
        this.resource = new Resource[4];
        this.resource[0] = new Resource();
        this.resource[1] = new Resource();
        this.resource[2] = new Resource();
        this.resource[3] = new Resource();

        this.permission = new Permission[1];
        this.permission[0] = new Permission();

        this.flute = new Flute[1];
        this.flute[0] = new Flute();

        this.balloon = new Balloon[1];
        this.balloon[0] = new Balloon();

        this.musicalMasteryLvl = new MusicalMasteryLvl[1];
        this.musicalMasteryLvl[0] = new MusicalMasteryLvl();

        this.musicSheet = new MusicSheet[1];
        this.musicSheet[0] = new MusicSheet();

        this.audienceResult = new AudienceResult[3];
        this.audienceResult[0] = new AudienceResult();
        this.audienceResult[1] = new AudienceResult();
        this.audienceResult[2] = new AudienceResult();


        this.name = playerName;
        this.isWoman = playerGender;
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
        this.balloon[0].name = "balloonLvl1";
        this.musicalMasteryLvl[0].name = "apprentice";
        this.musicSheet[0].name = "partiture1";
        this.audienceResult[0].name = "kasakirResult";
        this.audienceResult[0].result = 0;
        this.audienceResult[1].name = "quizaniResult";
        this.audienceResult[1].result = 0;
        this.audienceResult[2].name = "naranResult";
        this.audienceResult[2].result = 0;
    }

    // Player Update
    public GameData(string name, Resource[] resource, Permission[] permission, Flute[] flute, Balloon[] balloon, MusicalMasteryLvl[] musicalMasteryLvl, MusicSheet[] musicSheet, AudienceResult[] audienceResults) {
        this.name = name;
        this.resource = resource;
        this.permission = permission;
        this.flute = flute;
        this.balloon = balloon;
        this.musicalMasteryLvl = musicalMasteryLvl;
        this.musicSheet = musicSheet;
        this.audienceResult = audienceResult;
    }

    public bool DoesHavePermit(string permitType) {
        for(int i = 0; i < permission.Length; i++) {
            if(permitType == permission[i].name) {
                return true;
            }
        }
        
        return false;
    }
    
    public bool DoesHavePartiture(string partitureName) {
        for(int i = 0; i < musicSheet.Length; i++) {
            if(partitureName == musicSheet[i].name) {
                return true;
            }
        }
        
        return false;
    }

    public int ReturnAudienceResult(int id)
    {
        for(int i = 0; i < audienceResult.Length; i++)
        {
            if(id == i)
            {
                return audienceResult[i].result;
            }
        } 

        return 0;
        
    }
}
