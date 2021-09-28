using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{

    public bool finishedPartiture = false;
    public int percentageToPass;
    public int res;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetPercentage();
    }

    public void GetPercentage()
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                Debug.Log("Enhorabuena, has pasado");
                this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();

                res = (60) + (((PentagramManager.maxStreak)*(40))/(PentagramManager.instance.TotalNotes()));
                Debug.Log("Max Streak: " + PentagramManager.maxStreak);
                Debug.Log("Porcentaje de aprobacion de dirigente: " + res); 

                // Sent this to stats and do the following math in the game
            }
            else
            {
                Debug.Log("No me terminas de convencer, intentalo de nuevo");
                finishedPartiture = false;
            }
            Debug.Log("Correct Notes: " + PentagramManager.instance.correctNotes);
            Debug.Log("Total Notes: " + PentagramManager.instance.TotalNotes());
        }
    }
}
