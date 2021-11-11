using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineColliders : MonoBehaviour
{
    public static MineColliders instance;

    private void Start() {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collider) {
        Debug.Log("Collision");
        ResourcesManager.instance.GetNewSpawnPosition();
    }
}
