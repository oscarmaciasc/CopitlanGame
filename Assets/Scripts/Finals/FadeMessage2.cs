using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMessage2 : MonoBehaviour
{
    public static FadeMessage2 instance;
    [SerializeField] private GameObject message2;
    public bool shouldFadeToColor2;
    public bool shouldFadeToTransparent2;
    private float fadeSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToColor2)
        {
            message2.SetActive(true);
            message2.GetComponent<SpriteRenderer>().color = new Color(message2.GetComponent<SpriteRenderer>().color.r, message2.GetComponent<SpriteRenderer>().color.g, message2.GetComponent<SpriteRenderer>().color.b, Mathf.MoveTowards(message2.GetComponent<SpriteRenderer>().color.a, 1f, fadeSpeed * Time.fixedDeltaTime));

            if (message2.GetComponent<SpriteRenderer>().color.a == 1f)
            {
                shouldFadeToColor2 = false;
            }
        }

        if (shouldFadeToTransparent2)
        {
            message2.GetComponent<SpriteRenderer>().color = new Color(message2.GetComponent<SpriteRenderer>().color.r, message2.GetComponent<SpriteRenderer>().color.g, message2.GetComponent<SpriteRenderer>().color.b, Mathf.MoveTowards(message2.GetComponent<SpriteRenderer>().color.a, 0f, fadeSpeed * Time.fixedDeltaTime));

            if (message2.GetComponent<SpriteRenderer>().color.a == 0f)
            {
                shouldFadeToTransparent2 = false;
                message2.SetActive(false);
            }
        }
    }

    public void FadeToColor2()
    {
        shouldFadeToColor2 = true;
        shouldFadeToTransparent2 = false;
    }

    public void FadeToTransparent2()
    {
        shouldFadeToColor2 = false;
        shouldFadeToTransparent2 = true;
    }
}
