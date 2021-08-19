using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayPanel : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<Note>() != null) {
            collision.gameObject.GetComponent<Note>().OpacityFull();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        collision.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(true);
    }
}
