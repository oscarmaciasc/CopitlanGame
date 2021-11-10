using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseID : MonoBehaviour
{
    public static HouseID instance;
    public int houseID;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Write id on tempFile
        XmlManager.instance.HouseIDTempFile(houseID);
    }
}
