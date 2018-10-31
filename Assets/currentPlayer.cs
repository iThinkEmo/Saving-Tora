using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentPlayer : MonoBehaviour {

	private GameManager gameManagerDelJuego;
	public int player;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		player = gameManagerDelJuego.GetCurrentPlayer();
	}
}
