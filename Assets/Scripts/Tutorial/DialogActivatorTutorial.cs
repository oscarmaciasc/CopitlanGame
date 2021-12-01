using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogActivatorTutorial : MonoBehaviour
{

    public static DialogActivatorTutorial instance;
    [SerializeField] private GameObject talkHud;
    public string[] lines;
    public bool canActivate;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canActivate = false;
        DeactivateTalkHud();
    }

    public void DeactivateTalkHud()
    {
        if(SceneManager.GetActiveScene().name == "InitSequence2"){
            talkHud.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if we are in the interactuable zone and we press enter and the dialog box is not already open
        if (canActivate && Input.GetKeyDown(KeyCode.Return) && !DialogManagerTutorial.instance.dialogBox.activeInHierarchy)
        {
            DialogManagerTutorial.instance.ShowDialog(lines);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
            talkHud.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
            talkHud.SetActive(false);
        }
    }

    public bool CanActive()
    {
        return canActivate;
    }

    public bool CanActiveFalse()
    {
        canActivate = false;
        return canActivate;
    }
}
