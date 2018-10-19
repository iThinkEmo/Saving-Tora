//Author: Karlo
//For documentation please refer to itemsForHumans.txt
using System;
using System.Collections;
using System.Collections.Generic;

//This class holds the items descriptions and perks
//Overall structure for perks is (HP,MP,AP,DP,SP,number)
//Overall structure for nameandDesc is (Name, Description)
public class Items
{
    public int[,] perks;
    public string[,] nameAndDesc;

    public Items()
    {
        perks = new int[,] { { -10, 0, 0, 0, 0, 0 }, { 10, 0, 0, 0, 0, 1 }, { 30, 0, 0, 0, 0, 2 }, { 70, 0, 0, 0, 0, 3 }, { 100, 0, 0, 0, 0, 4 }, { 150, 0, 0, 0, 0, 5 }, { 0, 5, 0, 0, 0, 6 }, { 0, 10, 0, 0, 0, 7 }, { 0, 15, 0, 0, 0, 8 }, { 0, 25, 0, 0, 0, 9 }, { 0, 30, 0, 0, 0, 10 }, { 0, 0, 5, 0, 0, 11 }, { 0, 0, 10, 0, 0, 12 }, { 0, 0, 15, 0, 0, 13 }, { 0, 0, 25, 0, 0, 14 }, { 0, 0, 30, 0, 0, 15 }, { 2, 0, 0, 5, 0, 16 }, { 4, 0, 0, 10, 0, 17 }, { 10, 0, 0, 15, 0, 18 }, { 20, 0, 0, 25, 0, 19 }, { 30, 0, 0, 30, 0, 20 }, { 0, 0, 0, 0, 5, 21 }, { 0, 0, 0, 0, 10, 22 }, { 0, 0, 0, 0, 15, 23 }, { 0, 0, 0, 0, 25, 24 }, { 0, 0, 0, 0, 30, 25 }, { 0, 0, 15, 20, 20, 26 }, { 0, 0, 0, 0, 0, 27 }, { 20, 15, 30, 0, 0, 28 }, { -100, 50, 50, 30, 0, 29 } };
        nameAndDesc = new string[,] { { "Trash", "Some trash found on the floor." }, { "Food", "Some food. Looks stale." }, { "Great Food", "Some good looking food." }, { "Delicious Food", "Finally something delicious." }, { "A Delicacy", "Looks expensive and really tasty." }, { "Pizza ", "Greatest food ever!" }, { "Almost-Empty Machine Core", "Was filled with magic... Not so much now." }, { "Half Full Machine Core", "This one is half full. Or half empty..." }, { "Almost-Full Machine Core", "Nearly filled... You can smell the magic." }, { "Full Machine Core ", "Fully loaded and ready to be consumed." }, { "Big Machine Core", "So big you can already feel the magic." }, { "A Rock", "It's a rock." }, { "Two Rocks ", "What's better than one rock?" }, { "The Same Rock. But With A Hat", "So cute." }, { "Two Rocks With Hats", "OMG look at their little hats!" }, { "Two Really Elegant Rocks", "They look so handsome... And cute!" }, { "A bit Of Defence Stew", "It's like a spoonful..." }, { "Some Defence Stew", "Meh. At least it's not a spoonful." }, { "A Bowl Defence Stew", "Some decent sized stew." }, { "A Pot Of Defence Stew", "A bit too much but ok. It looks yummy." }, { "Defence Stew Supreme", "You are not gonna be able to eat all that." }, { "OK Shoelaces", "OK." }, { "Nice Shoelaces", "Nice." }, { "Great Shoelaces", "Great." }, { "Shoelaces That Look Like Snakes", "They should be called pythons." }, { "Shoelaces With Flames Painted On ", "Because anything with flames on is faster." }, { "Astral Food", "Some out of this world food." }, { "Abstract Food", "This looks like a melted clock..." }, { "Cubist Food", "So blue. Why is it so blue? " }, { "The Ear Af An Artist", "I wouldnt eat this if I were you." } };
    }


    //Method to get 1 Items Data in the shape of an item
    public ItemRPG GetItemRPG(int itemNo)
    {
        return new ItemRPG(nameAndDesc[itemNo, 0], nameAndDesc[itemNo, 1], PerksArr(itemNo), itemNo, 1);
    }

    //Method to get N Items Data in the shape of an item
    public ItemRPG GetItemRPG(int itemNo, int quantityItem)
    {
        return new ItemRPG(nameAndDesc[itemNo, 0], nameAndDesc[itemNo, 1], PerksArr(itemNo), itemNo, quantityItem);
    }

    //Helper Method for Returning an array of Perks
    public int[] PerksArr(int eNo)
    {
        List<int> intList = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            intList.Add(perks[eNo, i]);
        }
        return intList.ToArray();
    }
}