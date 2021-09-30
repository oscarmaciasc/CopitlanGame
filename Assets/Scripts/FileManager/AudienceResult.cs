using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class AudienceResult
{
    [XmlAttribute("name")]
    public string name { get; set; }

    [XmlElement("result")]
    public int result { get; set; }
}
