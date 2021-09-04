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

        DeactivatePlayerArrows();

        //Deactive Button
        startGameButton.GetComponent<Button>().interactable = false;

        SetMediumButtonsTextOppacitty();
    }

    // Update is called once per frame
    void Update()
    {
        ChangingButtonState();
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

    private void DeactivatePlayerArrows()
    {
        femaleArrow.SetActive(false);
        maleArrow.SetActive(false);
    }

    private void SetMediumButtonsTextOppacitty()
    {
        //Changin color and outline color when no character or name is selected
        if (startGameButton.GetComponent<Button>().interactable == false)
        {
            startGameButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(173, 134, 80, 140);
            startGameButton.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 140);
        }
    }

    private void ChangingButtonState()
    {
        if (hasBeenSelected && nameInputField.transform.Find("Text").GetComponent<Text>().text != "")
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

    public void InitGame()
    {
        bool gender;

        if(femaleArrow.gameObject.activeInHierarchy) {
            gender = true;
        }
        else {
            gender = false;
        }
        
        XmlManager.instance.Create(nameInputField.transform.Find("Text").GetComponent<Text>().text, gender);

        SceneManager.LoadScene("InitSequence1");
    }
}
