using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    // Called when a resource is collected.
    // Wood = 0
    // Iron = 1
    // Gold = 2
    // Fuel = 3
    public void resourceCollected(int resourceID, int quantity) {
        // ******************************FAKE*****************************
        int index = 2;
        // ******************************FAKE*****************************
        XmlManager.instance.IncreaseResource(index, resourceID, quantity);
    }
}
