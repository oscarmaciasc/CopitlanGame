using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] private GameObject dialog1;
    [SerializeField] private GameObject dialog2;
    [SerializeField] private GameObject dialog3;
    [SerializeField] private GameObject startPlayingPanel;
    [SerializeField] private GameObject pentagramPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        // deactivating all dialogs and panels in start
        dialog1.gameObject.SetActive(false);
        dialog2.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(false);
        startPlayingPanel.gameObject.SetActive(false);

        Debug.Log("Entering TutorialManager");
    }

    private void Update()
    {
        DeactivateDialogs();
    }

    private void DeactivateDialogs()
    {
        // Deactivate the current dialog if pertinent
        if (!dialog2.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            dialog1.gameObject.SetActive(false);
            dialog3.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else if (dialog2.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.anyKeyDown)
            {
                dialog2.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Note.instance.SetGreen();
                }
                else
                {
                    Note.instance.SetRed();
                }
                Time.timeScale = 1;
            }
        }
    }

    public void ProcessDialog1()
    {
        dialog1.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ProcessDialog2()
    {
        dialog2.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ProcessDialog3()
    {
        dialog3.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartPlaying()
    {
        this.gameObject.SetActive(false);
        pentagramPanel.gameObject.SetActive(true);

        
        // Get to the Game Scene
    }
}
