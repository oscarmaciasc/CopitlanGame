using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class LastSaved
{
    [XmlAttribute("wasLoadedAlready")]
    public bool wasLoadedAlready { get; set; }

    [XmlElement("Scene")]
    public string scene { get; set; }

    [XmlElement("CoordX")]
    public float coordX { get; set; }

    [XmlElement("CoordY")]
    public float coordY { get; set; }
}