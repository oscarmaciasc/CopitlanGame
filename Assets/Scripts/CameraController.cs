using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public Transform target;
    // Reference to the tilemap
    //public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerController.instance.transform;

        // Search all the objects on the scene and find any object that has the PlayerController script attached to.
        target = FindObjectOfType<PlayerController>().transform;

        // Limit the camera
        //bottomLeftLimit = theMap.localBounds.min;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
    }
}
