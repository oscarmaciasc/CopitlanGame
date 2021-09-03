using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PentagramManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    public float timeLastNote = 2f;
    public static int streak = 0;
    public GameObject objectTest;
    public string partitureName;
    public static PentagramManager instance;
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
        if(SceneManager.GetActiveScene().name == "InitSequence2")
        {
            partitureName = PartitureSelectionTutorial.instance.panelPartitureName;
        }
        else
        {
            partitureName = InGame.instance.panelPartitureName;
        }
        Debug.Log("You have selected: " + partitureName);
        Partitures.instance.setVelocity(partitureName);
    }

    // Update is called once per frame
    void Update()
    {
        // Generating random notes for testing purposes
        
            
            if ((Time.time - timeLastNote) >= Partitures.instance.velocity)
            {
                Instantiate(this.notePrefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
                timeLastNote = Time.time;
            }
    }
}
