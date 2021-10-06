using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class HabitantResult
{
    [XmlAttribute("id")]
    public int id { get; set; }
    
    [XmlElement("Result")]
    public int result { get; set; }
}
