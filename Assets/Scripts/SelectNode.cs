using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNode : MonoBehaviour {

	public RiceMonk r;
	public Magician w;
	public Samurai s;
	public Undead u;

	private GameManager gameManagerDelJuego;

	int currentPlayer = 0;
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
				Debug.Log("Te vas a quedar aqui o nel??");
				if (Input.GetKeyDown(KeyCode.Y)) {
					//Evento especial o next player o pelea
					//Pelea:0, Tienda:1,Cofre:2,Hospital:3,Fabrica:4,Inn:5
					spacesLeft = 3;
					//Se obtiene el siguiente jugador
					//Se debe agregar el el lugar correspondiente
					gameManagerDelJuego.nextPlayer();
					currentPlayer = gameManagerDelJuego.GetCurrentPlayer();

					switch (currentPlayer) {
						case 1:
							typeOfSpace = NodesMap.nodesArray[w.currentNode, 4];
							break;
						case 2:
							typeOfSpace = NodesMap.nodesArray[r.currentNode, 4];
							break;
						case 3:
							typeOfSpace = NodesMap.nodesArray[s.currentNode, 4];
							break;
						case 4:
							typeOfSpace = NodesMap.nodesArray[u.currentNode, 4];
							break;

					}
					Debug.Log(typeOfSpace);
					switch (typeOfSpace) {
						case 0:
							break;
						case 1:
							break;
						case 2:
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

				} else if (Input.GetKeyDown(KeyCode.N)) {
					//se obtiene el jugador para saber cuál mover
					currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
					switch (currentPlayer) {
						case 1:
							w.MoveBack();
							break;
						case 2:
							r.MoveBack();
							break;
						case 3:
							s.MoveBack();
							break;
						case 4:
							u.MoveBack();
							break;

					}
				}
			} else {
				//se obtiene el jugador para saber cuál mover
				currentPlayer = gameManagerDelJuego.GetCurrentPlayer();
				GameObject characters = GameObject.FindGameObjectWithTag("Characters");
				if (characters){
					Saviour script = characters.transform.GetChild(currentPlayer-1).GetComponent<Saviour>();
					GetMovement(script);
				}
			}
			
		}
	}

	private void GetMovement(Saviour saviour) {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (MoveSucceded(saviour, 0)) {
				saviour.Walk(saviour.currentNode, 0);
				Debug.Log(spacesLeft);
			}
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			if (MoveSucceded(saviour, 1)) {
				saviour.Walk(saviour.currentNode, 1);
				Debug.Log(spacesLeft);
			}
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			if (MoveSucceded(saviour, 2)) {
				saviour.Walk(saviour.currentNode, 2);
				Debug.Log(spacesLeft);
			}
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			if (MoveSucceded(saviour, 3)) {
				saviour.Walk(saviour.currentNode, 3);
				Debug.Log(spacesLeft);
			}
		}
	}

	private bool MoveSucceded(Saviour saviour, int i) {
		int current_node = saviour.currentNode;
		return NodesMap.nodesArray[current_node, i] != 0;
	}
}
