using CharacterSelector.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCharacterSelect : MonoBehaviour {
    //Clase que es utilizada para el comportamiento de los botones de la pantalla de selecciond e personaje

    public InputField NombreUsuario;

    public void SwitchCharacter()
    {//METODO QUE CAMBIAR EL PERSONAJE (seleccionaron un boton con nombre personaj)
        string nombrePersonaje = transform.Find("textoBoton").GetComponent<Text>().text; //SE OBTIENE EL TIPO DEL PERSONJAE (EJEMPLO: Witch, Riceman, etc).
        Debug.Log(nombrePersonaje);
        CharacterManager.Instance.SeleccionarPersonajeNoDefinitivo(nombrePersonaje);
    }


    public void SeleccionDefinitiva()
    { //metodo QUE SELECCIONA DE FORMA DEFINITIVA UN PERSONAJE (SE ACTIVDA CUANDO LE DAN EN SELECT CHARACTER)
        CharacterManager.Instance.SeleccionarPersonajeDefinitivo(NombreUsuario.text);
    }

    public void RegresarUltimaSeleccion() {
        CharacterManager.Instance.RegresarUltimaSeleccion();
    }
}
