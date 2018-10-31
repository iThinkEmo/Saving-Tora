using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectNode : MonoBehaviour {

	private static GameManager gameManagerDelJuego;

	static int currentPlayer = 0;
	public static int spacesLeft;
	int typeOfSpace;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Dice.finishedBouncing) {
			
			if (spacesLeft == 0) {
				HideSpacesLeftBtn();
				ShowConfirmSpaceBtn();
				if (Input.GetKeyDown(KeyCode.Y)) {
					LoadSpecialEventProxy();
				} else if (Input.GetKeyDown(KeyCode.N)) {
					MoveBackCurrentPlayer();
				}
			} else {
				MoveForwardCurrentPlayer();
			}
			
		}
	}

	void ShowConfirmSpaceBtn(){
		GameObject confirmSpaceBtn = GameObject.FindGameObjectWithTag("confirmSpace");
		if(confirmSpaceBtn){
			for (int i = 0; i < confirmSpaceBtn.transform.childCount; i++){
				confirmSpaceBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}
	void HideConfirmSpaceBtn(){
		GameObject confirmSpaceBtn = GameObject.FindGameObjectWithTag("confirmSpace");
		if(confirmSpaceBtn){
			for (int i = 0; i < confirmSpaceBtn.transform.childCount; i++){
				confirmSpaceBtn.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
	public static void ShowSpacesLeftBtn(){
		GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			for (int i = 0; i < dialogText.transform.childCount; i++){
				dialogText.transform.GetChild(i).gameObject.SetActive(true);
			}
			UpdateSpacesLeft();
		}
	}
	void HideSpacesLeftBtn(){
		GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			for (int i = 0; i < dialogText.transform.childCount; i++){
				dialogText.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public static void UpdateSpacesLeft(){
        GameObject dialogText = GameObject.FindGameObjectWithTag("spacesLeft");
		if (dialogText){
			string spacesLeft = dialogText.transform.GetChild(1).gameObject.GetComponent<Text>().text;
			dialogText.transform.GetChild(1).gameObject.GetComponent<Text>().text = spacesLeft.Remove(spacesLeft.Length - 1) + SelectNode.spacesLeft;
		}
    }

	public void LoadSpecialEventProxy(){
		StartCoroutine(LoadSpecialEvent());
        //LoadFightScene();
		//LoadEspecialEvent();
	}

	public IEnumerator LoadSpecialEvent(){
		
		//se obtiene el jugador para saber cuál mover
		currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
			if (script.gameObject != this.gameObject){
				yield break;
			}
			yield return null;

			// En este punto ya se sabe que hay solo una instancia de SelectNode activa
			// Las demás instancias ya se "salieron" debido al yield break;

			HideConfirmSpaceBtn();

			//Pelea:0,Tienda:1,Cofre:2,Hospital:3,Fabrica:4,Inn:5
			string character = gameManagerDelJuego.idCharacter[currentPlayer];
			GameObject currentCharacter = GameObject.Find(character);
			if (currentCharacter){
				Saviour saviour = currentCharacter.GetComponent<Saviour>();
				gameManagerDelJuego.currentSaviour = saviour;
				if (saviour) {
					typeOfSpace = NodesMap.nodesArray[saviour.currentNode, 4];
					Debug.Log("TypeOfSpace: "+typeOfSpace);
					PlayerUber player = gameManagerDelJuego.PlayerUberSaviour();
					switch (typeOfSpace) {
						case 0:
							StartCoroutine(LoadNextPlayer());
							currentPlayer = PlayerUber.normalizeCurrentPlayer(currentPlayer);
							//LoadFightScene();
							break;
						case 1:
							LoadStoreScene(saviour, player);
							break;
						case 2:
							GiveMeSomeMoney(saviour, player);
							break;
						case 3:
							break;
						case 4:
							break;
						case 5:
							break;
						default:
							break;
					}
				}
			}
			
		}		
	}

	public static IEnumerator LoadNextPlayer(){
		gameManagerDelJuego.nextPlayer();
		currentPlayer = gameManagerDelJuego.GetCurrentPlayer();

		string character = gameManagerDelJuego.idCharacter[currentPlayer];
		string characterArm = character.ToLower()+"Arm";

		LookAt.LookAtNextCharacter(character, characterArm);

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
		Debug.Log("algo 1");
		yield return null;
		yield return null;

		Debug.Log("algo 2");
		GameObject currentCharacter = GameObject.Find(character);
		Debug.Log(currentCharacter);
		if (currentCharacter){
			Saviour saviour = currentCharacter.GetComponent<Saviour>();
			if (saviour) {
				int area = NodesMap.nodesArea["Node "+saviour.currentNode];
				Debug.Log(NodesMap.areaName[area]);
				GameObject node = GameObject.Find("Node "+saviour.currentNode);
				if (node){
					GameObject dice = GameObject.FindGameObjectWithTag("Dice");
					GameObject box = GameObject.FindGameObjectWithTag("Box");
					if (dice && box){
						dice.transform.position = node.transform.position;
						dice.transform.Translate(0, 3.23f, 0, Space.World);
						box.transform.position = node.transform.position;
						box.transform.Translate(0, 2.4f, 0, Space.World);
					}
				}
			}
		}
	}

	public void LoadStoreScene(Saviour saviour, PlayerUber player){

		// currentplayer, area, typeOfFight = 3 (STORE)
		StatusMaker sm = new StatusMaker(currentPlayer, 1, 3);
		sm.MakeAndPostJSONFight();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
		Debug.Log("savage: "+saviour);
		gameManagerDelJuego.currentSaviour = saviour;

		// Hay que arreglar esto porque cuando se carga BuyStuff al crear la escena
		// se vuelve a setear el valor de null
		// Quizás lo mejor sería tener una referencia en GameManager y hacer un GetCurrentSaviour
		
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "Store";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
	}

	public void GiveMeSomeMoney(Saviour saviour, PlayerUber player){
		GameObject node = GameObject.Find("Node "+saviour.currentNode);
		if (node){
			GameObject coins = GameObject.FindGameObjectWithTag("Coin");
			if (coins){
				coins.transform.position = node.transform.position;
				coins.transform.Translate(0, 1.34f, 0, Space.World);
				SpecialGameEvents specialGameEvents = new SpecialGameEvents();
				string[] text = specialGameEvents.ChestInteraction(1, player, saviour);
				GameObject coinsGO = coins.transform.GetChild(0).gameObject;
				StartCoroutine(PlayCoinAnimation(coinsGO, text));
			}
		}
		Dice.finishedBouncing = false;
	}

	private IEnumerator PlayCoinAnimation(GameObject coins, string[] text){
		coins.SetActive(true);
		Animator anim = coins.GetComponent<Animator>();
		float counter = 0;
		float waitTime = 1.50f;

		GameObject foundGold = GameObject.FindGameObjectWithTag("foundGold");
		if (foundGold){
			GameObject goldBox = foundGold.transform.GetChild(0).gameObject;
			GameObject textGold = foundGold.transform.GetChild(1).gameObject;
			goldBox.SetActive(true);
			textGold.SetActive(true);
			textGold.GetComponent<Text>().text = text[0] + text[1] + "\n" + text[2];

			while (counter < (waitTime)){
				counter += Time.deltaTime;
				yield return null;
			}

			Debug.Log("Done Playing");
			coins.SetActive(false);
			goldBox.SetActive(false);
			textGold.SetActive(false);
			StartCoroutine(LoadNextPlayer());
		}
		
	}

	public void LoadFightScene(){
		// Guarda el numero de jugador
		StatusMaker sm = new StatusMaker(currentPlayer, 1, 0);
		sm.MakeAndPostJSONFight();
		// Guarda el jugador en un json 
		PlayerUber pu = new PlayerUber(currentPlayer);
		sm.SetPlayer(currentPlayer, pu);

        gameManagerDelJuego.NombreNivelQueSeVaCargar = "FightScene";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

	
	

	

	

	public void MoveForwardCurrentPlayer(){
		//se obtiene el jugador para saber cuál mover
		currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
			if (script.gameObject == this.gameObject){
				GetMovement(script);
			}
		}
	}

	public void MoveBackCurrentPlayer(){
		
		HideConfirmSpaceBtn();
		ShowSpacesLeftBtn();

		//se obtiene el jugador para saber cuál mover
		currentPlayer = gameManagerDelJuego.GetCurrentPlayer();

		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
			if (script.gameObject == this.gameObject){
				script.MoveBack();
			}
		}

	}

	void OnEnable(){
		ShowConfirmSpaceBtn();

		GameObject moveBackBtn = GameObject.FindGameObjectWithTag("moveBack");
		if(moveBackBtn){
			Button btn = moveBackBtn.GetComponent<Button>();
			btn.onClick.AddListener(MoveBackCurrentPlayer);
		}

		GameObject stayHereBtn = GameObject.FindGameObjectWithTag("stayHere");
		if(stayHereBtn){
			Button btn = stayHereBtn.GetComponent<Button>();
			btn.onClick.AddListener(LoadSpecialEventProxy);
		}

		HideConfirmSpaceBtn();
	}

	void OnDisable(){
		//ShowConfirmSpaceBtn();

		// GameObject moveBackBtn = GameObject.FindGameObjectWithTag("moveBack");
		// if(moveBackBtn){
		// 	Button btn = moveBackBtn.GetComponent<Button>();
		// 	btn.onClick.RemoveListener(MoveBackCurrentPlayer);
		// }

		// GameObject stayHereBtn = GameObject.FindGameObjectWithTag("stayHere");
		// if(stayHereBtn){
		// 	Button btn = stayHereBtn.GetComponent<Button>();
		// 	btn.onClick.RemoveListener(LoadSpecialEventProxy);
		// }

		// HideConfirmSpaceBtn();
	}

	private void GetMovement(Saviour saviour) {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (MoveSucceded(saviour, 0)) {
				saviour.Walk(saviour.currentNode, 0);
			}
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if (MoveSucceded(saviour, 1)) {
				saviour.Walk(saviour.currentNode, 1);
			}
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (MoveSucceded(saviour, 2)) {
				saviour.Walk(saviour.currentNode, 2);
			}
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (MoveSucceded(saviour, 3)) {
				saviour.Walk(saviour.currentNode, 3);
			}
		}
	}

	private bool MoveSucceded(Saviour saviour, int i) {
		int current_node = saviour.currentNode;
		return NodesMap.nodesArray[current_node, i] != 0;
	}
}
