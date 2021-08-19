using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public string number = "";
    private int[] arrayPositions = {34, 67, 100, 133, 166};
    private string[] arrayNumberNotes = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("NoteManager Started");

        int positionY = arrayPositions[Random.Range(0, arrayPositions.Length)];
        number = arrayNumberNotes[Random.Range(0, arrayNumberNotes.Length)];

        //Replace "+700" by the anchor position of the Pentagram
        //This only works for FullHD Resolutions
        gameObject.transform.position = new Vector3(transform.parent.position.x + 730, transform.parent.position.y + positionY, 0);
        gameObject.transform.Find("NoteText").gameObject.GetComponent<Text>().text = number;
    }

    // Update is called once per frame
    void Update()
    {
         gameObject.transform.position = new Vector3(gameObject.transform.position.x - 180 * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
