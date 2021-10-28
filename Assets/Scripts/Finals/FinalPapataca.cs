using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPapataca : MonoBehaviour
{
    
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();
        StartCoroutine(FinishGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
