using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnInit : MonoBehaviour {

	private GameManager gameManagerDelJuego;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;
        GameObject shopkeeper = GameObject.Find("Shopkeeper");
		if (shopkeeper){
			shopkeeper.SetActive(false);
		}

		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			// - 1 porque tenemos al ShopKeeper.
			for (int i = 0; i < characters.transform.childCount - 1; i++){
				GameObject spawnedCharacter = characters.transform.GetChild(i).gameObject;
				Vector3 chPosition = spawnedCharacter.transform.position;
				Vector3 chRotation = spawnedCharacter.transform.eulerAngles;
				StatusMaker.setInitialScreenjson(chPosition, chRotation, i);
				
				if(gameManagerDelJuego.playerDictionary.ContainsValue(i+1)){
					spawnedCharacter.SetActive(true);
					PlayerUber player;
					StatusMaker statusMaker = new StatusMaker();
					string name = spawnedCharacter.name;
					switch (name){
						case "Witch":
							player = gameManagerDelJuego.witch;
							statusMaker.SetPlayer(1, player);
							break;
						case "RiceMan":
							player = gameManagerDelJuego.ricemonk;
							statusMaker.SetPlayer(4, player);
							break;
						case "Samurai":
							player = gameManagerDelJuego.samurai;
							statusMaker.SetPlayer(2, player);
							break;
						case "Undead":
							player = gameManagerDelJuego.undead;
							statusMaker.SetPlayer(3, player);
							break;
						default:
							player = gameManagerDelJuego.witch;
							statusMaker.SetPlayer(1, player);
							break;
					}

					Saviour script = characters.transform.GetChild(i).GetComponent<Saviour>();
					SetPlayerAttributes(player, script);
				}
			}
		}

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
		gameManagerDelJuego.firstLoadMainScene = false;
	}

	public static void SetPlayerAttributes(PlayerUber player, Saviour script){
		if (player != null) {
			script.maxhp = player.maxhp;
			script.hp = player.hp;
			script.mp = player.mp;
			script.ap = player.ap;
			script.dp = player.dp;
			script.sp = player.sp;
			script.lv = player.lv;
			script.exp = player.exp;
			script.maxExp = player.maxExp;
			script.money = player.money;
			script.turnsToSkip = player.turnsToSkip;
			script.onDuty = player.onDuty;
			script.fans = player.fans;
			script.gender = player.gender;
			script.items = player.items;
			script.playerEquipment = player.playerEquipment;
			script.magica = player.magica;
			script.perkArray = player.perkArray;
			script.level = player.level;
		}
	}
}
