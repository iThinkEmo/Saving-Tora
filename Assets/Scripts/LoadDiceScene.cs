using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDiceScene : MonoBehaviour {

	private GameManager gameManagerDelJuego;

	// Use this for initialization
	void Start () {
		gameManagerDelJuego = GameManager.Instance;

		GameObject characters = GameObject.FindGameObjectWithTag("Characters");
		if (characters){
			for (int i = 0; i < characters.transform.childCount; i++){

				if(gameManagerDelJuego.userNameCharacter.ContainsValue(i+1)){
					characters.transform.GetChild(i).gameObject.SetActive(true);

					string name = characters.transform.GetChild(i).gameObject.name;
					PlayerUber player;

					switch (name){
						case "Witch":
							player = gameManagerDelJuego.witch;
							break;
						case "Riceman":
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

		SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
