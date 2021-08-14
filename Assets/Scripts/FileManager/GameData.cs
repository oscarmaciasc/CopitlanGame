using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("gameData")]
public class GameData
{
    [XmlAttribute ("name")]
    public string name { get; set; }

    [XmlArray ("Resources")]
    public Resource[] resource { get; set; }
    
    [XmlArray ("Permissions")]
    public Permission[] permission { get; set; }
    
    [XmlArray ("Flutes")]
    public Flute[] flute { get; set; }

    [XmlArray ("Balloons")]
    public Balloon[] balloon { get; set; }

    

    public GameData() {}

    public GameData(string name, Resource[] resource, Permission[] permission, Flute[] flute, Balloon[] balloon) {
        this.name = name;
        this.resource = resource;
        this.permission = permission;
        this.flute = flute;
        this.balloon = balloon;
    }
}
