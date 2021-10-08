using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Collectable
{
    [XmlAttribute("quantity")]
    public int quantity;
}
