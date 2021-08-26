using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{

    public string[] lines;
    private bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        canActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if we are in the interactuable zone and we press enter and the dialog box is not already open
        if(canActivate && Input.GetKeyDown(KeyCode.Return) && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
