using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    public float timeLastNote = 2f;
    public static int streak = 0;
    public GameObject objectTest;
    public string partitureName;
    public int generatedNotes = 0;
    public int passedNotes = 0;
    public static PentagramManager instance;
    public bool partitureFinished = false;
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
        partitureName = PartitureSelection.instance.panelPartitureName;

        Debug.Log("You have selected: " + partitureName);
        Partitures.instance.setVelocity(partitureName);
    }

    // Update is called once per frame
    void Update()
    {
        // Generating random notes for testing purposes
        if (generatedNotes < Partitures.instance.numberOfPartitureNotes)
        {
            if ((Time.time - timeLastNote) >= Partitures.instance.velocity)
            {
                Instantiate(this.notePrefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
                timeLastNote = Time.time;
                generatedNotes++;
            }
        }
        else if (passedNotes >= generatedNotes)
        {
            partitureFinished = true;

            InGame.instance.HasFinishedPartiture();

            // This is not working and i dont know why
            PartitureHabitant.instance.HasFinishedPartiture();

            Mines.instance.HasFinishedPartiture();
        }
    }

    public bool PartitureFinished()
    {
        return partitureFinished;
    }

    public int TotalNotes()
    {
        return passedNotes;
    }
}
