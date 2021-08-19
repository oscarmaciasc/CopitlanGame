using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public static Note instance;
    private void Awake() {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        // Giving initial parameters for position and color
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        gameObject.transform.position = new Vector3(transform.parent.position.x + 730, transform.parent.position.y + 100, 0);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Making the note move all along the pentagram
        gameObject.transform.position = new Vector3(gameObject.transform.position.x - 180 * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    public void OpacityFull() {
        // Setting full oppacity
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void SetGreen() {
        // Setting note green
        gameObject.GetComponent<Image>().color = new Color32(87, 234, 91, 255);
    }
    
    public void SetRed() {
        // Setting note green
        gameObject.GetComponent<Image>().color = new Color32(234, 87, 91, 255);
    }
}
