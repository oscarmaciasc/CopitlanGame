using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSequence1 : MonoBehaviour
{

    public string[] linesInitSequence1;
    [SerializeField] private GameObject exclamation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.myAnim.SetFloat("lastMoveX", 0);
        PlayerController.instance.myAnim.SetFloat("lastMoveY", 1);
        StartCoroutine(InitSequence1Dialogs());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //This function is IEnumerator because we need a delay before we display the message in the scene
    IEnumerator InitSequence1Dialogs()
    {
        PlayerController.instance.canMove = false;
        // Delay in initSequence1 scene
        exclamation.SetActive(true);
        yield return new WaitForSeconds(3);
        exclamation.SetActive(false);
        DialogManager.instance.ShowDialog(linesInitSequence1);
    }
}
