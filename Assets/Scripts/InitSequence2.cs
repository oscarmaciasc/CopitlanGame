using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSequence2 : MonoBehaviour
{
    public static InitSequence2 instance;
    [SerializeField] private GameObject tutorialInterface;
    [SerializeField] private GameObject startPlayingPanel;
    [SerializeField] private GameObject pressVPanel;
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject backArrow;
    public bool hasBeenActivated;
    private bool justStarted;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hasBeenActivated = false;
        justStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // DialogManager.instance.conversationIsFinished was the previous condition when we display the tutorial after the conversation
        if (DialogManager.instance.conversationIsFinished)
        {
            Debug.Log("STARTING TUTORIAL");
            StartCoroutine(StartTutorial());
            ActivatePartiturePanel();
            if(pressVPanel.activeInHierarchy || partitureSelectionPanel.activeInHierarchy || PartitureSelectionTutorial.instance.pentagramPanel.activeInHierarchy)
            {
                PlayerController.instance.canMove = false;
            }
            else
            {
                PlayerController.instance.canMove = true;
            }
        }
    }

    //This function is IEnumerator because we need a delay before we display the tutorial
    IEnumerator StartTutorial()
    {
        if(justStarted)
        {
            PlayerController.instance.canMove = false;
        }
        justStarted = false;
        yield return new WaitForSeconds(1);
        if (!hasBeenActivated)
        {
            pressVPanel.SetActive(true);
        }
        hasBeenActivated = true;
    }

    private void ActivatePartiturePanel()
    {
        if (GameManager.instance.vPressed && hasBeenActivated)
        {
            pressVPanel.SetActive(false);
            partitureSelectionPanel.SetActive(true);
            backArrow.SetActive(false);
        }
    }
}
