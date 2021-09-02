using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    // We attached this script to the canvas and not to the image itself because
    // we want to turn the image on and off and if we attached the script to the
    // image, the script will turn off too.

    public static UIFade instance;

    public Image fadeScreen;
    public float fadeSpeed;

    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;

    // Use this for initialization
    void Start()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if (shouldFadeToBlack)
        {
            Debug.Log("Cambiando a Negro");
            Debug.Log("a: " + fadeScreen.color.a);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.fixedDeltaTime));

            if (fadeScreen.color.a == 1f)
            {
                Debug.Log("Ya cambie a Negro");
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.fixedDeltaTime));
            Debug.Log("Cambiando a Transparente");
            Debug.Log("a: " + fadeScreen.color.a);

            if (fadeScreen.color.a == 0f)
            {
                Debug.Log("Ya cambie a Transparente");
                shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;

    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}

