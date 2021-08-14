using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Permission
{
    [XmlAttribute("nombre")]
    public string name;
}
