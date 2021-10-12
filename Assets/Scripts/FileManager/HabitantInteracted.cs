using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class HabitantInteracted
{
    [XmlAttribute("id")]
    public int id { get; set; }
    
    [XmlElement("Interacted")]
    public bool interacted { get; set; }
}
