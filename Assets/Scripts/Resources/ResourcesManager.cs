using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance;
    [SerializeField] private bool isWood;
    [SerializeField] private bool isIron;
    [SerializeField] private bool isGold;
    [SerializeField] private GameObject[] resourcesPreFabs = new GameObject[3];
    private int resourceIndex = 0;
    private float lastSpawn;
    private float spawnLapse;

    void Start()
    {
        instance = this;

        if(isWood) {
            resourceIndex = 0;
            spawnLapse = 1f;
        }
        else if(isIron) {
            resourceIndex = 1;
            spawnLapse = 2f;
        }
        else if(isGold) {
            resourceIndex = 2;
            spawnLapse = 3f;
        }

        lastSpawn = Time.time;
    }

    void Update()
    {
        ControlGeneration();
    }

    private void ControlGeneration()
    {
        // if (generatedNotes < Partitures.instance.numberOfPartitureNotes)
        // {
            if ((Time.time - lastSpawn) >= spawnLapse)
            {
                Instantiate(this.resourcesPreFabs[resourceIndex], this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
                lastSpawn = Time.time;
            }
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
