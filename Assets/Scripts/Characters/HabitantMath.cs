using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantMath : MonoBehaviour
{

    public bool finishedPartiture = false;
    public int uniqueHabitantPercentage = 0;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPercentage(GameObject habitant)
    {
        if(finishedPartiture)
        {
           uniqueHabitantPercentage = (PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes());

           XmlManager.instance.SaveHabitantsResults(habitant.name, index, uniqueHabitantPercentage);
           index++;
        }
    }
}
