using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    //Descripción: Esta clase se va encargar de manejar las acciones del menu principal.

    //Metodos de instancia publicos (van a manejar las acciones  de los botones)
    public void IniciarJuego() {

        SceneManager.LoadScene(1); //1 es la pantalla del juego
    }

    public void Salir()
    {
        Application.Quit();

    }
}
