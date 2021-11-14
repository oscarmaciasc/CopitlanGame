using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class LastSaved
{
    [XmlElement("scene")]
    public string scene { get; set; }

    [XmlElement("coordX")]
    public float coordX { get; set; }

    [XmlElement("coordY")]
    public float coordY { get; set; }
}