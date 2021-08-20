using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{
    [SerializeField] private GameObject notePrefab;
    public float timeLastNote = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("PentagramManager Started");
    }

    // Update is called once per frame
    void Update()
    {
        // Generating random notes for testing purposes
            Partitures partitura = new Partitures("1");
            if ((Time.time - timeLastNote) >= partitura.velocity)
            {
                Instantiate(this.notePrefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
                timeLastNote = Time.time;
            }
    }
}
