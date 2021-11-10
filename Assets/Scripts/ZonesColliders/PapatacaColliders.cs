using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapatacaColliders : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.GetComponent<EmptyObject>() != null) {
            Debug.Log("Collision on Papataca");
            ResourcesManager.instance.GetNewSpawnPosition();
        }
    }
}
