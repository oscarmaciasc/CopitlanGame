using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRewardPartiture : MonoBehaviour
{
    public bool finishedPartiture = false;
    private int randomResourceID;
    private int randomQuantity;
    public bool rewardGiven = false;
    public bool conversationFinishedReward = false;
    public string[] reward = { "Me ha gustado tu interpretacion, te otorgo " };
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.habitantResult[index].result >= 60)
        {
            rewardGiven = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GiveResourceReward(GameObject habitant)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (finishedPartiture && !rewardGiven)
        {
            // GetRandomResourceID
            randomResourceID = (int)Random.Range(0, 2);

            // GetRandomQuantityToAdd depending on the resoure
            switch (randomResourceID)
            {
                case 0:
                    randomQuantity = (int)Random.Range(1, 15);
                    reward[0] += randomQuantity + " de madera";
                    break;
                case 1:
                    randomQuantity = (int)Random.Range(1, 8);
                    reward[0] += randomQuantity + " de hierro";
                    break;
                case 2:
                    randomQuantity = (int)Random.Range(1, 4);
                    reward[0] += randomQuantity + " de oro";
                    break;
            }

            habitant.GetComponent<DialogActivator>().lines = reward;

            Debug.Log("Cambio lineas y doy recurso");

            XmlManager.instance.IncreaseResource(randomResourceID, randomQuantity);
            rewardGiven = true;
            
        }
        else
        {
            Debug.Log("finished partiture: " + finishedPartiture);
            Debug.Log("gameData.habitantResult[index].result: " + gameData.habitantResult[index].result);
            Debug.Log("regarw given: " + rewardGiven);
        }
    }
}
