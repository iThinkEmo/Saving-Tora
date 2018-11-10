using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;



public class BuyStuff : MonoBehaviour {

	public GameObject DialogText;
	public GameObject WealthText;

	private GameObject MenuText;
	private GameObject Buttons;
	private GameObject StuffBoxes;


	private GameObject Menu;

	private Store storeStuff;

	public string category;
	private int index = 0;
	public string stuff;

	private PlayerUber player;

	public static Saviour saviour;
	private GameManager gameManagerDelJuego;


	public GameObject button;
	public GameObject parent;
	private int count = 0;

	private StatusMaker st = new StatusMaker();
	// private StatusForFight sf;

	private int nivel;

	// Use this for initialization

	public BuyStuff() {

	}

	void Start () {
		gameManagerDelJuego = GameManager.Instance;
		Buttons = GameObject.Find("CatButtons");
		Menu = GameObject.Find("Menu");
		Menu.SetActive(false);

		// sf = st.FightGetter();
		
		int currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
		currentPlayer = PlayerUber.normalizeCurrentPlayer(currentPlayer);
		player = st.getPlayer(currentPlayer);
		saviour = gameManagerDelJuego.GetCurrentSaviour();

		WealthText.GetComponent<Text>().text = "wealth: " + player.money;
		DialogText.GetComponent<Text>().text = "Hi, I'm <Insert name>. What do you want to buy?";

		stuff = "";
		nivel = player.lv;
		storeStuff = new Store(nivel);
	}

	public void BtnMagic() {
		Buttons.SetActive(false);
		Menu.SetActive(true);

		count = storeStuff.magicNames.Length;
		for (int i = 0; i < count; i++) {
			GameObject magicBtn = Instantiate(button, parent.transform);
			if (magicBtn) {
				storeMenuBtn smb = magicBtn.GetComponent<storeMenuBtn>();
				if (smb) {
					smb.id = i;
					smb.label.text = storeStuff.magicNames[i]; 
				}
			}
		}
		
		DialogText.GetComponent<Text>().text = "Which type of magic do you want to buy?";
		category ="Magic";

	}
	public void BtnItems() {
		Buttons.SetActive(false);
		Menu.SetActive(true);

		count = storeStuff.itemNames.Length;
		for (int i = 0; i < count; i++) {
			GameObject itemBtn = Instantiate(button, parent.transform);
			if (itemBtn) {
				storeMenuBtn smb = itemBtn.GetComponent<storeMenuBtn>();
				if (smb) {
					smb.id = i;
					smb.label.text = storeStuff.itemNames[i];
				}
			}
		}

		DialogText.GetComponent<Text>().text = "Which type of item do you want to buy?";
		category = "Item";

	}
	public void BtnEquipment() {
		Buttons.SetActive(false);
		Menu.SetActive(true);

		count = storeStuff.equipmentNames.Length;
		for (int i = 0; i < count; i++) {
			GameObject eqBtn = Instantiate(button, parent.transform);
			if (eqBtn) {
				storeMenuBtn smb = eqBtn.GetComponent<storeMenuBtn>();
				if (smb) {
					smb.id = i;
					smb.label.text = storeStuff.equipmentNames[i];
				}
			}
		}

		DialogText.GetComponent<Text>().text = "Which type of equipment do you want to buy?";
		category = "Equipment";
	}

	public void ShowInfo() {

		switch (category) {
			case "Magic":
				index = storeStuff.GetMagiaIndex(stuff);
				if (index != -1) {
					DialogText.GetComponent<Text>().text = storeStuff.magicDescr[index] + ". It cost " + storeStuff.magicCosts[index] + "\nDo you want to buy it?";
				} else {
					DialogText.GetComponent<Text>().text = "Item not found";
				}
				break;
			case "Item":
				index = storeStuff.GetItemIndex(stuff);
				if (index != -1) {
					DialogText.GetComponent<Text>().text = storeStuff.itemDescription[index]+". It cost "+storeStuff.itemCosts[index]+"\nDo you want to buy it?";
				} else {
					DialogText.GetComponent<Text>().text = "Item not found";
				}
				break;
			case "Equipment":
				index = storeStuff.GetEquipIndex(stuff);
				if (index != -1) {
					DialogText.GetComponent<Text>().text = storeStuff.equipmentNames[index] + ". It cost " + storeStuff.equipmentCosts[index] + "\nDo you want to buy it?"; 
				} else {
					DialogText.GetComponent<Text>().text = "Item not found";
				}
				break;
		}
		
			
	}

	public void BtnBuy() {
		switch (category) {
			case "Magic":
				string[] data = storeStuff.BuyMagia(player.money, stuff);
				//(status, newWealth, magiaName, textOfStatus)
				//Method for returning 1 if users wealth is enough for article selected, 0 if not.
				if (System.Convert.ToInt32(data[0]) > 0) {
					DialogText.GetComponent<Text>().text = data[3]+ "\nDo yo want something else?";
					player.ChangeMagic(data[2]);
					player.RecalculateWealth(System.Convert.ToInt32(data[1]));
				} else {
					DialogText.GetComponent<Text>().text = data[3];
				}
				break;
			case "Item":
				string[] data2 = storeStuff.BuyItem(player.money, storeStuff.GetItemIndex(stuff));
				//(status, newWealth, itemno,  textOfStatus)
				if (System.Convert.ToInt32(data2[0]) > 0) {
					DialogText.GetComponent<Text>().text = data2[3] + "\nDo yo want something else?";
					player.NewItem(System.Convert.ToInt32(data2[2]), 1);
					player.RecalculateWealth(System.Convert.ToInt32(data2[1]));
				} else {
					DialogText.GetComponent<Text>().text = data2[3];
				}
				break;
			case "Equipment":
				string[] data3 = storeStuff.BuyEquipment(player.money, stuff);
				//(status, newWealth, area, level,  textOfStatus)
				//Method for returning 1 if users wealth is enough for article selected, 0 if not.
				if (System.Convert.ToInt32(data3[0]) > 0) {
					DialogText.GetComponent<Text>().text = data3[3] + "\nDo yo want something else?";
					player.Equip(System.Convert.ToInt32(data3[2]), System.Convert.ToInt32(data3[3]));
					player.money = System.Convert.ToInt32(data3[1]);
				} else {
					DialogText.GetComponent<Text>().text = data3[3];
				}
				break;
		}
		Buttons.SetActive(true);
		Menu.SetActive(false);
		deleteChildrens();
		WealthText.GetComponent<Text>().text = "wealth: " + player.money;
	}


	public void BtnBack() {
		Buttons.SetActive(true);
		Menu.SetActive(false);
		deleteChildrens();
	}

	public void BtnQuit() {

		int currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
		currentPlayer = PlayerUber.normalizeCurrentPlayer(currentPlayer);
		st.SetPlayer(currentPlayer, player);
		
		player = st.getPlayer(currentPlayer);

		SpawnInit.SetPlayerAttributes(player, saviour);


		
		gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
		SceneManager.LoadScene("PantallaCargandoLoadingScreen");
	}

	private void deleteChildrens() {
		foreach (Transform child in parent.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	}
