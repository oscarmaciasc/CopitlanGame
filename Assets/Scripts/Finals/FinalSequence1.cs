using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSequence1 : MonoBehaviour
{
    private string[] finalDialogs = { "Bienvenido al final", "perro del mal" };

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();
        StartCoroutine(FinalDialogs());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FinalDialogs()
    {
        //Delay
        yield return new WaitForSeconds(3);
        DialogManagerFinal.instance.ShowDialog(finalDialogs);
    }
}
