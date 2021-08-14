using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class MusicalMasteryLvl
{
    [XmlAttribute("name")]
    public string name { get; set; }
}
