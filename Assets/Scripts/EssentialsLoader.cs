using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    //public GameObject UIScreen;
    public GameObject player;
    public GameObject camera;

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

        if(CameraController.instance == null)
        {
            CameraController cloneCamera = Instantiate(camera).GetComponent<CameraController>();
            CameraController.instance = cloneCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
