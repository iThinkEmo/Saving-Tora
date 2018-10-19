using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

//For documentation please refer to MagicForNonHumans.txt
public class Magica
{
    public string[] magicType = { "Basic Magic", "Fire", "Blizzard", "Shock", "Natura" };
    public string[] magicLevel = { "", "", "Big ", "Death ", "God " };
    public string[] descriptionA = { "It is almost like no magic", "It burns ", "It cools ", "It electrocutes ", "It calms " };
    public string[] descriptionB = { "...", "a little.", "a lot.", "so much that it might kill somebody.", "like a god." };
    public int[] magicStats = new int[] { 5, 20, 50, 70, 100 };

    string magic;
    int power;
    string description;

    //This constructor uses the arrays in this class to set the name and power of the selected magic
    public Magica(int mLevel, int mType)
    {
        UpdateMagic(mLevel, mType);
    }

    //For user
    public int GetPower()
    {
        return this.power;
    }

    public string GetName()
    {
        return this.magic;
    }

    public void UpdateMagic(int mLevel, int mType)
    {
        this.magic = magicLevel[mLevel] + magicType[mType];
        this.power = magicStats[mLevel];
        this.description = descriptionA[mLevel] + descriptionB[mLevel];
    }

    //ForStore

    //gets names
    public string[] GetMagics()
    {
        List<string> myAL = new List<string>();
        for (int i = 1; i < magicType.Length; i++)
        {
            for (int j = 1; j < magicType.Length; j++)
            {
                myAL.Add(magicLevel[j] + magicType[i]);
            }
        }
        return myAL.ToArray();
    }

    //gets descriptions
    public string[] GetMagicDescriptions()
    {
        List<string> myAL = new List<string>();
        for (int i = 1; i < descriptionA.Length; i++)
        {
            for (int j = 1; j < descriptionA.Length; j++)
            {
                myAL.Add(descriptionA[i] + descriptionB[j]);
            }
        }
        return myAL.ToArray();
    }

    //get powers
    public int[] GetMagicPowers()
    {
        List<int> myAL = new List<int>();
        for (int i = 1; i < descriptionA.Length; i++)
        {
            for (int j = 1; j < descriptionA.Length; j++)
            {
                myAL.Add(magicStats[j]);
            }
        }
        return myAL.ToArray();
    }


    //gets costs
    public int[] GetCosts()
    {
        List<int> myAL = new List<int>();
        for (int i = 1; i < descriptionA.Length; i++)
        {
            int counter = 100;
            for (int j = 1; j < descriptionA.Length; j++)
            {
                if (counter == 100)
                {
                    myAL.Add(counter);
                    counter *= 5;
                }
                else
                {
                    myAL.Add(counter);
                    counter /= 10;
                    counter *= 25;
                }
            }
        }
        return myAL.ToArray();
    }
}