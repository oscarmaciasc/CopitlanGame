using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class TimeBalloon
{
    [XmlAttribute("time")]
    public float time { get; set; }
}
