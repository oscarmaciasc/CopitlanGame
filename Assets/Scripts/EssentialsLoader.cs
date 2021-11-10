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
                PlayerController clonePlayer = Instantiate(woman).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;

                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(womanBalloon).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;
                    clonePlayer.transform.position = new Vector3(12, -39, 0);
                }
            }
            else
            {
                PlayerController clonePlayer = Instantiate(man).GetComponent<PlayerController>();
                PlayerController.instance = clonePlayer;

                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    BalloonPlayerController cloneBalloon = Instantiate(maleBalloon).GetComponent<BalloonPlayerController>();
                    BalloonPlayerController.instance = cloneBalloon;
                    clonePlayer.transform.position = new Vector3(12, -39, 0);
                }
            }



        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
