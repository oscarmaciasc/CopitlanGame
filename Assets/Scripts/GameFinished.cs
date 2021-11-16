using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject musicalMasteryLevel;
    [SerializeField] private GameObject happinessPercentage;
    [SerializeField] private GameObject minPlayed;
    [SerializeField] private GameObject minWalked;
    [SerializeField] private GameObject minBalloon;
    [SerializeField] private GameObject interactedHabitants;
    [SerializeField] private GameObject collectables;
    [SerializeField] private GameObject outterCirclePermission;
    [SerializeField] private GameObject trianglePermission;
    [SerializeField] private GameObject innerCirclePermission;
    [SerializeField] private GameObject femalePanel;
    [SerializeField] private GameObject malePanel;

    private void Start()
    {
        GameData gameData = XmlManager.instance.LoadGame();
        int counter = 0;

        name.GetComponent<Text>().text = gameData.name;

        if (gameData.isWoman)
        {
            femalePanel.SetActive(true);
        }
        else
        {
            malePanel.SetActive(true);
        }

        musicalMasteryLevel.transform.GetComponent<Text>().text = gameData.GetMusicalMasteryLevel();

        Debug.Log("Necalli: " + gameData.GetAudienceResult("necalliResult"));
        happinessPercentage.transform.GetComponent<Text>().text = gameData.happinessPercentage.percentage.ToString();

        minPlayed.transform.GetComponent<Text>().text = FormatTime(gameData.timePlayed.time);
        minWalked.transform.GetComponent<Text>().text = FormatTime(gameData.timeWalked.time);
        minBalloon.transform.GetComponent<Text>().text = FormatTime(gameData.timeBalloon.time);
        if (gameData.collectable == null)
        {
            collectables.transform.GetComponent<Text>().text = "0";
        }
        else
        {
            Debug.Log("Length collectables: " + gameData.collectable.Length);
            collectables.transform.GetComponent<Text>().text = gameData.collectable.Length.ToString();
        }

        for (int i = 0; i < gameData.habitantInteracted.Length; i++)
        {
            if (gameData.habitantInteracted[i].interacted)
            {
                Debug.Log("counter: " + counter);
                counter++;
            }
        }

        interactedHabitants.transform.GetComponent<Text>().text = counter.ToString();

        if (gameData.DoesHavePermit("outterCircle"))
        {
            outterCirclePermission.SetActive(true);
        }
        else
        {
            outterCirclePermission.SetActive(false);
        }

        if (gameData.DoesHavePermit("triangle"))
        {
            trianglePermission.SetActive(true);
        }
        else
        {
            trianglePermission.SetActive(false);
        }

        if (gameData.DoesHavePermit("innerCircle"))
        {
            innerCirclePermission.SetActive(true);
        }
        else
        {
            innerCirclePermission.SetActive(false);
        }
    }
    public void Return()
    {
        SceneManager.LoadScene("GameSelection");
    }

    // Formats a float into a minutes fomat
    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)(time - (60 * minutes));
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
