using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour {
    //Esta clase es util para relacionar un modelo 3D (personaje) y poder definir su tipo 
    public int IdentificadorPersonaje; // 1.Bruja, 2.Riceman, 3.Samurai, 4.Undead"
    [HideInInspector]
    public string NombreUsuario; //Nombre del usuario
    //Constructores
    public CharacterInfo() { }
    public CharacterInfo(CharacterInfo instanciaCharacterInfo) { //este constructor es util para crear una instancia characterinfor apartir de un objeto de la misma clase.
        this.IdentificadorPersonaje = instanciaCharacterInfo.IdentificadorPersonaje;
        this.NombreUsuario = instanciaCharacterInfo.NombreUsuario;
    }
}
