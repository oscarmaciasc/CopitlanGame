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
    public bool fPressed;
    public bool ePressed;
    public bool mPressed;

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
        fPressed = false;
        ePressed = false;
        mPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapePressed = true;
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            pPressed = true;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            vPressed = true;
        }


        if(Input.GetKeyDown(KeyCode.F))
        {
            fPressed = true;
        }


        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Inventario");
            ePressed = true;
        }


        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Mapa");
            mPressed = true;
        }
    }
}
