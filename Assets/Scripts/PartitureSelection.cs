using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartitureSelection : MonoBehaviour
{

    public static PartitureSelection instance;
    [SerializeField] public GameObject partitureSelectionPanel;
    [SerializeField] private GameObject interpretatePartitureButton;
    [SerializeField] private GameObject[] partiturePanels = new GameObject[10];
    [SerializeField] private GameObject[] arrowSelectionPartiture = new GameObject[10];
    [SerializeField] private GameObject pentagramPanel;
    public string panelPartitureName;
    private bool partituresFound = false;
    private bool isByDefault;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //DeactivateNoOwnedPartiturePanels();
        //DeativateArrowsPartitureSelection();
    }

    void Update()
    {

    }

    void OnEnable()
    {
        partituresFound = false;

    }

    public void DeactivateNoOwnedPartiturePanels()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < 10; i++)
        {
            if (gameData.DoesHavePartiture("partiture" + (i + 1)))
            {
                partiturePanels[i].SetActive(true);
            }
            else
            {
                partiturePanels[i].SetActive(false);
            }
        }
    }

    public void DeactivateMinePartitures(string name, GameObject habitant)
    {

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < 10; i++)
        {
            if ((gameData.DoesHavePartiture("partiture" + (i + 1))) && ((partiturePanels[i].name == name)))
            {
                partituresFound = true;
                partiturePanels[i].SetActive(true);
            }
            else
            {
                partiturePanels[i].SetActive(false);
            }
        }

        if (!partituresFound)
        {
            if (habitant.name == "Tecalli0")
            {
                Tecalli.instance.NotFoundPartitures();
            }
            else if (habitant.name == "Acan0")
            {
                Acan.instance.NotFoundPartitures();
            }
        }

        FluteFilter();
    }

    public void DeactivateMinePartituresSeti(string name, string name2, string name3, GameObject habitant)
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        Debug.Log("FUNCION MINAS SETI");

        for (int i = 0; i < 10; i++)
        {
            if ((gameData.DoesHavePartiture("partiture" + (i + 1))) && ((partiturePanels[i].name == name) || (partiturePanels[i].name == name2)))
            {
                Debug.Log("Si encuentro");
                partituresFound = true;
                partiturePanels[i].SetActive(true);
            }
            else
            {
                partiturePanels[i].SetActive(false);
            }
        }

        FluteFilter();


        if (!partituresFound)
        {
            if (habitant.name == "Seti0")
            {
                Seti.instance.NotFoundPartitures();
            }
            else if (habitant.name == "Seti1")
            {
                Seti2.instance.NotFoundPartitures();
            }
        }

    }

    //*************************************************************************************************************************

    // Dirigents
    public void DeactivateDirigentPartitures(string name, string name2, string name3)
    {
        Debug.Log("Entering Deactivate");

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < 10; i++)
        {
            if ((gameData.DoesHavePartiture("partiture" + (i + 1))) && ((partiturePanels[i].name == name) || (partiturePanels[i].name == name2) || (partiturePanels[i].name == name3)))
            {
                partituresFound = true;
                partiturePanels[i].SetActive(true);
            }
            else
            {
                partiturePanels[i].SetActive(false);
            }
        }

        FluteFilter();

        // If partitures are not found set boolean in Audience algorithm
        if (!partituresFound)
        {
            Audience.instance.NotFoundPartitures();
        }

        if (NotFoundFlute())
        {
            Audience.instance.NotFoundFlute();
        }

    }

    //*************************************************************************************************************************

    private bool NotFoundFlute()
    {
        for (int i = 0; i < partiturePanels.Length; i++)
        {
            if (partiturePanels[i].activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }

    //*************************************************************************************************************************

    // FluteFilter

    public void FluteFilter()
    {

        Debug.Log("Flute Filter");
        // Desactivar la que no tiene que tener


        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < gameData.flute.Length; i++)
        {
            if (gameData.flute[i].isByDefault)
            {
                Debug.Log("flauta: " + gameData.flute[i].name);
                if (gameData.flute[i].name == "woodenFlute")
                {
                    // we shouldnt active partiturePanel4, 5, 6, 7, 8, 9, 10
                    for (int j = 0; j < partiturePanels.Length; j++)
                    {
                        if (partiturePanels[j].name == "PanelPartiture4" || partiturePanels[j].name == "PanelPartiture5" || partiturePanels[j].name == "PanelPartiture6" || partiturePanels[j].name == "PanelPartiture6" || partiturePanels[j].name == "PanelPartiture7" || partiturePanels[j].name == "PanelPartiture8" || partiturePanels[j].name == "PanelPartiture9" || partiturePanels[j].name == "PanelPartiture10")
                        {
                            partiturePanels[j].SetActive(false);
                        }
                    }
                }
                else if (gameData.flute[i].name == "woodenIronFlute")
                {
                    Debug.Log("Checkpoint");
                    // we shouldnt active partiturePanel7, 8, 9, 10
                    for (int k = 0; k < partiturePanels.Length; k++)
                    {
                        if (partiturePanels[k].name == "PanelPartiture7" || partiturePanels[k].name == "PanelPartiture8" || partiturePanels[k].name == "PanelPartiture9" || partiturePanels[k].name == "PanelPartiture10")
                        {
                            partiturePanels[k].SetActive(false);
                        }
                    }
                }
                else if (gameData.flute[i].name == "ironFlute")
                {
                    Debug.Log("Esto si funciona");
                    // we shouldnt active partiturePanel10
                    for (int l = 0; l < partiturePanels.Length; l++)
                    {
                        if (partiturePanels[l].name == "PanelPartiture10")
                        {
                            partiturePanels[l].SetActive(false);
                        }
                    }
                }
            }
        }
        // We have to identify if after flute partitures the panels are still inactive, then change dialog to no flute dialog and close the partiture selection interface
    }

    //*************************************************************************************************************************

    //*************************************************************************************************************************

    // Leader
    public void DeactivateLeaderPartitures(string name)
    {

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        for (int i = 0; i < 10; i++)
        {
            if ((gameData.DoesHavePartiture("partiture" + (i + 1))) && (partiturePanels[i].name == name))
            {
                partituresFound = true;
                partiturePanels[i].SetActive(true);
            }
            else
            {
                partiturePanels[i].SetActive(false);
            }
        }

        FluteFilter();

        // If partitures are not found set boolean in Audience algorithm
        if (!partituresFound)
        {
            Leader.instance.NotFoundPartitures();

        }
    }

    //*************************************************************************************************************************
    public bool AllPanelsInactive()
    {
        for (int i = 0; i < partiturePanels.Length; i++)
        {
            if (partiturePanels[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    public void BackPartitureSelection()
    {
        DeactivatePartitureSelectionPanel();
    }

    public void DeactivatePartitureSelectionPanel()
    {
        partitureSelectionPanel.gameObject.SetActive(false);
        GameManager.instance.vPressed = false;
        DeativateArrowsPartitureSelection();

        interpretatePartitureButton.gameObject.GetComponent<Button>().interactable = false;
        interpretatePartitureButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(50, 30, 14, 140);
    }

    public void DeativateArrowsPartitureSelection()
    {
        for (int i = 0; i < 10; i++)
        {
            arrowSelectionPartiture[i].SetActive(false);
        }
    }

    public void onClickPartiturePanel1()
    {
        partiturePanelPressed(0);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[0].gameObject.transform.Find("InfoLayout1").gameObject.transform.Find("HorizontalLayout1").gameObject.transform.Find("Name1").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel2()
    {
        partiturePanelPressed(1);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[1].gameObject.transform.Find("InfoLayout2").gameObject.transform.Find("HorizontalLayout2").gameObject.transform.Find("Name2").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel3()
    {
        partiturePanelPressed(2);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[2].gameObject.transform.Find("InfoLayout3").gameObject.transform.Find("HorizontalLayout3").gameObject.transform.Find("Name3").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel4()
    {
        partiturePanelPressed(3);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[3].gameObject.transform.Find("InfoLayout4").gameObject.transform.Find("HorizontalLayout4").gameObject.transform.Find("Name4").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel5()
    {
        partiturePanelPressed(4);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[4].gameObject.transform.Find("InfoLayout5").gameObject.transform.Find("HorizontalLayout5").gameObject.transform.Find("Name5").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel6()
    {
        partiturePanelPressed(5);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[5].gameObject.transform.Find("InfoLayout6").gameObject.transform.Find("HorizontalLayout6").gameObject.transform.Find("Name6").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel7()
    {
        partiturePanelPressed(6);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[6].gameObject.transform.Find("InfoLayout7").gameObject.transform.Find("HorizontalLayout7").gameObject.transform.Find("Name7").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel8()
    {
        partiturePanelPressed(7);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[7].gameObject.transform.Find("InfoLayout8").gameObject.transform.Find("HorizontalLayout8").gameObject.transform.Find("Name8").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel9()
    {
        partiturePanelPressed(8);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[8].gameObject.transform.Find("InfoLayout9").gameObject.transform.Find("HorizontalLayout9").gameObject.transform.Find("Name9").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void onClickPartiturePanel10()
    {
        partiturePanelPressed(9);

        // get partiture name and send it to PentagramManager
        panelPartitureName = partiturePanels[9].gameObject.transform.Find("InfoLayout10").gameObject.transform.Find("HorizontalLayout10").gameObject.transform.Find("Name10").gameObject.GetComponent<Text>().text;

        Debug.Log(panelPartitureName);
    }

    public void partiturePanelPressed(int partitureSelected)
    {
        interpretatePartitureButton.gameObject.GetComponent<Button>().interactable = true;
        interpretatePartitureButton.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(50, 30, 14, 255);

        for (int i = 0; i < 10; i++)
        {
            if (partitureSelected == i)
            {
                arrowSelectionPartiture[i].SetActive(true);
            }
            else
            {
                arrowSelectionPartiture[i].SetActive(false);
            }
        }
    }

    public void onPartitureSelected()
    {
        DeactivatePartitureSelectionPanel();
        pentagramPanel.SetActive(true);
    }
}
