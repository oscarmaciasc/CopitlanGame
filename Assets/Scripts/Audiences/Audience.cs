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
    public string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] KasakirSuccess1 = { "Me has convencido", "te otorgo el permiso del triangulo para que vayas con el dirigente Quizani y hagas felices a mas personas" };
    private string[] KasakirSuccess2 = { "Estoy realmente sorprendido", "tienes que mostrarle esto al mundo entero", "te otorgo el permiso del triangulo para que vayas con el dirigente Quizani y hagas felices a mas personas" };
    private string[] KasakirSuccess3 = { "Estoy profundamente conmovido", "jamas habia escuchado nada igual", "mis ojos se humedecen al escuchar algo tan bello", "te otorgo el permiso del triangulo para que vayas con el dirigente Quizani y hagas felices a mas personas" };
    public string[] KasakirFailure = { "No me has convencido del todo", "no veo el valor de esto en la ciudad", "reintentalo si quieres..." };
    private string[] kasakirNormalLines = { "Hola viajero, soy el dirigente Kasakir", "He escuchado que quieres mostrarme algo", "Adelante..." };
    private string[] QuizaniSuccess1 = { "Me has convencido", "te otorgo el permiso del circulo interior para que vayas con el dirigente Naran y hagas entrar en razon a Naran", "esto tiene que ser escuchado" };
    private string[] QuizaniSuccess2 = { "Estoy realmente sorprendido", "te otorgo el permiso del circulo interior para que vayas con el dirigente Naran lo hagas entrar en razon", "suerte viajero" };
    private string[] QuizaniSuccess3 = { "Estoy profundamente conmovido", "me siento feliz y diferente, como si mi alma sonriera", "te otorgo el permiso del circulo interior para que vayas con el dirigente Naran y lo hagas entrar en razon" };
    public string[] QuizaniFailure = { "Ni siquiera tocaste bien", "se escuchaba algo raro", "vuelve a intentarlo" };
    private string[] quizaniNormalLines = { "Hola, soy el dirigente Quizani", "Asi que has venido hasta aqui con ese objeto raro entre las manos...", "Tengo que admitir que me causa cierto interes", "*Expresion de intriga*" };
    private string[] NaranSucces1 = { "Me has convencido", "A mis espaldas hay una puerta, el guardia te dejara pasar", "En la otra habitacion esta el lider Necalli" };
    private string[] NaranSucces2 = { "Estoy realmente sorprendido", "A mis espaldas hay una puerta, el guardia te dejara pasar", "En la otra habitacion esta el lider Necalli, estara feliz de escuchar esto"};
    private string[] NaranSucces3 = { "Estoy profundamente conmovido", "ahora la vida cobra color y me siento muy feliz", "no tengo palabras para agradecerte", "A mis espaldas hay una puerta, el guardia te dejara pasar", "En la otra habitacion esta el lider Necalli" };
    public string[] NaranFailure = { "No veo por que el lider Necalli deberia de escuchar algo asi", "reintentalo a ver si me gusta esta vez..." };
    private string[] naranNormalLines = { "Soy el dirigente Naran", "A mi no me suele impresionar nadie", "Puedes intentarlo, pero no creo que resulte" };
    public string[] goodLinesKasakir = { "Adelante, continua tu camino al triangulo", "Quizani no es facil de convencer" };
    public string[] goodLinesQuizani = { "Adelante, continua tu camino al circulo interior", "Naran no es facil de convencer" };
    public string[] goodLinesNaran = { "Has llegado demasiado lejos", "continua con tu camino y convence al lider Necalli" };
    public string[] noFlutesDialog = { "No tienes la flauta necesaria para interpretar la siguiente partitura", "prueba mejorando tu flauta" };
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPercentage(GameObject habitant)
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= 60)
            {
                canPass = true;
                res = (60) + (((PentagramManager.streakRes) * (40)) / ((PentagramManager.instance.TotalNotes())));

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
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
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

            if (notFound)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noPartituresDialog;
                //canActivatePartiturePanel = false;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }

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
        if (habitant.name == "Kasakir")
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = kasakirNormalLines;
        }
        else if (habitant.name == "Quizani")
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = quizaniNormalLines;
        }
        else if (habitant.name == "Naran")
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = naranNormalLines;
        }
    }
}
