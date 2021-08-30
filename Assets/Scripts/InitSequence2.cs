using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSequence2 : MonoBehaviour
{

    [SerializeField] private GameObject partitureSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogManager.instance.conversationIsFinished)
        {
            // Select partiture 1
            Debug.Log("Open partiture 1");

            //PentagramManager.instance.partitureName = "Partitura 1";
        }
    }
}
