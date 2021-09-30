using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{

    public int kasakirResult = 0;
    public int quizaniResult = 0;
    public int naranResult = 0;
    public int res = 0;
    public GameObject kasakir;
    public GameObject quizani;
    public GameObject naran;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetAudienceResults()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        kasakirResult = gameData.ReturnAudienceResult(0);
        quizaniResult = gameData.ReturnAudienceResult(1);
        naranResult = gameData.ReturnAudienceResult(2);

        res = kasakirResult + quizaniResult + naranResult;
        Debug.Log("Leader Res: " + res);
    }
}
