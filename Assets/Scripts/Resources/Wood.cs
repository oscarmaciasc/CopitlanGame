using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.GetComponent<PlayerController>() != null) {
            ResourcesManager.instance.resourceCollected(0, 1);
            AudioManager.instance.PlaySFX(2);
            Destroy(this.gameObject);
        }
    }
}
