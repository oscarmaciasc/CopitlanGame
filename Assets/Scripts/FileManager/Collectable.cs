using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Collectable
{
    [XmlAttribute("id")]
    public int id { get; set; }
    
    [XmlAttribute("shouldBeDestroyed")]
    public bool shouldBeDestroyed { get; set; }

    public Collectable(){}

    public Collectable(int id, bool shouldBeDestroyed) {
        this.id = id;
        this.shouldBeDestroyed = shouldBeDestroyed;
    }
}
