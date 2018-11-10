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

    public static int nightCost;
    string[] ChestDialogue;

    public static bool innInteraction, hospitalInteraction;


    public SpecialGameEvents(){
        hospitalRescuedDialogue = new string[] {"You were saved and treated in the Hospital. Your HP is fully restored.","Some of your money fell from your pocket.","Some fans stopped following you."};
        hospitalDialogue = new string[] {"Do you want to be healed in the hospital?", "Your HP is fully restored.", "OK, nevermind then."};
        InnDialogue      = new string[] {"Do you want to spend", "the night ","Great, your room is ready.","There was no vacancy anyway...", "You don't have enough money.\nGo away!"};
        ChestDialogue    = new string[] {"You found $"," on the floor."};
    }

    //Method for returning the whole of an interaction with the hospital
    //dead1: was dead  dead 0: landed on hospital place
    public string[] HospitalInteraction(bool dead)
    {
        hospitalInteraction = true;
        List<string> myList = new List<string>();
        if (dead){
            return hospitalRescuedDialogue;
        }
        myList.Add(hospitalDialogue[0]);
        //myList.Add(MoneyCalculator(area,50));
        myList.Add(hospitalDialogue[1]);
        myList.Add(hospitalDialogue[2]);
        return myList.ToArray();
    }

    public string[] InnInteraction(int area)
    {
        innInteraction = true;
        List<string> myList = new List<string>();
        myList.Add(InnDialogue[0]);
        myList.Add(InnDialogue[1]);
        myList.Add(MoneyCalculator(area, 80));
        myList.Add(InnDialogue[2]);
        myList.Add(InnDialogue[3]);
        myList.Add(InnDialogue[4]);
        return myList.ToArray();
    }

    public string[] ChestInteraction(int area, PlayerUber playerUber, Saviour saviour, int currentPlayer){
        List<string> myList = new List<string>();
        myList.Add(ChestDialogue[0]);
        int moneyVal = LootCalculator(area);
        playerUber.money += moneyVal;
        saviour.money += moneyVal;
        StatusMaker sm = new StatusMaker();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), playerUber);
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
                nightCost = i;
                break;
            case 2:
                nightCost = i * 2;
                break;
            case 3:
                nightCost = i * 4;
                break;
            case 4:
                nightCost = i * 6;
                break;
            default:
                nightCost = 100000000;
                break;
        }
        return "for $" + nightCost + "?";
        // return "FOR ALL YOUR MONEY, CHEATER?";
    }

    public static void RecoverHealth(PlayerUber player, Saviour saviour, int currentPlayer){        
        player.hp = player.maxhp;
        saviour.hp = saviour.maxhp;
        StatusMaker sm = new StatusMaker();
		sm.SetPlayer(PlayerUber.normalizeCurrentPlayer(currentPlayer), player);
    }

}

