using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogActivator : MonoBehaviour
{

    public static DialogActivator instance;
    public string[] lines;
    private int fluteDifficulty;
    public bool canActivate;
    private string[] kasakirNormalLines = { "Hola viajero", "He escuchado que quieres mostrarme algo", "Adelante..." };
    private string[] quizaniNormalLines = { "Asi que has venido hasta aqui con ese objeto raro entre las manos...", "Tengo que admitir que me causa cierto interes", "*Expresion de intriga*" };
    private string[] naranNormalLines = { "Soy el dirigente Naran", "A mi no me suele impresionar nadie", "Puedes intentarlo, pero no creo que resulte muchahito" };
    private string[] necalliNormalLines = { "Soy el lider de la ciudad", "Me han dicho que lo que tienes para mostrarme cambiara la vida de todos en la ciudad", "Veamos si es cierto..." };

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if we are in the interactuable zone and we press enter and the dialog box is not already open

        if (canActivate && Input.GetKeyDown(KeyCode.Return) && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            // Only show dialogs when partitureSelectionPanel and pentagramManager are not active
            if (!DialogManager.instance.partitureSelectionPanel.activeInHierarchy && !DialogManager.instance.pentagramPanel.activeInHierarchy)
            {
                PartitureHabitant.instance.GetHabitant(this.gameObject);
                DialogManager.instance.GetHabitant(this.gameObject);

                if (this.gameObject.GetComponent<Audience>() != null)
                {
                    if (this.gameObject.name == "Kasakir")
                    {
                        ChangeDialogs("partiture3", 0, kasakirNormalLines, "woodenFlute");
                    }
                    else if (this.gameObject.name == "Quizani")
                    {
                        Debug.Log("Entro al if quizani");
                        ChangeDialogs("partiture6", 1, quizaniNormalLines, "woodenIronFlute");
                    }
                    else if (this.gameObject.name == "Naran")
                    {
                        ChangeDialogs("partiture9", 2, naranNormalLines, "ironFlute");
                    }
                }

                if (this.gameObject.GetComponent<Leader>() != null)
                {
                    ChangeDialogsLeader("partiture10", 2, necalliNormalLines, "ironFlute", "goldenFlute");
                }

                DialogManager.instance.ShowDialog(lines);

                //set boolean of habitant.interacted to true 
                if (this.gameObject.GetComponent<InteractedHabitants>() != null)
                {
                    this.gameObject.GetComponent<InteractedHabitants>().SetInteracted();
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }

    public bool CanActive()
    {
        return canActivate;
    }

    public bool CanActiveFalse()
    {
        canActivate = false;
        return canActivate;
    }

    private void GetFluteDifficulty()
    {

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < gameData.flute.Length; i++)
        {
            if (gameData.flute[i].isByDefault)
            {
                if (gameData.flute[i].name == "woodenFlute")
                {
                    fluteDifficulty = 0;
                }
                else if (gameData.flute[i].name == "woodenIronFlute")
                {
                    fluteDifficulty = 1;
                }
                else if (gameData.flute[i].name == "ironFlute")
                {
                    fluteDifficulty = 2;
                }
                else if (gameData.flute[i].name == "goldenFlute")
                {
                    fluteDifficulty = 2;
                }
            }
        }
    }

    private void ChangeDialogs(string partiture, int partitureDifficulty, string[] normalLines, string flute)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        GetFluteDifficulty();

        if (gameData.DoesHavePartiture(partiture) && partitureDifficulty <= fluteDifficulty && gameData.DoesHaveFlute(flute))
        {
            lines = normalLines;
        }
    }

    private void ChangeDialogsLeader(string partiture, int partitureDifficulty, string[] normalLines, string flute1, string flute2)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        GetFluteDifficulty();


        if (gameData.DoesHavePartiture(partiture) && partitureDifficulty <= fluteDifficulty && (gameData.DoesHaveFlute(flute1) || gameData.DoesHaveFlute(flute2)))
        {
            lines = normalLines;
            this.gameObject.GetComponent<Leader>().canActivatePartiturePanel = true;
        }
    }
}
