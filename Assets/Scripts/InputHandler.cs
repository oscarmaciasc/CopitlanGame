using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    public InputField nameInputField;

    // Start is called before the first frame update
    void Start()
    {
        //Changes the caracter name limit in the main input field
       // nameInputField.characterLimit = playerName.Length(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LimitInputString()
    {
        if(nameInputField)
        {

        }
    }
}
