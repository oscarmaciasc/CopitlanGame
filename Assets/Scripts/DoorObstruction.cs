using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObstruction : MonoBehaviour
{
    public string doorName;
    private string[] noPermit = { "No tienes el permiso necesario", "Obten el permiso convenciendo a los dirigentes", "Suerte y hasta la proxima" };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameData gameData = new GameData();
            gameData = XmlManager.instance.LoadGame();

            if (!gameData.DoesHavePermit(doorName))
            {
                DialogManager.instance.ShowDialog(noPermit);
            }
        }
    }
}
