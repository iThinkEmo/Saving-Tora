using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using System.IO;


public class Walk : MonoBehaviour {
	public Camera cam;
	public Animator animator;
	public NavMeshAgent _agent;

    public LayerMask mask;
	private int nodoActual=1;
	private int nodoViejo=1;
	private int counter = 0;
	

	//string path = "Assets/nodes.txt";

	private void Update(){
		//counter = dado.side;
		//Debug.Log("Counter:" + counter);
		if (Input.GetKey(KeyCode.UpArrow))	{
			if (NodesMap.nodesArray[nodoActual - 1, 0] != 0){
				nodoActual = NodesMap.nodesArray[nodoActual - 1, 0];
				_agent.SetDestination(NodesMap.nodesPosition[nodoActual-1]);
				if (nodoActual == nodoViejo) {
					counter++;
				} else {
					counter--;
				}
				nodoViejo = NodesMap.nodesArray[nodoActual - 1, 1];
				//Debug.Log("Counter:" + counter);
			}
		}
		else if (Input.GetKey(KeyCode.DownArrow)){
			if (NodesMap.nodesArray[nodoActual - 1, 1] != 0) {
				nodoActual = NodesMap.nodesArray[nodoActual - 1, 1];
				_agent.SetDestination(NodesMap.nodesPosition[nodoActual - 1]);
				if (nodoActual == nodoViejo) {
					counter++;
				} else {
					counter--;
				}
				nodoViejo = NodesMap.nodesArray[nodoActual - 1, 1];
				//Debug.Log("Counter:" + counter);
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow))	{
			if (NodesMap.nodesArray[nodoActual - 1, 2] != 0) {
				nodoActual = NodesMap.nodesArray[nodoActual - 1, 2];
				_agent.SetDestination(NodesMap.nodesPosition[nodoActual - 1]);
				if (nodoActual == nodoViejo) {
					counter++;
				} else {
					counter--;
				}
				nodoViejo = NodesMap.nodesArray[nodoActual - 1, 1];
				//Debug.Log("Counter:" + counter);
			}
		}
		else if (Input.GetKey(KeyCode.LeftArrow)){
			if (NodesMap.nodesArray[nodoActual - 1, 3] != 0) {
				nodoActual = NodesMap.nodesArray[nodoActual - 1, 3];
				_agent.SetDestination(NodesMap.nodesPosition[nodoActual - 1]);
				if (nodoActual == nodoViejo) {
					counter++;
				} else {
					counter--;
				}
				nodoViejo = NodesMap.nodesArray[nodoActual - 1, 1];
				//Debug.Log("Counter:" + counter);
			}
		}
		else {
			
			
		}

		animator.SetFloat("Speed", _agent.velocity.magnitude);	
	}
}
