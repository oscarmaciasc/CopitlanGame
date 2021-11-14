using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasakirGuard : MonoBehaviour
{
    private string[] noPermit = { "No tienes el permiso necesario", "Obten el permiso convenciendo a los dirigentes", "Suerte y hasta la proxima" };

    private string[] permit = { "Adelante caballero", "Tiene el permiso necesario" };
    private string[] finished = { "Sigue tu aventura", "espero regreses pronto" };
    [SerializeField] private GameObject doorObstruction;


    public GameObject habitant;
    [SerializeField] private GameObject theEntrance;

    public bool conversationFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        //destiny = new Vector2(transform.position.x + 2, transform.position.y);

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if(gameData.dirigentEntrance[0].shouldBeActive)
        {
            theEntrance.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        GameData gameData = new GameData();

        gameData = XmlManager.instance.LoadGame();

        if (gameData.DoesHavePermit("outterCircle"))
        {
            // The player has the requested permission
            habitant.GetComponent<DialogActivator>().lines = permit;
            //theEntrance.SetActive(true);
            XmlManager.instance.SaveDirigentEntranceState(0, true);
            doorObstruction.SetActive(false);
            

            if (conversationFinished)
            {
                //Move();
                habitant.GetComponent<DialogActivator>().lines = finished;
            }
        }
        else
        {
            habitant.GetComponent<DialogActivator>().lines = noPermit;
        }

    }

    // public void Move()
    // {
    //     if (destiny.x != gameObject.transform.position.x)
    //     {
    //         gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
    //         myAnim.SetFloat("moveX", 1);

    //         // Making the player Idle in the last direction
    //         myAnim.SetFloat("lastMoveY", -1);
    //     }
    //     else
    //     {
    //         // Finish the movement
    //         myAnim.SetFloat("moveX", 0);
    //         theEntrance.SetActive(true);
    //         XmlManager.instance.SaveDirigentEntranceState(0, true);
    //         Debug.Log("ya guarde la info");
    //     }
    // }

}
