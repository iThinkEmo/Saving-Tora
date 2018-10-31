using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScenes : MonoBehaviour {

	private GameManager gameManagerDelJuego;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
