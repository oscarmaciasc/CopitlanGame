using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedHabitants : MonoBehaviour
{
    public int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInteracted()
    {
        if (!XmlManager.instance.InteractedTrue(index))
        {
            XmlManager.instance.SaveInteractedHabitantState(index);
        }
    }
}