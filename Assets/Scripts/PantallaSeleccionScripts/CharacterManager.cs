using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace CharacterSelector.Scripts
{
    public class CharacterManager : SingletonBase<CharacterManager>
    { //Esta clase se utiliza en la seleccion de personaje, sirve para obtener información de lo que selecciono el usuario y mandar la información seleccionada a la pantalla de inicio de juego.
        public CharacterInfo[] ListaPersonajesPosibles;
        //información del personaje que se selecciono (aun no es seleccion definitiva).
        public CharacterInfo InstanciaModeloEInformacionDelPersonajeSeleccionado; //el de default es riceman
        public Text TextoJugadorQueEstaSeleccionado; //player 1, player 2, player 3, player 4... eso va decir :v
        public Button BotonRegresar, BotonWitch, BotonRiceman, BotonSamurai, BotonUndead;
        public GameObject ObjectCancion;

        //informacion del personaje que se selecciono definitivamente
        private CharacterInfo _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente = null;
        private GameManager _gameManagerDelJuego;
        private GameObject[] _objetosCancion;
        private bool _banderaCancionDestruida;


        public void Start() {
            this._gameManagerDelJuego = GameManager.Instance; //se obtinene el gamemanager del juego  (recordar que es singleton por lo que solamente existira uno)
            TextoJugadorQueEstaSeleccionado.text = "player "+this._gameManagerDelJuego.NumeroJugadorSeleccionandoPersonaje;
            if (_gameManagerDelJuego.NumeroJugadorSeleccionandoPersonaje > 1) {
                //BotonRegresar.gameObject.SetActive(true); //se oculta esto por ahora...
            }
            _banderaCancionDestruida = false;
            _objetosCancion = GameObject.FindGameObjectsWithTag("cancionMenu");

            //verificando que no exista otro objeto de cancion PARA EVITAR QUE SE DUPLIQUE. :v
            if (_objetosCancion != null && _objetosCancion.Length>1) { //ya existe mas de un objeto :v (SINGLETON)
                _banderaCancionDestruida = true;
                Destroy(ObjectCancion);
            }

            ConfiguracionParaMostrarModelosEnPantallaDefault();

            //Verificando que personajes han sido seleccionados para evitar que otro usuario lo seleccione
            ModificarInterfazAcodeSeleccionPersonajes();

        }

        protected override void Init()
        {
            Persist = false; //para que se destruya 
            base.Init();
        }

        public void SeleccionarPersonajeNoDefinitivo(string nombrePersonaje)
        {
            int identificadorPersonaje = 2; //el de default es riceman, es 2.
            switch (nombrePersonaje.ToUpper()) {
                // 1.Bruja, 2.Riceman, 3.Samurai, 4.Undead
                case "WITCH":
                    identificadorPersonaje = 1;
                    break;
                case "SAMURAI":
                    identificadorPersonaje = 3;
                    break;
                case "UNDEAD":
                    identificadorPersonaje = 4;
                    break;
            }
            Debug.Log("identificadorPersonaje: "+ identificadorPersonaje);
            foreach (CharacterInfo informacionDelPersonajePosible in ListaPersonajesPosibles)
            {
                if (informacionDelPersonajePosible.IdentificadorPersonaje == identificadorPersonaje)
                {
                    //se obtiene la informacion del personaje seleciconado
                    InstanciaModeloEInformacionDelPersonajeSeleccionado = new CharacterInfo(informacionDelPersonajePosible);
                    Debug.Log("Id de la instancia seleccionada "+ InstanciaModeloEInformacionDelPersonajeSeleccionado.IdentificadorPersonaje);
                    break;
                }
            }
        }

        //Descripcion: Este metodo se activa cuando el usuario presiona el boton "select character"
        //y lo que hace es actualizar la lista del gamemanagaer de la seleccion del personaje-usuario
        //y verificar si se debe de volver a mostrar una pantalla de seleccion de personaje 
        //o se deba de pasar a la siguiente escena (cargar el juego).
        public void SeleccionarPersonajeDefinitivo(string nombreUsuarioInputField)
        {//se activda cuando seleccionan al personaje (ya fue seleccion deifnitiva)
            InstanciaModeloEInformacionDelPersonajeSeleccionado.NombreUsuario = nombreUsuarioInputField.Trim();
            _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente = new CharacterInfo(InstanciaModeloEInformacionDelPersonajeSeleccionado);
            Debug.Log("Numero de personaje: 1.Bruja, 2.Riceman, 3.Samurai, 4.Undead");
            Debug.Log("Personaje seleccionado (número): " + _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.IdentificadorPersonaje);
            

            //verificando si el diccionario ya tiene la llave, si ya la tiene entonces agregarle un comodin..
            if (this._gameManagerDelJuego.ExisteLaLlaveEnDiccionario(_instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.NombreUsuario)) {
                _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.NombreUsuario = _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.NombreUsuario + _gameManagerDelJuego.ComodinNombre;
                _gameManagerDelJuego.ComodinNombre += 1;
            }
            Debug.Log("Nombre del jugador: " + _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.NombreUsuario);
            this._gameManagerDelJuego.AddSelectUserCharacter(_instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.NombreUsuario,
                                                            _instanciaModeloEInformacionPersonajeSeleccionadoDefinitivamente.IdentificadorPersonaje);

            this._gameManagerDelJuego.NumeroJugadorSeleccionandoPersonaje += 1; //un jugador YA selecciono un personaje. (empieza la cuenta en 1 y avanza en 1 en 1).
            this._gameManagerDelJuego.AgregarNombresUltimoJugadorQueSeleccionoPersonaje(InstanciaModeloEInformacionDelPersonajeSeleccionado.NombreUsuario); //indica quien fue el ultimo jugador que selecciono personaje
            if (this._gameManagerDelJuego.NumeroJugadorSeleccionandoPersonaje<=this._gameManagerDelJuego.numberOfPlayers)
            {//entonces todavia faltan jugadores por seleccionar personaje, se vuelve a mostrar la pantalla
                MostrarSeleccionPersonajeNuevamente();
            }
            else { //todos los jugadores ya seleccionaron un personaje
                //NOTA IMPORTANTE: TODA LA INFORMACION DE LA SELECCION DE LOS PERSONAJES
                //SE ENCUENTA EN EL GAME MANAGER EN EL ATRIBUTO userNameCharacter
                //en el siguiente debug.log la clase gamemanager se indico que imprimiera
                //como mensaje la lsita con esta informacion:
                Debug.Log(_gameManagerDelJuego);

                // Aquí se crean las instancias de los personajes seleccionados por los jugadores
                _gameManagerDelJuego.instanciarPersonajes();

                Destroy(GameObject.FindGameObjectWithTag("cancionMenu")); //se destruye el objeto de la cancion para que no se sobreponga
                _gameManagerDelJuego.NombreNivelQueSeVaCargar = "EscenaVideoHistoriaDelJuego";  //CARGAR ESCENA DEL VIDEO
                SceneManager.LoadScene("PantallaCargandoLoadingScreen"); //mostrar pantalla de escena de cargar
            }
        }

        //Descripcion: Este metodo se activa cuando el usuario presiona el boton "return"
        //y lo que hace es permitir al usuario regresar a la seleccion del personaje del jugador nActual-1
        //por ejemplo, si estas en la seleccion del jugador 2 y se quiere cambiar la seleccion  del jugador 1
        //entonces se presiona el boton return y el jugador 1 ya podra seleccionar su personaje de nuevo
        public void RegresarUltimaSeleccion() {
            this._gameManagerDelJuego.DeleteSelectedUserCharacter(this._gameManagerDelJuego.RegresarUltimoAgregadoNombresUltimoJugadorQueSeleccionoPersonaje());
            this._gameManagerDelJuego.QuitarUltimoNombresUltimoJugadorQueSeleccionoPersonaje();
            this._gameManagerDelJuego.NumeroJugadorSeleccionandoPersonaje -= 1;
            MostrarSeleccionPersonajeNuevamente();
        }

        //Descripcion: Este metodo privado es utilizado para recargar la escena, es decir, mostrar
        //nuevamente la escena de selecciond de personaje
        private void MostrarSeleccionPersonajeNuevamente() {
            if (!_banderaCancionDestruida)
            {
                DontDestroyOnLoad(ObjectCancion);//que no se destruya el objeto de la cancion
            }
            Destroy(this);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        private void ModificarInterfazAcodeSeleccionPersonajes() {
            if (_gameManagerDelJuego.VerificarExistenciaCharacterEnDiccionario(1))
            { //ya selecciona a witch? Sí
                BotonWitch.gameObject.SetActive(false);
                //Ahora el de default es otro que no este en la lista de ya seleccionados..
                InstanciaModeloEInformacionDelPersonajeSeleccionado = RegresarInstanciaDefaultQueNoHaSidoSeleccionada();
            }
            else {
                BotonWitch.gameObject.SetActive(true);
            }
          
    
            if (_gameManagerDelJuego.VerificarExistenciaCharacterEnDiccionario(3))
            { //ya selecciona a samurai? Sí
                BotonSamurai.gameObject.SetActive(false);
                //Ahora el de default es otro que no este en la lista de ya seleccionados, se busca y se selecciona
                InstanciaModeloEInformacionDelPersonajeSeleccionado = RegresarInstanciaDefaultQueNoHaSidoSeleccionada();
            }
            else {
                BotonSamurai.gameObject.SetActive(true);
            }

            if (_gameManagerDelJuego.VerificarExistenciaCharacterEnDiccionario(4))
            { //ya selecciona a undead? Sí
                BotonUndead.gameObject.SetActive(false);
                //Ahora el de default es otro que no este en la lista de ya seleccionados..
                InstanciaModeloEInformacionDelPersonajeSeleccionado = RegresarInstanciaDefaultQueNoHaSidoSeleccionada();

            }
            else {
                BotonUndead.gameObject.SetActive(true);
            }

   

            if (_gameManagerDelJuego.VerificarExistenciaCharacterEnDiccionario(2))
            { //ya selecciona a riceman? Sí
                BotonRiceman.gameObject.SetActive(false);
                //Ahora el de default es otro que no este en la lista de ya seleccionados..
                InstanciaModeloEInformacionDelPersonajeSeleccionado = RegresarInstanciaDefaultQueNoHaSidoSeleccionada();
            }
            else
            {
                BotonRiceman.gameObject.SetActive(true);
                InstanciaModeloEInformacionDelPersonajeSeleccionado = ListaPersonajesPosibles[0]; //el primero es riceman.
            }

        }

        //Regresda la instancia que va ser el default y que no ha sido seleccionado, los que ya fueron seleccionados desactiva
        //sus modelos para que no se vean al principio al cargar la pantalla
        private CharacterInfo RegresarInstanciaDefaultQueNoHaSidoSeleccionada()
        {
            foreach (CharacterInfo personaje in ListaPersonajesPosibles)
            {
                if (!_gameManagerDelJuego.VerificarExistenciaCharacterEnDiccionario(personaje.IdentificadorPersonaje))
                {
                    personaje.gameObject.SetActive(true);
                    return personaje;
                }
                else {
                    personaje.gameObject.SetActive(false);
                }
            }
            return null;
        }

        
        private void ConfiguracionParaMostrarModelosEnPantallaDefault() {
            //solo son 4 entonces..
            ListaPersonajesPosibles[0].gameObject.SetActive(true); //riceman..
            ListaPersonajesPosibles[1].gameObject.SetActive(false); //witch..
            ListaPersonajesPosibles[2].gameObject.SetActive(false); //samurai..
            ListaPersonajesPosibles[3].gameObject.SetActive(false); //undead..
        }




    }


}
