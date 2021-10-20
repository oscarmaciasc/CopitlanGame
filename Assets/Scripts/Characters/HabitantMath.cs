using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HabitantMath : MonoBehaviour
{

    public bool finishedPartiture = false;
    public int uniqueHabitantPercentage = 0;
    public int index;
    private string[] lines1 = {"Si te soy sincero... he quedado un poco indiferente"};
    private string[] lines2 = {"Me siento reconfortado y feliz"};
    private string[] lines3 = {"Estoy realmente sorprendido, me has dejado impactado"};
    private string[] lines4 = {"Estoy profundamente conmovido, mi corazon sonrie"};

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

            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            gameData.GetAndSaveHappinesPercentage();
        }
    }

    public void ChangeHabitantDialogLines(GameObject habitant)
    {
        if (uniqueHabitantPercentage > 0 && uniqueHabitantPercentage < 30)
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = lines1;
        }
        else if (uniqueHabitantPercentage >= 30 && uniqueHabitantPercentage < 60)
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = lines2;
        }
        else if (uniqueHabitantPercentage >= 60 && uniqueHabitantPercentage < 80)
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = lines3;
        }
        else if (uniqueHabitantPercentage >= 80 && uniqueHabitantPercentage <= 100)
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = lines4;
        }
    }
}
