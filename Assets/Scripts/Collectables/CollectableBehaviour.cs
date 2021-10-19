using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] private int collectableID;
    // Start is called before the first frame update
    void Start()
    {
        ShouldBeDestroyed();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            CollectableCollected();
            Destroy(this.gameObject);
        }
    }

    public void CollectableCollected()
    {
        XmlManager.instance.IncreaseCollectable(collectableID);
    }

    private void ShouldBeDestroyed()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();
        if (gameData.collectable != null)
        {
            for (int i = 0; i < gameData.collectable.Length; i++)
            {
                if (gameData.collectable[i].id == collectableID)
                {
                    if (gameData.collectable[i].shouldBeDestroyed)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
