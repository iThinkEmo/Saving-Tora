using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FigthBtn : MonoBehaviour, IPointerClickHandler {
	public Text label;
	public int id;

	private FightMg fMg; 

	public void OnPointerClick(PointerEventData eventData) {
		fMg = GameObject.Find("ButtonsFight").GetComponent<FightMg>();
		fMg.nameItem = label.text;
		fMg.showInfo();

	}
}