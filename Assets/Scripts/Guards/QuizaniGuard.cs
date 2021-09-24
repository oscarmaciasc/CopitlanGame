using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizaniGuard : MonoBehaviour
{

    private string[] noPermit = { "No tienes el permiso necesario", "Obten el permiso convenciendo a los dirigentes", "Suerte y hasta la proxima" };

    private string[] permit = { "Adelante caballero", "Tiene el permiso necesario" };

    public GameObject habitant;

    public Animator myAnim;
    public float moveSpeed;
    private Vector2 destiny;
    [SerializeField] private GameObject theEntrance;

    public bool conversationFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        destiny = new Vector2(transform.position.x + 2, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        GameData gameData = new GameData();

        gameData = XmlManager.instance.LoadGame();

        if (gameData.DoesHavePermit("triangle"))
        {
            // The player has the requested permission
            habitant.GetComponent<DialogActivator>().lines = permit;

            if (conversationFinished)
            {
                Move();
            }
        }
        else
        {
            habitant.GetComponent<DialogActivator>().lines = noPermit;
        }

    }

    public void Move()
    {
        if (destiny.x != gameObject.transform.position.x)
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position, destiny, moveSpeed * Time.deltaTime);
            myAnim.SetFloat("moveX", 1);

            // Making the player Idle in the last direction
            myAnim.SetFloat("lastMoveX", 1);
        }
        else
        {
            // Finish the movement
            myAnim.SetFloat("moveX", 0);
            theEntrance.SetActive(true);
        }
    }

}
