//Author: Karlo
//For documentation please refer to itemsForHumans.txt
using System;
using System.Collections;

//This class uses the values on the Items class to swiftly create an item instance containing the name, description and perks of an item.
public class ItemRPG
{
    public string name;
    public string description;
    public int[] perks;
    public int itemNum;
    //Quantity of items
    public int itemQ;

    public ItemRPG(string na, string desc, int[] perkArr,int numberItem, int itemq)
    {
        this.name = na;
        this.description = desc;
        this.perks = perkArr;
        this.itemNum = numberItem;
        this.itemQ = itemq;
    }
}