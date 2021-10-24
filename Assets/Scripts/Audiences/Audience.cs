using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public static Audience instance;
    public bool finishedPartiture = false;
    public int res = 0;
    public bool canPass = false;
    public bool notFound = false;
    public bool notFoundFlute = false;
    public bool canActivatePartiturePanel = true;
    public int percentageToPass;
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] KasakirSuccess1 = { "Me has convencido", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirSuccess2 = { "Estoy realmente sorprendido", "tienes que mostrarle esto al mundo entero", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirSuccess3 = { "Estoy profundamente conmovido", "jamas habia escuchado nada igual", "mis ojos se humedecen al escuchar algo tan bello", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirFailure = { "No me has convencido del todo", "no veo el valor de esto en la ciudad", "reintentalo si quieres..." };
    private string[] QuizaniSuccess1 = { "Me has convencido", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran", "esto tiene que ser escuchado" };
    private string[] QuizaniSuccess2 = { "Estoy realmente sorprendido", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran", "suerte viajero" };
    private string[] QuizaniSuccess3 = { "Estoy profundamente conmovido", "me siento feliz y diferente, como si mi alma sonriera", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran" };
    private string[] QuizaniFailure = { "Ni siquiera tocaste bien", "se escuchaba algo extraño", "vuelve a intentarlo" };
    private string[] NaranSucces1 = { "Me has convencido", "adelante, ve con nuestro lider" };
    private string[] NaranSucces2 = { "Estoy realmente sorprendido", "adelante, nuestro lider Necalli estara feliz de escuchar esto" };
    private string[] NaranSucces3 = { "Estoy profundamente conmovido", "ahora la vida cobra color y me siento muy feliz", "no tengo palabras para agradecerte", "tienes que mostrarle esto al lider Naran, sigue tu camino" };
    private string[] NaranFailure = { "No veo por que el lider deberia de escuchar algo asi", "reintentalo a ver si me gusta esta vez..." };
    public string[] goodLinesKasakir = { "Adelante, continua tu camino viajero", "Quizani no es facil de convencer" };
    public string[] goodLinesQuizani = { "Adelante, continua tu camino viajero", "Naran no es facil de convencer" };
    public string[] goodLinesNaran = { "Has llegado demasiado lejos", "continua con tu camino y convence al lider Necalli" };
    public string[] noFlutesDialog = { "No tienes la flauta necesaria para interpretar la siguiente partitura", "prueba mejorando tu flauta" };
    private string[] normalLines;
    private bool successInterpretation = false;
    public bool hasFinished = false;
    public GameObject habitant;
    [SerializeField] private GameObject partitureSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.audienceResult[0].result >= 60)
        {
            habitant = GameObject.Find("Kasakir");
            if (habitant != null)
            {
                habitant.GetComponent<Audience>().hasFinished = true;
                habitant.GetComponent<DialogActivator>().lines = goodLinesKasakir;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }

        if (gameData.audienceResult[1].result >= 60)
        {
            habitant = GameObject.Find("Quizani");
            if (habitant != null)
            {
                habitant.GetComponent<Audience>().hasFinished = true;
                habitant.GetComponent<DialogActivator>().lines = goodLinesQuizani;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }

        if (gameData.audienceResult[2].result >= 60)
        {
            habitant = GameObject.Find("Naran");
            if (habitant != null)
            {
                habitant.GetComponent<Audience>().hasFinished = true;
                habitant.GetComponent<DialogActivator>().lines = goodLinesNaran;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }

        // Change this to a place where habitant already exist on the scene, for example: when you talk to the npc.
        if (habitant != null)
        {
            normalLines = habitant.GetComponent<DialogActivator>().lines;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPercentage(GameObject habitant)
    {
        if (finishedPartiture)
        {
            Debug.Log("Notas correctas: " + PentagramManager.instance.correctNotes);
            Debug.Log("Notes: " + PentagramManager.instance.TotalNotes());
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= 60)
            {
                canPass = true;
                res = (60) + (((PentagramManager.streakRes) * (40)) / ((PentagramManager.instance.TotalNotes())));
                Debug.Log("streakRes: " + PentagramManager.streakRes);
                Debug.Log("Resultado: " + res);

                GameData gameData = new GameData();
                gameData = XmlManager.instance.LoadGame();


                // send res as array to a file
                if (habitant.name == "Kasakir")
                {
                    XmlManager.instance.SaveAudienceResult(0, res);
                    if (res >= percentageToPass)
                    {
                        if (!gameData.DoesHavePermit("triangle"))
                        {
                            XmlManager.instance.AddPermission("triangle");
                        }
                    }
                }
                else if (habitant.name == "Quizani")
                {
                    XmlManager.instance.SaveAudienceResult(1, res);
                    if (res >= percentageToPass)
                    {
                        if (!gameData.DoesHavePermit("innerCircle"))
                        {
                            XmlManager.instance.AddPermission("innerCircle");
                        }
                    }
                }
                else if (habitant.name == "Naran")
                {
                    XmlManager.instance.SaveAudienceResult(2, res);
                    if (res >= percentageToPass)
                    {
                        if (!gameData.DoesHavePermit("necalliRoyalPalace"))
                        {
                            XmlManager.instance.AddPermission("necalliRoyalPalace");
                        }
                    }
                }

                if (res >= percentageToPass)
                {
                    successInterpretation = true;
                }
                else
                {
                    successInterpretation = false;
                    canPass = false;
                    finishedPartiture = false;
                }
            }
            else
            {
                canPass = false;
                finishedPartiture = false;
            }
        }
    }

    public void LimitPartitures(GameObject habitant)
    {
        if (habitant.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass /*&& canActivatePartiturePanel*/)
        {
            Debug.Log("ENTROOOOOOO AAAA");
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            Debug.Log("Entro OOOOOOOOOOOO");
            if (habitant.name == "Kasakir")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture3", habitant);
            }
            else if (habitant.name == "Quizani")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture6", habitant);
            }
            else if (habitant.name == "Naran")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture9", habitant);
            }

            Debug.Log("1: " + notFound);
            if (notFound)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noPartituresDialog;
                //canActivatePartiturePanel = false;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }

            Debug.Log("2: " + notFoundFlute);
            if (notFoundFlute)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noFlutesDialog;
                //canActivatePartiturePanel = false;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }
    }

    public void ChangeDirigentDialogLines(GameObject habitant)
    {
        if (successInterpretation)
        {
            if (habitant.name == "Kasakir")
            {
                if (res >= 60 && res < 80)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = KasakirSuccess1;
                }
                else if (res >= 80 && res < 90)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = KasakirSuccess2;
                }
                else if (res >= 90)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = KasakirSuccess3;
                }
            }
            else if (habitant.name == "Quizani")
            {
                if (res >= 70 && res < 80)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = QuizaniSuccess1;
                }
                else if (res >= 80 && res < 90)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = QuizaniSuccess2;
                }
                else if (res >= 90)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = QuizaniSuccess3;
                }
            }
            else if (habitant.name == "Naran")
            {
                if (res >= 80 && res < 90)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = NaranSucces1;
                }
                else if (res >= 90 && res < 95)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = NaranSucces2;
                }
                else if (res >= 95)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = NaranSucces3;
                }
            }
        }
        else
        {
            if (habitant.name == "Kasakir")
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = KasakirFailure;
            }
            else if (habitant.name == "Quizani")
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = QuizaniFailure;
            }
            else if (habitant.name == "Naran")
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = NaranFailure;
            }
        }
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }

    public bool NotFoundFlute()
    {
        notFoundFlute = true;
        return notFoundFlute;
    }  

    public void SetFound()
    {
        notFoundFlute = false;
        notFound = false;
    }

    public void SetNormalLines(GameObject habitant)
    {
        habitant.GetComponent<DialogActivator>().lines = normalLines;
        Debug.Log("Lines: " + habitant.GetComponent<DialogActivator>().lines);
    }
}
