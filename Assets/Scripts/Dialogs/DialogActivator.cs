using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogActivator : MonoBehaviour
{

    public static DialogActivator instance;
    public string[] lines;
    public string[] linesInitSequence1;
    public bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canActivate = false;
        StartCoroutine(InitSequence1Dialogs());
    }

    // Update is called once per frame
    void Update()
    {
        // if we are in the interactuable zone and we press enter and the dialog box is not already open
        if (canActivate && Input.GetKeyDown(KeyCode.Return) && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }

    //This function is IEnumerator because we need a delay before we display the message in the scene
    IEnumerator InitSequence1Dialogs()
    {
        if (SceneManager.GetActiveScene().name == "InitSequence1")
        {
            PlayerController.instance.canMove = false;

            // Delay in initSequence1 scene
            yield return new WaitForSeconds(3);
            DialogManager.instance.ShowDialog(linesInitSequence1);
        }
    }
}
