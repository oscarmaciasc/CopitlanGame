using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class HappinessPercentage
{
    [XmlAttribute("percentage")]
    public int percentage { get; set; }
}
