using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{

    public InputField nameInputField;
    public GameObject femaleArrow;
    public GameObject maleArrow;
    [SerializeField] private GameObject placeHolder;
    [SerializeField] private GameObject startGameButton;
    private bool hasBeenSelected;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenSelected = false;
        femaleArrow.SetActive(false);
        maleArrow.SetActive(false);

        //Deactive Button
        startGameButton.GetComponent<Button>().interactable = false;

        //Changin color and outline color when no character or name is selected
        if (startGameButton.GetComponent<Button>().interactable == false)
        {
            startGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
            startGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenSelected && nameInputField.transform.Find("Text").GetComponent<Text>().text != "")
        {
            //Activate Button
            startGameButton.GetComponent<Button>().interactable = true;
        }
        else
        {
             startGameButton.GetComponent<Button>().interactable = false;
        }

        //Changin color and outline color when no character or name is selected
        if (startGameButton.GetComponent<Button>().interactable == true)
        {
            startGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 255);
            startGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 255);
        }
        else
        {
            startGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
            startGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LimitInputString()
    {
        nameInputField.characterLimit = 12;
    }

    public void Male()
    {
        hasBeenSelected = true;
        femaleArrow.SetActive(false);
        maleArrow.SetActive(true);
    }

    public void Female()
    {
        hasBeenSelected = true;
        maleArrow.SetActive(false);
        femaleArrow.SetActive(true);
    }

    public void DeactivatePlaceHolder()
    {
        nameInputField.placeholder.GetComponent<Text>().text = "";
    }
    public void InitGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
