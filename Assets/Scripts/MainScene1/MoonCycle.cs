using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class MoonCycle : MonoBehaviour {

	private static GameManager gameManagerDelJuego;

	public PostProcessingProfile ppProfile;


    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
		GameObject sunMoon = GameObject.FindGameObjectWithTag("SunMoon");
		Debug.Log(sunMoon);
		if (sunMoon){
			if (gameManagerDelJuego.deadMoonPhase){
				GameObject timeline = sunMoon.transform.GetChild(2).gameObject;
				timeline.SetActive(true);
			}
			else {
				GameObject timeline = sunMoon.transform.GetChild(3).gameObject;
				timeline.SetActive(true);
			}
		}
    }

	void Start(){
		gameManagerDelJuego = GameManager.Instance;
	}

	void Update(){
		GameObject moon = GameObject.Find("Moon");
		if (moon){
			// Quiere decir que nos encontramos en la escena de Dead Moon
			// y queremos regresar a las escenas principales
			gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
       		SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		}
		
		GameObject cleverCamera = GameObject.FindGameObjectWithTag("cleverCamera");
		GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		if (cleverCamera && mainCamera){
			// Quiere decir que nos encontramos en la escena principal y hay que cambiar de luz

			// Copy current Colour Grading Settings from the profile into a temporary variable
			ColorGradingModel.Settings colorGradingSettings = ppProfile.colorGrading.settings;
			if (gameManagerDelJuego.deadMoonPhase) {
				colorGradingSettings.colorWheels.linear.lift = new Color(1.0f, 0.40342485904693606f, 0.6392751336097717f, 0.0f);
				colorGradingSettings.colorWheels.linear.gamma = new Color(0.4487677812576294f, 0.5023999214172363f, 1.0f, -0.2237762212753296f);
				colorGradingSettings.colorWheels.linear.gain = new Color(0.8814182281494141f, 0.0f, 1.0f, -0.29836714267730715f);
			}
			else {
				colorGradingSettings.colorWheels.linear.lift = new Color(0.6471949219703674f, 0.5677330493927002f, 1.0f, 0.0f);
				colorGradingSettings.colorWheels.linear.gamma = new Color(0.7739930152893066f, 0.8391232490539551f, 1.0f, 0.0f);
				colorGradingSettings.colorWheels.linear.gain = new Color(1.0f, 0.9476133584976196f, 0.7559407949447632f, 0.0f);
			}
			// Set the color grading settings in the actual profile to the temp settings with the changed value
			ppProfile.colorGrading.settings = colorGradingSettings;
		}
	}

	public static void ChangeMoonCycle(){
		GameObject moons = GameObject.Find("Moons");
		if (moons){
			moons.transform.GetChild(gameManagerDelJuego.moonPhase).gameObject.SetActive(false);
			gameManagerDelJuego.moonPhase ++;
			if (gameManagerDelJuego.moonPhase == 4){
				gameManagerDelJuego.samePlayer = true; // Va a cargar al mismo jugador en el siguiente turno
				gameManagerDelJuego.changedScene = true;
				gameManagerDelJuego.deadMoonPhase = false;
				gameManagerDelJuego.NombreNivelQueSeVaCargar = "DeadMoonOut";
				SceneManager.LoadScene("PantallaCargandoLoadingScreen");
			} else if (gameManagerDelJuego.moonPhase >= 8){
				gameManagerDelJuego.moonPhase = 0;
			}
			if (gameManagerDelJuego.moonPhase == 0){
				gameManagerDelJuego.samePlayer = true; // Va a cargar al mismo jugador en el siguiente turno
				gameManagerDelJuego.changedScene = true;
				gameManagerDelJuego.deadMoonPhase = true;
				gameManagerDelJuego.NombreNivelQueSeVaCargar = "DeadMoonIn";
				SceneManager.LoadScene("PantallaCargandoLoadingScreen");
			}
			moons.transform.GetChild(gameManagerDelJuego.moonPhase).gameObject.SetActive(true);
		}
	}

}
