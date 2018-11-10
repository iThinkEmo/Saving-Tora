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

	public static string[] innDialogue, hospitalDialogue;

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
				if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
					LoadSpecialEventProxy();
				} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
					MoveBackCurrentPlayer();
				}
			} else {
				MoveForwardCurrentPlayer();
			}
		} else if (SpecialGameEvents.innInteraction){
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
				RestAtTheInn();
			} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
				NoVacancy();
			}
		} else if (SpecialGameEvents.hospitalInteraction){
			if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Return)) {
				UseHospital();
			} else if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.Backspace)) {
				RefuseHospital();
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

	void ShowConfirmStayNightBtn(){
		GameObject spendNightBtn = GameObject.FindGameObjectWithTag("spendNight");
		if(spendNightBtn){
			for (int i = 0; i < spendNightBtn.transform.childCount; i++){
				spendNightBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void ShowConfirmHealthCareBtn(){
		GameObject healthCareBtn = GameObject.FindGameObjectWithTag("healthCare");
		if(healthCareBtn){
			for (int i = 0; i < healthCareBtn.transform.childCount; i++){
				healthCareBtn.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	void HideConfirmSpaceBtn(){
		GameObject confirmSpaceBtn = GameObject.FindGameObjectWithTag("confirmSpace");
		GameObject spendNightBtn = GameObject.FindGameObjectWithTag("spendNight");
		GameObject healthCareBtn = GameObject.FindGameObjectWithTag("healthCare");
		if(confirmSpaceBtn){
			for (int i = 0; i < confirmSpaceBtn.transform.childCount; i++){
				confirmSpaceBtn.transform.GetChild(i).gameObject.SetActive(false);
				spendNightBtn.transform.GetChild(i).gameObject.SetActive(false);
				healthCareBtn.transform.GetChild(i).gameObject.SetActive(false);
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

			//Pelea:0,Tienda:1,Cofre:2,Hospital:3,Inn:4,Fábrica:5
			string character = gameManagerDelJuego.idCharacter[currentPlayer];
			GameObject currentCharacter = GameObject.Find(character);
			if (currentCharacter){
				Saviour saviour = currentCharacter.GetComponent<Saviour>();
				
				gameManagerDelJuego.currentSaviour = saviour;
				if (saviour) {
					typeOfSpace = NodesMap.nodesArray[saviour.currentNode, 4];
					
					// This is ok, and tested. Do not move or change.
					PlayerUber player = gameManagerDelJuego.GetPlayerUber();

					switch (typeOfSpace) {
						case 0:
							//StartCoroutine(LoadNextPlayer());
							
							ResetPila(saviour);
							ActivateCharacters(false);
							LoadFightScene(saviour, player);
							break;
						case 1:
							ResetPila(saviour);
							ActivateCharacters(false);
							LoadStoreScene(saviour, player);
							break;
						case 2:
							ResetPila(saviour);
							GiveMeSomeMoney(saviour, player);
							break;
						case 3:
							GiveMeHealthCare(saviour, player);
							break;
						case 4:
							GiveMeSomeRest(saviour, player);
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

	public void LoadFightScene(Saviour saviour, PlayerUber player){
		int cp = PlayerUber.normalizeCurrentPlayer(currentPlayer);

		// Guarda el número de jugador
		StatusMaker sm = new StatusMaker(cp, 1, 0);
		sm.MakeAndPostJSONFight();
		
		sm.SetPlayer(cp, player);
		gameManagerDelJuego.currentSaviour = saviour;

		Dice.finishedBouncing = false;

		bool firstTimeTutorial = gameManagerDelJuego.firstTimeTutorial[gameManagerDelJuego.GetCurrentPlayer()-1];
		if (firstTimeTutorial){
			// Este jugador ya vio por primera vez el tutorial de la pelea
			gameManagerDelJuego.firstTimeTutorial[gameManagerDelJuego.GetCurrentPlayer()-1] = false;
			gameManagerDelJuego.NombreNivelQueSeVaCargar = "Tutorial";
        	SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		} else{
			gameManagerDelJuego.NombreNivelQueSeVaCargar = "FightScene";
        	SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		}
        
    }
	public static IEnumerator LoadNextPlayer(){
		// En el caso de que regrese de la pelea o de la tienda,
		// vuelve a prender a los objetos 
		SelectNode.ActivateCharacters(true);

		gameManagerDelJuego.nextPlayer();
		currentPlayer = gameManagerDelJuego.GetCurrentPlayer();

		string character = gameManagerDelJuego.idCharacter[currentPlayer];
		string characterArm = character.ToLower()+"Arm";

		LookAt.LookAtNextCharacter(character, characterArm);

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
		yield return null;
		yield return null;

		GameObject currentCharacter = GameObject.Find(character);
		Debug.Log(character);
		if (currentCharacter){
			Saviour saviour = currentCharacter.GetComponent<Saviour>();
			if (saviour) {
				// int area = NodesMap.nodesArea["Node "+saviour.currentNode];
				// Debug.Log(NodesMap.areaName[area]);
				Debug.Log("Node "+saviour.currentNode);
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

	public void MoveForwardCurrentPlayer(){
		PlayerUber player = gameManagerDelJuego.GetPlayerUber();
		Saviour saviour = gameManagerDelJuego.GetCurrentSaviour();
		Debug.Log("Turns to skip :"+ player.turnsToSkip);
		Debug.Log("CP: "+currentPlayer);
		if (player.turnsToSkip == 0){
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
		// Quiere decir que este jugador debe pasar un turno, pues perdió en batalla
		else{
			NotYetYourTurn(player);
		}
	}

	public void NotYetYourTurn(PlayerUber player){	

		GameObject stayTurn = GameObject.FindGameObjectWithTag("stayTurn");
		if (stayTurn){
			
			GameObject timeline = stayTurn.transform.GetChild(2).gameObject;
			GameObject dialogText = stayTurn.transform.GetChild(1).gameObject;
			dialogText.GetComponent<Text>().text = 
				gameManagerDelJuego.orderedPlayers[0] + " is still recovering from that horrid fight.";
			timeline.SetActive(true);

			player.turnsToSkip --;
			StatusMaker sm = new StatusMaker();
			sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
			
			HideSpacesLeftBtn();
			StartCoroutine(LoadNextPlayer());
		}
	}

	public void LoadStoreScene(Saviour saviour, PlayerUber player){

		// currentplayer, area, typeOfFight = 3 (STORE)
		StatusMaker sm = new StatusMaker(currentPlayer, 1, 3);
		sm.MakeAndPostJSONFight();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
		gameManagerDelJuego.currentSaviour = saviour;

		Dice.finishedBouncing = false;
		
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
				string[] text = specialGameEvents.ChestInteraction(1, player, saviour, currentPlayer);
				Debug.Log("Dinero: "+player.money);
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

			coins.SetActive(false);
			goldBox.SetActive(false);
			textGold.SetActive(false);
			StartCoroutine(LoadNextPlayer());
		}
		
	}
	
	public void GiveMeHealthCare(Saviour saviour, PlayerUber player){
		Dice.finishedBouncing = false;
		SpecialGameEvents specialGameEvents = new SpecialGameEvents();
		string[] text = specialGameEvents.HospitalInteraction(false);
		hospitalDialogue = text;
		
		GameObject healthCare = GameObject.FindGameObjectWithTag("healthCare");
		if (healthCare){
			for (int i=0; i<healthCare.transform.childCount; i++){
				healthCare.transform.GetChild(i).gameObject.SetActive(true);
			}
			GameObject textInn = healthCare.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = hospitalDialogue[0];
		}	
	}

	public void GiveMeSomeRest(Saviour saviour, PlayerUber player){
		Dice.finishedBouncing = false;
		int area = NodesMap.nodesArea["Node " + saviour.currentNode];

		SpecialGameEvents specialGameEvents = new SpecialGameEvents();
		string[] text = specialGameEvents.InnInteraction(area);
		innDialogue = text;

		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			for (int i=0; i<spendNight.transform.childCount; i++){
				spendNight.transform.GetChild(i).gameObject.SetActive(true);
			}
			GameObject textInn = spendNight.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = innDialogue[0] + "\n" + innDialogue[1] + innDialogue[2];
		}
	}

	private static void ResetPila(Saviour saviour){
		saviour.pila.Clear();
		saviour.oldNode = saviour.currentNode;
		// Hacer el setup inicial para que no dé la condición de oldNode != currentNode
	}

	public static void ActivateCharacters(bool show){
		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			// - 1 porque tenemos al ShopKeeper.
			for (int i = 0; i < characters.transform.childCount - 1; i++){
				GameObject spawnedCharacter = characters.transform.GetChild(i).gameObject;
				if(gameManagerDelJuego.playerDictionary.ContainsValue(i+1)){
					spawnedCharacter.SetActive(show);
				}
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

	public void UseHospital(){
		HideConfirmHospitalBtn();
		StartCoroutine(DisplayHospitalMsg(hospitalDialogue[1], true));
	}

	public void RefuseHospital(){
		HideConfirmHospitalBtn();
		StartCoroutine(DisplayHospitalMsg(hospitalDialogue[2], false));
	}

	/// <summary>
	/// Displays the corresponding message depending on the player choice
	/// of spending the night in the Inn or going one space back.
	/// /<sumamary> 
	public IEnumerator DisplayHospitalMsg(string message, bool stay){
		Saviour saviour = gameManagerDelJuego.GetCurrentSaviour();
		PlayerUber player = gameManagerDelJuego.GetPlayerUber();

		if (saviour.gameObject != this.gameObject){
			yield break;
		}
		yield return null;

		// En este punto ya se sabe que hay solo una instancia de SelectNode activa
		// Las demás instancias ya se "salieron" debido al yield break;

		GameObject useHospital = GameObject.FindGameObjectWithTag("healthCare");
		if (useHospital){
			GameObject textInn = useHospital.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = message;
		}

		float counter = 0;
		float waitTime = 1.50f;

		while (counter < (waitTime)){
			counter += Time.deltaTime;
			yield return null;
		}

		if (useHospital){
			useHospital.transform.GetChild(0).gameObject.SetActive(false);
			useHospital.transform.GetChild(1).gameObject.SetActive(false);
		}

		SpecialGameEvents.hospitalInteraction = false;

		if (stay){
	        SpecialGameEvents.RecoverHealth(player, saviour, currentPlayer);
			Debug.Log(player.hp);
			Debug.Log(saviour.hp);
			player.turnsToSkip = 2;
			saviour.turnsToSkip = 2;
			// Se guardan los cambios en el JSON para que puedan ser cargados
			// Siempre que se utilice el gameManagerDelJuego.GetPlayerUber();
			StatusMaker sm = new StatusMaker();
			sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
		}
		ResetPila(gameManagerDelJuego.currentSaviour);
		StartCoroutine(LoadNextPlayer());
	}

	public void RestAtTheInn(){
		HideConfirmNightBtn();
		StartCoroutine(DisplayInnMsg(innDialogue[3], true));
	}

	public void NoVacancy(){
		HideConfirmNightBtn();
		StartCoroutine(DisplayInnMsg(innDialogue[4], false));
	}

	/// <summary>
	/// Checks if the current saviour has enough money for staying in the Inn
	/// If he does, then his wealth is decreased by _nightCost_
	/// If he doesn't, then he shall return one space.
	/// /<sumamary> 
	public bool HasEnoughMoney(int nightCost){
		return gameManagerDelJuego.GetCurrentSaviour().money>=nightCost;
	}

	/// <summary>
	/// Displays the corresponding message depending on the player choice
	/// of spending the night in the Inn or going one space back.
	/// /<sumamary> 
	public IEnumerator DisplayInnMsg(string message, bool stay){
		Saviour saviour = gameManagerDelJuego.GetCurrentSaviour();
		PlayerUber player = gameManagerDelJuego.GetPlayerUber();

		if (saviour.gameObject != this.gameObject){
			yield break;
		}
		yield return null;

		// En este punto ya se sabe que hay solo una instancia de SelectNode activa
		// Las demás instancias ya se "salieron" debido al yield break;

		if (stay && !HasEnoughMoney(SpecialGameEvents.nightCost)){
			message = innDialogue[5];
			stay = false;
		}

		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			GameObject textInn = spendNight.transform.GetChild(1).gameObject;
			textInn.GetComponent<Text>().text = message;
		}

		float counter = 0;
		float waitTime = 1.50f;

		while (counter < (waitTime)){
			counter += Time.deltaTime;
			yield return null;
		}

		if (spendNight){
			spendNight.transform.GetChild(0).gameObject.SetActive(false);
			spendNight.transform.GetChild(1).gameObject.SetActive(false);
		}

		SpecialGameEvents.innInteraction = false;

		if (stay){
			saviour.money -= SpecialGameEvents.nightCost;
			player.money -= SpecialGameEvents.nightCost;
	        SpecialGameEvents.RecoverHealth(player, saviour, currentPlayer);
			ResetPila(gameManagerDelJuego.currentSaviour);
			StartCoroutine(LoadNextPlayer());
			// Implementar el pasar turno
		}
		else{
			Dice.finishedBouncing = true;
			MoveBackCurrentPlayer();
		}
	}

	public void HideConfirmNightBtn(){
		GameObject spendNight = GameObject.FindGameObjectWithTag("spendNight");
		if (spendNight){
			spendNight.transform.GetChild(2).gameObject.SetActive(false);
			spendNight.transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	public void HideConfirmHospitalBtn(){
		GameObject healthCare = GameObject.FindGameObjectWithTag("healthCare");
		if (healthCare){
			healthCare.transform.GetChild(2).gameObject.SetActive(false);
			healthCare.transform.GetChild(3).gameObject.SetActive(false);
		}
	}

	void OnEnable(){
		ShowConfirmSpaceBtn();
		ShowConfirmStayNightBtn();
		ShowConfirmHealthCareBtn();
		
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

		GameObject stayNightkBtn = GameObject.FindGameObjectWithTag("stayNight");
		if(stayNightkBtn){
			Button btn = stayNightkBtn.GetComponent<Button>();
			btn.onClick.AddListener(RestAtTheInn);
		}

		GameObject refuseNightBtn = GameObject.FindGameObjectWithTag("refuseNight");
		if(refuseNightBtn){
			Button btn = refuseNightBtn.GetComponent<Button>();
			btn.onClick.AddListener(NoVacancy);
		}

		GameObject useHospitalBtn = GameObject.FindGameObjectWithTag("useHospital");
		if(useHospitalBtn){
			Button btn = useHospitalBtn.GetComponent<Button>();
			btn.onClick.AddListener(UseHospital);
		}

		GameObject refuseHospitalBtn = GameObject.FindGameObjectWithTag("refuseHospital");
		if(refuseHospitalBtn){
			Button btn = refuseHospitalBtn.GetComponent<Button>();
			btn.onClick.AddListener(RefuseHospital);
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
