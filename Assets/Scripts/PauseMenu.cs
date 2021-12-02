using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] private GameObject MapButton;
    [SerializeField] private GameObject InventoryButton;
    [SerializeField] private GameObject BalloonButton;
    [SerializeField] private GameObject InfoButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] public GameObject MapPanel;
    [SerializeField] public GameObject InventoryPanel;
    [SerializeField] private GameObject BalloonPanel;
    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private GameObject InvResourcesPanel;
    [SerializeField] private GameObject InvFlutesPanel;
    [SerializeField] private GameObject InvPartituresPanel;
    [SerializeField] private GameObject[] resourceQuantity = new GameObject[4];
    [SerializeField] private GameObject[] flutes = new GameObject[4];
    [SerializeField] private GameObject[] defaultFluteLabel = new GameObject[4];
    [SerializeField] private GameObject[] predetermineFluteButton = new GameObject[4];
    [SerializeField] private GameObject[] partitures = new GameObject[10];
    [SerializeField] private GameObject balloon1Panel;
    [SerializeField] private GameObject balloon2Panel;
    [SerializeField] private GameObject balloon3Panel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject musicalMasteryLevel;
    [SerializeField] private GameObject happinessPercentage;
    [SerializeField] private GameObject minPlayed;
    [SerializeField] private GameObject minWalked;
    [SerializeField] private GameObject minBalloon;
    [SerializeField] private GameObject interactedHabitants;
    [SerializeField] private GameObject collectables;
    [SerializeField] private GameObject outterCirclePermission;
    [SerializeField] private GameObject trianglePermission;
    [SerializeField] private GameObject innerCirclePermission;
    [SerializeField] private GameObject femalePanel;
    [SerializeField] private GameObject malePanel;
    private int counter = 0;


    private GameData gameData;

    void Start()
    {
        instance = this;


        ActivatePanel();
    }

    void OnEnable()
    {
        counter = 0;
    }

    public void ActivatePanel()
    {
        LoadGameData();
        ActivateMapPanel();
        pauseMenuPanel.SetActive(true);
    }

    // Loads the current gameData
    private void LoadGameData()
    {
        XmlManager.instance.UpdateTimePlayed(Time.time - InGame.instance.lastSaved);
        InGame.instance.lastSaved = Time.time;
        gameData = XmlManager.instance.LoadGame();
    }

    // Activates the map panel and deactivates others
    public void ActivateMapPanel()
    {
        MapPanel.SetActive(true);
        ChangeByColor.instance.Enable();
        ZoomImage.instance.Enable();
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
        //AudioManager.instance.PlaySFX(4);
    }

    // Activates the inventory panel and deactivates others
    public void ActivateInventoryPanel()
    {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(true);
        ActivateInvResourcesPanel();
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
        //AudioManager.instance.PlaySFX(4);
    }

    // Activates the balloon panel and deactivates others
    public void ActivateBalloonPanel()
    {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(true);
        SetBallon();
        SetSlider();
        InfoPanel.SetActive(false);
        //AudioManager.instance.PlaySFX(4);
    }

    // Sets the ballon information reading from the gameData
    private void SetBallon()
    {
        string balloonName = "";

        for (int i = 0; i < 3; i++)
        {
            balloonName = "balloonLvl" + (i + 1).ToString();
            if (gameData.DoesHaveBalloon(balloonName))
            {
                switch (i)
                {
                    case 0:
                        balloon1Panel.SetActive(true);
                        balloon2Panel.SetActive(false);
                        balloon3Panel.SetActive(false);
                        break;
                    case 1:
                        balloon1Panel.SetActive(false);
                        balloon2Panel.SetActive(true);
                        balloon3Panel.SetActive(false);
                        break;
                    case 2:
                        balloon1Panel.SetActive(false);
                        balloon2Panel.SetActive(false);
                        balloon3Panel.SetActive(true);
                        break;
                }
            }
        }
    }

    // Sets the fuel level reading from the gameData
    public void SetSlider()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        BalloonPanel.transform.Find("FuelLayout").Find("FuelLevel").GetComponent<Slider>().maxValue = gameData.GetBalloonCapacity();
        BalloonPanel.transform.Find("FuelLayout").Find("FuelLevel").GetComponent<Slider>().value = gameData.GetCurrentResource(3);
    }

    // Activates the info panel and deactivates others
    public void ActivateInfoPanel()
    {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(true);
        SetInfo();
        //AudioManager.instance.PlaySFX(4);
    }

    // Sets the game info reading from the gameData
    private void SetInfo()
    {
        if (gameData.isWoman)
        {
            femalePanel.SetActive(true);
        }
        else
        {
            malePanel.SetActive(true);
        }
        musicalMasteryLevel.transform.GetComponent<Text>().text = gameData.GetMusicalMasteryLevel();

        Debug.Log("Necalli: " + gameData.GetAudienceResult("necalliResult"));
        happinessPercentage.transform.GetComponent<Text>().text = gameData.happinessPercentage.percentage.ToString();

        minPlayed.transform.GetComponent<Text>().text = FormatTime(gameData.timePlayed.time);
        minWalked.transform.GetComponent<Text>().text = FormatTime(gameData.timeWalked.time);
        minBalloon.transform.GetComponent<Text>().text = FormatTime(gameData.timeBalloon.time);
        if (gameData.collectable == null)
        {
            collectables.transform.GetComponent<Text>().text = "0";
        }
        else
        {
            Debug.Log("Length collectables: " + gameData.collectable.Length);
            collectables.transform.GetComponent<Text>().text = gameData.collectable.Length.ToString();
        }

        for (int i = 0; i < gameData.habitantInteracted.Length; i++)
        {
            if (gameData.habitantInteracted[i].interacted)
            {
                Debug.Log("counter: " + counter);
                counter++;
            }
        }

        interactedHabitants.transform.GetComponent<Text>().text = counter.ToString();

        if (gameData.DoesHavePermit("outterCircle"))
        {
            outterCirclePermission.SetActive(true);
        }
        else
        {
            outterCirclePermission.SetActive(false);
        }

        if (gameData.DoesHavePermit("triangle"))
        {
            trianglePermission.SetActive(true);
        }
        else
        {
            trianglePermission.SetActive(false);
        }

        if (gameData.DoesHavePermit("innerCircle"))
        {
            innerCirclePermission.SetActive(true);
        }
        else
        {
            innerCirclePermission.SetActive(false);
        }

    }

    // Activates the resources panel in inventory panel and deactivates others
    public void ActivateInvResourcesPanel()
    {
        InvResourcesPanel.SetActive(true);
        SetResourcesQuantity();
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(false);
    }

    // Sets the resources quantity reading from files
    private void SetResourcesQuantity()
    {
        for (int i = 0; i < 4; i++)
        {
            resourceQuantity[i].transform.GetComponent<Text>().text = gameData.resource[i].quantity.ToString();
        }
    }

    // Activates the flutes panel in inventory panel and deactivates others
    public void ActivateInvFlutesPanel()
    {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(true);
        SetFlutes();
        InvPartituresPanel.SetActive(false);
    }

    // Activate or deactivate the flute panels depending in if they exist in the gameData
    private void SetFlutes()
    {
        int fluteIndex = 1000;

        for (int i = 0; i < gameData.flute.Length; i++)
        {
            flutes[i].SetActive(true);

            if (gameData.flute[i].isByDefault)
            {
                fluteIndex = i;
            }
        }

        SetDefaultFluteOnInterface(fluteIndex);

        if (gameData.flute.Length < 4)
        {
            for (int i = gameData.flute.Length; i < 4; i++)
            {
                flutes[i].SetActive(false);
            }
        }
    }

    // Set wooden flute as default
    public void PredetermineWoodenFlute()
    {
        XmlManager.instance.SetDefaultFlute("woodenFlute");
        SetDefaultFluteOnInterface(0);
    }

    // Set woodenIron flute as default
    public void PredetermineWoodenIronFlute()
    {
        XmlManager.instance.SetDefaultFlute("woodenIronFlute");
        SetDefaultFluteOnInterface(1);
    }

    // Set iron flute as default
    public void PredetermineIronFlute()
    {
        XmlManager.instance.SetDefaultFlute("ironFlute");
        SetDefaultFluteOnInterface(2);
    }

    // Set golden flute as default
    public void PredetermineGoldenFlute()
    {
        XmlManager.instance.SetDefaultFlute("goldenFlute");
        SetDefaultFluteOnInterface(3);
    }

    private void SetDefaultFluteOnInterface(int fluteIndex)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == fluteIndex)
            {
                defaultFluteLabel[i].SetActive(true);
                predetermineFluteButton[i].SetActive(false);
            }
            else
            {
                defaultFluteLabel[i].SetActive(false);
                predetermineFluteButton[i].SetActive(true);
            }
        }
    }

    // Activates the partitures panel in inventory panel and deactivates others
    public void ActivateInvPartituresPanel()
    {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(true);
        SetPartitures();
    }

    // Activate or deactivate the partiture panels depending in if they exist in the gameData
    private void SetPartitures()
    {
        string partitureName = "";

        for (int i = 0; i < 10; i++)
        {
            partitureName = "partiture" + (i + 1).ToString();

            Debug.Log(partitureName);
            if (gameData.DoesHavePartiture(partitureName))
            {
                partitures[i].SetActive(true);
            }
            else
            {
                partitures[i].SetActive(false);
            }
        }
    }

    // Deactivates the whole pause menu interface
    public void DeactivatePauseMenuPanel()
    {
        pauseMenuPanel.gameObject.SetActive(false);
        GameManager.instance.pPressed = false;
        //AudioManager.instance.PlaySFX(3);
    }

    // Formats a float into a minutes fomat
    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)(time - (60 * minutes));
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
