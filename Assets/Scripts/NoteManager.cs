using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{

    

    private int[] arrayPositions = {34, 67, 100, 133, 166};

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("NoteManager Started");

        int positionY = arrayPositions[Random.Range(0, arrayPositions.Length)];

        //Replace "+700" by the anchor position of the Pentagram
        gameObject.transform.position = new Vector3(transform.parent.position.x + 730, transform.parent.position.y + positionY, 0);
    }

    // Update is called once per frame
    void Update()
    {
         gameObject.transform.position = new Vector3(gameObject.transform.position.x - 180 * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
