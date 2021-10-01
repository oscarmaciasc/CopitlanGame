using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    public float timeLastNote = 2f;
    public static int streak = 0;
    public static int auxStreak = 0;
    public static int maxStreak = 0;
    public static int maxStreak2 = 0;
    public static int streakRes = 0;
    public GameObject objectTest;
    public string partitureName;
    public int generatedNotes = 0;
    public int passedNotes = 0;
    public static PentagramManager instance;
    public bool partitureFinished = false;
    public bool doOnlyOnce = true;
    public int correctNotes = 0;
    public static int globalCounter = 0;
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
        else if (globalCounter == generatedNotes)
        {
            partitureFinished = true;

            InGame.instance.HasFinishedPartiture();

            if (habitant.GetComponent<PartitureHabitant>() != null)
            {
                habitant.GetComponent<PartitureHabitant>().partitureFinished = true;
            }

            if (habitant.GetComponent<Mines>() != null)
            {
                habitant.GetComponent<Mines>().finishedPartiture = true;
            }

            if (habitant.GetComponent<Audience>() != null)
            {
                habitant.GetComponent<Audience>().finishedPartiture = true;
                if (doOnlyOnce)
                {
                    habitant.GetComponent<Audience>().GetPercentage(habitant);
                    habitant.GetComponent<Audience>().ChangeDirigentDialogLines(habitant);
                    doOnlyOnce = false;
                }
            }

            if (habitant.GetComponent<Leader>() != null)
            {
                // do calculate function when partitureFinished
                if (habitant.name == "Naran")
                {
                    habitant.GetComponent<Leader>().GetAudienceResults();
                }
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
    }
}
