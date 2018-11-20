using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Dice : MonoBehaviour{

	Rigidbody rb;
	public Animator anim;
	public static int side;

	private static bool firstToss = true;

	private bool rolling = true;

	public static bool finishedBouncing;

	public GameObject starSparks, dice;

	private GameManager gameManagerDelJuego;

	void Start(){
		gameManagerDelJuego = GameManager.Instance;
		finishedBouncing = false;
	}
	void Update() {
		if(finishedBouncing) {
			side = anim.GetInteger("side");
			starSparks.SetActive(true);
			dice.SetActive(false);
			TurnOff();
		}

		if (rolling && Input.GetKeyDown(KeyCode.Space)) {
			TossDice();
		}

	}

	void TossDice() {
		gameManagerDelJuego.BanderaYaSeDecidioCurrentPlayer = true;
		// gameManagerDelJuego.changedScene = true;

		int currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
		string character = gameManagerDelJuego.idCharacter[currentPlayer];
		string characterArm = character.ToLower()+"Arm";
		
		if (firstToss){
			firstToss = false;
			LookAt.LookAtNextCharacter(character, characterArm);
		}

		if(gameManagerDelJuego.changedScene){
			gameManagerDelJuego.changedScene = false;
		}

		GameObject nodesType = GameObject.FindGameObjectWithTag("nodesType");
		if (nodesType){
			nodesType.transform.GetChild(0).gameObject.SetActive(true);
		}

		rolling = false;
		finishedBouncing = false;
		rb = GetComponent<Rigidbody>();
		rb.useGravity = true;
		int side = Random.Range(1, 7);
		anim.SetInteger("side", side);

		SelectNode.spacesLeft = side;
		// SelectNode.spacesLeft = 12;
		// SelectNode.spacesLeft = 505;

		Vector3 force = Vector3.up * Random.Range(360.0f, 380.0f);
		rb.AddForce(force);

		Vector3 v = Vector3.forward;
		v = Random.rotation * v;
		rb.AddTorque(v * 25.0f);

	}

	public void TurnOff(){
		this.enabled = false;
		SceneManager.UnloadSceneAsync("Dice");
	}

}