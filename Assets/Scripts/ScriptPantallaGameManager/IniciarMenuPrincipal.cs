using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarMenuPrincipal : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(1);
        //Nota, el gamemanager al ser singleton ya tiene implementado 
        //el dont destroy on load en su awake por lo que no es necesario especificarlo aqui.
        SceneManager.LoadScene("PantallaPrincipal");
    }
}
