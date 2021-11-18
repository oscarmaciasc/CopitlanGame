using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Partitures : MonoBehaviour
{
    public static Partitures instance;
    public string partitureName;
    public string partitureDifficulty;
    public float velocity;
    public float partitureVelocity;
    public int partitureToPlay;
    public int upStreak = 10;
    public int limitStreak;
    public bool canAddAuxStreak = false;
    public string[] numberNotes = new string[10];
    public int numberOfPartitureNotes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void OnEnable()
    {
        //this.numberNotes = new string[10];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LimitStreak()
    {

        // We have to Limit the streak to a certain number of streak
        // Streak now aumenting in 15% instead of 20%
        // Partiture easy max streak: 30
        // Partiture medium max streak: 20
        // Partiture hard max streak: 20
        // Partiture epic max streak: 10

        if (PentagramManager.streak < limitStreak)
        {
            if (PentagramManager.streak == upStreak)
            {
                velocity -= 0.15f;
                upStreak += 10;
            }

            if (PentagramManager.streak == 0)
            {
                velocity = partitureVelocity;
            }
        }
        else
        {
            PentagramManager.streak = limitStreak;
            PentagramManager.maxStreak = limitStreak;
            canAddAuxStreak = true;
        }
    }

    public void SetVelocity(string partitureName)
    {
        this.partitureName = partitureName;
        if (partitureName == "Partitura 1" || partitureName == "Partitura 2" || partitureName == "Partitura 3")
        {
            this.partitureDifficulty = "easy";
            this.velocity = 1f;
            this.partitureVelocity = 1f;
            this.limitStreak = 10;
            this.numberOfPartitureNotes = 19;

            for (int i = 0; i < 4; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "Partitura 1")
            {
                this.partitureToPlay = 0;
            }
            if (partitureName == "Partitura 2")
            {
                Debug.Log("sound 1");
                this.partitureToPlay = 1;
            }
            if (partitureName == "Partitura 3")
            {
                this.partitureToPlay = 2;
                this.numberOfPartitureNotes = 66;
            }
        }

        if (partitureName == "Partitura 4" || partitureName == "Partitura 5" || partitureName == "Partitura 6")
        {
            this.partitureDifficulty = "medium";
            this.velocity = 0.9f;
            this.partitureVelocity = 0.9f;
            this.limitStreak = 20;
            this.numberOfPartitureNotes = 20;

            for (int i = 0; i < 7; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "Partitura 4")
            {
                this.partitureToPlay = 3;
            }
            if (partitureName == "Partitura 5")
            {
                this.partitureToPlay = 4;
            }
            if (partitureName == "Partitura 6")
            {
                this.partitureToPlay = 5;
                this.numberOfPartitureNotes = 73;
            }
        }

        if (partitureName == "Partitura 7" || partitureName == "Partitura 8" || partitureName == "Partitura 9")
        {
            this.partitureDifficulty = "hard";
            this.velocity = 0.8f;
            this.partitureVelocity = 0.8f;
            this.limitStreak = 20;
            this.numberOfPartitureNotes = 23;

            for (int i = 0; i < 10; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "Partitura 7")
            {
                this.partitureToPlay = 6;
            }
            if (partitureName == "Partitura 8")
            {
                this.partitureToPlay = 7;
            }
            if (partitureName == "Partitura 9")
            {
                this.partitureToPlay = 8;
                this.numberOfPartitureNotes = 83;
            }
        }

        if (partitureName == "Partitura 10")
        {
            this.partitureDifficulty = "epic";
            this.velocity = 0.7f;
            this.partitureVelocity = 0.7f;
            this.limitStreak = 10;
            this.numberOfPartitureNotes = 412;

            for (int i = 0; i < 10; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            this.partitureToPlay = 9;
        }
    }
}
