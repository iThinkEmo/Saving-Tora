using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Saviour : MonoBehaviour {

    #region stats

    //maximum health points
    public int maxhp;
    //current health points
    public int hp;
    //magic points
    public int mp;
    //attack points
    public int ap;
    //defense points
    public int dp;
    //speed points
    public int sp;
    //level 
    public int lv;
    //current experience points  
    public int exp;
    //level maximum exp
    public int maxExp;
    //money
    public int money = 0;
    //fans
    public int fans = 0;
    //Gender
    public int gender;
    //Items, HashTable de la manera:  (LLave, Valor) = (itemNumber, Instancias de ese item)
    public PlayerItems items;
    //Equipment: Para referencia vease la clase PlayerEquipment
    public PlayerEquipment playerEquipment = new PlayerEquipment();
    //Magia con la que empieza el jugador
    public Magica magica;
    //An array that has the perks to be used on the fight(HP,MP,AP,DP,SP)
    public int[] perkArray = { 0, 0, 0, 0, 0 };
    //LEVEL es el objeto que se encarga de calcular los subidones en el subido de nivel. 
    public LV level;

    #endregion

    #region level

    public void LevelUp()
    {
        ApplyLevelUp(this.level.LevelUP(this.lv));
        this.exp = this.exp - this.maxExp;
        this.maxExp = level.MaxExp(this.maxExp);
        this.lv++;
    }

    public void ApplyLevelUp(int[] arr)
    {
        this.hp += arr[0] * 5;
        this.mp += arr[1];
        this.ap += arr[2];
        this.dp += arr[3];
        this.sp += arr[4];
    }

    #endregion

    #region Health

    //moreLess; 0 less, an attack. 1 more, an Item or heal
    public void RecalculateHealth(int moreLess, int hpChange)
    {
        if (moreLess == 0)
        {
            this.hp -= hpChange;
        }
        else
        {
            this.hp += hpChange;
        }

    }

    #endregion

    #region Magic
    //To change magic
    //0:"Basic Magic",1:"Fire",2:"Blizzard",3:"Shock",4:"Natura"
    //0:"",1:"",2:"Big ",3:"Death ",4:"God "
    public void ChangeMagic(String magic)
    {
        int mlevel, mtype;
        if (magic.Contains("Fire"))
        {
            mtype = 1;
        }
        else if (magic.Contains("Blizzard"))
        {
            mtype = 2;
        }
        else if (magic.Contains("Shock"))
        {
            mtype = 3;
        }
        else
        {
            mtype = 4;
        }

        if (magic.Contains("Big"))
        {
            mlevel = 2;
        }
        else if (magic.Contains("Death"))
        {
            mlevel = 3;
        }
        else if (magic.Contains("God"))
        {
            mlevel = 4;
        }
        else
        {
            mlevel = 1;
        }
        magica.UpdateMagic(mlevel, mtype);
    }

    #endregion

    #region Items

    //Method to use item A.K.A. remove item from bag
    // ONLY FOR HP, for other perks please use perks.
    public void UseItem(int itemNo)
    {
        RecalculateHealth(1, items.GetItem(itemNo).perks[0]);
        items.UseItem(itemNo);
    }

    //Method to add items to the bag.
    public void NewItem(int itemNo, int nummer)
    {
        items.NewItem(itemNo, nummer);
    }

    //To list my Items on the Inventory
    public ItemRPG[] GetItems()
    {
        return items.GetMyItems();
    }

    #endregion

    #region Equipment

    //Method to equip equipment 
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public void Equip(int part, int equip)
    {
        playerEquipment.EquipEquipment(part, equip);
        ApplyEquipment(part, equip, 0);
    }

    //Method to unequip equipment 
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public void UnEquip(int part, int equip)
    {
        playerEquipment.UnEquipEquipment(part);
        ApplyEquipment(part, equip, 1);
    }

    //Unequip 1 true, 0 false
    public void ApplyEquipment(int part, int equip, int unequip)
    {
        //Overall order is (HP,MP,AP,DP,SP,NUMBER)
        int[] tempPerks = playerEquipment.GetEquipmentPerks(part, equip);
        if (unequip == 0)
        {
            this.hp += tempPerks[0];
            this.mp += tempPerks[1];
            this.ap += tempPerks[2];
            this.dp += tempPerks[3];
            this.sp += tempPerks[4];
        }
        else
        {
            this.hp -= tempPerks[0];
            this.mp -= tempPerks[1];
            this.ap -= tempPerks[2];
            this.dp -= tempPerks[3];
            this.sp -= tempPerks[4];
        }

    }

    #endregion

    #region Perk

    //0- Begin, 1- End
    public void ApplyPerk(int beginEnd)
    {
        //(HP,MP,AP,DP,SP,number)
        //(HP,MP,AP,DP,SP)
        if (beginEnd == 0)
        {
            this.hp += perkArray[0];
            this.mp += perkArray[1];
            this.ap += perkArray[2];
            this.dp += perkArray[3];
            this.sp += perkArray[4];
        }
        else
        {
            this.hp -= perkArray[0];
            this.mp -= perkArray[1];
            this.ap -= perkArray[2];
            this.dp -= perkArray[3];
            this.sp -= perkArray[4];
        }
    }

    public void UpdatePerk(int[] newPerkArr)
    {
        int[] tempPerk = new int[perkArray.Length];
        for (int i = 0; i < perkArray.Length; i++)
        {
            tempPerk[i] = perkArray[i];
        }
        for (int i = 0; i < perkArray.Length; i++)
        {
            perkArray[i] = newPerkArr[i];
        }
        ApplyPerk(0);
        for (int i = 0; i < perkArray.Length; i++)
        {
            perkArray[i] = newPerkArr[i] + tempPerk[i];
        }
    }

    public void ResetPerks()
    {
        ApplyPerk(1);
        for (int i = 0; i < this.perkArray.Length; i++)
        {
            this.perkArray[i] = 0;
        }
    }

    #endregion

    #region Wealth

    //Method to recalculate wealth based on a new wealth
    public void RecalculateWealth(int newWealth)
    {
        this.money = newWealth;
    }


    #endregion



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
			Move();
		} else{
		    currentNode=selectedNode;
            SelectNode.spacesLeft++;
            oldNode = pila[pila.Count - 2];
            pila.RemoveAt(pila.Count - 1);
			Move();
		}
        
		Move();
        
    }

    public void Move(){
        switch(selectedNode){
            case 1: case 2: case 3: case 4: {
                //agent.speed = 10;
                agent.speed = 10;
                break;
            }
            default: {
                //agent.speed = 80;
                agent.speed = 10;
                break;
            }
        }
        agent.SetDestination(NodesMap.nodesPosition[selectedNode]);
        SelectNode.UpdateSpacesLeft();
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

