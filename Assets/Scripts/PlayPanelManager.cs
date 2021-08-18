using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayPanelManager : MonoBehaviour
{
    private bool noteSuccessful;
    private bool canPress;
    char numberNote = '1';
    char keyToCompare = '\0';
    int goodNotes = 0;

    // Start is called before the first frame update
    void Start()
    {
        canPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Key to compare = NoteText
        //This condition do CompareKeys only OnTrigger and when a key is pressed
        if (canPress && Input.anyKeyDown)
        {
            CompareKeys();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
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
    }

    public void CompareKeys()
    {
        //Detecting the key pressed
        //Si la tecla presionada corresponde al número de la nota entonces marcar correcto
        var allKeys = System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>();
        foreach (var key in allKeys)
        {
            if (Input.GetKeyDown(key))
            {
                //keyToCompare = (char)key;
                Debug.Log("Key in foreach is: " + key);
                keyToCompare = (char)key;
            }
        }
        if (keyToCompare == numberNote)
        {
            Debug.Log("You got one point");
            noteSuccessful = true;
            goodNotes ++;
            canPress = false;
        }
        else if(keyToCompare != '\0')
        {
            Debug.Log("You miss that key");
            noteSuccessful = false;
            canPress = false;
        }
        Debug.Log("KeyToCompare is: " + keyToCompare);
        Debug.Log("NumberNote is: " + numberNote);
        Debug.Log("Correct notes: " + goodNotes);
    }

    //Streaks
    public void NoteStreak()
    {
        if(goodNotes == 10 /*&& Partitura == Facil*/)
        {
            //PartitureVelocity = PartituraFacil.velocidad * 1.2;
        }

        //Etc etc...
    }  
}
