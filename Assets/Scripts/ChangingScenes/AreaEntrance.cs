using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public static AreaEntrance instance;
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        CheckTransitionName();
        FindObjectOfType<UIFade>().FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTransitionName()
    {
        if(transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = this.transform.position;
        }
    }
}
