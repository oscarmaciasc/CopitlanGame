using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partitures : MonoBehaviour
{
    public static Partitures instance;
    public string partitureName;
    public string partitureDifficulty;
    public float velocity;
    public float partitureVelocity;
    public string musicToPlay;
    public int limitStreak = 10;

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
        if (PentagramManager.streak == limitStreak)
        {
            velocity -= 0.20f;
            limitStreak += 10;
        }

        if (PentagramManager.streak == 0)
        {
            velocity = partitureVelocity;
        }

        Debug.Log("PARTITURE VELOCITY: " + velocity);
    }

    public void setVelocity(string partitureName)
    {
        this.partitureName = partitureName;

        if (partitureName == "1" || partitureName == "2" || partitureName == "3")
        {
            this.partitureDifficulty = "easy";
            this.velocity = 1f;
            this.partitureVelocity = 1f;

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

        if (partitureName == "4" || partitureName == "5" || partitureName == "6")
        {
            this.partitureDifficulty = "medium";
            this.velocity = 0.5f;
            this.partitureVelocity = 0.5f;

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

        if (partitureName == "7" || partitureName == "8" || partitureName == "9")
        {
            this.partitureDifficulty = "hard";
            this.velocity = 0.2f;
            this.partitureVelocity = 0.2f;

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

        if (partitureName == "10")
        {
            this.partitureDifficulty = "epic";
            this.velocity = 0.1f;
            this.partitureVelocity = 0.1f;

            this.musicToPlay = "/track10";
        }
    }
}
