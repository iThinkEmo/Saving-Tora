using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomGen : MonoBehaviour {
	public GameObject TextBox;
	public int num;

	private void Start() {
		//TextBox.GetComponent<Text>().text = "Dado";
	}

	/*private void Update(){
		if (Input.GetKeyDown(KeyCode.D)) {
			num = Random.Range(1, 6);
			TextBox.GetComponent<Text>().text = "" + num;
		}
	}
	*/
}
