using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public static HudController instance;
    [SerializeField] GameObject exitMenuHud;
    [SerializeField] GameObject pauseMenuHud;
    [SerializeField] GameObject talkHud;
    [SerializeField] GameObject balloonHud;
    [SerializeField] GameObject fluteHud;
    [SerializeField] GameObject mapHud;
    [SerializeField] GameObject inventoryHud;

    void Start()
    {
        instance = this;
        DeactivateTalkHud();
        SetBalloon();
    }

    public void DeactivateTalkHud()
    {
        talkHud.SetActive(false);
    }

    public void ActivateTalkHud()
    {
        talkHud.SetActive(true);
    }

    public bool IsTalkHudActive()
    {
        return talkHud.gameObject.activeInHierarchy;
    }

    private void SetBalloon()
    {
        if(!InGame.instance.canActivateBalloon) {
            balloonHud.SetActive(false);
        }
        else{
            balloonHud.SetActive(true);
        }
    }
}
