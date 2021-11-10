using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutterCircleCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.GetComponent<EmptyObject>() != null) {
            Debug.Log("Collision on Copitlan");
            ResourcesManager.instance.GetNewSpawnPosition();
        }
    }
}
