using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager> {

    #region ***Atributos****

    public PlayerUber undead;
	public PlayerUber witch;
	public PlayerUber samurai;
	public PlayerUber ricemonk;

    public Saviour currentSaviour;

    public bool firstLoadMainScene = true;

    public bool[] firstTimeTutorial = new bool[4]{
        true,
        true,
        true,
        true
    };

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
    public Dictionary<string, int> playerDictionary = new Dictionary<string, int>(5);
    public Dictionary<int, string> idCharacter = new Dictionary<int, string>
        {
            { 1, "Witch" },
            { 2, "RiceMan" },
            { 3, "Samurai" },
            { 4, "Undead" }
        };

    public string[] orderedPlayers;
    public int[] orderedPlayersID;


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

    
    //bandera para evitar errores en el update de "ActualizadorTarjetasStats" para saber que stats mostrar en tarjeta
    public bool BanderaYaSeDecidioCurrentPlayer { get; set; }



    public static int currentPlayer = 1, index = 1;
    #endregion

    #region ***Métodos***
    protected virtual void Start() {
        //primer jsonhb para cargar escena principal :v
        StatusMaker mk = new StatusMaker();
        //Deberia recibir posiciones y rotaciones iniciales de los players men
        // Mandar posiciones y rotaciones de los personajes 
        // Consultar status maker para ver que recibe este método
        //mk.setInitialScreenjson();      
    }

    //para obtener el numero de area basada en el node en el que esta
    /// <summary>
    /// Regresa un entero (número de área / nivel) dependiendo el nodo que recibe
    /// </summary>
    public int GetAreaBasedOnNodesPos(int node)
    {
        return 0;
    }

    /// <summary>
    /// Determina mediante un shuffle el orden en el que jugarán los jugadores
    /// </summary>
    public void WhoStarts(){
        orderedPlayers = playerDictionary.Keys.ToArray();
        orderedPlayersID = playerDictionary.Values.ToArray();
        for (int i = 0; i < Random.Range(1, 5); i ++){
        //for (int i = 0; i < 1; i ++){
            orderedPlayers = LeftShift(orderedPlayers);
            orderedPlayersID = LeftShift(orderedPlayersID);
        }
        currentPlayer = playerDictionary[orderedPlayers[0]];
    }

    static string[] LeftShift(string[] array){            
        // all elements except for the first one... and at the end, the first one. to array.
        return array.Skip(1).Concat(array.Take(1)).ToArray();
    }

    static int[] LeftShift(int[] array){            
        // all elements except for the first one... and at the end, the first one. to array.
        return array.Skip(1).Concat(array.Take(1)).ToArray();
    }

    //Método que regresa el jugador actual
    public int GetCurrentPlayer() {
        return currentPlayer;
    }

    // Regresa el Saviour actual
    public Saviour GetCurrentSaviour() {
        return currentSaviour;
    }

    //Metodo que cambia el jugador
    public void nextPlayer() {
        orderedPlayers = LeftShift(orderedPlayers);
        orderedPlayersID = LeftShift(orderedPlayersID);
        currentPlayer = orderedPlayersID[0];
    }

    /// <summary>
    /// Inicializa los personajes que fueron seleccionados por el jugador,
    /// con los valores de sus respectivas clases
    /// </summary>
    public void instanciarPersonajes(){
		foreach (KeyValuePair<string, int> entry in playerDictionary){
			int characterType = entry.Value;
			switch (characterType){
				case 1:
					witch = new PlayerUber(2);
					break;
				case 2:
					ricemonk = new PlayerUber(4);
					break;
				case 3:
					samurai = new PlayerUber(3);
					break;
				case 4:
					undead = new PlayerUber(1);
					break;
				default:
					break;
			}
		}
	}

    public PlayerUber PlayerUberSaviour(){
        PlayerUber player;
        Debug.Log("currPlay: "+ currentPlayer);
        switch (currentPlayer){
            case 1:
                player = witch;
                break;
            case 2:
                player = ricemonk;
                break;
            case 3:
                player = samurai;
                break;
            case 4:
                player = undead;
                break;
            default:
                player = witch;
                break;
        }
        return player;
    }

    public PlayerUber GetPlayerUber() {
        StatusMaker statusMaker = new StatusMaker();
        PlayerUber player = statusMaker.getPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer));
        
        // foreach (KeyValuePair<int, ItemRPG> entry in player.items.myItems) {
        //     Debug.Log("llave: "+entry.Key);
		// 	Debug.Log("name: "+entry.Value.name);
        // }
        
        return player;
    }

    #region ***Métodos para manipulación y consulta de lista que tiene la información de seleccion personaje-usuario****
    //Descripción: Método de instancia útil para poder agregar
    //la informacicon de seleccion personaje-usuario en el diccionario de la clase.
    public void AddSelectUserCharacter(string name, int characterId) {
        this.playerDictionary.Add(name, characterId);
    }

    //Descripción: Método de instancia útil para poder  eliminar 
    //el ultimo elemento agregado de la lista de seleccion personaje-usuario
    public void DeleteSelectedUserCharacter(string name) {
        this.playerDictionary.Remove(name);
    }

    //Descripción: Permite el reinicio del diccionario para una nueva aventura, por lo que 
    //evita que se agreguen los personajes los personajes de la aventura pasada.
    public void ResetSelectedUserCharacter() {
        this.playerDictionary = new Dictionary<string, int>(5);
    }


    //Descripción: Permite buscar en el diccionario si ya exitse el valor indicado 
    public bool VerificarExistenciaCharacterEnDiccionario(int identificadorPersonaje) {
        return this.playerDictionary.Any(elemento => elemento.Value == identificadorPersonaje);
    }


    public bool ExisteLaLlaveEnDiccionario(string nombre) {
        return this.playerDictionary.ContainsKey(nombre);

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
        foreach (KeyValuePair<string, int> entry in playerDictionary) {
            mensaje.Append("Selección de personaje <NombreUsuario, #Personaje>: " + "<" + entry.Key + ", " + entry.Value + ">\n");
        }
        return mensaje.ToString();
    }
    #endregion
}