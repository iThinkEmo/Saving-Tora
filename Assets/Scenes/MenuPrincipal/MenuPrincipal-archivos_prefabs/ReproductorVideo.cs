using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

//Autor: Irvin Emmanuel Trujillo Díaz
public class ReproductorVideo : MonoBehaviour {

    //Atributos publicos que seran asignados desde la GUI.
    public RawImage rawImage;
    public GameObject tituloJuego, elementosMenuPrincipal;
    public VideoPlayer reproductorVideo;
    public AudioSource recursoAudio;
   
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        reproductorVideo.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1); //1 segundo
        while (!reproductorVideo.isPrepared)
        {
            yield return waitForSeconds; //esperando 1 segundo, con yield permite la espera y reunuda,el break rompera el ciclo
            break;
        }
        //se asigna a la rawimagen el video para que lo vea el usuario, asi como mostrar el titulo del juego y los respectivos botones:D
        rawImage.texture = reproductorVideo.texture;
        reproductorVideo.Play();
        recursoAudio.Play();
        tituloJuego.SetActive(true); elementosMenuPrincipal.SetActive(true); 
    }
}
