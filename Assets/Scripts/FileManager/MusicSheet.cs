using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class MusicSheet
{
    [XmlAttribute("name")]
    public string name;

    public MusicSheet(){}
    public MusicSheet(string name)
    {
        this.name = name;
    }
}
