using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewSpawnPosition : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.GetComponent<EmptyObject>() != null) {
            Debug.Log("Collision on Collider xDn't");
            ResourcesManager.instance.GetNewSpawnPosition();
        }
    }
}
