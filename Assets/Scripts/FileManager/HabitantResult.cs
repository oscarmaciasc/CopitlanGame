using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class HabitantResult
{
    [XmlAttribute("name")]
    public string name { get; set; }
    
    [XmlElement("res")]
    public int result { get; set; }
}
