using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leader : MonoBehaviour
{
    public static Leader instance;
    public int kasakirResult = 0;
    public int quizaniResult = 0;
    public int naranResult = 0;
    public int resAudience = 0;
    public bool notFound = false;
    public bool notFoundFlute = false;
    public bool canPass = false;
    public bool canActivatePartiturePanel = true;
    public bool finishedPartiture = false;
    private int resNecalli = 0;
    private float timeToWait = 4f;
    private int aprobationPercentageNecalli = 0;
    private int cityHappinessPercentage = 0;
    public bool canActivateFinal = false;
    private bool successInterpretation = false;
    public string[] noPartituresDialog = { "Parece que no tienes la partitura necesaria", "Vuelve cuando la tengas" };
    public string[] necalliNormalLines = { "Soy el lider de la ciudad", "Me han dicho que lo que tienes para mostrarme cambiara la vida de todos en la ciudad", "Veamos si es cierto..." };
    public string[] necalliSuccess1 = { "Me has convencido", "Enhoabuena, la musica regresara a Copitlan" };
    public string[] necalliSuccess2 = { "Estoy realmente sorprendido", "No entiendo como podiamos vivir sin esto", "te nombro Fundador de la Musica en Copitlan" };
    public string[] necalliSuccess3 = { "Estoy profundamente conmovido", "mis ojos se llenan de lagrimas pero no siento tristeza, solo una inmensa alegria", "esto es lo mejor que le ha pasado a Copitlan en siglos", "la musica volvera y te nombrare guardian de la felicidad" };
    public string[] necalliFailure = { "No me convence", "no veo por que te dejaron pasar a mi palacio", "quiza estes nervioso", "reintentalo si tienes el valor..." };
    private string[] goodLinesNecalli = { "*se escuchan sollozos de felicidad*", "es increible, no tengo palabras" };
    public string[] noFlutesDialog = { "No tienes la flauta necesaria para interpretar la siguiente partitura", "prueba mejorando tu flauta" };
    [SerializeField] private GameObject partitureSelectionPanel;
    [SerializeField] private GameObject pentagramPanel;
    [SerializeField] private GameObject dialogBox;
    public bool hasFinished = false;
    public bool shouldTryAgain = false;
    public bool shouldGetCityHappinessPercentage = false;
    private GameObject habitant;
    public bool conversationFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (finishedPartiture && canActivateFinal && shouldGetCityHappinessPercentage)
        {
            GetCityHappinessPercentage();
        }
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
            if (((PentagramManager.instance.correctNotes * 100) / (PentagramManager.instance.TotalNotes())) >= 60)
            {
                canPass = true;
                resNecalli = (60) + (((PentagramManager.streakRes) * (40)) / ((PentagramManager.instance.TotalNotes())));
                aprobationPercentageNecalli = (resNecalli) + (resAudience / 30);

                if (aprobationPercentageNecalli >= 100)
                {
                    // This is to avoid strange situations where the calculate returns more than 100
                    aprobationPercentageNecalli = 100;
                }

                GameData gameData = new GameData();
                gameData = XmlManager.instance.LoadGame();

                // send res as array to a file
                XmlManager.instance.SaveAudienceResult(3, aprobationPercentageNecalli);

                if (aprobationPercentageNecalli >= 90)
                {
                    successInterpretation = true;
                    canActivateFinal = true;
                }
                else
                {
                    successInterpretation = false;
                    canPass = false;
                    finishedPartiture = false;
                    shouldTryAgain = true;
                }
            }
            else
            {
                canPass = false;
                finishedPartiture = false;
                shouldTryAgain = true;
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
                StartCoroutine(ShowDialogs(necalliSuccess1, habitant));
            }
            else if (aprobationPercentageNecalli >= 94 && aprobationPercentageNecalli < 97)
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = necalliSuccess2;
                StartCoroutine(ShowDialogs(necalliSuccess2, habitant));

            }
            else if (aprobationPercentageNecalli >= 97)
            {
                habitant.gameObject.GetComponent<DialogActivator>().lines = necalliSuccess3;
                StartCoroutine(ShowDialogs(necalliSuccess3, habitant));
            }
        }
        else if (!successInterpretation && shouldTryAgain)
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
                PartitureSelection.instance.DeactivateLeaderPartitures("PanelPartiture10", habitant);
            }

            if (notFound)
            {
                partitureSelectionPanel.SetActive(false);
                habitant.GetComponent<DialogActivator>().lines = noPartituresDialog;
                canActivatePartiturePanel = false;
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

    public void GetCityHappinessPercentage()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        cityHappinessPercentage = gameData.GetAndSaveHappinesPercentage();
        Debug.Log("CityHappiness: " + cityHappinessPercentage);

        if (cityHappinessPercentage >= 72 && cityHappinessPercentage < 90)
        {
            StartCoroutine(ActivateFinal(1));
        }
        else if (cityHappinessPercentage >= 90 && cityHappinessPercentage < 95)
        {
            StartCoroutine(ActivateFinal(2));
        }
        else if (cityHappinessPercentage >= 95 && cityHappinessPercentage <= 100)
        {
            StartCoroutine(ActivateFinal(3));
        }
    }

    IEnumerator ActivateFinal(int final)
    {
        UIFade.instance.FadeToBlack();
        if (conversationFinished && (this.gameObject.GetComponent<DialogActivator>().lines == necalliSuccess1 || this.gameObject.GetComponent<DialogActivator>().lines == necalliSuccess2 || this.gameObject.GetComponent<DialogActivator>().lines == necalliSuccess3) && !dialogBox.activeInHierarchy)
        {
            yield return new WaitForSeconds(2);
            if (final == 1)
            {
                Debug.Log("Final 1");
                SceneManager.LoadScene("Final1");
            }
            else if (final == 2)
            {
                Debug.Log("Final 2");
                SceneManager.LoadScene("Final2");
            }
            else if (final == 3)
            {
                Debug.Log("Final 3");
                SceneManager.LoadScene("Final3");
            }
        }
    }

    public bool NotFoundPartitures()
    {
        notFound = true;
        return notFound;
    }

    IEnumerator ShowDialogs(string[] lines, GameObject habitant)
    {
        yield return new WaitForSeconds(2);

        Debug.Log("1");
        DialogManager.instance.ShowDialog(lines);
        DialogManager.instance.justStarted = false;
    }

    public void ShouldGetCityHappinessPercentage()
    {
        shouldGetCityHappinessPercentage = true;
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
        habitant.gameObject.GetComponent<DialogActivator>().lines = necalliNormalLines;
    }
}
