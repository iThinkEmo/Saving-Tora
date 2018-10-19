using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonIiniciarJuego : MonoBehaviour {

    public void IniciarCargaJuego() {
        GameManager gameManagerDelJuego = GameManager.Instance;
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }
}
