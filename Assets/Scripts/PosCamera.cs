using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCamera : MonoBehaviour {

	public Animator cameraAnim;

	void Awake(){
		DontDestroyOnLoad(this);
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("hola mundo");
		cameraAnim.SetInteger("reposition", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
