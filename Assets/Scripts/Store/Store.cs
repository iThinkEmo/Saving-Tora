using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Store
{
    public  string[] magicNames;
    public  string[] magicDescr;
    public  int[] magicPower;
    public  int[] magicCosts;

    public  string[] itemNames;
    public  string[] itemDescription;
    public  int[] itemNumbers;
    public  int[][] itemPower;
    public  int[] itemCosts;
    Items items = new Items();

    public  string[] equipmentNames;
    public  int[] equipmentArea;
    public  int[] equipmentLV;
    public  int[][] equipmentPower;
    public  int[] equipmentCosts;

    public Store(int area)
    {
        FillStore(area);
    }

    public void FillStore(int area)
    {
        FillEquipment(area);
        FillMagica(area);
        FillItems(area);
    }

    public void FillItems(int area)
    {
        //Temp Lists
        List<string> tempNames = new List<string>();
        List<string> tempDescription = new List<string>();
        List<int> tempNumbers = new List<int>();
        List<int[]> tempPower = new List<int[]>();
        List<int> tempCosts = new List<int>();

        //Reading Method
        string line;
        string[] thisElements;
        string[] thisPerks;
        List<int> littleInt = new List<int>();
        System.IO.StreamReader file =
            new System.IO.StreamReader("Assets/Scripts/Store/itemo"+area+".csv");
        while ((line = file.ReadLine()) != null)
        {
            thisElements = line.Split(',');
            tempNames.Add(thisElements[0]);
            tempDescription.Add(thisElements[1]);
            tempNumbers.Add(Int32.Parse(thisElements[2]));
            thisPerks = thisElements[3].Split('|');
            littleInt.Clear();
            foreach (var item in thisPerks)
            {
                littleInt.Add(Int32.Parse(item));
            }
            tempPower.Add(littleInt.ToArray());
            tempCosts.Add(Int32.Parse(thisElements[4]));
        }
        file.Close();

        //Final Assignmentm8
        itemNames = tempNames.ToArray();//GetSubArray(tempNames, area, 5, 1);
        itemDescription = tempDescription.ToArray();//GetSubArray(tempDescription, area, 5, 1);
        itemNumbers = tempNumbers.ToArray();//GetSubArray(tempNumbers, area, 5, 1);
        // List<int> miniInt = new List<int>();
        // for (int i = 0; i < tempPower.Count; i++)
        // {
        //     miniInt.Clear();
        //     for (int j = 0; j < 6; j++)
        //     {
        //         miniInt.Add(tempPower[i][j]);
        //     }
        //     tempPower.Add(miniInt.ToArray());
        // }
        itemPower = tempPower.ToArray();//GetSubArray(tempPower, area, 5, 1);
        itemCosts = tempCosts.ToArray();//GetSubArray(tempCosts, area, 5, 1);
    }

    public void FillEquipment(int area)

    {
        //Temp Lists
        List<string> tempNames = new List<string>();
        List<int> tempArea = new List<int>();
        List<int> tempNumbers = new List<int>();
        List<int[]> tempPower = new List<int[]>();
        List<int> tempCosts = new List<int>();

        //Reading Method
        string line;
        string[] thisElements;
        string[] thisPerks;
        List<int> littleInt = new List<int>();
        System.IO.StreamReader file =
            new System.IO.StreamReader("Assets/Scripts/Store/equipo" + area + ".csv");
        while ((line = file.ReadLine()) != null)
        {
            thisElements = line.Split(',');
            tempNames.Add(thisElements[0]);
            tempArea.Add(Int32.Parse(thisElements[1]));
            tempNumbers.Add(Int32.Parse(thisElements[2]));
            thisPerks = thisElements[3].Split('|');
            littleInt.Clear();
            foreach (var item in thisPerks)
            {
                littleInt.Add(Int32.Parse(item));
            }
            tempPower.Add(littleInt.ToArray());
            tempCosts.Add(Int32.Parse(thisElements[4]));
        }
        file.Close();

        //Final Assignmentm8
        equipmentNames = tempNames.ToArray();//GetSubArray(tempNames, area, 11, 2);
        equipmentArea = tempArea.ToArray();//GetSubArray(tempArea, area, 11, 2);
        equipmentLV = tempNumbers.ToArray();//GetSubArray(tempNumbers, area, 11, 2);
        List<int> miniInt = new List<int>();

        // for (int i = 0; i < tempPower.Count; i++)
        // {
        //     miniInt.Clear();
        //     for (int j = 0; j < 6; j++)
        //     {
        //         miniInt.Add(tempPower[i][j]);
        //     }
        //     tempPower.Add(miniInt.ToArray());
        // }
        equipmentPower = tempPower.ToArray();//GetSubArray(tempPower, area, 11, 2);
        equipmentCosts = tempCosts.ToArray();//GetSubArray(tempCosts, area, 11, 2);
    }

    public void FillMagica(int area)
    {
        //Temp Lists
        List<string> tempNames = new List<string>();
        List<string> tempDescription = new List<string>();
        List<int> tempPower = new List<int>();
        List<int> tempCosts = new List<int>();

        //Reading Method
        string line;
        string[] thisElements;
        List<int> littleInt = new List<int>();
        System.IO.StreamReader file =
            new System.IO.StreamReader("Assets/Scripts/Store/magia" + area + ".csv");
        while ((line = file.ReadLine()) != null)
        {
            thisElements = line.Split(',');
            tempNames.Add(thisElements[0]);
            tempDescription.Add(thisElements[1]);
            tempPower.Add(Int32.Parse(thisElements[2]));
            tempCosts.Add(Int32.Parse(thisElements[3]));
        }
        file.Close();

        //Final Assignmentm8
        magicNames = tempNames.ToArray();//GetSubArray(tempNames, area - 1, 4, 3);
        magicDescr = tempDescription.ToArray();//GetSubArray(tempDescription, area - 1, 4, 3);
        magicPower = tempPower.ToArray();//GetSubArray(tempPower, area - 1, 4, 3);
        magicCosts = tempCosts.ToArray();//GetSubArray(tempCosts, area - 1, 4, 3);
    }


    //Gets Elements Acording to the rules set, this one is for ints
    //itemEquip magi order is (1:item, 2:Equip, 3:Magic )
    public int[] GetSubArray(List<int> myL, int startPoint, int increment, int itemEquipMagi)
    {
        List<int> result = new List<int>();
        int reallInt = 1;
        if (startPoint != 1 && itemEquipMagi == 2)
        {
            startPoint *= 2;
        }
        for (int i = startPoint, counter = 0; i < myL.Count; i += increment, counter++)
        {
            if (itemEquipMagi == 1)
            {
                result.Add(myL[i]);
                if (startPoint == 4)
                {
                    result.Add(myL[i + 1]);
                }
            }
            else if (itemEquipMagi == 2)
            {
                if (counter != 1)
                {
                    result.Add(myL[i]);
                    result.Add(myL[i + 1]);
                    result.Add(myL[i + 2]);
                }
                else
                {
                    if (startPoint != 1)
                    {
                        reallInt = startPoint / 2;
                    }
                    for (int jk = reallInt; jk < 20; jk += 5)
                    {
                        result.Add(myL[reallInt]);
                        result.Add(myL[reallInt + 1]);
                    }
                    i += 10;
                }
            }
            else
            {
                result.Add(myL[i]);
            }
        }
        return result.ToArray();
    }

    //Gets Elements Acording to the rules set, this one is for strings
    //itemEquip magi order is (1:item, 2:Equip, 3:Magic )
    public string[] GetSubArray(List<string> myL, int startPoint, int increment, int itemEquipMagi)
    {
        List<string> result = new List<string>();
        int reallInt = 1;
        if (startPoint != 1 && itemEquipMagi == 2)
        {
            startPoint *= 2;
        }
        for (int i = startPoint, counter = 0; i < myL.Count; i += increment, counter++)
        {
            if (itemEquipMagi == 1)
            {
                result.Add(myL[i]);
                if (startPoint == 4)
                {
                    result.Add(myL[i + 1]);
                }
            }
            else if (itemEquipMagi == 2)
            {
                if (counter != 1)
                {
                    result.Add(myL[i]);
                    result.Add(myL[i + 1]);
                    result.Add(myL[i + 2]);
                }
                else
                {
                    if (startPoint != 1)
                    {
                        reallInt = startPoint / 2;
                    }
                    for (int jk = reallInt; jk < 20; jk += 5)
                    {
                        result.Add(myL[reallInt]);
                        result.Add(myL[reallInt + 1]);
                    }
                    i += 10;
                }
            }
            else
            {
                result.Add(myL[i]);
            }
        }
        return result.ToArray();
    }

    //Gets Elements Acording to the rules set, this one is for ints
    //itemEquip magi order is (1:item, 2:Equip, 3:Magic )
    public int[][] GetSubArray(List<int[]> myL, int startPoint, int increment, int itemEquipMagi)
    {
        List<int[]> result = new List<int[]>();
        int reallInt = 1;
        if (startPoint != 1 && itemEquipMagi == 2)
        {
            startPoint *= 2;
        }
        for (int i = startPoint, counter = 0; i < myL.Count; i += increment, counter++)
        {
            if (itemEquipMagi == 1)
            {
                result.Add(myL[i]);
                if (startPoint == 4)
                {
                    result.Add(myL[i + 1]);
                }
            }
            else if (itemEquipMagi == 2)
            {
                if (counter != 1)
                {
                    result.Add(myL[i]);
                    result.Add(myL[i + 1]);
                    result.Add(myL[i + 2]);
                }
                else
                {
                    if (startPoint != 1)
                    {
                        reallInt = startPoint / 2;
                    }
                    for (int jk = reallInt; jk < 20; jk += 5)
                    {
                        result.Add(myL[reallInt]);
                        result.Add(myL[reallInt + 1]);
                    }
                    i += 10;
                }
            }
            else
            {
                result.Add(myL[i]);
            }
        }
        return result.ToArray();
    }

    //Return needed dat for buying an Item:
    //(status, newWealth, itemno,  textOfStatus)
    public string[] BuyItem(int currentWealth, int itemno)
    {
        int tempCost = GetItemCost(itemno);
        int status = NotEnoughWealth(currentWealth, tempCost);
        string textStor = BuyStatus(status);
        int newW = currentWealth;
        if (status > 0)
        {
            newW = NewWealth(currentWealth, tempCost);
        }
        string[] thisList = new string[] { status.ToString(), newW.ToString(), getRelativeItemNo(GetItemName(itemno)).ToString(), textStor };
        return thisList;
    }

    //To get the absolut value of item to buy
    public int getRelativeItemNo(string itemName)
    {
        return items.GetItemNoByName(itemName);
    }

    //Return needed dat for buying an object:
    //(status, newWealth, area, level,  textOfStatus)
    public string[] BuyEquipment(int currentWealth, string itemno)
    {
        int[] tempArr = GetEquipCost(itemno);
        int indexeMen = tempArr[0];
        int tempCost = tempArr[1];
        int status = NotEnoughWealth(currentWealth, tempCost);
        string textStor = BuyStatus(status);
        int newW = currentWealth;
        if (status > 0)
        {
            newW = NewWealth(currentWealth, tempCost);
        }
        string[] thisList = new string[] { status.ToString(), newW.ToString(), equipmentArea[indexeMen].ToString(), equipmentLV[indexeMen].ToString(), textStor };
        return thisList;
    }

    /// <summary>
    /// Returns a string array of: [status, newWealth, magiaName,  textOfStatus]
    /// </summary>
    public string[] BuyMagia(int currentWealth, string magiaName)
    {
        int tempCost = 0;
		string[] thisList=new string[4];
		if (GetMagiaCost(magiaName) != -1) {
			tempCost = GetMagiaCost(magiaName);
			int status = NotEnoughWealth(currentWealth, tempCost);
			string textStor = BuyStatus(status);
			int newW = currentWealth;
			if (status > 0) {
				newW = NewWealth(currentWealth, tempCost);
			}
			thisList[0] = status.ToString();
			thisList[1] = newW.ToString();
			thisList[2] = magiaName;
			thisList[3] = textStor;
		} else {
			Debug.Log("Article not found");
		}
        
        return thisList;
    }

    //To Get item cost
    public int GetItemCost(int itemNo)
    {
        return itemCosts[itemNo];
    }

    //To Get item cost
    public string GetItemName(int itemNo)
    {
        return itemNames[itemNo];
    }

    //To Get EquipmentCost and index
    //(index, cost)
    public int[] GetEquipCost(string nameMen)
    {
        int indexe = GetEquipIndex(nameMen);
        return new int[] { indexe, equipmentCosts[indexe] };
    }

    //To get the index for the equipment cost
    public int GetEquipIndex(string magiN)
    {
        for (int i = 0; i < equipmentNames.Length; i++)
        {
            if (equipmentNames[i].ToLower().Equals(magiN.ToLower()))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Retrieves the cost of the magic or returns -1 if article not found
    /// </summary>
    public int GetMagiaCost(string magiN)
    {
		if (GetMagiaIndex(magiN) != -1) {
			int indexM = GetMagiaIndex(magiN);
			return magicCosts[indexM];
		}
        
        return -1;
    }

    /// <summary>
    /// Gets the index for the magic cost or returns -1 if article not found
    /// </summary>
    public int GetMagiaIndex(string magiN)
    {
        for (int i = 0; i < magicNames.Length; i++)
        {
            if (magicNames[i].ToLower().Equals(magiN.ToLower()))
            {
                return i;
            }
        }
        return -1;
    }

	public int GetItemIndex(string itemN) {
		for (int i = 0; i < itemNames.Length; i++) {
			if (itemNames[i].ToLower().Equals(itemN.ToLower())) {
				return i;
			}
		}
		return -1;
	}




    /// <summary>
    /// Returns 1 if user's wealth is enough for article selected, 0 if not.
    /// </summary>
	public int NotEnoughWealth(int wealth, int cost)
    {
        if (cost > wealth)
        {
            return 0;
        }
        return 1;
    }

    //Method for returning a string for the user in case he bought something or not
    //1 YES, 0 NOT
    public string BuyStatus(int wealth)
    {
        if (wealth == 1)
        {
            return "Thank you!";
        }
        return "You don't have enough money!";
    }

    /// <summary>
    /// Returns user's new wealth (wealth - cost).
    /// </summary>
    public int NewWealth(int wealth, int cost)
    {
        return wealth - cost;
    }

    //public static void Main()
    //{
    //    Store myS = new Store(1);

    //    for (int i = 0; i < magicNames.Length; i++)
    //    {
    //        Console.WriteLine(magicNames[i] + "," + magicDescr[i] + "," + magicPower[i] + "," + magicCosts[i]);
    //        for (int j = 0; j < magicPower.Length; j++)
    //        {
    //            Console.Write(magicPower[j] + ",");
    //        }
    //        Console.Write("\n");
    //    }
    //    for (int i = 0; i < itemNames.Length; i++)
    //    {
    //        Console.WriteLine(itemNames[i] + "," + itemDescription[i] + "," + itemNumbers[i] + "," + itemPower[i] + "," + itemCosts[i]);
    //        for (int j = 0; j < itemPower[i].Length; j++)
    //        {
    //            Console.Write(itemPower[i][j] + ",");
    //        }
    //        Console.Write("\n");
    //    }
    //    for (int i = 0; i < equipmentNames.Length; i++)
    //    {
    //        Console.WriteLine(equipmentNames[i] + "," + equipmentArea[i] + "," + equipmentLV[i] + "," + equipmentPower[i] + "," + equipmentCosts[i]);
    //        for (int j = 0; j < equipmentPower[i].Length; j++)
    //        {
    //            Console.Write(equipmentPower[i][j] + ",");
    //        }
    //        Console.Write("\n");
    //    }
    //}
}



