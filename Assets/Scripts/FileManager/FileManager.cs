using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using static System.Environment;
using static System.IO.Path;


public class FileManager : MonoBehaviour
{
    public void Save()
    {


        /*public Resource[] resources = new Resource[3];

    public Resource resource = new Resource();


    XmlSerializer serializer = new XmlSerializer(typeof(Resource[]));
    FileStream xmlWriter = new FileStream(CurrentDirectory + "/gameData1.xml", FileMode.Create);
    serializer.Serialize(xmlWriter, resources);
        xmlWriter.Close();
*/

//****************************************************************************************************************

        //Original code

        /*XmlSerializer serializer = new XmlSerializer(typeof(Resource));
        FileStream xmlWriter = new FileStream(CurrentDirectory + "/gameData1.xml", FileMode.Create);
        serializer.Serialize(xmlWriter, new Resource(name="gold", quantity: 14));
        xmlWriter.Close();*/
        
    }
}
