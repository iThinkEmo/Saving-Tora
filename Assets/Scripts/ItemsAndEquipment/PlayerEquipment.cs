using System;

//This class holds the players current equipment, in integer format.
public class PlayerEquipment
{
    public int head;
    public int hands;
    public int torso;
    public int feet;
    public Equipment equipmentPlayer;

    public PlayerEquipment()
    {
        head = 0;
        hands = 0;
        torso = 0;
        feet = 0;
        equipmentPlayer = new Equipment();
    }

    public void EquipEquipment(int part, int equip)
    {
        //Method to equip equipment 
        //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
        switch (part)
        {
            case 1:
                this.head = equip;
                break;
            case 2:
                this.hands = equip;
                break;
            case 3:
                this.torso = equip;
                break;
            case 4:
                this.feet = equip;
                break;
            default:
                break;
        }
    }

    public void UnEquipEquipment(int part)
    {
        //Method to unequip equipment 
        //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
        //Method to equip equipment 
        //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
        switch (part)
        {
            case 1:
                this.head = 0;
                break;
            case 2:
                this.hands = 0;
                break;
            case 3:
                this.torso = 0;
                break;
            case 4:
                this.feet = 0;
                break;
            default:
                break;
        }
    }

    //Method to get equipment Names
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public string GetEquipmentName(int area, int equipmentNumber)
    {
        return equipmentPlayer.GetEquipmentName(area,equipmentNumber);
    }

    //Method to get the actual Perk values of the equipment
    //area: (1-Head | 2-Hands | 3-Torso | 4-Feet);
    public int[] GetEquipmentPerks(int area, int equipmentNumber)
    {
        return equipmentPlayer.GetEquipmentPerks(area, equipmentNumber);
    }
}