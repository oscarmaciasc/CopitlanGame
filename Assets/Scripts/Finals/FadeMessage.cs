using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMessage : MonoBehaviour
{

    public static FadeMessage instance;
    public GameObject message;
    public bool shouldFadeToColor;
    public bool shouldFadeToTransparent;
    private float fadeSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToColor)
        {
            message.SetActive(true);
            message.GetComponent<SpriteRenderer>().color = new Color(message.GetComponent<SpriteRenderer>().color.r, message.GetComponent<SpriteRenderer>().color.g, message.GetComponent<SpriteRenderer>().color.b, Mathf.MoveTowards(message.GetComponent<SpriteRenderer>().color.a, 1f, fadeSpeed * Time.fixedDeltaTime));

            if (message.GetComponent<SpriteRenderer>().color.a == 1f)
            {
                shouldFadeToColor = false;
            }
        }

        if (shouldFadeToTransparent)
        {
            message.GetComponent<SpriteRenderer>().color = new Color(message.GetComponent<SpriteRenderer>().color.r, message.GetComponent<SpriteRenderer>().color.g, message.GetComponent<SpriteRenderer>().color.b, Mathf.MoveTowards(message.GetComponent<SpriteRenderer>().color.a, 0f, fadeSpeed * Time.fixedDeltaTime));

            // Debug.Log("Transparencia message1: " + message.GetComponent<SpriteRenderer>().color.a);
            // Debug.Log("ShouldFadeToTransparent: " + shouldFadeToTransparent);
            if (message.GetComponent<SpriteRenderer>().color.a == 0f)
            {
                Debug.Log("Entro mamarracho");
                shouldFadeToTransparent = false;
                message.SetActive(false);
            }
        }
    }

    public void FadeToColor()
    {
        shouldFadeToColor = true;
        shouldFadeToTransparent = false;
    }

    public void FadeToTransparent()
    {
        shouldFadeToColor = false;
        shouldFadeToTransparent = true;
    }
}
