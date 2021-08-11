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

    // Start is called before the first frame update
    void Start()
    {
        femaleArrow.SetActive(false);
        maleArrow.SetActive(false);
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
        nameInputField.characterLimit = 12;
    }

    public void Male()
    {
        femaleArrow.SetActive(false);
        maleArrow.SetActive(true);
    }

    public void Female()
    {
        maleArrow.SetActive(false);
        femaleArrow.SetActive(true);
    }

    public void DeactivatePlaceHolder()
    {
        nameInputField.placeholder.GetComponent<Text>().text = "";
    }
}
