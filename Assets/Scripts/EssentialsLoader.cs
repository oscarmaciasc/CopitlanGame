using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EssentialsLoader : MonoBehaviour
{
    //public GameObject UIScreen;
    public GameObject woman;

    public GameObject man;

    public GameObject womanBalloon;

    public GameObject maleBalloon;

    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (PlayerController.instance == null)
        {
            if (gameData.isWoman)
            {
                PlayerController clonePlayer =
                    Instantiate(woman).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;

                if (SceneManager.GetActiveScene().name == "SE-Papataca")
                {
                    clonePlayer.transform.position = new Vector3(166, -162, 0);
                }
            }
            else
            {
                PlayerController clonePlayer =
                    Instantiate(man).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;

                if (SceneManager.GetActiveScene().name == "SE-Papataca")
                {
                    clonePlayer.transform.position = new Vector3(166, -162, 0);
                }
            }
        }
        else if (
            PlayerController.instance != null &&
            PlayerController.instance.areaTransitionName ==
            "Tutorial-PapatacaSE"
        )
        {
            // If the player is already loaded due to the tutorial but were in the first scene
            BalloonPlayerController cloneBalloon =
                Instantiate(womanBalloon)
                    .GetComponent<BalloonPlayerController>();
            BalloonPlayerController.instance = cloneBalloon;

            cloneBalloon.GetComponent<BalloonPlayerController>().enabled =
                false;
            cloneBalloon.GetComponent<BalloonManager>().enabled = false;
            cloneBalloon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
