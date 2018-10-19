using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

//Autor: Irvin Emmanuel Trujillo Díaz
public class ReproductorVideo : MonoBehaviour {

    //Atributos publicos que seran asignados desde la GUI.
    public RawImage rawImage;
    public GameObject tituloJuego, elementosMenuPrincipal;
    public VideoPlayer reproductorVideo;
    public AudioSource recursoAudio;

    public int indiceVideoSeReproduce; //0 Menu princial, 1 Video de historia
    private GameManager gameManagerDelJuego;
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
        switch (indiceVideoSeReproduce) {
          case 0: //video del menu princoal
                tituloJuego.SetActive(true); elementosMenuPrincipal.SetActive(true);
                recursoAudio.Play(); //cancion del menu principal
                break;
            case 1: //video historia
                while (reproductorVideo.isPlaying)
                {
                    yield return null;
                }
                //Entonces el video ya termino...
                gameManagerDelJuego = GameManager.Instance;
                gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
                SceneManager.LoadScene("PantallaCargandoLoadingScreen");
                break;
            default: break; //todos los demas..
        }
    }
}
