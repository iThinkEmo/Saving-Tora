using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizadorTarjetaStats : MonoBehaviour {

    //Variables públicas que se van asignar en el inspector
    public GameObject ConjuntoTarjetaStats;
    public RawImage ImagenJugador;
    public List<Texture> listaIconosJugador;
    public Text WealthTexto, FansTexto, HealthTexto, MpTexto, ApTexto, DpTexto, SpTexto, LevelTexto, ExperienciaTexto;
    //Variable privada del gamemanager que es un singleton-
    private GameManager _gameManagerDelJuego;

    public void Start() {
        _gameManagerDelJuego = GameManager.Instance;
    }

    // Update is called once per frame
    void Update () {
        if (_gameManagerDelJuego.BanderaYaSeDecidioCurrentPlayer){

            if (!ConjuntoTarjetaStats.activeInHierarchy) {
                ConjuntoTarjetaStats.SetActive(true);
            }

            PlayerUber player = _gameManagerDelJuego.GetPlayerUber();
            WealthTexto.text = "Ŧ" + player.money;
            FansTexto.text = ""+player.fans;
            HealthTexto.text = "" + player.hp;
            MpTexto.text = "" + player.mp;
            ApTexto.text = "" + player.ap;
            DpTexto.text = "" + player.dp;
            SpTexto.text = "" + player.sp;
            LevelTexto.text = "Level " + player.lv;
            ExperienciaTexto.text = "" + player.exp + "/" + player.maxExp;
            
            switch (_gameManagerDelJuego.GetCurrentPlayer()){
                case 1: //witch
                    ImagenJugador.texture = listaIconosJugador[0];
                    break;
                case 2:// riceman
                    ImagenJugador.texture = listaIconosJugador[1];
                    break;
                case 3: //samurai
                    ImagenJugador.texture = listaIconosJugador[2];
                    break;
                case 4: //calaca :v
                    ImagenJugador.texture = listaIconosJugador[3];
                    break;
            }
        }
   

    }
}
