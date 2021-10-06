using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public static Leader instance;
    public int kasakirResult = 0;
    public int quizaniResult = 0;
    public int naranResult = 0;
    public int resAudience = 0;
    public bool notFound = false;
    public bool canPass = false;
    public bool canActivatePartiturePanel = true;
    public bool finishedPartiture = false;
    private int percentageToPass = 90;
    private int resNecalli = 0;
    private int aprobationPercentageNecalli = 0;
    private double cityHappinessPercentage = 0;
    private int habitantHappinessPercentage = 0;
    public bool canActivateFinal = false;
    private bool successInterpretation = false;
    private string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    private string[] necalliSuccess1 = { "Me has convencido", "Enhoabuena, la musica regresara a Copitlan" };
    private string[] necalliSuccess2 = { "Estoy realmente sorprendido", "No entiendo como podiamos vivir sin esto", "te nombro Fundador de la Musica en Copitlan" };
    private string[] necalliSuccess3 = { "Estoy profundamente conmovido", "mis ojos se llenan de lagrimas pero no siento tristeza, solo una inmensa alegria", "esto es lo mejor que le ha pasado a Copitlan en siglos", "la musica volvera y te nombrare guardian de la felicidad" };
    private string[] necalliFailure = { "No me convence", "no veo por que te dejaron pasar a mi palacio", "quiza estes nervioso", "reintentalo si tienes el valor..." };
    private string[] goodLinesNecalli = {"*se escuchan sollozos de felicidad*", "es increible, no tengo palabras"};
    [SerializeField] private GameObject partitureSelectionPanel;
    public bool hasFinished = false;
    private GameObject habitant;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.audienceResult[3].result >= 90)
        {
            habitant = GameObject.Find("Necalli");
            if (habitant != null)
            {
                habitant.GetComponent<Leader>().hasFinished = true;
                habitant.GetComponent<DialogActivator>().lines = goodLinesNecalli;
                habitant.GetComponent<PartitureHabitant>().canShowPartitures = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetAudienceResults()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();


        kasakirResult = gameData.audienceResult[0].result;
        quizaniResult = gameData.audienceResult[1].result;
        naranResult = gameData.audienceResult[2].result;

        resAudience = kasakirResult + quizaniResult + naranResult;
        Debug.Log("Leader Res: " + resAudience);
    }

    public void GetPercentage(GameObject habitant)
    {
        if (finishedPartiture)
        {
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= percentageToPass)
            {
                canPass = true;
                resNecalli = (60) + (((PentagramManager.streakRes) * (40)) / ((PentagramManager.instance.TotalNotes())));
                aprobationPercentageNecalli = (resNecalli) + (resAudience / 30);

                if(aprobationPercentageNecalli >= 100)
                {
                    // This is to avoid strange situations where the calculate returns more than 100
                    aprobationPercentageNecalli = 100;
                }

                GameData gameData = new GameData();
                gameData = XmlManager.instance.LoadGame();

                // send res as array to a file
                XmlManager.instance.SaveAudienceResult(3, resNecalli);
                successInterpretation = true;
                canActivateFinal = true;
            }
            else
            {
                canPass = false;
                finishedPartiture = false;
            }
        }
    }

    public void ChangeLeaderDialogLines(GameObject habitant)
    {
        if (successInterpretation)
        {

            if (aprobationPercentageNecalli >= 90 && aprobationPercentageNecalli < 94)
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = necalliSuccess1;
            }
            else if (aprobationPercentageNecalli >= 94 && aprobationPercentageNecalli < 97)
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = necalliSuccess2;
            }
            else if (aprobationPercentageNecalli >= 97)
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = necalliSuccess3;
            }
        }
        else
        {
            habitant.gameObject.GetComponent<DialogActivator>().lines = necalliFailure;
        }
    }

    public void LimitPartitures(GameObject habitant)
    {
        if (habitant.GetComponent<PartitureHabitant>().conversationFinished == true && !canPass && canActivatePartiturePanel)
        {
            partitureSelectionPanel.SetActive(true);
        }

        if (partitureSelectionPanel.activeInHierarchy)
        {
            
            if (habitant.name == "Necalli")
            {
                PartitureSelection.instance.DeactivateLeaderPartitures("PanelPartiture10");
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

    public void GetCityHappinessPercentage()
    {
        cityHappinessPercentage = (aprobationPercentageNecalli * 0.8) + (habitantHappinessPercentage * 0.2);

        if(cityHappinessPercentage >= 72 && cityHappinessPercentage < 90)
        {
            ActivateFinal(1);
        } else if (cityHappinessPercentage >= 90 && cityHappinessPercentage < 95)
        {
            ActivateFinal(2);
        } else if (cityHappinessPercentage >= 95 && cityHappinessPercentage <= 100)
        {
            ActivateFinal(3);
        }

    }

    public void ActivateFinal(int final)
    {
        if (final == 1)
        {
            Debug.Log("Final 1");
        } else if (final == 2)
        {
            Debug.Log("Final 2");
        } else if (final == 3)
        {
            Debug.Log("Final 3");
        }
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }
}
