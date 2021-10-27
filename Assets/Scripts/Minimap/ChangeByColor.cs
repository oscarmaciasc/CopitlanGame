using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeByColor : MonoBehaviour
{
    [SerializeField] private GameObject colorPanel;
    [SerializeField] private GameObject[] infoPanel = new GameObject[2];
    public static ChangeByColor instance;

    void Start()
    {
        instance = this;
    }

    public void Enable() {
        colorPanel.GetComponent<Image>().color = new Color32(50, 30, 14, 255);
        SetPlaceInfo(GetCopitlanInfo());
    }
    
    public void ColorSelected(Color colorRGBA)
    {
        string colorHEX = ColorUtility.ToHtmlStringRGBA(colorRGBA);

        string[] placeData = GetPlaceData(GetPlaceIndex(colorHEX));

        SetPlaceInfo(placeData);
    }

    private void SetPlaceInfo(string[] placeData)
    {
        for (int i = 0; i < 2; i++)
        {
            infoPanel[i].GetComponent<Text>().text = placeData[i];
        }
    }

    private string[] GetCopitlanInfo() {
        return new string[] {
            "Copitlan" ,
            "Copitlan es una ciudad oculta entre los arboles del bosque Papataca.\nTiene tres zonas principales divididas por murallas, con un Dirigente encangado de cada una de ellas.\nCuenta tambien con un Lider que esta al mando y se encarga de garantizar la estabilidad de toda la ciudad."
        };
    }

    private int GetPlaceIndex(string colorHEX) {
        string[] colorCodes = {
            "1D5C38FF" , // Papataca
            "9C4A2EFF" , // RoyalRoad
            "B59865FF" , // Excir
            "9C4141FF" , // Triar
            "4B577DFF" , // InCir
            "A07D49FF" , // ExcirHouses
            "9F7D49FF" , // TriarHouses
            "9E7D49FF" , // IncirHouses
            "5E4335FF" , // Graveyard
            "64793DFF" , // School
            "764161FF" , // Library
            "594596FF" , // TradeHouse
            "7B2008FF" , // Workshop
            "8E7668FF" , // Blacksmith
            "C18F46FF" , // KasakirPalace
            "8E2121FF" , // QuizaniPalace
            "363C8EFF" , // NaranPalace
            "AF7200FF" , // NecalliRoyalPalace
            "A17D49FF" , // CharacterHouse
            "787676FF" , // TecalliMine
            "767676FF" , // AcanMine
            "9A7900FF" , // SetiMine
            "595652FF"   // Walls
        };

        for(int i = 0; i < colorCodes.Length; i++) {
            if(colorCodes[i] == colorHEX) {
                return i;
            }
        }

        return 1000;
    }

    private string[] GetPlaceData(int placeID) {
        string[] placeName = {
            "Bosque Papataca" , // Papataca
            "Camino real" , // RoyalRoad
            "Circulo exterior" , // Excir
            "Triangulo" , // Triar
            "Circulo interior" , // InCir
            "Vivienda del Circulo exterior" , // ExcirHouses
            "Vivienda del Triangulo" , // TriarHouses
            "Vivienda del Circulo interior" , // IncirHouses
            "Cementerio" , // Graveyard
            "Escuela abandonada" , // School
            "Biblioteca" , // Library
            "Casa de comercio" , // TradeHouse
            "Taller" , // Workshop
            "Herreria" , // Blacksmith
            "Palacio del Dirigente Kasakir" , // KasakirPalace
            "Palacio del Dirigente Quizani" , // QuizaniPalace
            "Palacio del Dirigente Naran" , // NaranPalace
            "Palacio Real del Lider Necalli" , // NecalliRoyalPalace
            "Hogar" , // CharacterHouse
            "Mina de Hierro Tecalli" , // TecalliMine
            "Mina de Hierro Acan" , // AcanMine
            "Mina de Oro Seti" ,  // SetiMine
            "Murallas de la ciudad"   // Walls
        };

        string[] placeDescription = {
            "El bosque Papataca se extiende desde las murallas del circulo exterior hasta mas alla del punto mas lejano que haya alcanzado cualquier explorador.\nHay tanto silencio que si estas aqui puedes escuchar incluso los debiles aleteos de los insectos." , // Papataca
            "El camino real conecta todas las zonas importantes de Copitlan y sus alrededores.\nEste camino existe desde hace mucho tiempo atras, los habitantes mas ancianos de Copitlan relatan que sus abuelos solian contar historias relacionadas con su construccion." , // RoyalRoad
            "El Circulo Exterior es la zona con mas extension de Copitlan y tambien la mas poblada.\nPara transitar o residir dentro de sus murallas es necesario poseer el Permiso del Circulo Exterior.\nLa gente oriunda de esta zona suele dedicarse a la agricultura y la ganaderia." , // Excir
            "El Triangulo es la zona intermedia de la ciudad.\nPara transitar o residir dentro de sus murallas es necesario poseer el Permiso del Triangulo.\nLa gente oriunda de esta zona suele dedicarse a la construccion o a la restauracion de edificaciones y espacios." , // Triar
            "El Circulo Interior es la zona con menos extension de Copitlan.\nPara transitar o residir dentro de sus murallas es necesario poseer el Permiso del Circulo Interior.\nLa gente oriunda de esta zona suele dedicarse a administrar recursos y espacios entre los habitantes de Copitlan." , // InCir
            "Es una de las viviendas promedio de los habitantes de la zona del Circulo exterior de Copitlan.\nEsta hecha de madera y esta equipada con el mobiliario y servicios basicos para la vida digna y sana de los habitantes." , // ExcirHouses
            "Es una de las viviendas promedio de los habitantes de la zona del Triangulo de Copitlan.\nEsta hecha de ladrillos de arcilla y son mas compactas en comparacion con las viviendas del circulo exterior." , // TriarHouses
            "Es una de las viviendas promedio de los habitantes de la zona del Circulo interior de Copitlan.\nEsta hecha de bloques solidos de piedra blanca y, aunque las mas compactas,  son las viviendas mejor equipadas de la ciudad." , // IncirHouses
            "El cementerio es un lugar muy tranquilo a las afueras de la ciudad donde reposan los restos de todos los fallecidos de Copitlan.\nLos habitantes suelen venir aqui para recordar y meditar acerca de la vida de los que ya no estan." , // Graveyard
            "Es una antigua escuela abandonada que esta dentro de las murallas del triangulo.\nSe dejo de utilizar debido a que su estructura era ya demasiado vieja para poder ser restaurada.\nAhora los habitantes de Copitlan educan a sus hijos en casa." , // School
            "Fundada hace poco menos de 4 generaciones, atesora libros con valiosa informacion para entender el entorno natural y, de la misma manera, informacion para llegar a dominar las diversas tecnologias que han ido surgiendo en la historia de Copitlan." , // Library
            "En la casa de comercio es posible acudir para realizar intercambio de unos recursos por otros.\nAqui se puede intercambiar con madera, hierro, oro y combustible para globo." , // TradeHouse
            "En el taller es posible acudir para intercambiar recursos por un producto unico elaborado a mano segun los requerimientos del que acude.\nLos artesanos crean piezas de calidad y las detallan de tal forma que es muy dificil replicarlas para un habitante comun." , // Workshop
            "A la herreria se acude para intercambiar recursos por un producto elaborado segun los requerimientos del que acude.\nLos herreros crean artefactos resistentes de uso cotidiano.\nAqui se pueden obtener globos aerostaticos personales y sus respectivas mejoras." , // Blacksmith
            "Desde este palacio, el Dirigente Kasakir se hace cargo de velar por las necesidades de los habitantes del Circulo exterior.\nCualquier habitante puede solicitar una audiencia para exponer alguna situacion que considere debe ser atendida y corresponde al Dirigente decidir." , // KasakirPalace
            "Desde este palacio, el Dirigente Quizani se hace cargo de velar por las necesidades de los habitantes del Triangulo.\nCualquier habitante puede solicitar una audiencia para exponer alguna situacion que considere debe ser atendida y corresponde al Dirigente decidir." , // QuizaniPalace
            "Desde este palacio, el Dirigente Naran se hace cargo de velar por las necesidades de los habitantes del Circulo interior.\nCualquier habitante puede solicitar una audiencia para exponer alguna situacion que considere debe ser atendida y corresponde al Dirigente decidir." , // NaranPalace
            "Desde el palacio real, el Lider Necalli se encarga de garatizar la estabilidad y prosperidad de Copitlan.\nSi bien no todos los habitantes tienen permitido entrar aqui, los mas destacados pueden hacerlo cuando el Lider deba anunciar un cambio importante en la ciudad." , // NecalliRoyalPalace
            "Este es tu hogar fuera de las murallas, en el bosque Papataca, donde todo es mas tranquilo.\nAqui tienes lo necesario para vivir el dia a dia comodamente sin preocupaciones, aunque seguro que la ordinariez no es tu destino." , // CharacterHouse
            "La mina Tecalli se encuentra en la zona sur del bosque Papataca.\nLos habitantes de Copitlan pueden internase en ella libremente para obtener hierro que despues pueden utilizar para intercambiar por algun producto u objeto que necesiten." , // TecalliMine
            "La mina Acan se encuentra en la zona norte del bosque Papataca.\nLos habitantes de Copitlan pueden internase en ella libremente para obtener hierro que despues pueden utilizar para intercambiar por algun producto u objeto que necesiten." , // AcanMine
            "La mina Seti se encuentra en la zona norte del bosque Papataca.\nLos habitantes de Copitlan pueden internase en ella libremente para obtener oro, aunque es muy dificil de encontrar." ,  // SetiMine
            "Las murallas rodean cada zona de la ciudad y estan hechas de bloques masivos de piedra gris.\nHan estado ahi desde hace tanto tiempo que ni los habitantes mas ancianos de Copitlan tienen una idea de cuando pudieron haber sido construidas."   // Walls
        };
        
        return new string[] {placeName[placeID], placeDescription[placeID]};
    }
}
