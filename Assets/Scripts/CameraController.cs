using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {


    public Transform target;
    public GameObject theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // These values are for the camera

    private float halfHeight;
    private float halfWidth;
    void start()
    {
        // target = PlayerController.instance.transform;

        // halfHeight = Camera.main.orthographicSize;
        // halfWidth = halfHeight * Camera.main.aspect;

        // // We assign the corners of our map
        // // Maybe we`ll have to change the image for a tileMap to use: tileMap.localBounds 
        // bottomLeftLimit = theMap.GetComponent<SpriteRenderer>().sprite.bounds.min + new Vector3(halfWidth, halfHeight, 0f);
        // topRightLimit = theMap.GetComponent<SpriteRenderer>().sprite.bounds.max  + new Vector3(-halfWidth, -halfHeight, 0f);

        // // We send the bound limits to the PlayerController script to keep the player inside the map
        // PlayerController.instance.SetBounds(theMap.GetComponent<SpriteRenderer>().sprite.bounds.min, theMap.GetComponent<SpriteRenderer>().sprite.bounds.max);
    }

    // void LateUpdate()
    // {
    //     transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

    //     // Keep the camera inside the bounds
    //     transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    // }
}