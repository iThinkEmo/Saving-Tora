using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Esta clase lo que hace es encargarse de evaluar el input field, si este es vacio
//entonces no mostrara el boton de seleccion de personaje ya que se debe de dar un nombre.
public class InputFieldSelecciconPersonajes : MonoBehaviour
{

    public InputField InputFieldSeleccionPersonaje;
    public Button botonSeleccionarPersonaje;

    //Se evalua el input field para que el usuario solamente se muestre el boton si el usuario
    //ingreso algo en el input field.
    public void EvaluarInputField()
    {
        if (InputFieldSeleccionPersonaje.text.Trim().Equals(""))
        {
            botonSeleccionarPersonaje.gameObject.SetActive(false);
        }
        else
        {
            botonSeleccionarPersonaje.gameObject.SetActive(true);
        }

    }
}
