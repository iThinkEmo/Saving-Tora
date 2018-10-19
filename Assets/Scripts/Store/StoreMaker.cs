using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class StoreMaker
{
    public static string[] magicNames;
    public static string[] magicDescr;
    public static int[] magicCosts;
    public static int[] magicPower;

    public static ItemRPG[] itemsToSell;

    public static string[] Equipments;
    public static List<int[]> equipmentsData;
    public static List<int[]> equipmentsPower;
    public static PlayerEquipment playerEquipment;
    public static Magica magica;
    public static PlayerItems items;

    public StoreMaker()
    {
        playerEquipment = new PlayerEquipment();
        magica = new Magica(0, 0);
        items = new PlayerItems();
        magicNames = magica.GetMagics();
        magicDescr = magica.GetMagicDescriptions();
        magicCosts = magica.GetCosts();
        magicPower = magica.GetMagicPowers();
        itemsToSell = items.GetItems();
        Equipments = playerEquipment.equipmentPlayer.GetEquipmentNames();
        equipmentsData = playerEquipment.equipmentPlayer.GetEquipmentPositionCost();
        equipmentsPower = playerEquipment.equipmentPlayer.GetPerks();
    }

    public void WritecsvMagica()
    {
        var csv = new StringBuilder();

        for (int i = 0; i < magicNames.Length; i++)
        {
            var first = magicNames[i].ToString();
            var second = magicDescr[i].ToString();
            var third = magicPower[i].ToString();
            var fourth = magicCosts[i].ToString();

            var newLine = string.Format("{0},{1},{2},{3}", first, second, third, fourth);
            csv.AppendLine(newLine);
        }

        File.WriteAllText("magia.csv", csv.ToString());
    }

    public void writeCSVEquip()
    {
        var csv = new StringBuilder();
        var mini = new StringBuilder();

        for (int i = 0; i < Equipments.Length; i++)
        {
            var first = Equipments[i].ToString();
            var second = equipmentsData[i][0].ToString();
            var third = equipmentsData[i][1].ToString();
           // mini.Clear();
            foreach (var item in equipmentsPower[i])
            {
                mini.Append(item + "|");
            }
            mini.Remove(mini.Length - 1, 1);
            var fourth = mini.ToString();
            var fifth = equipmentsData[i][2].ToString();

            var newLine = string.Format("{0},{1},{2},{3},{4}", first, second, third, fourth, fifth);
            csv.AppendLine(newLine);
        }

        File.WriteAllText("equipo.csv", csv.ToString());
    }

    public void writeCSVItems()
    {
        var csv = new StringBuilder();
        var mini = new StringBuilder();

        for (int i = 0; i < itemsToSell.Length; i++)
        {
            var first = itemsToSell[i].name.ToString();
            var second = itemsToSell[i].description.ToString();
           // mini.Clear();
            foreach (var item in itemsToSell[i].perks)
            {
                mini.Append(item + "|");
            }
            mini.Remove(mini.Length - 1, 1);
            var fourth = mini.ToString();
            var third = itemsToSell[i].itemNum.ToString();
            int[] myarrCost = GenerateItemsCost();
            var fifth = myarrCost[i].ToString();

            var newLine = string.Format("{0},{1},{2},{3},{4}", first, second, third, fourth, fifth);
            csv.AppendLine(newLine);
        }

        File.WriteAllText("itemo.csv", csv.ToString());
    }


    public int[] GenerateItemsCost()
    {
        int thisCounter = 0;
        List<int> eNames = new List<int>();
        for (int i = 0; i < 30; i++)
        {
            if (i < 26)
            {
                switch (i)
                {
                    case 6:
                        thisCounter = 1;
                        break;
                    case 11:
                        thisCounter = 1;
                        break;
                    case 16:
                        thisCounter = 1;
                        break;
                    case 21:
                        thisCounter = 1;
                        break;
                    default:
                        break;
                }
                eNames.Add(50 * thisCounter + 20);
                thisCounter++;
            }
            else
            {
                eNames.Add(1500);
            }
        }
        return eNames.ToArray();
    }

    static void Main()
    {
        StoreMaker myS = new StoreMaker();
        myS.WritecsvMagica();
        myS.writeCSVEquip();
        myS.writeCSVItems();
    }

}
