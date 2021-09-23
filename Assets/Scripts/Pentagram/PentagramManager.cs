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
    [SerializeField] private GameObject habitant;

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

    void OnEnable()
    {
        Debug.Log("He vuelto a activar el Pentagram");
        passedNotes = 0;
        generatedNotes = 0;
        correctNotes = 0;
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

            if(habitant.GetComponent<PartitureHabitant>() != null)
            {
                habitant.GetComponent<PartitureHabitant>().partitureFinished = true;
            }

            if(habitant.GetComponent<Mines>() != null)
            {
                habitant.GetComponent<Mines>().finishedPartiture = true;
            }

            

        }
    }

    public bool PartitureFinished()
    {
        return partitureFinished;
    }

    public int TotalNotes()
    {
        return Partitures.instance.numberOfPartitureNotes;
    }

    public void GetHabitant(GameObject getHabitant)
    {
        // habitant is the npc im talking to
        habitant = getHabitant;
        Debug.Log("Nombre de habitante: " + getHabitant.name);
    }
}
