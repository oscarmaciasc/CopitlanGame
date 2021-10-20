using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManagerFinal : MonoBehaviour
{
    public static DialogManagerFinal instance;
    [SerializeField] private Text dialogText;
    public GameObject dialogBox;
    public string[] dialogLines;
    private float fadeSpeed = 1f;
    private bool dialogBoxShouldBeActive = false;

    //Keep track in wich line we are
    public int currentLine;
    public bool justStarted;

    private float timeToWait = 3f;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBoxShouldBeActive)
        {
            ActivateDialogs();
        }

        // if dialog box is open and the player release the Enter key we pass to other line and update the text
        if (dialogBox.activeInHierarchy)
        {
            ChangeMessagePosition();
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBoxShouldBeActive = false;
                        FadeMessage.instance.FadeToTransparent();
                        FadeMessage2.instance.FadeToTransparent2();
                        DeactivateDialogs();

                        ChangeScene();
                    }
                    else
                    {
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    private void ChangeScene()
    {
        // timeToWait -= Time.deltaTime;
        // if (timeToWait <= 0)
        // {
            UIFade.instance.FadeToBlack();
            SceneManager.LoadScene("PapatacaFinal");
        //}
    }

    private void ChangeMessagePosition()
    {
        if ((currentLine >= 0 && currentLine <= 3) || (currentLine == 7))
        {
            FadeMessage.instance.FadeToColor();
            FadeMessage2.instance.FadeToTransparent2();
        }
        else
        {
            FadeMessage.instance.FadeToTransparent();
            FadeMessage2.instance.FadeToColor2();
        }
    }

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;

        currentLine = 0;

        dialogText.text = dialogLines[0];
        dialogBoxShouldBeActive = true;
        justStarted = false;
    }

    public void ActivateDialogs()
    {
        dialogBox.SetActive(true);
        dialogBox.GetComponent<Image>().color = new Color(dialogBox.GetComponent<Image>().color.r, dialogBox.GetComponent<Image>().color.g, dialogBox.GetComponent<Image>().color.b, Mathf.MoveTowards(dialogBox.GetComponent<Image>().color.a, 1f, fadeSpeed * Time.fixedDeltaTime));

        dialogText.color = new Color(dialogText.color.r, dialogText.color.g, dialogText.color.b, Mathf.MoveTowards(dialogText.color.a, 1f, fadeSpeed * Time.fixedDeltaTime));

        dialogText.GetComponent<Outline>().effectColor = new Color(dialogText.GetComponent<Outline>().effectColor.r, dialogText.GetComponent<Outline>().effectColor.g, dialogText.GetComponent<Outline>().effectColor.b, Mathf.MoveTowards(dialogText.GetComponent<Outline>().effectColor.a, 1f, fadeSpeed * Time.fixedDeltaTime));
    }

    public void DeactivateDialogs()
    {
        dialogBox.GetComponent<Image>().color = new Color(dialogBox.GetComponent<Image>().color.r, dialogBox.GetComponent<Image>().color.g, dialogBox.GetComponent<Image>().color.b, Mathf.MoveTowards(dialogBox.GetComponent<Image>().color.a, 0f, fadeSpeed * Time.fixedDeltaTime));

        dialogText.color = new Color(dialogText.color.r, dialogText.color.g, dialogText.color.b, Mathf.MoveTowards(dialogText.color.a, 0f, fadeSpeed * Time.fixedDeltaTime));

        dialogText.GetComponent<Outline>().effectColor = new Color(dialogText.GetComponent<Outline>().effectColor.r, dialogText.GetComponent<Outline>().effectColor.g, dialogText.GetComponent<Outline>().effectColor.b, Mathf.MoveTowards(dialogText.GetComponent<Outline>().effectColor.a, 0f, fadeSpeed * Time.fixedDeltaTime));

        if (dialogBox.GetComponent<Image>().color.a == 0f && dialogText.color.a == 0f && dialogText.GetComponent<Outline>().effectColor.a == 0f)
        {
            dialogBox.SetActive(false);
        }
    }
}
