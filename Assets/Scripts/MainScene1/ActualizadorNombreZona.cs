using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizadorNombreZona : MonoBehaviour {

    public GameObject ImagenDeLuna;
    public GameObject LetreroNombreZona;
    public Text TextoNombreDeLaZona;
    //Variable privada del gamemanager que es un singleton-
    private GameManager _gameManagerDelJuego;


    // Use this for initialization
    void Start () {
        _gameManagerDelJuego = GameManager.Instance;
    }
	
	// Update is called once per frame
	void Update () {
        if (_gameManagerDelJuego.BanderaYaSeDecidioCurrentPlayer)
        {
            if (!LetreroNombreZona.activeInHierarchy)
            {
                LetreroNombreZona.SetActive(true);
            }
            if (!ImagenDeLuna.activeInHierarchy)
            {
                ImagenDeLuna.SetActive(true);
            }
            int currentPlayer = _gameManagerDelJuego.GetCurrentPlayer();
            string character = _gameManagerDelJuego.idCharacter[currentPlayer];
            GameObject currentCharacter = GameObject.Find(character);
            if (currentCharacter)
            {
                Saviour saviour = currentCharacter.GetComponent<Saviour>();
                if (saviour)
                {
                    int area = NodesMap.nodesArea["Node " + saviour.currentNode];
                    string areaName = NodesMap.areaName[area];
                    TextoNombreDeLaZona.text = areaName;
                }

                GameObject moons = GameObject.Find("Moons");
                if (moons){
                    moons.transform.GetChild(_gameManagerDelJuego.moonPhase).gameObject.SetActive(true);
                }
            }
        }
    }
}
