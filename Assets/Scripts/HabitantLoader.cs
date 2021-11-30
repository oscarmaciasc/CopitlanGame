using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitantLoader : MonoBehaviour
{
    public static HabitantLoader instance;

    [SerializeField] private GameObject CE1HouseHabitantA; //NPC0
    [SerializeField] private GameObject CE1HouseHabitantB; //NPC1
    [SerializeField] private GameObject CE2HouseHabitantA; //NPC2
    [SerializeField] private GameObject CE2HouseHabitantB; //NPC3
    [SerializeField] private GameObject CE3HouseHabitantA; //NPC4
    [SerializeField] private GameObject CE3HouseHabitantB; //NPC0
    [SerializeField] private GameObject CE4HouseHabitantA; //NPC1
    [SerializeField] private GameObject CE4HouseHabitantB; //NPC2
    [SerializeField] private GameObject T1HouseHabitantA; //NPC3
    [SerializeField] private GameObject T1HouseHabitanB; // NPC4
    [SerializeField] private GameObject T2HouseHabitantA; //NPC0
    [SerializeField] private GameObject T2HouseHabitanB; // NPC1
    [SerializeField] private GameObject T3HouseHabitantA; //NPC2
    [SerializeField] private GameObject T3HouseHabitanB; // NPC3
    [SerializeField] private GameObject T4HouseHabitantA; //NPC4
    [SerializeField] private GameObject T4HouseHabitanB; // NPC0
    [SerializeField] private GameObject CI1HouseHabitantA; //NPC1
    [SerializeField] private GameObject CI1HouseHabitantB; //NPC2
    [SerializeField] private GameObject CI2HouseHabitantA; //NPC3
    [SerializeField] private GameObject CI2HouseHabitantB; //NPC4
    [SerializeField] private GameObject partiture5;
    public int houseID;
    public int habitantID;
    // Start is called before the first frame update
    void Start()
    {

        instance = this;

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
        if ((houseID >= 9 && houseID <= 17) && houseID % 2 == 0)
        {
            CE2HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 9 && houseID <= 17) && houseID % 2 != 0)
        {
            CE2HouseHabitantB.SetActive(true);
        }

        //OutterCircle3 houses
        if ((houseID >= 18 && houseID <= 27) && houseID % 2 == 0)
        {
            CE3HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 18 && houseID <= 27) && houseID % 2 != 0)
        {
            CE3HouseHabitantB.SetActive(true);
        }

        //OutterCircle4 houses
        if ((houseID >= 28 && houseID <= 36) && houseID % 2 == 0)
        {
            CE4HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 28 && houseID <= 36) && houseID % 2 != 0)
        {
            CE4HouseHabitantB.SetActive(true);
        }

        //Triangle1 houses
        if ((houseID >= 37 && houseID <= 41) && houseID % 2 == 0)
        {
            T1HouseHabitantA.SetActive(true);
            if(houseID == 38)
            {
                partiture5.SetActive(true);
            }
        }
        else if ((houseID >= 37 && houseID <= 41) && houseID % 2 != 0)
        {
            T1HouseHabitanB.SetActive(true);
        }

        //Triangle2 houses
        if ((houseID >= 42 && houseID <= 47) && houseID % 2 == 0)
        {
            T2HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 42 && houseID <= 47) && houseID % 2 != 0)
        {
            T2HouseHabitanB.SetActive(true);
        }

        //Triangle3 houses
        if ((houseID >= 48 && houseID <= 53) && houseID % 2 == 0)
        {
            T3HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 48 && houseID <= 53) && houseID % 2 != 0)
        {
            T3HouseHabitanB.SetActive(true);
        }

        //Triangle4 houses
        if ((houseID >= 54 && houseID <= 58) && houseID % 2 == 0)
        {
            T4HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 54 && houseID <= 58) && houseID % 2 != 0)
        {
            T4HouseHabitanB.SetActive(true);
        }

        //InnerCircle1 houses
        if ((houseID >= 59 && houseID <= 63) && houseID % 2 == 0)
        {
            CI1HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 59 && houseID <= 63) && houseID % 2 != 0)
        {
            CI1HouseHabitantB.SetActive(true);
        }

        //InnerCircle2 houses
        if ((houseID >= 64 && houseID <= 68) && houseID % 2 == 0)
        {
            CI2HouseHabitantA.SetActive(true);
        }
        else if ((houseID >= 64 && houseID <= 68) && houseID % 2 != 0)
        {
            CI2HouseHabitantB.SetActive(true);
        }

        // Formula y despues mandar el id resultante a la instancia de habitant interacted
        habitantID = houseID + 91;

        if(InteractedHabitants.instance != null)
        {
            InteractedHabitants.instance.index = habitantID;
            Debug.Log("InteractedhabitantIndex: " + InteractedHabitants.instance.index);
        }

        CheckEntrances();

        AreaExit.instance.areaTransitionName += "-" + houseID;
        AreaExit.instance.theEntrance.transitionName = AreaExit.instance.areaTransitionName;
        AreaEntrance.instance.CheckTransitionName();

        Debug.Log("habitantID: " + habitantID);
        Debug.Log("houseID: " + houseID);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckEntrances()
    {
        // S-OutterCircle
        if(houseID == 0 || houseID == 13 || houseID == 18 || houseID == 23 || houseID == 28)
        {
            AreaExit.instance.areaToLoad = "S-OutterCircle";

            if(houseID == 0)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleS-HouseCE1";
            } 

            if(houseID == 13)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleS-HouseCE2";
            }

            if(houseID == 18 || houseID == 23)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleS-HouseCE3";
            } 

            if(houseID == 28)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleS-HouseCE4";
            }

        } 
        
        // E-OutterCircle
        if((houseID >= 5 && houseID <= 8) || (houseID >= 14 && houseID <= 17) || (houseID >= 24 && houseID <= 27) || (houseID >= 33 && houseID <= 36))
        {
            AreaExit.instance.areaToLoad = "E-OutterCircle";
            
            if(houseID >= 5 && houseID <= 8)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleE-HouseCE1";
            }

            if(houseID >= 14 && houseID <= 17)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleE-HouseCE2";
            }

            if(houseID >= 24 && houseID <= 27)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleE-HouseCE3";
            }

            if(houseID >= 33 && houseID <= 36)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleE-HouseCE4";
            }
        }

        // W-OutterCircle
        if((houseID >= 1 && houseID <= 4) || (houseID >= 9 && houseID <= 12) || (houseID >= 19 && houseID <= 22) || (houseID >= 29 && houseID <= 32))
        {
            AreaExit.instance.areaToLoad = "W-OutterCircle";

            if(houseID >= 1 && houseID <= 4)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleW-HouseCE1";
            }

            if(houseID >= 9 && houseID <= 12)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleW-HouseCE2";
            }

            if(houseID >= 19 && houseID <= 22)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleW-HouseCE3";
            }

            if(houseID >= 29 && houseID <= 32)
            {
                AreaExit.instance.areaTransitionName = "OutterCircleW-HouseCE4";
            }
        }
    }
}
