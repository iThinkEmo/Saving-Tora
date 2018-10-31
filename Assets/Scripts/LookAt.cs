using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject objectToLookAt = GameObject.FindGameObjectWithTag("Node");
		GameObject virtualCameraGO = GameObject.FindGameObjectWithTag("Virtual Camera 2");
		if (virtualCameraGO) {
			CinemachineVirtualCamera virtualCamera = virtualCameraGO.GetComponent<CinemachineVirtualCamera>();
			if(virtualCamera){
				virtualCamera.LookAt = objectToLookAt.transform;
			}
		}		
	}

	public static void LookAtNextCharacter(string character, string characterArm){
		GameObject player = GameObject.FindGameObjectWithTag(character);
		GameObject charArm = GameObject.FindGameObjectWithTag(characterArm);
		GameObject virtualCameraGO = GameObject.FindGameObjectWithTag("Virtual Camera 2");
		Debug.Log(virtualCameraGO);	
		if(virtualCameraGO){
			CinemachineVirtualCamera virtualCamera = virtualCameraGO.GetComponent<CinemachineVirtualCamera>();
			if(virtualCamera){
				Debug.Log(virtualCamera);
				virtualCamera.LookAt = charArm.transform;
				virtualCamera.Follow = player.transform;
				Debug.Log(virtualCamera.LookAt);
				Debug.Log(virtualCamera.Follow);
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
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
