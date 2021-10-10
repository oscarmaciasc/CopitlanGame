using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Flute
{
   [XmlAttribute("name")]
   public string name{ get; set;}

   [XmlAttribute("isByDefault")]
   public bool isByDefault{ get; set; }

   public Flute() {}

    public Flute(string name, bool isByDefault) {
        this.name = name;
        this.isByDefault = isByDefault;
    }
}
