using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedBouncing : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Dice.finishedBouncing = true;
	}
}
