using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // These values are for the camera
    private float halfHeight;
    private float halfWidth;

    // These values handles music
    public int musicToPlay;
    private bool musicStarted;

    void Start()
    {

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        //  We assign the corners of our map
        theMap.CompressBounds();
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        if(FindObjectOfType<PlayerController>(true).gameObject.activeInHierarchy) {
            target = FindObjectOfType<PlayerController>().transform;
        }
        else if (FindObjectOfType<BalloonPlayerController>(true).gameObject != null){
            target = FindObjectOfType<BalloonPlayerController>().transform;
        }
        

        //  We send the bound limits to the PlayerController script to keep the player inside the map
        if (PlayerController.instance != null)
        {
            PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
        }

        if (BalloonPlayerController.instance != null)
        {
            BalloonPlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
        }
    }

    void Update()
    {
        if (InGame.instance != null)
        {
            if (!InGame.instance.balloonActive)
            {
                if (PlayerController.instance != null)
                {
                    target = FindObjectOfType<PlayerController>(true).transform;
                }
            }
            else if (InGame.instance.balloonActive)
            {
                if (BalloonPlayerController.instance != null)
                {
                    target = FindObjectOfType<BalloonPlayerController>().gameObject.transform;
                }
            }
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

        if (!musicStarted)
        {
            musicStarted = true;
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayBGM(musicToPlay);
            }
        }
    }
}