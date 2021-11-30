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
            CE1HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Bienvenido a mi casa, espero te sientas comodo"};
        }
        else if ((houseID >= 0 && houseID <= 8) && houseID % 2 != 0)
        {
            CE1HouseHabitantB.SetActive(true);
            CE1HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"Hola, estas casas son modestas pero acogedoras"};
        }

        //OutterCircle2 houses
        if ((houseID >= 9 && houseID <= 17) && houseID % 2 == 0)
        {
            CE2HouseHabitantA.SetActive(true);
            CE2HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Me meti a la casa porque afuera hacia mucho calor"};
        }
        else if ((houseID >= 9 && houseID <= 17) && houseID % 2 != 0)
        {
            CE2HouseHabitantB.SetActive(true);
            CE2HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"Vengo de las minas, ahora soy rico en hierro"};
        }

        //OutterCircle3 houses
        if ((houseID >= 18 && houseID <= 27) && houseID % 2 == 0)
        {
            CE3HouseHabitantA.SetActive(true);
            CE3HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Quisiera vivir en el triangulo, ahi las casas son mas bonitas"};
        }
        else if ((houseID >= 18 && houseID <= 27) && houseID % 2 != 0)
        {
            CE3HouseHabitantB.SetActive(true);
            CE3HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"Mi hermana salio al bosque y no ha regresado..."};
        }

        //OutterCircle4 houses
        if ((houseID >= 28 && houseID <= 36) && houseID % 2 == 0)
        {
            CE4HouseHabitantA.SetActive(true);
            CE4HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Acabo de llegar de visitar a Kasakir, ultimamente esta muy serio"};
        }
        else if ((houseID >= 28 && houseID <= 36) && houseID % 2 != 0)
        {
            CE4HouseHabitantB.SetActive(true);
            CE4HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"La proxima semana voy a ir al circulo a comprar un globo"};
        }

        //Triangle1 houses
        if ((houseID >= 37 && houseID <= 41) && houseID % 2 == 0)
        {
            T1HouseHabitantA.SetActive(true);
            T1HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Bienvenido al triangulo, espero te guste la zona"};

            if(houseID == 38)
            {
                partiture5.SetActive(true);
            }
        }
        else if ((houseID >= 37 && houseID <= 41) && houseID % 2 != 0)
        {
            T1HouseHabitanB.SetActive(true);
            T1HouseHabitanB.GetComponent<DialogActivator>().lines = new string[] {"Acabo de llegar del cementerio, se encuentra en la parte Este del bosque"};
        }

        //Triangle2 houses
        if ((houseID >= 42 && houseID <= 47) && houseID % 2 == 0)
        {
            T2HouseHabitantA.SetActive(true);
            T2HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Mi padre se dedica a la construccion de casas", "El hizo la mayoria del triangulo"};
        }
        else if ((houseID >= 42 && houseID <= 47) && houseID % 2 != 0)
        {
            T2HouseHabitanB.SetActive(true);
            T2HouseHabitanB.GetComponent<DialogActivator>().lines = new string[] {"La escuela esta abandonada, asi que estudiamos desde casa"};
        }

        //Triangle3 houses
        if ((houseID >= 48 && houseID <= 53) && houseID % 2 == 0)
        {
            T3HouseHabitantA.SetActive(true);
            T3HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"El dirigente Quizani es dificil de convencer", "pero todos aqui confiamos en su juicio"};
        }
        else if ((houseID >= 48 && houseID <= 53) && houseID % 2 != 0)
        {
            T3HouseHabitanB.SetActive(true);
            T3HouseHabitanB.GetComponent<DialogActivator>().lines = new string[] {"Las casas del circulo interior son hermosas, aunque algo mas pequeñas que las de aqui"};
        }

        //Triangle4 houses
        if ((houseID >= 54 && houseID <= 58) && houseID % 2 == 0)
        {
            T4HouseHabitantA.SetActive(true);
            T4HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"El herrero de esta zona es impresionante", "Aunque dicen que el del circulo interior puede hacer artefactos mas avanzados"};
        }
        else if ((houseID >= 54 && houseID <= 58) && houseID % 2 != 0)
        {
            T4HouseHabitanB.SetActive(true);
            T4HouseHabitanB.GetComponent<DialogActivator>().lines = new string[] {"Aunque la madera es muy util a veces es mejor intentar conseguir hierro u oro","Son mas valiosos..."};
        }

        //InnerCircle1 houses
        if ((houseID >= 59 && houseID <= 63) && houseID % 2 == 0)
        {
            CI1HouseHabitantA.SetActive(true);
            CI1HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Bienvenido al Circulo Interior", "Esta es la zona mas avanzada de la ciudad"};
        }
        else if ((houseID >= 59 && houseID <= 63) && houseID % 2 != 0)
        {
            CI1HouseHabitantB.SetActive(true);
            CI1HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"El dirigente Naran es muy exigente", "tanto asi que muy pocos han podido convencerlo de ver al lider Necalli"};
        }

        //InnerCircle2 houses
        if ((houseID >= 64 && houseID <= 68) && houseID % 2 == 0)
        {
            CI2HouseHabitantA.SetActive(true);
            CI2HouseHabitantA.GetComponent<DialogActivator>().lines = new string[] {"Algun dia quiero llegar a ser un herrero", "Es por eso que me la paso todo el dia en la biblioteca"};
        }
        else if ((houseID >= 64 && houseID <= 68) && houseID % 2 != 0)
        {
            CI2HouseHabitantB.SetActive(true);
            CI2HouseHabitantB.GetComponent<DialogActivator>().lines = new string[] {"Vi al lider Necalli", "lo juro, lo vi con mis propios ojos"};
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
