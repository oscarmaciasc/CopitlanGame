using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelection : MonoBehaviour
{
    public GameObject ConfirmationWindowDelete;

    

    // Start is called before the first frame update
    void Start()
    {
        ConfirmationWindowDelete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmationWindowDisplayDelete()
    {
        ConfirmationWindowDelete.SetActive(true);
    }

    public void Delete()
    {

    }

    public void NoDelete()
    {
        ConfirmationWindowDelete.SetActive(false);
    }
}
