using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class StatusMaker
{
    public StatusForFight fStat;
    string filenamec;
    string path1;
    string jsonString;
    string[] monsterFiles = { "P1Monster.json" , "P2Monster.json" , "P3Monster.json" , "P4Monster.json" };

    //Constructor to Know its a fight one
    //cP = currentPlayer: To know which character to spawn
    //1:Witch, 2:Samurai,3:Undead,4: RiceMonk
    //a  = Area         : To know which monster to spawn based on the area
    //tOF= Type of Fight: 0:Normal, 1:BOSS, 2:ULTRABOSS
    public StatusMaker(int cP, int a, int tOF)
    {
        fStat = new StatusForFight(cP, a, tOF);
    }

    public StatusMaker()
    {;}

    //To make and post the JSON
    public void MakeAndPostJSONFight()
    {
        filenamec = "statusFight.json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(fStat);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
    }

    //To Set the JSON for latter continue of the fight
    public void ToprepareTheContinuedFight(int playerNum)
    {
        filenamec = getMonsterFile(playerNum);
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(fStat);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
    }

    //To get the JSONs to continue the fight
    public EnemyClass ToContinueJSONFight(int playerNum)
    {
        filenamec = getMonsterFile(playerNum);
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        EnemyClass sFight = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
        return sFight;
    }
    
    //This Method checks if files exists, and if they do not, it creates them with 0 hp
    public void InitFight()
    {
        foreach (string item in monsterFiles)
        {
            path1 = Application.persistentDataPath + "/" + item;
            if (!System.IO.File.Exists(path1))
            {
                jsonString = JsonConvert.SerializeObject(new EnemyClass(0));
                Debug.Log(path1);
                System.IO.File.WriteAllText(path1, jsonString);
            }
        }
    }

    //Method for initializing a monster for a fight
    public EnemyClass InitMonster(int numberPlayer,int monsterNo)
    {
        filenamec = getMonsterFile(numberPlayer);
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(new EnemyClass(monsterNo));
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
        EnemyClass sFight = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
        return sFight;
    }

    //Method to get the monster fileJSON using player as reference
    //so 1..4
    public string getMonsterFile(int eNum)
    {
        switch (eNum)
        {
            case 1:
                return monsterFiles[eNum-1];
            case 2:
                return monsterFiles[eNum - 1];
            case 3:
                return monsterFiles[eNum - 1];
            case 4:
                return monsterFiles[eNum - 1];
            default:
                break;
        }
        return "";
    }

    //To get all 
    public StatusForFight FightGetter()
    {
        applyThatFight(RetrieveDatFight());
        return fStat;
    }

    //To retrieve dat JSON
    public StatusForFight RetrieveDatFight()
    {
        filenamec = "statusFight.json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        StatusForFight sFight = JsonConvert.DeserializeObject<StatusForFight>(jsonString);
        return sFight;
    }

    public void applyThatFight(StatusForFight thisStatus)
    {
        fStat = thisStatus;
    }

    //1:Witch, 2:Samurai,3:Undead,4: RiceMonk
    public PlayerUber getPlayer(int charM8)
    {
        switch (charM8)
        {
            case 1:
                filenamec = "magician.json";
                break;
            case 2:
                filenamec = "samurai.json";
                break;
            case 3:
                filenamec = "undead.json";
                break;
            case 4:
                filenamec = "rice.json";
                break;
            default:
                break;
        }
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        PlayerUber sFight = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
        return sFight;
    }


}

class StatusForFight
{
    //For Determining which character to load
    public int currentPlayer;
    public int area;
    public int typeOfFight;

    public StatusForFight(int cP, int a, int tOF)
    {
        this.currentPlayer = cP;
        this.area          = a;
        this.typeOfFight   = tOF;
    }
}
