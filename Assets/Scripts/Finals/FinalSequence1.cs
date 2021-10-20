using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSequence1 : MonoBehaviour
{
    private string[] finalDialogs1 = { "Que linda nuestra escuela de musica", "Cuenta la leyenda que antes no exstia la musica", "Hasta que llego un viajero y demostro lo bella que era", "Y en su honor el lider Necalli fundo esta escuela", "Wow, es una gran historia", "el nombre de la escuela es: Escuela de Musica ", "supongo que le nombraron asi por la leyenda que regreso la musica a la ciudad", "bueno, de cualquier manera, ahora somos felices gracias a ese misterioso heroe" };
    private string name;

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();
        StartCoroutine(FinalDialogs());

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        name = gameData.name;
        finalDialogs1[5] += name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FinalDialogs()
    {
        //Delay
        yield return new WaitForSeconds(3);
        DialogManagerFinal.instance.ShowDialog(finalDialogs1);
    }
}
