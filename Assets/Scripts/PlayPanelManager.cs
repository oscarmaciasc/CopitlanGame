using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject note;
    public bool noteSuccessful;
    public bool canPress;
    public bool haveBeenPressed;
    string numberNote = "";
    string keyPressed = "";
    int goodNotes = 0;

    // Start is called before the first frame update
    void Start()
    {
        canPress = false;
        noteSuccessful = false;
        haveBeenPressed = false;
        note.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 200);
    }

    // Update is called once per frame
    void Update()
    {

        //This condition do CompareKeys only OnTrigger and when a key is pressed
        if (canPress && Input.anyKeyDown)
        {
            CompareKeys();
            note.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 200);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");

        // Key to compare = NoteText
        // This was in update but it fits better here
        numberNote = NoteManager.instance.number;
        Debug.Log("Prueba texto: " + note.transform.Find("NoteText").gameObject.GetComponent<Text>().text);

        if (col.gameObject.GetComponent<NoteManager>() != null)
        {
            Debug.Log("Detecting note...");
            col.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            canPress = true;
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
        canPress = false;

        if (keyPressed == "" && !haveBeenPressed)
        {
            Debug.Log("You miss that note");

            // Change the note color to red
            note.gameObject.GetComponent<Image>().color = new Color32(234, 87, 91, 200);
            keyPressed = "";
            noteSuccessful = false;
            canPress = false;
        }
    }

    public void CompareKeys()
    {
        //Detecting the key pressed
        //Si la tecla presionada corresponde al número de la nota entonces marcar correcto

        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            keyPressed = "0";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            keyPressed = "1";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            keyPressed = "2";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            keyPressed = "3";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            keyPressed = "4";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            keyPressed = "5";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            keyPressed = "6";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            keyPressed = "7";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            keyPressed = "8";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            keyPressed = "9";
        }
        else
        {
            haveBeenPressed = false;
        }

        if (keyPressed == numberNote)
        {
            Debug.Log("You got one point");

            // Change the note color to green
            note.gameObject.GetComponent<Image>().color = new Color(87, 234, 91, 200);

            noteSuccessful = true;
            goodNotes++;
            canPress = false;
            haveBeenPressed = true;
        }
        else if (keyPressed != "")
        {
            Debug.Log("Wrong key");

            // Change the note color to red
            note.gameObject.GetComponent<Image>().color = new Color32(234, 87, 91, 200);

            noteSuccessful = false;
            canPress = false;
            haveBeenPressed = true;
        }
        Debug.Log("KeyToCompare is: " + keyPressed);
        Debug.Log("NumberNote is: " + numberNote);
        Debug.Log("Correct notes: " + goodNotes);
    }

    //Streaks
    public void NoteStreak()
    {
        if (goodNotes == 10 /*&& Partitura == Facil*/)
        {
            //PartitureVelocity = PartituraFacil.velocidad * 1.2;
        }

        //Etc etc...
    }
}
