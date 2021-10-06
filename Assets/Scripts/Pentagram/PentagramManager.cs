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
        globalCounter = 0;
        streakRes = 0;
        maxStreak = 0;
        maxStreak2 = 0;
        auxStreak = 0;
        streak = 0;
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

            // If the habitant doesnt has audience, then streak = 0.
            if (habitant.GetComponent<Audience>() == null && habitant.GetComponent<Leader>() == null)
            {
                Debug.Log("Hago referencia a: " + habitant.name);
                Debug.Log("pongo a 0 la racha");
                streak = 0;
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

            if (habitant.GetComponent<Tecalli>() != null)
            {
                habitant.GetComponent<Tecalli>().finishedPartiture = true;
            }

            if (habitant.GetComponent<Acan>() != null)
            {
                habitant.GetComponent<Acan>().finishedPartiture = true;
            }

            if (habitant.GetComponent<Seti>() != null)
            {
                habitant.GetComponent<Seti>().finishedPartiture = true;
                if (!Seti.instance.hasFinished)
                {
                    Seti.instance.GetPercentage();
                }
                habitant.GetComponent<Seti>().BothFinished(habitant);
            }

            if (habitant.GetComponent<Audience>() != null)
            {
                habitant.GetComponent<Audience>().finishedPartiture = true;
                habitant.GetComponent<Audience>().GetPercentage(habitant);
                habitant.GetComponent<Audience>().ChangeDirigentDialogLines(habitant);
            }

            if (habitant.GetComponent<Leader>() != null)
            {
                habitant.GetComponent<Leader>().finishedPartiture = true;
                habitant.GetComponent<Leader>().GetAudienceResults();
                habitant.GetComponent<Leader>().GetPercentage(habitant);
                habitant.GetComponent<Leader>().ChangeLeaderDialogLines(habitant);
                if (doOnlyOnce && habitant.GetComponent<Leader>().canActivateFinal)
                {
                    habitant.GetComponent<Leader>().GetCityHappinessPercentage();
                    doOnlyOnce = false;
                }
            }

            if (habitant.GetComponent<HabitantMath>() != null)
            {
                habitant.GetComponent<HabitantMath>().finishedPartiture = true;
                habitant.GetComponent<HabitantMath>().GetPercentage(habitant);
            }

            // Save musicalmasterylevel
            SaveMusicalMasteryLvl();

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

    public void SaveMusicalMasteryLvl()
    {
        if (Partitures.instance.partitureDifficulty == "easy")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            XmlManager.instance.AddMusicalMasteryLvl("apprentice");
        }

        if (Partitures.instance.partitureDifficulty == "medium")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            XmlManager.instance.AddMusicalMasteryLvl("experienced");
        }

        if (Partitures.instance.partitureDifficulty == "hard")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            XmlManager.instance.AddMusicalMasteryLvl("master");
        }

        if (Partitures.instance.partitureDifficulty == "epic")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            XmlManager.instance.AddMusicalMasteryLvl("legend");
        }
    }
}
