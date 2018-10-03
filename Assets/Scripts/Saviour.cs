using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saviour : MonoBehaviour {

    public int node, strength, wealth, food, tally, mk, sword, amulet, axe, torch, suit;

	public Camera cam;
	public Animator animator;
	public NavMeshAgent agent;
	public LayerMask mask;

    public int currentNode, oldNode, selectedNode;
    public List<int> pila;

    // Use this for initialization
    void Start() {
        strength = 60 + (int)(Random.Range(0, 100));
        wealth = 30 + (int)(Random.Range(0, 100));
        food = 0;
        tally = 0;
        mk = 0;
        sword = 0;
        amulet = 0;
        axe = 0;
        suit = 0;
		torch = 0;
        node = 0;
        currentNode = 0;
        oldNode = 0;
        selectedNode = 0;
        pila = new List<int>();
    }

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void ChangeNode(int newNode) {
        this.node = newNode;
    }

    public void Walk(int node, int direction) {
               
		selectedNode=NodesMap.nodesArray[node, direction];
        ChangeNode(selectedNode);

        if (selectedNode!=oldNode){
            oldNode=currentNode;
            currentNode=selectedNode;
            SelectNode.spacesLeft--;
            pila.Add(oldNode);
        }else{
		    currentNode=selectedNode;
            SelectNode.spacesLeft++;
            oldNode = pila[pila.Count - 2];
            pila.RemoveAt(pila.Count - 1);
        }
        
		Move();
        
    }

    public void Move(){
        agent.SetDestination(NodesMap.nodesPosition[this.node]);
    }

    public void MoveBack(){
        selectedNode = oldNode;
        ChangeNode(selectedNode);
        currentNode = selectedNode;
        oldNode = pila[pila.Count - 2];
        pila.RemoveAt(pila.Count - 1);
        SelectNode.spacesLeft++;
        Move();
    }
}
