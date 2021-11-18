using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPapataca : MonoBehaviour
{
    public AudioSource partiture3;

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();
        partiture3 = GetComponent<AudioSource>();
        partiture3.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        Debug.Log("Final");
        UIFade.instance.FadeToBlack();
        SceneManager.LoadScene("MainMenu");
    }
}
