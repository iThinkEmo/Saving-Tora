using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour{

	Rigidbody rb;
	public Animator anim;
	public static int side;

	private bool rolling = true;

	public static bool finishedBouncing;

	private PosCamera posCamera;

	public GameObject starSparks, dice;
	
	void Update() {

		if(finishedBouncing) {
			//GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			Debug.Log(anim.GetInteger("side"));
			side = anim.GetInteger("side");
			posCamera = GameObject.Find("Main Camera").GetComponent<PosCamera>();
			posCamera.enabled = true;
			starSparks.SetActive(true);
			dice.SetActive(false);
			TurnOff();
		}

		if (rolling && Input.GetKeyDown(KeyCode.Space)) {
			TossDice();
		}
		
	}

	void TossDice() {
		rolling = false;
		finishedBouncing = false;
		rb = GetComponent<Rigidbody>();
		rb.useGravity = true;
		int side = Random.Range(6, 7);
		anim.SetInteger("side", side);

		//SelectNode.spacesLeft = side;
		SelectNode.spacesLeft = 12;

		Vector3 force = Vector3.up * Random.Range(360.0f, 380.0f);
		rb.AddForce(force);

		Vector3 v = Vector3.forward;
		v = Random.rotation * v;
		rb.AddTorque(v * 25.0f);

	}

	public void TurnOff(){
		this.enabled = false;
	}

}