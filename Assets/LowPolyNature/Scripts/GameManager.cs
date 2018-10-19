using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    #region ***Atributos****

    public PlayerUber undead;
	public PlayerUber witch;
	public PlayerUber samurai;
	public PlayerUber ricemonk;


    // How many players are there
    public int numberOfPlayers = 2;

    //lista que contiene los indices de los personajes que los jugadores seleccionaron
    //estan en orden por jugador, es decir, el jugador 1 selecciono el personaje que esta el primero en la lista
    //el jugador 2 selecciono el personaje que esta segundo en la lista, el jugador 3 selecciono el personaje que esta
    //tercero en la lista y el jugador 4 selecciono el personaje que esta cuarto en la lista.
    //LOS PERSONAJES SELECCIONADOS SE IDENTIFICAN DE LA SIGUIENTE MANERA: 
    //1.Bruja, 2.Riceman, 3.Samurai, 4.Undead

    // Dictionary that will save the 'Player': n,
    // selected in the 'Select your characters' screen
    public Dictionary<string, int> userNameCharacter = new Dictionary<string, int>(5);
    public Dictionary<int, string> idCharacter = new Dictionary<int, string>
        {
            { 1, "Witch" },
            { 2, "Riceman" },
            { 3, "Samurai" },
            { 4, "Undead" }
        };


    //Este atributo se utiliza en la pantalla de seleccion de personajes 
    //y sirve para determinar cuantos jugadores YA seleccionaron  un personaje
    //con esta variable se sabe cuando ya se debe de cargar la siguiente escena o
    //volver a mostrar la seleccion de personaje
    public int NumeroJugadorSeleccionandoPersonaje = 1;//llega hasta el numero n de jugadores, comenzaria en 1

    //Esta variable es util ya que solamente va existir un gamemanager en el juego
    //por que esta variable se leera cuando se llegue a la escena "PantallaCargandoLoadingScreen"
    //y en esa pantalla se consutura este atributo para saber que nivel debe de cargar
    public string NombreNivelQueSeVaCargar;


    //Esta variable se utiliza en el character mangaer ya que como SE CAMBIO LO DE LISTAS DE LISTAS A UN DICCIONARIO
    //sabemos que un diccionario no lleva un orden de lo que se le va agregando, entonces para saber cual fue el ultimo
    //jugador que selecciono un personaje aqui se guarda esos nombrewS y es utilizado 
    //cuando se presiona el boton "return" y saber que llave-valor del diccionario se debe eliminbar
    private List<string> NombresUltimoJugadorQueSeleccionoPersonaje = new List<string>(5);
    public int ComodinNombre = 2; //este comodin sirve para que no haya llave duplicadas si es que los usuairos se llaman igual..



    public static int currentPlayer = 1, index = 0;
    #endregion

    #region ***Métodos***
    protected virtual void Start() {
        //Se selecciona como primer jugador el elemento que esta al inicio de la lista.
        //El orden es como se van registrando es su turno
        //currentPlayer =Int32.Parse(ListaDeListasUsuarioPersonajesSeleccionados[index][1]);

    }

    //Método que regresa el jugador actual
    public int GetCurrentPlayer() {
        return currentPlayer;
    }

    //Metodo que cambia el jugador
    public int nextPlayer() {
        return 0;
        // Falta crear una variable en donde se guarde en qué orden quedaron los personajes al inicio
        // de tal forma que List[] = ["Player2", "Player3", "Player4"] contenga los Key del diccionario userNameCharacter.

        // //1. se aumenta el index
        // index++;
        // //2. Si es igual al numero de jugadores se regresa al primer jugador
        // if (index == ListaDeListasUsuarioPersonajesSeleccionados.Count()) {
        // 	index = 0;
        // 	currentPlayer = Int32.Parse(ListaDeListasUsuarioPersonajesSeleccionados[index][1]);
        // 	return currentPlayer;
        // //3. De lo contrario se va al siguiente de la lista
        // } else {
        // 	currentPlayer = userNameCharacter["asfgjkl"];
        // 	currentPlayer = Int32.Parse(ListaDeListasUsuarioPersonajesSeleccionados[index][1]);
        // 	return currentPlayer;
        // }
    }

    /// <summary>
    /// Inicializa los personajes que fueron seleccionados por el jugador,
    /// con los valores de sus respectivas clases
    /// </summary>
    public void instanciarPersonajes(){
		Debug.Log("entrooo");
		foreach (KeyValuePair<string, int> entry in userNameCharacter){
			int characterType = entry.Value;
			switch (characterType){
				case 1:
					witch = new PlayerUber(2);
					Debug.Log(witch.maxhp);
					break;
				case 2:
					samurai = new PlayerUber(3);
					break;
				case 3:
					ricemonk = new PlayerUber(4);
					break;
				case 4:
					undead = new PlayerUber(1);
					break;
				default:
					break;
			}
			Debug.Log(entry.Key + ", " + entry.Value);
		}
	}


    #region ***Métodos para manipulación y consulta de lista que tiene la información de seleccion personaje-usuario****
    //Descripción: Método de instancia útil para poder agregar
    //la informacicon de seleccion personaje-usuario en el diccionario de la clase.
    public void AddSelectUserCharacter(string name, int characterId) {
        this.userNameCharacter.Add(name, characterId);
    }

    //Descripción: Método de instancia útil para poder  eliminar 
    //el ultimo elemento agregado de la lista de seleccion personaje-usuario
    public void DeleteSelectedUserCharacter(string name) {
        this.userNameCharacter.Remove(name);
    }

    //Descripción: Permite el reinicio del diccionario para una nueva aventura, por lo que 
    //evita que se agreguen los personajes los personajes de la aventura pasada.
    public void ResetSelectedUserCharacter() {
        this.userNameCharacter = new Dictionary<string, int>(5);
    }


    //Descripción: Permite buscar en el diccionario si ya exitse el valor indicado 
    public bool VerificarExistenciaCharacterEnDiccionario(int identificadorPersonaje) {
        return this.userNameCharacter.Any(elemento => elemento.Value == identificadorPersonaje);
    }


    public bool ExisteLaLlaveEnDiccionario(string nombre) {
        return this.userNameCharacter.ContainsKey(nombre);

    }

    #endregion

    #region **METODOS PARA MANI´PULAÑCION LISTA NOMBRE PERSONAJES QUE SE ESTAN SELECCIONANDO***

    public void AgregarNombresUltimoJugadorQueSeleccionoPersonaje(string nombre) {
        this.NombresUltimoJugadorQueSeleccionoPersonaje.Add(nombre);
    }
    public void QuitarUltimoNombresUltimoJugadorQueSeleccionoPersonaje()
    {
        this.NombresUltimoJugadorQueSeleccionoPersonaje.RemoveAt(NombresUltimoJugadorQueSeleccionoPersonaje.Count - 1);
    }

    public string RegresarUltimoAgregadoNombresUltimoJugadorQueSeleccionoPersonaje() {
        return NombresUltimoJugadorQueSeleccionoPersonaje[NombresUltimoJugadorQueSeleccionoPersonaje.Count - 1];
    }

    public void ReseteaNombresUltimoJugadorQueSeleccionoPersonajer()
    {
        this.NombresUltimoJugadorQueSeleccionoPersonaje = new List<string>(5);
    }
    #endregion



    //Metodo to string para imprimir cosas utiles en cosnola
    public override string ToString()
    {
        StringBuilder mensaje = new StringBuilder("");
        foreach (KeyValuePair<string, int> entry in userNameCharacter) {
            mensaje.Append("Selección de personaje <NombreUsuario, #Personaje>: " + "<" + entry.Key + ", " + entry.Value + ">\n");
        }
        return mensaje.ToString();
    }
    #endregion
}