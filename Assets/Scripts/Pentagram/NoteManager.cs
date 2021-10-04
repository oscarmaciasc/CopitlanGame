using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public string number = "";
    private int[] arrayPositions = { 34, 67, 100, 133, 166 };

    [SerializeField] private GameObject note;
    public bool noteSuccessful;
    public bool canPress;
    public bool haveBeenPressed;
    string numberNote = "";
    string keyPressed = "";
    public int correctNotes = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int positionY = arrayPositions[Random.Range(0, arrayPositions.Length)];
        number = Partitures.instance.numberNotes[Random.Range(0, Partitures.instance.numberNotes.Length)];

        //Replace "+700" by the anchor position of the Pentagram
        //This only works for FullHD Resolutions
        gameObject.transform.position = new Vector3(transform.parent.position.x + 730, transform.parent.position.y + positionY, 0);
        gameObject.transform.Find("NoteText").gameObject.GetComponent<Text>().text = number;

        //***************************************************************************************************************************

        canPress = false;
        noteSuccessful = false;
        haveBeenPressed = false;

        SetMediumOpacity();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 240 * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);

        //This condition do CompareKeys only OnTrigger and when a key is pressed
        if (canPress && Input.anyKeyDown)
        {
            CompareKeys();
        }


    }


    //Identfy that the collider is the playpanel collider
    void OnTriggerEnter2D(Collider2D col)
    {
        // Key to compare = NoteText
        // This was in update but it fits better here
        numberNote = number;
        if (col.gameObject.GetComponent<PlayPanelManager>() != null)
        {
            SetFullOpacity();
            canPress = true;
        }
        PentagramManager.instance.passedNotes++;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        this.gameObject.SetActive(false);
        canPress = false;

        if (keyPressed == "" && !haveBeenPressed)
        {
            Debug.Log("You miss that note");
            keyPressed = "";
            noteSuccessful = false;
            canPress = false;
            PentagramManager.streak = 0;
            PentagramManager.globalCounter++;
            Debug.Log(PentagramManager.globalCounter);
            if (Partitures.instance.canAddAuxStreak)
            {
                PentagramManager.auxStreak = 0;
            }
        }
        gameObject.GetComponent<NoteManager>().SetMediumOpacity();
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
            // Change the note color to green
            SetGreen();
            PentagramManager.globalCounter++;
            Debug.Log(PentagramManager.globalCounter);
            //Debug.Log("ARRIBA");

            noteSuccessful = true;
            Debug.Log("streakAFUERA: " + PentagramManager.streak);
            if (PentagramManager.streak < Partitures.instance.limitStreak)
            {
                Debug.Log("streakADENTRO: " + PentagramManager.streak);
                PentagramManager.streak++;
                 Debug.Log("streakADENTRO++: " + PentagramManager.streak);
            }
            Debug.Log("streakDESPUES: " + PentagramManager.streak);
            //Debug.Log("ARRIBA2");

            if (Partitures.instance.canAddAuxStreak)
            {
                PentagramManager.auxStreak++;
            }
            //Debug.Log("ARRIBA3");

            // Setting the max streak to calculate the dirigent aprobation percentage
            if (PentagramManager.streak > PentagramManager.maxStreak)
            {
                Debug.Log("Entrando");
                PentagramManager.maxStreak = PentagramManager.streak;
            }
            //Debug.Log("ARRIBA4");

            if (PentagramManager.maxStreak == Partitures.instance.limitStreak)
            {
                Debug.Log("Entrando2");
                if (PentagramManager.auxStreak > PentagramManager.maxStreak2)
                {
                    Debug.Log("Entrando3");
                    PentagramManager.maxStreak2 = PentagramManager.auxStreak;
                }
            }
            
            PentagramManager.streakRes = PentagramManager.maxStreak + PentagramManager.maxStreak2;
            Debug.Log("streakRes: " + PentagramManager.streakRes + " = maxStreak[" + PentagramManager.maxStreak + "] + maxStreak2[" + PentagramManager.maxStreak2 + "]");
            //Debug.Log("ARRIBA5");

            canPress = false;
            haveBeenPressed = true;
            PentagramManager.instance.correctNotes++;
        }
        else if (keyPressed != numberNote && keyPressed != "")
        {
            // Change the note color to red
            SetRed();
            PentagramManager.globalCounter++;
            Debug.Log(PentagramManager.globalCounter);
            noteSuccessful = false;
            canPress = false;
            haveBeenPressed = true;
            PentagramManager.streak = 0;
            if (Partitures.instance.canAddAuxStreak)
            {
                PentagramManager.auxStreak = 0;
            }
        }

        Partitures.instance.LimitStreak();
    }

    public void SetGreen()
    {
        gameObject.GetComponent<Image>().color = new Color32(87, 234, 91, 255);
    }

    public void SetRed()
    {
        gameObject.GetComponent<Image>().color = new Color32(234, 87, 91, 255);
    }

    public void SetMediumOpacity()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }

    public void SetFullOpacity()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
