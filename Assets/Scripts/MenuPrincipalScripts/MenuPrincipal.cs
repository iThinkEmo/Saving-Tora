using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Descripción: Esta clase se va encargar de manejar las acciones del menu principal.
public class MenuPrincipal : MonoBehaviour {

    private GameManager gameManagerDelJuego;//es un singleton por lo que creara un objeto si no existe y si existe uno pues va eliminar el nuevo :v

    void Start(){
        gameManagerDelJuego = GameManager.Instance;
    }

    //Metodos de instancia publicos (van a manejar las acciones  de los botones)

    //Este método se activa al seleccionar el boton (2 players o 3 players o 4 players)
    public void IrPantallaSeleccionPersonajees(int numeroJugadores) {
        this.ReiniciarDiccionarioPersonajeUsuarioGameManager(); //con este reinicio se evitará que se acarrean prsonajes de aventuras pasadas...
        gameManagerDelJuego.ReseteaNombresUltimoJugadorQueSeleccionoPersonajer();
        Debug.Log("Numero de jugadores: "+numeroJugadores);
        gameManagerDelJuego.numberOfPlayers = numeroJugadores;
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "SeleccionPersonajeAventura"; //esta escena debe de cargar la pantalla de loading (cargara la de seleccion de personajes) y esta se leera en "PantallaCargandoLoadingScreen" 
        //Nota, el gamemanager al ser singleton ya tiene implementado 
        //el dont destroy on load en su awake por lo que no es necesario especificarlo aqui.
        SceneManager.LoadScene("PantallaCargandoLoadingScreen"); 
    }

    private void ReiniciarDiccionarioPersonajeUsuarioGameManager() {
        gameManagerDelJuego.ResetSelectedUserCharacter();
    }

   

    public void Salir()
    {
        Application.Quit();

    }
}
