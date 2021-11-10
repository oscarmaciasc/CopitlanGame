using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantLoader : MonoBehaviour
{
    [SerializeField] private GameObject CE1HouseHabitantA;
    [SerializeField] private GameObject CE1HouseHabitantB;
    [SerializeField] private GameObject CI1HouseHabitantA;
    [SerializeField] private GameObject CI1HouseHabitantB;
    private int houseID;
    private int habitantID;
    // Start is called before the first frame update
    void Start()
    {
        houseID = XmlManager.instance.GetHouseId();

        //OutterCircle1 houses
        if ((houseID >= 0 && houseID <= 8) && houseID % 2 == 0)
        {
            CE1HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 0 && houseID <= 8) && houseID % 2 != 0)
        {
            CE1HouseHabitantB.SetActive(true);
        }

        //OutterCircle2 houses


        //InnerCircle1 houses
        if ((houseID >= 59 && houseID <= 63) && houseID % 2 == 0)
        {
            CI1HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 59 && houseID <= 63) && houseID % 2 != 0)
        {
            CI1HouseHabitantB.SetActive(true);
        }

        // Formula y despues mandar el id resultante a la instancia de habitant interacted
        habitantID = houseID + 91;
        InteractedHabitants.instance.SetIndex(habitantID);

        Debug.Log("habitantID: " + habitantID);
        Debug.Log("houseID: " + houseID);
        Debug.Log("InteractedhabitantIndex: " + InteractedHabitants.instance.index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
