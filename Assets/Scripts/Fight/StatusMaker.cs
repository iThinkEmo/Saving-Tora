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
    string[] monsterFiles = { "P1Monster.json", "P2Monster.json", "P3Monster.json", "P4Monster.json" };
    static string[] playerMainFiles = { "P1.json", "P2.json", "P3.json", "P4.json" };

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
    {; }

    //To make and post the JSON
    public void MakeAndPostJSONFight()
    {
        filenamec = "statusFight.json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(fStat, Formatting.Indented);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
    }


    //To Set the JSON for latter continue of the fight
    public void SetMonster(int playerNum, EnemyClass e1)
    {
        filenamec = getMonsterFile(playerNum);
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(e1, Formatting.Indented);
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
                jsonString = JsonConvert.SerializeObject(new EnemyClass(0), Formatting.Indented);
                Debug.Log(path1);
                System.IO.File.WriteAllText(path1, jsonString);
            }
        }
    }

    //Method for initializing a monster for a fight
    public EnemyClass InitMonster(int numberPlayer, int monsterNo)
    {
        filenamec = getMonsterFile(numberPlayer);
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(new EnemyClass(monsterNo), Formatting.Indented);
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
                return monsterFiles[eNum - 1];
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

    //1:Witch, 2:Samurai,3:Undead,4: RiceMonk
    public void SetPlayer(int charM8, PlayerUber player)
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
        //jsonString = System.IO.File.ReadAllText(path1);
        //PlayerUber sFight = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
        jsonString = JsonConvert.SerializeObject(player, Formatting.Indented);        
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
    }


    public static string GetPlayerFile(int eNum)
    {
        return playerMainFiles[eNum];
    }

    #region statusGlobalM8

    public static void setInitialScreenjson(Vector3 position, Vector3 rotation, int idPlayer){
        PlayerStatusGlobal playerStatusGlobal =
            //position, rotation, int one, bool true, int overworlded, bool truemen, string magic
            new PlayerStatusGlobal(position, rotation, 1, true, 0, false, "Basic Magic")
        ;
        string path1 = Application.persistentDataPath + "/" + GetPlayerFile(idPlayer);
        string jsonString = JsonConvert.SerializeObject(playerStatusGlobal, Formatting.Indented);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
            
    }

    public void setUltrajson(List<PlayerStatusGlobal> p11)
    {
        for (int i = 0; i < 4; i++)
        {
            path1 = Application.persistentDataPath + "/" + GetPlayerFile(i);
            jsonString = JsonConvert.SerializeObject(p11[i], Formatting.Indented);
            Debug.Log(path1);
            System.IO.File.WriteAllText(path1, jsonString);
        }
    }

    public List<PlayerStatusGlobal> GetUltrajson()
    {
        List<PlayerStatusGlobal> statsM8 = new List<PlayerStatusGlobal>();
        for (int i = 0; i < 4; i++)
        {
            path1 = Application.persistentDataPath + "/" + GetPlayerFile(i);
            jsonString = System.IO.File.ReadAllText(path1);
            PlayerStatusGlobal sFight = JsonConvert.DeserializeObject<PlayerStatusGlobal>(jsonString);
            Debug.Log(path1);
            statsM8.Add(sFight);
        }
        return statsM8;
    }

    #endregion

    #region fightend

    public FightEnd GetEnd()
    {
        filenamec = "fightended.json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        FightEnd sFight = JsonConvert.DeserializeObject<FightEnd>(jsonString);
        Debug.Log(path1);
        return sFight;
    }

    //awarded exp -1 means that is an inmediate level up 
    public void SetEnd(int playerNum, int awardedEXP, int awardedFans, int awardedMoney, int turnsLost, bool didWin)
    {
        FightEnd fend = new FightEnd(playerNum, awardedEXP, awardedFans, awardedMoney, turnsLost, didWin);
        filenamec = "fightended.json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = JsonConvert.SerializeObject(fend, Formatting.Indented);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
    }

    #endregion
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

public class PlayerStatusGlobal
{
    //Contains overworld character rotation and position
    public Vector3 pos;
    // Rotation is meant to be a Quaternion.Euler(f, f, f)
    public Vector3 rot;
    // 1: first One,2: ComeBack from Fight,3:Comebackfrom item, 4: Come
    public int statusScene;
    //To know whether or not to show the intro to fight
    public bool firstFight = true;
    //To know if current character is dead, and how much turns dead left
    public int dead = 0;
    public bool win = false;

    public string magic = "";

    public PlayerStatusGlobal(Vector3 posy, Vector3 roty, int one, bool truer, int overworldded, bool truemen, string magic)
    {
        this.pos = posy;
        this.rot = roty;
        statusScene = one;
        firstFight = truer;
        dead = overworldded;
        win = truemen;
        this.magic = magic;
    }
}

public class FightEnd
{
    //EXP that will be alloted to the user
    public int exp;
    //numplayer
    public int numplayer;
    //fanses that were won or lost
    public int fans;
    //money that will be lost or won
    public int money;
    //winorlose
    public bool win;
    //turnslost
    public int tLost;

    public FightEnd(int playerNum, int awardedEXP, int awardedFans, int awardedMoney, int turnsLost, bool didWin)
    {
        this.exp = awardedEXP;
        this.numplayer = playerNum;
        this.fans = awardedFans;
        this.money = awardedMoney;
        this.win = didWin;
        this.tLost = turnsLost;
    }
}

