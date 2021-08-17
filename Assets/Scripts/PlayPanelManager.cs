using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");

        if(col.gameObject.GetComponent<NoteManager>() != null)
        {
            Debug.Log("Detecting note...");
            col.gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
            //col.gameObject.transform.Find("Text").gameObject.GetComponent<Text>().color = new Color32(170, 133, 80, 255);
            //col.transform.Find("Text").gameObject.GetComponent<Outline>().effectColor = new Color32(62, 38, 19, 255);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.SetActive(false);
    }
}
