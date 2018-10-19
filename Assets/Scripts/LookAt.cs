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
			Debug.Log("yes");
			CinemachineVirtualCamera virtualCamera = virtualCameraGO.GetComponent<CinemachineVirtualCamera>();
			if(virtualCamera){
				Debug.Log("yess");
				virtualCamera.LookAt = objectToLookAt.transform;
			}
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
