using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class storeMenuBtn : MonoBehaviour, IPointerClickHandler {
	public Text label;
	public int id;
	private GameObject bs;

	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log("Presionaron el boton " + label.text);
		bs = GameObject.Find("Store Script");
		BuyStuff info = bs.GetComponent<BuyStuff>();
		info.stuff = label.text;
		info.ShowInfo();
	}
}