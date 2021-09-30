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
    public string musicToPlay;
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

    public void setVelocity(string partitureName)
    {
        this.partitureName = partitureName;
        if (partitureName == "Partitura 1" || partitureName == "Partitura 2" || partitureName == "Partitura 3")
        {
            this.partitureDifficulty = "easy";
            this.velocity = 1f;
            this.partitureVelocity = 1f;
            this.limitStreak = 10;
            this.numberOfPartitureNotes = 10;

            for (int i = 0; i < 4; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "1")
            {
                this.musicToPlay = "/track1";
            }
            if (partitureName == "2")
            {
                this.musicToPlay = "/track2";
            }
            if (partitureName == "3")
            {
                this.musicToPlay = "/track3";
            }
        }

        if (partitureName == "Partitura 4" || partitureName == "Partitura 5" || partitureName == "Partitura 6")
        {
            this.partitureDifficulty = "medium";
            this.velocity = 0.9f;
            this.partitureVelocity = 0.9f;
            this.limitStreak = 20;
            this.numberOfPartitureNotes = 30;

            for (int i = 0; i < 7; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "4")
            {
                this.musicToPlay = "/track4";
            }
            if (partitureName == "5")
            {
                this.musicToPlay = "/track5";
            }
            if (partitureName == "6")
            {
                this.musicToPlay = "/track6";
            }
        }

        if (partitureName == "Partitura 7" || partitureName == "Partitura 8" || partitureName == "Partitura 9")
        {
            this.partitureDifficulty = "hard";
            this.velocity = 0.8f;
            this.partitureVelocity = 0.8f;
            this.limitStreak = 20;
            this.numberOfPartitureNotes = 10;

            for (int i = 0; i < 10; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (partitureName == "7")
            {
                this.musicToPlay = "/track7";
            }
            if (partitureName == "8")
            {
                this.musicToPlay = "/track8";
            }
            if (partitureName == "9")
            {
                this.musicToPlay = "/track9";
            }
        }

        if (partitureName == "Partitura 10")
        {
            this.partitureDifficulty = "epic";
            this.velocity = 0.7f;
            this.partitureVelocity = 0.7f;
            this.limitStreak = 10;
            this.numberOfPartitureNotes = 3;

            for (int i = 0; i < 10; i++)
            {
                this.numberNotes[i] = i + "";
            }

            // Filling a new array deleting the null positions of numberNotes[]
            this.numberNotes = numberNotes.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            this.musicToPlay = "/track10";
        }
    }
}
