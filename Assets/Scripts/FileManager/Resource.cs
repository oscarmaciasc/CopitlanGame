using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Resource
{
    [XmlAttribute("name")]
    public string name { get; set; }

    [XmlElement("quantity")]
    public int quantity { get; set; }

    public Resource()
    {

    }

    public Resource(string name, int quantity)
    {
        this.name = name;
        this.quantity = quantity;
    }
}
