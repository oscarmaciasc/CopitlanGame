using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //ExitMenu.instance.gameobject.setActive(true);
            Debug.Log("Menu de salida");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            //
            Debug.Log("Menu de pausa");
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            //
            Debug.Log("Flauta");
        }

        
        if(Input.GetKeyDown(KeyCode.F))
        {
            //
            Debug.Log("Globo");
        }

        
        if(Input.GetKeyDown(KeyCode.E))
        {
            //
            Debug.Log("Inventario");
        }

        
        if(Input.GetKeyDown(KeyCode.M))
        {
            //
            Debug.Log("Mapa");
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);
    }
}
