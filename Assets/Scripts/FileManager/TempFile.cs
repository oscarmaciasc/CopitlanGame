using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("TempFile")]
public class TempFile
{
    [XmlAttribute ("gameIndex")]
    public int gameIndex { get; set; }
    
    [XmlAttribute ("houseID")]
    public int houseID { get; set; }

    public TempFile(){}

    public TempFile(int gameIndex, int houseID) {
        this.gameIndex = gameIndex;
        this.houseID = houseID;
    }
}
