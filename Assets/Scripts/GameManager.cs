using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public bool escapePressed;
    public bool vPressed;
    public bool pPressed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        escapePressed = false;
        vPressed = false;
        pPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapePressed = true;
            Debug.Log("Menu de salida");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            pPressed = true;
            Debug.Log("Menu de pausa");
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            vPressed = true;
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

        if(Input.GetKeyDown(KeyCode.Alpha0)||Input.GetKeyDown("[0]"))
        {
            //
            Debug.Log("Pressed 0");
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)||Input.GetKeyDown("[1]"))
        {
            //
            Debug.Log("Pressed 1");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)||Input.GetKeyDown("[2]"))
        {
            //
            Debug.Log("Pressed 2");
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)||Input.GetKeyDown("[3]"))
        {
            //
            Debug.Log("Pressed 3");
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)||Input.GetKeyDown("[4]"))
        {
            //
            Debug.Log("Pressed 4");
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)||Input.GetKeyDown("[5]"))
        {
            //
            Debug.Log("Pressed 5");
        }

        if(Input.GetKeyDown(KeyCode.Alpha6)||Input.GetKeyDown("[6]"))
        {
            //
            Debug.Log("Pressed 6");
        }

        if(Input.GetKeyDown(KeyCode.Alpha7)||Input.GetKeyDown("[7]"))
        {
            //
            Debug.Log("Pressed 7");
        }

        if(Input.GetKeyDown(KeyCode.Alpha8)||Input.GetKeyDown("[8]"))
        {
            //
            Debug.Log("Pressed 8");
        }

        if(Input.GetKeyDown(KeyCode.Alpha9)||Input.GetKeyDown("[9]"))
        {
            //
            Debug.Log("Pressed 9");
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
