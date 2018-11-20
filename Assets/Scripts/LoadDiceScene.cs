using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDiceScene : MonoBehaviour {

	private GameManager gameManagerDelJuego;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
		GameObject rules = GameObject.FindGameObjectWithTag("rules");
		if (rules){
			// En transition se encuentra el Script de SpawnInit,
			// que es el que muestra a los personajes y el dado
			rules.transform.GetChild(22).gameObject.SetActive(true);
			
			// Prende el timeline de las instrucciones
			// rules.transform.GetChild(23).gameObject.SetActive(true);
		}
		GameObject shopkeeper = GameObject.FindGameObjectWithTag("Shopkeeper");
		if (shopkeeper){
			shopkeeper.transform.GetChild(0).gameObject.SetActive(true);
		}

		gameManagerDelJuego.AddSelectUserCharacter("Ald", 1);
		// gameManagerDelJuego.AddSelectUserCharacter("Mar", 2);
		// gameManagerDelJuego.AddSelectUserCharacter("Kar", 3);
		gameManagerDelJuego.AddSelectUserCharacter("Irv", 4);
		gameManagerDelJuego.numberOfPlayers = 2;
		gameManagerDelJuego.WhoStarts();
		// Debug.Log(gameManagerDelJuego.numberOfPlayers);
		// Debug.Log(gameManagerDelJuego.orderedPlayers[0]);
		// Debug.Log(gameManagerDelJuego.orderedPlayers[1]);
		// Debug.Log(gameManagerDelJuego.orderedPlayers[2]);
		// Debug.Log(gameManagerDelJuego.orderedPlayers[3]);

		gameManagerDelJuego.instanciarPersonajes();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
