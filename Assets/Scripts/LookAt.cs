using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour {

	private static GameManager gameManagerDelJuego  = GameManager.Instance;

	// Use this for initialization
	void Start () {
			
	}

	public static void LookAtNextCharacter(string character, string characterArm){
		DisplayTurn();
		
		GameObject player = GameObject.FindGameObjectWithTag(character);
		GameObject charArm = GameObject.FindGameObjectWithTag(characterArm);
		GameObject virtualCameraGO = GameObject.FindGameObjectWithTag("Virtual Camera 2");
		if(virtualCameraGO){
			CinemachineVirtualCamera virtualCamera = virtualCameraGO.GetComponent<CinemachineVirtualCamera>();
			if(virtualCamera){
				virtualCamera.LookAt = charArm.transform;
				virtualCamera.Follow = player.transform;
				// virtualCamera.DestroyCinemachineComponent<CinemachineTrackedDolly>();
				// virtualCamera.AddCinemachineComponent<CinemachineTransposer>();
				// var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
				// transposer.m_BindingMode = Cinemachine.CinemachineTransposer.BindingMode.LockToTargetOnAssign;
				// transposer.m_FollowOffset = new Vector3(1.298643f, 4.17f, 6.615618f);
				var trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
				CinemachineSmoothPath path = GameObject.Find("DollyTrack3").GetComponent<CinemachineSmoothPath>();
				trackedDolly.m_Path = path;
				CinemachineTrackedDolly.AutoDolly autoDolly = new CinemachineTrackedDolly.AutoDolly(true, -0.25f, 2, 5);
				trackedDolly.m_AutoDolly = autoDolly;
				
				CoupleCameras(virtualCameraGO);
			}
		}
	}

	public static void CoupleCameras(GameObject vCam){
		GameObject cleverCamera = GameObject.FindGameObjectWithTag("cleverCamera");
		GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		if(cleverCamera && mainCamera){
			cleverCamera.transform.position = vCam.transform.position;
			cleverCamera.transform.rotation = vCam.transform.rotation;
			GameObject fadingTimeline = GameObject.FindGameObjectWithTag("fadingTimeline");
			if (fadingTimeline && !gameManagerDelJuego.changedScene){
				fadingTimeline.transform.GetChild(1).gameObject.SetActive(true);
			}
		}
	}

	public static void DisplayTurn(){
		GameObject whosNext = GameObject.FindGameObjectWithTag("whoNext");
		if (whosNext){
			GameObject timeline = whosNext.transform.GetChild(2).gameObject;
			GameObject dialogText = whosNext.transform.GetChild(1).gameObject;
			dialogText.GetComponent<Text>().text = 
				gameManagerDelJuego.orderedPlayers[0] + " , it's your turn!";

			PlayerUber player = gameManagerDelJuego.GetPlayerUber();
			if (player.onDuty){
				dialogText.GetComponent<Text>().text += "\n But you shall keep fighting!"; 
			}

			timeline.SetActive(true);
		}
		if (gameManagerDelJuego.turnsCount % gameManagerDelJuego.numberOfPlayers == 0){
			MoonCycle.ChangeMoonCycle();
		}
		gameManagerDelJuego.turnsCount ++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
