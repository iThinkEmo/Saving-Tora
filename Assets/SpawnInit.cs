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
			for (int i = 0; i < characters.transform.childCount; i++){
				if(gameManagerDelJuego.playerDictionary.ContainsValue(i+1)){
					GameObject spawnedCharacter = characters.transform.GetChild(i).gameObject;
					spawnedCharacter.SetActive(true);
					Vector3 chPosition = spawnedCharacter.transform.position;
					Vector3 chRotation = spawnedCharacter.transform.eulerAngles;
					StatusMaker.setInitialScreenjson(chPosition, chRotation, i+1);
					string name = spawnedCharacter.name;
					Debug.Log(name);

					PlayerUber player;

					switch (name){
						case "Witch":
							player = gameManagerDelJuego.witch;
							break;
						case "RiceMan":
							player = gameManagerDelJuego.ricemonk;
							break;
						case "Samurai":
							player = gameManagerDelJuego.samurai;
							break;
						case "Undead":
							player = gameManagerDelJuego.undead;
							break;
						default:
							player = gameManagerDelJuego.witch;
							break;
					}

					Saviour script = characters.transform.GetChild(i).GetComponent<Saviour>();
					SetPlayerAttributes(player, script);
				}
			}
		}

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
		GameObject nodesType = GameObject.FindGameObjectWithTag("nodesType");
		if (nodesType){
			nodesType.transform.GetChild(0).gameObject.SetActive(true);
		}

		gameManagerDelJuego.firstLoadMainScene = false;
	}

	public static void SetPlayerAttributes(PlayerUber player, Saviour script){
		Debug.Log(player);
		Debug.Log(script);
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
