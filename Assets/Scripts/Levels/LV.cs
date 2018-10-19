using System;

//This class is for calculating levels based on certain conditions.
public class LV
{
    //the array of perks, explained at DeterminePerks, and in levelUp
    int[] perks = new int[5];

    //constructor for the level.
    public LV(int playerClass)
    {
        //Depending on the class, it is as follow 
        //(0-Samurai,1-Magician,2-Monk,3-Undead)
        perks = DeterminePerks(playerClass);
    }

    //This class is for determining the perks to be automatically upgraded when leveling up
    //The class returns an array with 1 and 0s, with the perks to be augmented in this form:
    //(hp,mp,ap,dp,sp)
    //One means yes, zero means no.
    public int[] DeterminePerks(int n)
    {
        switch (n)
        {
            case 0:
                return new int[] { 1, 1, 1, 0, 0 };
                break;
            case 1:
                return new int[] { 1, 1, 0, 0, 1 };
                break;
            case 2:
                return new int[] { 1, 0, 1, 0, 1 };
                break;
            case 3:
                return new int[] { 1, 0, 1, 1, 0 };
                break;
            default:
                return new int[] { 0, 0, 0, 0, 0 };
                break;
        }
    }

    //This is the switcher for the level up method which basically uses the perks array for determining if user will get 
    //a trait powered up or not, using randoms.
    public int[] LevelUP(int lv)
    {
        if (lv <= 10)
        {
            return PerksCalculator(1);
        }
        else if (lv > 10 && lv <= 15)
        {
            return PerksCalculator(2);
        }
        else
        {
            return PerksCalculator(4);
        }
    }

    //This is the core method for calculating how the level up perks are gonna be.
    public int[] PerksCalculator(int super)
    {
        int[] perksArr = { 0, 0, 0, 0, 0 };
        Random randomizerMax = new Random();
        int truRand;
        for (int ai = 0; ai < 5; ai++)
        {
            if (perks[ai] == 1)
            {
                truRand = randomizerMax.Next(1, 1 + super);
                perksArr[ai] = truRand;
            }
            else
            {
                truRand = randomizerMax.Next(0, super);
                perksArr[ai] = truRand;
            }
        }
        return perksArr;
    }

    //Method for calculating the next max exp 
    public int MaxExp(int currentMax)
    {
        return (int)Math.Truncate(currentMax * 1.5);
    }
}