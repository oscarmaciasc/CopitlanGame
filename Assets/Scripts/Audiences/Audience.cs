using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    public static Audience instance;
    public bool finishedPartiture = false;
    public int percentageToPass;
    public int res = 0;
    public bool canPass = false;
    public bool notFound = false;
    public bool canActivatePartiturePanel = true;
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] KasakirSuccess1 = { "Me has convencido", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirSuccess2 = { "Estoy realmente sorprendido", "tienes que mostrarle esto al mundo entero", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirSuccess3 = { "Estoy profundamente conmovido", "jamas habia escuchado nada igual", "mis ojos se humedecen al escuchar algo tan bello", "te otorgo el permiso del triangulo para que vayas y hagas felices a mas personas" };
    private string[] KasakirFailure = { "No me has convencido del todo", "no veo el valor de esto en la ciudad", "reintentalo si quieres..." };
    private string[] QuizaniSuccess1 = { "Me has convencido", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran", "esto tiene que ser escuchado" };
    private string[] QuizaniSuccess2 = { "Estoy realmente sorprendido", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran", "suerte viajero" };
    private string[] QuizaniSuccess3 = { "Estoy profundamente conmovido", "me siento feliz y diferente, como si mi alma sonriera", "te otorgo el permiso del circulo interior para que vayas y hagas entrar en razon a Naran" };
    private string[] QuizaniFailure = { };
    private string[] NaranSucces1 = { };
    private string[] NaranSucces2 = { };
    private string[] NaranSucces3 = { };
    private string[] NaranFailure = { };
    private bool successInterpretation = false;
    [SerializeField] private GameObject partitureSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPercentage(GameObject habitant)
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                canPass = true;
                res = (60) + (((PentagramManager.streakRes) * (40)) / ((PentagramManager.instance.TotalNotes())));
                Debug.Log("Resultado: " + res);

                // send res as array to a file
                if (habitant.name == "Kasakir")
                {
                    XmlManager.instance.SaveAudienceResult(0, res);
                }
                else if (habitant.name == "Quizani")
                {
                    XmlManager.instance.SaveAudienceResult(1, res);
                }
                else if (habitant.name == "Naran")
                {
                    XmlManager.instance.SaveAudienceResult(2, res);
                }

                successInterpretation = true;
            }
            else
            {
                canPass = false;
                finishedPartiture = false;
            }
        }
    }

    public int GetRes()
    {
        return res;
    }

    public void LimitPartitures(GameObject habitant)
    {
        if (habitant.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass && canActivatePartiturePanel)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            if (habitant.name == "Kasakir")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture1", "PanelPartiture2", "PanelPartiture3");
            }
            else if (habitant.name == "Quizani")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture4", "PanelPartiture5", "PanelPartiture6");
            }
            else if (habitant.name == "Naran")
            {
                PartitureSelection.instance.DeactivateDirigentPartitures("PanelPartiture7", "PanelPartiture8", "PanelPartiture9");
            }

            if (notFound)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noPartituresDialog;
                canActivatePartiturePanel = false;
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
                    habitant.gameObject.GetComponent<DialogActivator>().lines = NaranSucces1;
                }
                else if (res >= 95)
                {
                    habitant.gameObject.GetComponent<DialogActivator>().lines = NaranSucces1;
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
}