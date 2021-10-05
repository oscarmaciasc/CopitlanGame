using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class DirigentEntrance
{
    [XmlAttribute("name")]
    public string name { get; set; }
    
    [XmlElement("shouldBeActive")]
    public bool shouldBeActive { get; set; }
}
