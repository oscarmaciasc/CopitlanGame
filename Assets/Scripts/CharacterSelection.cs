using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{

    public InputField nameInputField;
    public string auxString;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LimitInputString()
    {
        if (nameInputField.text.Length > 12)
        {
            /*auxString = nameInputField.text;
            nameInputField.text.Remove();
            for(int i=0; i<12; i++)
            {
                nameInputField.text += auxString[i].ToString();
            }*/
            nameInputField.characterLimit = 12;
        }
    }
}
