using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantMath : MonoBehaviour
{

    public bool finishedPartiture = false;
    public int uniqueHabitantPercentage = 0;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        //check if habitant has a partiture calification saved, if it has then hanitant.partiturefinished = true.
        if (gameData.habitantResult[index].result > 0)
        {
            //GameObject.Find(gameData.habitantResult[i].name).GetComponent<PartitureHabitant>().partitureFinished = true;
            this.gameObject.GetComponent<PartitureHabitant>().partitureFinished = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPercentage(GameObject habitant)
    {
        if (finishedPartiture)
        {
            uniqueHabitantPercentage = (PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes());

            XmlManager.instance.SaveHabitantsResults(index, uniqueHabitantPercentage);

            // Call Fabians function
            // Round the result to int
        }
    }
}
