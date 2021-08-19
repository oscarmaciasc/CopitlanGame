using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<Note>() != null) {
            TutorialManager.instance.ProcessDialog3();
        }
    }
}
