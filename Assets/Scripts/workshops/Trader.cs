using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{

    public bool conversationFinished = false;
    [SerializeField] private GameObject tradeHouseInterface;
    [SerializeField] private GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tradeHouseInterface.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
    }

    public void Trade(GameObject habitant)
    {
        if (habitant.GetComponent<Trader>().conversationFinished)
        {
            tradeHouseInterface.SetActive(true);
        }
    }
}
