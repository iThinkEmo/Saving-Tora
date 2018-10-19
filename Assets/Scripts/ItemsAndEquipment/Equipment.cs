using System;
using System.Collections.Generic;

//This class holds the arrays that show the equipments, and their details.
public class Equipment
{
    public string[] equipmentNames;
    public string[] weaponsNames;
    public int[,] headPerks;
    public int[,] handsPerks;
    public int[,] torsoPerks;
    public int[,] feetPerks;

    public Equipment()
    {
        //This array matches the equipment number with their respective name.
        //Reference to equipment details is available at equipmentsForHumans.txt
        equipmentNames = new string[] { "No", "Leather", "Wood", "Metal", "Ruby", "Sappire", "Pearl", "Saviour", "Supernatural", "Abstract", "Future" };

        //This array matches the weapon number with their respective name.
        //Reference to equipment details is available at equipmentsForHumans.txt
        weaponsNames = new string[] { "No Weapon", "Toy Knife", "Wood Sword", "Great Sword", "Hero Sword", "Astral Sword", "Old Hammer", "Not-So-Cool Hammer", "Great Hammer", "Destruction Hammer", "Thaors Hammer", "Tree Branch", "A Stick", "Magic Wand", "Magic Staff", "Deity's Staff", "A Rice Grain", "Some Rice", "Rice Bowl", "Rice Bag", "Ancient God Rice Supreme" };

        //Arrays that contain the actual perks of the equipment
        //Overall order is (HP,MP,AP,DP,SP,NUMBER)
        //Reference to equipment details is available at equipmentsForHumans.txt
        headPerks = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 2, 0, 1 }, { 0, 3, 0, 4, 0, 2 }, { 0, 0, 0, 6, 0, 3 }, { 0, 15, 0, 15, 0, 4 }, { 0, 10, 0, 20, 0, 5 }, { 0, 20, 0, 10, 0, 6 }, { 0, 20, 0, 20, 0, 7 }, { 0, 35, 0, 35, 0, 8 }, { 0, 60, 0, 0, 0, 9 }, { 0, 30, 0, 50, 0, 10 } };
        handsPerks = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 4, 0, 0, 1 }, { 0, 0, 10, 0, 0, 2 }, { 0, 5, 20, 0, 0, 3 }, { 0, 10, 50, 0, 0, 4 }, { 0, 20, 70, 0, 0, 5 }, { 0, 0, 7, 0, 0, 6 }, { 0, 0, 15, 0, 0, 7 }, { 0, 0, 40, 0, 0, 8 }, { 0, 0, 70, 0, 0, 9 }, { 0, 0, 100, 0, 0, 10 }, { 0, 5, 2, 0, 0, 11 }, { 0, 8, 3, 0, 0, 12 }, { 0, 15, 5, 0, 0, 13 }, { 0, 40, 15, 0, 0, 14 }, { 0, 60, 20, 0, 0, 15 }, { 0, 1, 1, 0, 0, 16 }, { 0, 2, 2, 0, 0, 17 }, { 0, 10, 10, 0, 0, 18 }, { 0, 30, 30, 0, 0, 19 }, { 0, 50, 50, 0, 0, 20 } };
        torsoPerks = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 3, 0, 0, 0, 0, 1 }, { 4, 0, 0, 4, 0, 2 }, { 15, 0, 0, 10, 0, 3 }, { 30, 5, 0, 10, 0, 4 }, { 30, 5, 0, 10, 0, 5 }, { 30, 5, 0, 10, 0, 6 }, { 60, 0, 0, 20, 0, 7 }, { 80, 0, 0, 20, 0, 8 }, { 100, 20, 0, 30, 0, 9 }, { 100, 0, 0, 50, 0, 10 } };
        feetPerks = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 2, 1 }, { 0, 0, 0, 0, 5, 2 }, { 0, 0, 0, 4, 2, 3 }, { 0, 10, 0, 0, 10, 4 }, { 0, 5, 0, 0, 10, 5 }, { 0, 10, 0, 0, 10, 6 }, { 0, 0, 0, 0, 20, 7 }, { 0, 0, 0, 0, 30, 8 }, { 0, 30, 0, 0, 40, 9 }, { 0, 0, 0, 0, 50, 10 } };
    }

    //Method to get equipment Names
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public string GetEquipmentName(int area, int equipmentNumber)
    {
        if (area == 2)
        {
            return weaponsNames[equipmentNumber];
        }
        else
        {
            switch (area)
            {
                case 1:
                    return String.Format("{0} Helmet", equipmentNames[equipmentNumber]);
                case 3:
                    return String.Format("{0} Armor", equipmentNames[equipmentNumber]);
                case 4:
                    return String.Format("{0} Shoes", equipmentNames[equipmentNumber]);
                default:
                    return "What are you talking about, cheater?";
            }
        }
    }

    //Method to get the actual Perk values of the equipment
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public int[] GetEquipmentPerks(int area, int equipmentNumber)
    {
        return MakeArray(area, equipmentNumber);
    }

    //Helper Method for the method above
    public int[] MakeArray(int area, int eNo)
    {
        List<int> intList = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            switch (area)
            {
                case 1:
                    intList.Add(headPerks[eNo, i]);
                    break;
                case 2:
                    intList.Add(handsPerks[eNo, i]);
                    break;
                case 3:
                    intList.Add(torsoPerks[eNo, i]);
                    break;
                case 4:
                    intList.Add(feetPerks[eNo, i]);
                    break;
                default:
                    break;
            }
        }
        return intList.ToArray();
    }


    //Method to get equipment Names
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public string[] GetEquipmentNames()
    {
        List<string> eNames = new List<string>();
        for (int area = 1; area <= 4; area++)
        {
            if (area == 2)
            {
                for (int j = 0; j < weaponsNames.Length; j++)
                {
                    eNames.Add(weaponsNames[j]);
                }
            }
            else
            {
                for (int j = 0; j < equipmentNames.Length; j++)
                {
                    switch (area)
                    {
                        case 1:
                            eNames.Add(String.Format("{0} Helmet", equipmentNames[j]));
                            break;
                        case 3:
                            eNames.Add(String.Format("{0} Armor", equipmentNames[j]));
                            break;
                        case 4:
                            eNames.Add(String.Format("{0} Shoes", equipmentNames[j]));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return eNames.ToArray();
    }

    //Method to get equipment data
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    // Data order is (area,itemno,Cost)
    public List<int[]> GetEquipmentPositionCost()
    {
        int thisCounter = 0;
        List<int[]> eNames = new List<int[]>();
        for (int area = 1; area <= 4; area++)
        {
            if (area == 2)
            {
                for (int j = 0; j < weaponsNames.Length; j++)
                {

                    eNames.Add(new int[] { area, j, 100 + ((j - thisCounter) * 500) });
                    switch (j)
                    {
                        case 5:
                            thisCounter = 5;
                            break;
                        case 10:
                            thisCounter = 10;
                            break;
                        case 15:
                            thisCounter = 15;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int j = 0; j < equipmentNames.Length; j++)
                {
                    eNames.Add(new int[] { area, j, 100 + (j * 400) });
                }
            }
        }
        return eNames;
    }

    public List<int[]> GetPerks()
    {
        List<int[]> eNames = new List<int[]>();
        List<int> littleL = new List<int>();
        for (int area = 1; area <= 4; area++)
        {
            if (area == 2)
            {
                for (int j = 0; j < handsPerks.GetLength(0); j++)
                {
                    littleL.Clear();
                    for (int k = 0; k < 6; k++)
                    {
                        littleL.Add(handsPerks[j, k]);
                    }
                    eNames.Add(littleL.ToArray());
                }
            }
            else
            {
                for (int j = 0; j < torsoPerks.GetLength(0); j++)
                {
                    switch (area)
                    {
                        case 1:
                            littleL.Clear();
                            for (int k = 0; k < 6; k++)
                            {
                                littleL.Add(headPerks[j, k]);
                            }
                            eNames.Add(littleL.ToArray());
                            break;
                        case 3:
                            littleL.Clear();
                            for (int k = 0; k < 6; k++)
                            {
                                littleL.Add(torsoPerks[j, k]);
                            }
                            eNames.Add(littleL.ToArray());
                            break;
                        case 4:
                            littleL.Clear();
                            for (int k = 0; k < 6; k++)
                            {
                                littleL.Add(feetPerks[j, k]);
                            }
                            eNames.Add(littleL.ToArray());
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return eNames;
    }
}