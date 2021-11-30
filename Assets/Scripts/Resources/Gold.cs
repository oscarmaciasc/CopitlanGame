using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.GetComponent<PlayerController>() != null) {
            ResourcesManager.instance.resourceCollected(2, 1);
            AudioManager.instance.PlaySFX(2);
            Destroy(this.gameObject);
        }
    }
}
