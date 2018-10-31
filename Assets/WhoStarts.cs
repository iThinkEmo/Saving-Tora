using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhoStarts : MonoBehaviour {

	private GameManager gameManagerDelJuego;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
        GameObject dialogText = GameObject.Find("Text (15)");
		if (dialogText){
			string legend = dialogText.GetComponent<Text>().text;
			dialogText.GetComponent<Text>().text =
				gameManagerDelJuego.orderedPlayers[0] + legend;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
