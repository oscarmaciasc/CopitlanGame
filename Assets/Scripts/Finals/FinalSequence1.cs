using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSequence1 : MonoBehaviour
{
    private string[] finalDialogs1 = { "Que linda nuestra escuela de musica", "Cuenta la leyenda que antes no exstia la musica", "Hasta que llego un viajero y demostro lo bella que era", "Y en su honor el lider Necalli fundo esta escuela", "Wow, es una gran historia", "el nombre de la escuela es: Escuela de Musica ", "supongo que le nombraron asi por la leyenda que regreso la musica a la ciudad", "bueno, de cualquier manera, ahora somos felices gracias a ese misterioso heroe" };
    private string[] finalDialogs2 = { "Cuentan las historias que hace mucho tiempo existio alguien que le devolvio la felicidad a la ciudad", "Se llamaba ", "LLego un dia con una flauta en la mano y una determinacion de acero", "convencio a todos los de la ciudad de que la musica valia la pena", "Y ahora miranos, afuera de una orquesta fundada en su nombre", "Wow, ahora entiendo por que la escuela de musica se llama asi", "Es una gran historia y sin duda una inspiracion para todos", "siempre sera recordado por la ciudad de Copitlan y todos sus habitantes" };
    private string[] finalDialogs3 = { "Wow, esta estatua es increible y la historia detras de ella es aun mejor", "Todos conocemos la historia de como ", "Incluso el lider Necalli fundo una escuela de musica y una orquesta en su nombre", "Es mi ejemplo a seguir, es verdaderamente admirable", "Espero que algun dia me hagan una estatua como esta" };
    [SerializeField] private GameObject maleStatue;
    [SerializeField] private GameObject femaleStatue;
    private string name;
    public GameObject player;
    [SerializeField] private GameObject years;
    private bool doOnlyOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack();

        //Destroy the player gameobject
        //MaleCharacter(Clone)
        player = FindObjectOfType<PlayerController>().transform.gameObject;
        Destroy(player);

        GameData gameData = new GameData();
        gameData = XmlManager.instance.LoadGame();

        name = gameData.name;
        finalDialogs1[5] += name;
        finalDialogs2[1] += name;
        finalDialogs3[1] += name + " trajo la musica a la ciudad y nos hizo felices a todos";

        if (SceneManager.GetActiveScene().name == "Final3")
        {
            if (gameData.isWoman)
            {
                femaleStatue.SetActive(true);
            }
            else
            {
                maleStatue.SetActive(true);
            }
        }

        years.GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(YearsScreen.instance.animationEnd && doOnlyOnce)
        {
            StartCoroutine(FinalDialogs());
            doOnlyOnce = false;
        }
    }

    IEnumerator FinalDialogs()
    {
        //Delay
        yield return new WaitForSeconds(3);
        if (SceneManager.GetActiveScene().name == "Final1")
        {
            DialogManagerFinal.instance.ShowDialog(finalDialogs1);
        }
        else if (SceneManager.GetActiveScene().name == "Final2")
        {
            DialogManagerFinal.instance.ShowDialog(finalDialogs2);
        }
        else if (SceneManager.GetActiveScene().name == "Final3")
        {
            DialogManagerFinal.instance.ShowDialog(finalDialogs3);
        }
    }
}
