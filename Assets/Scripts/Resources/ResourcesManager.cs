using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance;
    [SerializeField] private bool isWood;
    [SerializeField] private bool isIron;
    [SerializeField] private bool isGold;
    [SerializeField] private GameObject[] resourcesPreFabs = new GameObject[3];
    [SerializeField] private GameObject emptyObject;
    private int resourceIndex = 0;
    private float lastSpawn;
    private float spawnLapse;
    private Vector3 nextSpawnPosition;
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;

    void Start()
    {
        instance = this;

        if(isWood) {
            resourceIndex = 0;
            spawnLapse = 1f;
            maxX = 199f;
            minX = -199f;
            maxY = 199f;
            minY = -199f;
        }
        else if(isIron) {
            resourceIndex = 1;
            spawnLapse = 5f;
            if(SceneManager.GetActiveScene().name == "Acan") {
                maxX = 11.5f;
                minX = -11.5f;
                maxY = 11.5f;
                minY = -11.5f;
            }
            else {
                maxX = 16.5f;
                minX = -16.5f;
                maxY = 16.5f;
                minY = -16.5f;
            }
        }
        else if(isGold) {
            resourceIndex = 2;
            spawnLapse = 10f;
            maxX = 16.5f;
            minX = -16.5f;
            maxY = 13f;
            minY = -13f;
        }

        lastSpawn = Time.time;

        GetNewSpawnPosition();
    }

    void Update()
    {
        
        ControlGeneration();
    }

    private void ControlGeneration()
    {
        if((Time.time - lastSpawn) >= spawnLapse)
        {
            Debug.Log("Spawning");
            Instantiate(this.resourcesPreFabs[resourceIndex], nextSpawnPosition, Quaternion.identity).transform.SetParent(this.gameObject.transform);
            lastSpawn = Time.time;
            GetNewSpawnPosition();
        }
    }

    public void GetNewSpawnPosition() {
        bool outOfCopitlan = false;
        float distanceFromCenter = 0f;

        Debug.Log("Getting new position");

        do {
            nextSpawnPosition = new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 1f);

            distanceFromCenter = (float) System.Math.Sqrt((nextSpawnPosition.x*nextSpawnPosition.x) + (nextSpawnPosition.y*nextSpawnPosition.y));

            Debug.Log("Ramdom: " + distanceFromCenter);

            if(distanceFromCenter > 172) {
                outOfCopitlan = true;
                Debug.Log("Buena: " + distanceFromCenter);
            }
            
        } while(!outOfCopitlan);

        GameObject newPosition = Instantiate(this.emptyObject, nextSpawnPosition, Quaternion.identity);

        newPosition.transform.SetParent(this.gameObject.transform);

        Destroy(newPosition, .05f);
    }

    // Called when a resource is collected.
    // Wood = 0
    // Iron = 1
    // Gold = 2
    // Fuel = 3
    public void resourceCollected(int resourceID, int quantity) {
        if(XmlManager.instance.ThereIsEnoughSpace(resourceID, quantity)) {
            XmlManager.instance.IncreaseResource(resourceID, quantity);
        }
    }
}
