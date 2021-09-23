using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{

    public static Mines instance;
    [SerializeField] private GameObject theEntrance;
    public bool canPass = false;
    public Animator myAnim;
    public float moveSpeed;
    private Vector2 destiny;
    public bool finishedPartiture = false;
    public int percentageToPass;
    public int correctNotes = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        destiny = new Vector2(transform.position.x + 3, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {



        GetPercentage();
        CheckIfCanPass();

        // Falta aun que si falla pueda volver a intentarlo
        // Falta limitar a que solo se pueda interpretar partituras faciles en Tecalli y Acan y media en Seti
        // Falta aun que este script sea persnalizado para los habitantes de minas
    }

    public void GetPercentage()
    {
        if(finishedPartiture)
        {
            if(((PentagramManager.instance.correctNotes*100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                Debug.Log("Enhorabuena, has pasado");
                this.gameObject.GetComponent<DialogActivator>().CanActiveFalse();
                canPass = true;
            }
            else
            {
                Debug.Log("No me terminas de convencer, intentalo de nuevo");
                canPass = false;
                finishedPartiture = false;
            }
            Debug.Log("Correct Notes: " + PentagramManager.instance.correctNotes);
            Debug.Log("Total Notes: " + PentagramManager.instance.TotalNotes());
        }
    }

    public void CheckIfCanPass()
    {
        if (canPass)
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
                
                Destroy(this.gameObject);
                theEntrance.SetActive(true);
            }
        }
        else
        {
            // Interpretate Partiture again
        }
    }
}
