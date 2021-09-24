using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("TempFile")]
public class TempFile
{
    [XmlAttribute ("gameIndex")]
    public int gameIndex { get; set; }

    public TempFile(){}

    public TempFile(int gameIndex) {
        this.gameIndex = gameIndex;
    }
}
