using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramManager : MonoBehaviour
{

    [SerializeField] private GameObject notePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(this.notePrefab, this.transform.position, Quaternion.identity).transform.SetParent(this.gameObject.transform);
        Debug.Log("PentagramManager Started");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
