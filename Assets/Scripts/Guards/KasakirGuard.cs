using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasakirGuard : MonoBehaviour
{
    private string[] noPermit = { "No tienes el permiso necesario", "Obten el permiso convenciendo a los dirigentes", "Suerte y hasta la proxima" };

    private string[] permit = { "Adelante caballero", "Tiene el permiso necesario" };

    public GameObject habitant;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameData gameData = new GameData();

        // Replace the fake game index later...
        gameData = XmlManager.instance.LoadGame(1);

        if (gameData.DoesHavePermit("outterCircle"))
        {
            // The player has the requested permission
            habitant.GetComponent<DialogActivator>().lines = permit;
        }
        else
        {
            habitant.GetComponent<DialogActivator>().lines = noPermit;
        }

    }

}
