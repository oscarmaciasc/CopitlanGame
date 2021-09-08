using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.GetComponent<PlayerController>() != null) {
            ResourcesManager.instance.resourceCollected(1, 1);
            Destroy(this.gameObject);
        }
    }
}
