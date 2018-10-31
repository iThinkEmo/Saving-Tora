using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SpecialGameEvents
{
    //Structure of Dialogue are :
    //0: Initial statement or Question, 1:Negative Response, 2: Positive Response
    //If no choice was made, default will be all responses.
    string[] hospitalDialogue;
    string[] hospitalRescuedDialogue;
    string[] InnDialogue;
    string[] ChestDialogue;


    public SpecialGameEvents(){
        hospitalRescuedDialogue = new string[] {"You were saved and treated in the Hospital. Your HP is restored.","Some of your money fell from your pocket.","Some fans stopped following you."};
        hospitalDialogue = new string[] {"Do you want to be healed in the hospital ", "Your HP is restored.", "OK, nevermind then."};
        InnDialogue      = new string[] {"Do you want to spend the night ","Great, your room is ready.","There was no vacancy anyway..."};
        ChestDialogue    = new string[] {"You found $"," on the floor."};
    }

    //Method for returning the whole of an interaction with the hospital
    //dead1: was dead  dead 0: landed on hospital place
    public string[] HospitalInteraction(int area, int dead)
    {
        List<string> myList = new List<string>();
        if (dead==1)
        {
            return hospitalRescuedDialogue;
        }
        myList.Add(hospitalDialogue[0]);
        myList.Add(MoneyCalculator(area,50));
        myList.Add(hospitalDialogue[1]);
        myList.Add(hospitalDialogue[2]);
        return myList.ToArray();
    }

    public string[] InnInteraction(int area)
    {
        List<string> myList = new List<string>();
        myList.Add(InnDialogue[0]);
        myList.Add(MoneyCalculator(area, 80));
        myList.Add(InnDialogue[1]);
        myList.Add(InnDialogue[2]);
        return myList.ToArray();
    }

    public string[] ChestInteraction(int area, PlayerUber playerUber, Saviour saviour){
        List<string> myList = new List<string>();
        myList.Add(ChestDialogue[0]);
        int moneyVal = LootCalculator(area);
        playerUber.money += moneyVal;
        saviour.money += moneyVal;
        myList.Add(moneyVal.ToString());
        myList.Add(ChestDialogue[1]);
        return myList.ToArray();
    }

    public int LootCalculator(int area){
        System.Random randomizerMax = new System.Random();
        int moneyVal = randomizerMax.Next(0, 100);
        switch (area)
        {
            case 1:
                return moneyVal;
            case 2:
                return moneyVal * 4;
            case 3:
                return moneyVal * 8;
            case 4:
                return moneyVal * 15;
            default:
                // $0, Cheater.
                return 0;
        }
    }


    //To calculate Money for hospital and Inn
    public string MoneyCalculator(int area, int moneyVal)
    {
        int i = moneyVal;
        switch (area)
        {
            case 1:
                return "for $" + i + "?" ;
            case 2:
                return "for $" + i * 2 + "?";
            case 3:
                return "for $" + i * 4 + "?";
            case 4:
                return "for $" + i * 6 + "?";
            default:
                break;
        }
        return "FOR ALL YOUR MONEY, CHEATER?";
    }

}

