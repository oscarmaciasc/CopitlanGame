using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    public float timeLastNote = 2f;
    public static int streak = 0;
    public GameObject objectTest;
    public GameObject test;

    //Partitures partitura;

    // Start is called before the first frame update
    void Start()
    {
        //partitura = new Partitures("1");
        Debug.Log("PentagramManager Started");
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
