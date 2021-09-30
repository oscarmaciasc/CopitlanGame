using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Permission
{
    [XmlAttribute("name")]
    public string name;

    public Permission() {}

    public Permission(string name) {
        this.name = name;
    }
}
