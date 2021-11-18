using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject woman;
    public GameObject man;
    public GameObject womanBalloon;
    public GameObject maleBalloon;
    [SerializeField] private GameObject maleBalloonLvl2;
    [SerializeField] private GameObject maleBalloonLvl3;
    [SerializeField] private GameObject femaleBalloonLvl2;
    [SerializeField] private GameObject femaleBalloonLvl3;
    
    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (PlayerController.instance == null)
        {
            if (gameData.isWoman)
            {
                PlayerController clonePlayer = Instantiate(woman).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;
            }
            else
            {
                PlayerController clonePlayer = Instantiate(man).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;
            }
        }

        LoadBalloon();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadBalloon()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if(BalloonPlayerController.instance == null && SceneManager.GetActiveScene().name != "InitSequence1" && SceneManager.GetActiveScene().name != "InitSequence2")
        {
            if(gameData.isWoman)
            {
                if(gameData.balloon.name == "balloonLvl1")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(womanBalloon).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                } 
                else if (gameData.balloon.name == "balloonLvl2")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(femaleBalloonLvl2).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                }
                else if (gameData.balloon.name == "balloonLvl3")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(femaleBalloonLvl3).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                }
            } 
            else
            {
                if(gameData.balloon.name == "balloonLvl1")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(maleBalloon).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    InGame.instance.balloon = cloneBalloon.gameObject;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                } 
                else if (gameData.balloon.name == "balloonLvl2")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(maleBalloonLvl2).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                }
                else if (gameData.balloon.name == "balloonLvl3")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(maleBalloonLvl3).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;

                    cloneBalloon.GetComponent<BalloonPlayerController>().enabled = false;
                    cloneBalloon.GetComponent<BalloonManager>().enabled = false;
                    cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
                    cloneBalloon.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
    }
}
