using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNode : MonoBehaviour {

	public Saviour saviour;

	public static int spacesLeft;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Dice.finishedBouncing) {

			if (spacesLeft==0){
				Debug.Log("Te vas a quedar aqui o nel??");
				if (Input.GetKeyDown(KeyCode.Y)){
					//Evento especial o next player o pelea

				} else if (Input.GetKeyDown(KeyCode.N)){
					saviour.MoveBack();
				}
			}else{
				if (Input.GetKeyDown(KeyCode.UpArrow)){
					if (MoveSucceded(saviour, 0)){
						saviour.Walk(saviour.node, 0);
						Debug.Log(spacesLeft);
					}
				}
				else if (Input.GetKeyDown(KeyCode.DownArrow)){
					if (MoveSucceded(saviour, 1)){
						saviour.Walk(saviour.node, 1);
						Debug.Log(spacesLeft);
					}
				}
				else if (Input.GetKeyDown(KeyCode.RightArrow)){
					if (MoveSucceded(saviour, 2)){
						saviour.Walk(saviour.node, 2);
						Debug.Log(spacesLeft);
					}
				}
				else if (Input.GetKeyDown(KeyCode.LeftArrow)){
					if (MoveSucceded(saviour, 3)){
						saviour.Walk(saviour.node, 3);
						Debug.Log(spacesLeft);
					}
				}
			}
			
		}
	}

	private bool MoveSucceded(Saviour saviour, int i) {
		int current_node = saviour.node;
		return NodesMap.nodesArray[current_node, i] != 0;
	}
}
