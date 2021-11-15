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
    [SerializeField] private int papatacaSector;
    private int resourceIndex = 0;
    private float lastSpawn;
    private float spawnLapse;
    private Vector3 nextSpawnPosition;
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
    private int spawned;
    private int maxToSpawn;

    void Start()
    {
        // Dos pesos de cilantro
        
        instance = this;
        spawned = 0;

        if(isWood) {
            resourceIndex = 0;
            spawnLapse = 1f;
            maxToSpawn = 20;

            switch (papatacaSector) {
                case 1:
                    maxX = 0f;
                    minX = -200f;
                    maxY = 200f;
                    minY = 0f;
                break;
                case 2:
                    maxX = 200f;
                    minX = 0f;
                    maxY = 200f;
                    minY = 0f;
                break;
                case 3:
                    maxX = 0f;
                    minX = -200f;
                    maxY = 0f;
                    minY = -200f;
                break;
                case 4:
                    maxX = 200f;
                    minX = 0f;
                    maxY = 0f;
                    minY = -200f;
                break;
                default:
                    Debug.Log("Sector is 0");
                break;
            }
        }
        else if(isIron) {
            resourceIndex = 1;
            spawnLapse = 5f;
            maxToSpawn = 6;

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
            maxToSpawn = 5;

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
        if((Time.time - lastSpawn) >= spawnLapse && spawned < maxToSpawn)
        {
            Instantiate(this.resourcesPreFabs[resourceIndex], nextSpawnPosition, Quaternion.identity).transform.SetParent(this.gameObject.transform);
            lastSpawn = Time.time;
            GetNewSpawnPosition();
            spawned++;
        }
    }

    public void GetNewSpawnPosition() {
        bool outOfCopitlan = false;
        float distanceFromCenter = 0f;
        
        if(isWood)
        {
            do {
                nextSpawnPosition = new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 1f);

                distanceFromCenter = (float) System.Math.Sqrt((nextSpawnPosition.x*nextSpawnPosition.x) + (nextSpawnPosition.y*nextSpawnPosition.y));

                if(distanceFromCenter > 172) {
                    outOfCopitlan = true;
                }
            
            } while(!outOfCopitlan);
        }
        else
        {
            nextSpawnPosition = new Vector3(Random.Range(maxX, minX), Random.Range(maxY, minY), 1f);
        }

        GameObject newPosition = Instantiate(this.emptyObject, nextSpawnPosition, Quaternion.identity);

        newPosition.transform.SetParent(this.gameObject.transform);

        Destroy(newPosition, .5f);
    }

    // Called when a resource is collected.
    // Wood = 0
    // Iron = 1
    // Gold = 2
    // Fuel = 3
    public void resourceCollected(int resourceID, int quantity) {
        spawned--;
        if(XmlManager.instance.ThereIsEnoughSpace(resourceID, quantity)) {
            XmlManager.instance.IncreaseResource(resourceID, quantity);
        }
    }
}
