using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Clase que sirve para la carga de escena
public class CargadorDeEscenas : MonoBehaviour {

    //Variables que se asignan en el inspector
    public RawImage ImagenCargando;


    private AsyncOperation operadorAsincrono; //sera de utilidad para la cargfa de la escena
    private Vector3 rotationEulerParaImagen;
    private int rotacionGrados = 90;

    private string[] nombreEscenasQueSeVanCargarMultiple = new string[] {
        "Main Scene",
        //"Dice",
        "Level 1",
        "Level 2",
        "Level 3",
        "Level 4",
        "Continent",
        "Main Characters 1",
        "Navigation Mesh"
    };

    public void Start() {
        Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        GameManager gameManagerDelJuego = GameManager.Instance;
        StartCoroutine(AsynchronousLoad(gameManagerDelJuego.NombreNivelQueSeVaCargar));   
    }

    
    public void Update() {
                     rotationEulerParaImagen += Vector3.forward * rotacionGrados * Time.deltaTime;
                rotacionGrados += 30;
                ImagenCargando.transform.rotation = Quaternion.Euler(rotationEulerParaImagen);//aplicando el giro  a la imagen
    }



    IEnumerator AsynchronousLoad(string escena)
    {
        yield return null;
        if (escena.Equals("QueSeCargueElJuego:v"))
        {
            Scene originalScene = SceneManager.GetActiveScene();

            List<AsyncOperation> sceneLoads = new List<AsyncOperation>();
            for (int i = 0; i < nombreEscenasQueSeVanCargarMultiple.Length; ++i)
            {
                AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(nombreEscenasQueSeVanCargarMultiple[i], LoadSceneMode.Additive);
                sceneLoading.allowSceneActivation = false;
                sceneLoads.Add(sceneLoading);
                while (sceneLoads[i].progress < 0.9f) {
                    rotationEulerParaImagen += Vector3.forward * rotacionGrados * Time.deltaTime;
                    rotacionGrados += 30;
                    ImagenCargando.transform.rotation = Quaternion.Euler(rotationEulerParaImagen);//aplicando el giro  a la imagen
                    yield return null; }
            }
            // TODO: deactivate conflicting components from originalScene here
            // need to have at least one active scene before unloading the originalScene    
            for (int i = 0; i < sceneLoads.Count; ++i)
            {
                sceneLoads[i].allowSceneActivation = true;
                while (!sceneLoads[i].isDone) {
                    rotationEulerParaImagen += Vector3.forward * rotacionGrados * Time.deltaTime;
                    rotacionGrados += 30;
                    ImagenCargando.transform.rotation = Quaternion.Euler(rotationEulerParaImagen);//aplicando el giro  a la imagen
                    yield return null; }
            }

            AsyncOperation sceneUnloading = SceneManager.UnloadSceneAsync(originalScene);
            while (!sceneUnloading.isDone) {
                rotationEulerParaImagen += Vector3.forward * rotacionGrados * Time.deltaTime;
                rotacionGrados += 30;
                ImagenCargando.transform.rotation = Quaternion.Euler(rotationEulerParaImagen);//aplicando el giro  a la imagen
                yield return null; }
        }
        else
        {
            operadorAsincrono = SceneManager.LoadSceneAsync(escena);
            operadorAsincrono.allowSceneActivation = false;
            //Este while se ejecuta mientras se hace la carga de la escena
            while (!operadorAsincrono.isDone)
            {
                float progress = Mathf.Clamp01(operadorAsincrono.progress / 0.9f);
                Debug.Log("Progreso de carga: " + (progress * 100) + "%");
                rotationEulerParaImagen += Vector3.forward * rotacionGrados * Time.deltaTime;
                rotacionGrados += 30;
                ImagenCargando.transform.rotation = Quaternion.Euler(rotationEulerParaImagen);//aplicando el giro  a la imagen
                if (operadorAsincrono.progress == 0.9f)
                { //la escena se ha cargado completamente
                    operadorAsincrono.allowSceneActivation = true; // se activa la escena ya  ya se cargo
                }

                yield return null;
            }
        }
    }

}
