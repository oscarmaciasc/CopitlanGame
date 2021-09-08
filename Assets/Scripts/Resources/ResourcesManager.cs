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
        ControlGeneration();
    }

    private void ControlGeneration()
    {
        // if (generatedNotes < Partitures.instance.numberOfPartitureNotes)
        // {
        //     if ((Time.time - timeLastNote) >= Partitures.instance.velocity)
        //     {
        //         Instantiate(this.notePrefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
        //         timeLastNote = Time.time;
        //         generatedNotes++;
        //     }
        // }
        // else if (passedNotes >= generatedNotes)
        // {
        //     InitSequence2.instance.HasFinishedPartiture();
        // }
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
