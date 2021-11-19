using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitureCollectable : MonoBehaviour
{
    [SerializeField] private string partitureName;
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
            PartitureCollected();
            AudioManager.instance.PlaySFX(2);
            Destroy(this.gameObject);
        }
    }

    public void PartitureCollected()
    {
        XmlManager.instance.AddPartiture(partitureName);
    }

    private void ShouldBeDestroyed()
    {
        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        if (gameData.DoesHavePartiture(partitureName))
        {
            Destroy(this.gameObject);
        }
    }
}
