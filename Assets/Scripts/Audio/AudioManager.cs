using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // References to the objects
    public AudioSource[] sfx;
    public AudioSource[] backgroundMusic;
    public AudioSource[] partitureMusic;
    private int secondsToFadeOut = 5;

    public static AudioManager instance;


    // Start is called before the first frame update
    void Start()
    {
        // This means that there can be only one AudioManager in the scene
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // if theres another AudioManager with the instance set, destroy myself
            // but if the instance has been set but its me, then dont destroy me
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // SoundEffects
    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }

    public void PlayBGM(int musicToPlay)
    {
        // stop any other sound playing before
        StopMusic();

        if (musicToPlay < backgroundMusic.Length && musicToPlay != 1000)
        {
            backgroundMusic[musicToPlay].Play();
            while (backgroundMusic[musicToPlay].volume < 1f)
            {
                backgroundMusic[musicToPlay].volume += Time.deltaTime / secondsToFadeOut;
            }
        }
    }

    public void StopMusic()
    {
        for (int i = 0; i < backgroundMusic.Length; i++)
        {
            // Check Music Volume and Fade Out
            while (backgroundMusic[i].volume > 0.01f)
            {
                backgroundMusic[i].volume -= Time.deltaTime / secondsToFadeOut;
            }
            backgroundMusic[i].Stop();
        }
    }

    public void PlayPartiture(int partitureToPlay)
    {
        if (partitureToPlay < partitureMusic.Length)
        {
            partitureMusic[partitureToPlay].Play();
        }
    }
}
