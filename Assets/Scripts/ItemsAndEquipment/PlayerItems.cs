//Author: Karlo
//For documentation please refer to itemsForHumans.txt
using System;
using System.Collections;
using System.Collections.Generic;

//This class holds the players current Items, in integer format.
public class PlayerItems
{
    //Hashtable that holds player items
    //Format is (item, itemcuantity)
    public Dictionary<int, ItemRPG> myItems;
    public Items baseItems;

    //Cosntructor gives the player 5 small food Units.
    public PlayerItems()
    {
        baseItems = new Items();
        myItems = new Dictionary<int, ItemRPG>
        {
            { 1, GetItem(1, 5) }
        };
    }

    //Returns an item with all its values
    public ItemRPG GetItem(int itemNumber)
    {
        return baseItems.GetItemRPG(itemNumber);
    }

    //Returns an item with all its values and more than one instance.
    public ItemRPG GetItem(int itemNumber, int quantityI)
    {
        return baseItems.GetItemRPG(itemNumber, quantityI);
    }

    //Method to add an item to the bag.
    public void NewItem(int itemNo, int itemQua)
    {
        if (myItems.ContainsKey(itemNo))
        {
            myItems[itemNo].itemQ += itemQua;
        }
        else
        {
            myItems.Add(itemNo, GetItem(itemNo));
        }
    }

    //Method to get all personal Items
    public ItemRPG[] GetMyItems()
    {
        List<ItemRPG> myArr = new List<ItemRPG>();
        foreach (var item in myItems.Values)
        {
            myArr.Add(item);
        }
        return myArr.ToArray();
    }

    //Method to get all Items
    public ItemRPG[] GetItems()
    {
        List<ItemRPG> myArr = new List<ItemRPG>();
        //Console.WriteLine( baseItems.perks.GetLength(0));

        for (int i = 0; i < baseItems.perks.GetLength(0); i++)
        {
            //Console.WriteLine(i);
            myArr.Add(GetItem(i));
        }
        return myArr.ToArray();
    }

    //Method to use item A.K.A. remove item from bag
    public void UseItem(int itemNo)
    {
        if (myItems.ContainsKey(itemNo) && myItems[itemNo].itemQ > 1)
        {
            myItems[itemNo].itemQ -= 1;
        }
        else
        {
            myItems.Remove(itemNo);
        }
    }

}