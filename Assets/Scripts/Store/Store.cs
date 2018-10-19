using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

public class Store
{
    public string[] magicNames;
    public string[] magicDescr;
    public int[] magicPower;
    public int[] magicCosts;

    public string[] itemNames;
    public string[] itemDescription;
    public int[] itemNumbers;
    public int[][] itemPower;
    public int[] itemCosts;

    public string[] equipmentNames;
    public int[] equipmentArea;
    public int[] equipmentLV;
    public int[][] equipmentPower;
    public int[] equipmentCosts;

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
            new System.IO.StreamReader(@"c:\itemo.csv");
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
        itemNames = GetSubArray(tempNames, area, 5, 1);
        itemDescription = GetSubArray(tempDescription, area, 5, 1);
        itemNumbers = GetSubArray(tempNumbers, area, 5, 1);
        List<int> miniInt = new List<int>();
        for (int i = 0; i < tempPower.Count; i++)
        {
            miniInt.Clear();
            for (int j = 0; j < 6; j++)
            {
                miniInt.Add(tempPower[i][j]);
            }
            tempPower.Add(miniInt.ToArray());
        }
        itemPower = GetSubArray(tempPower, area, 5, 1);
        itemCosts = GetSubArray(tempCosts, area, 5, 1);
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
            new System.IO.StreamReader(@"c:\itemo.csv");
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
        equipmentNames = GetSubArray(tempNames, area, 11, 2);
        equipmentArea = GetSubArray(tempArea, area, 11, 2);
        equipmentLV = GetSubArray(tempNumbers, area, 11, 2);
        List<int> miniInt = new List<int>();
        for (int i = 0; i < tempPower.Count; i++)
        {
            miniInt.Clear();
            for (int j = 0; j < 6; j++)
            {
                miniInt.Add(tempPower[i][j]);
            }
            tempPower.Add(miniInt.ToArray());
        }
        equipmentPower = GetSubArray(tempPower, area, 11, 2);
        equipmentCosts = GetSubArray(tempCosts, area, 11, 2);
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
            new System.IO.StreamReader(@"c:\itemo.csv");
        while ((line = file.ReadLine()) != null)
        {
            thisElements = line.Split(',');
            tempNames.Add(thisElements[0]);
            tempDescription.Add(thisElements[1]);
            tempPower.Add(Int32.Parse(thisElements[2]));
            tempCosts.Add(Int32.Parse(thisElements[4]));
        }
        file.Close();

        //Final Assignmentm8
        magicNames = GetSubArray(tempNames, area - 1, 4, 3);
        magicDescr = GetSubArray(tempDescription, area - 1, 4, 3);
        magicPower = GetSubArray(tempPower, area - 1, 4, 3);
        magicCosts = GetSubArray(tempCosts, area - 1, 4, 3);
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
        string[] thisList = new string[] { status.ToString(), newW.ToString(), itemno.ToString(), textStor };
        return thisList;
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

    //Return needed dat for buying an object:
    //(status, newWealth, magiaName,  textOfStatus)
    public string[] BuyMagia(int currentWealth, string magiaName)
    {
        int tempCost = GetMagiaCost(magiaName);
        int status = NotEnoughWealth(currentWealth, tempCost);
        string textStor = BuyStatus(status);
        int newW = currentWealth;
        if (status > 0)
        {
            newW = NewWealth(currentWealth, tempCost);
        }
        string[] thisList = new string[] { status.ToString(), newW.ToString(), magiaName, textStor };
        return thisList;
    }

    //To Get item cost
    public int GetItemCost(int itemNo)
    {
        return itemCosts[itemNo];
    }

    //To Get EquipmentCost and index
    //(index, cost)
    public int[] GetEquipCost(string nameMen)
    {
        int indexe = GetEquipIndex(nameMen);
        return new int[] { indexe,equipmentCosts[indexe] };
    }

    //To get the index for the equipment cost
    public int GetEquipIndex(string magiN)
    {
        for (int i = 0; i < equipmentNames.Length; i++)
        {
            if (equipmentNames[i].Equals(magiN))
            {
                return i;
            }
        }
        return 0;
    }

    //For getting the cost of the magic
    public int GetMagiaCost(string magiN)
    {
        int indexM = GetMagiaIndex(magiN); 
        return magicCosts[indexM];
    }

    //To get the index for the magic cost
    public int GetMagiaIndex(string magiN)
    {
        for (int i = 0; i < magicNames.Length; i++)
        {
            if (magicNames[i].Equals(magiN))
            {
                return i;
            }
        }
        return 0;
    }

    //Method for returning 1 if users wealth is enough for article selected, 0 if not.
    public int NotEnoughWealth(int wealth, int cost)
    {
        if (cost>wealth)
        {
            return 0;
        }
        return 1;
    }

    //Method for returning a string for the user in case he bought something or not
    //1 YES, 0 NOT
    public string BuyStatus(int wealth)
    {
        if (wealth==1)
        {
            return "Thank you!";
        }
        return "You don't have enough money!";
    }

    //Method for returning users new wealth.
    public int NewWealth(int wealth, int cost)
    {
        return wealth-cost;
    }
}
