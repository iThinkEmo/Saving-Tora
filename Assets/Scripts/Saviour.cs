using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saviour : MonoBehaviour {

    //health points
	public int hp;
	//magic points
	public int mp;
	//attack points
	public int ap;
	//defense points
	public int dp;
	//speed points
	public int sp;
	//money
	public int money = 0;
	//fans
	public int fans = 0;
	//Gender
	public string gender;
	//Items
	public ArrayList items = new ArrayList();

	public Camera cam;
	public Animator animator;
	public NavMeshAgent agent;
	public LayerMask mask;

    public int currentNode, oldNode, selectedNode;
    public List<int> pila;

    // Use this for initialization
    void Start() {
        currentNode = 0;
        oldNode = 0;
        selectedNode = 0;
        pila = new List<int>();
    }

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }


    public void Walk(int node, int direction) {
               
		selectedNode=NodesMap.nodesArray[node, direction];

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
        switch(selectedNode){
            case 1: case 2: case 3: case 4: {
                agent.speed = 10;
                break;
            }
            default: {
                agent.speed = 80;
                break;
            }
        }

        agent.SetDestination(NodesMap.nodesPosition[selectedNode]);
    }

    public void MoveBack(){
        selectedNode = oldNode;
        currentNode = selectedNode;
        oldNode = pila[pila.Count - 2];
        pila.RemoveAt(pila.Count - 1);
        SelectNode.spacesLeft++;
        Move();
    }
}

