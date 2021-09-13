using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    //public GameObject UIScreen;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        /*if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }*/

        if(PlayerController.instance == null)
        {
            PlayerController clonePlayer = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clonePlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
