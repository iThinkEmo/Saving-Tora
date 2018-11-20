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


    /// <summary>
    /// Constructor to initialize a fight
    /// cP = currentPlayer: To know which character to spawn
    /// 1:Witch, 2:Samurai,3:Undead,4: RiceMonk
    /// a  = Area: To know which monster to spawn based on the area
    /// tOF= Type of Fight: 0:Normal, 1:BOSS, 2:ULTRABOSS
    /// </summary>
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
        //Debug.Log(path1);
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
        jsonString = JsonConvert.SerializeObject(new EnemyClass(monsterNo-1), Formatting.Indented);
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
        //Debug.Log(path1);
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
        // Debug.Log(path1);
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

    public CoreDatum GetCore(string folderWithSlash)
    {
        filenamec = "core.json";
        path1 = Application.persistentDataPath + "/"+ folderWithSlash + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        CoreDatum sFight = JsonConvert.DeserializeObject<CoreDatum>(jsonString);
        Debug.Log(path1);
        return sFight;
    }

    public void SetCore(int one, string folderWithSlash)
    {
        CoreDatum fend = new CoreDatum(1);
        filenamec = "core.json";
        path1 = Application.persistentDataPath + "/" + folderWithSlash + filenamec;
        jsonString = JsonConvert.SerializeObject(fend, Formatting.Indented);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
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

public class SaveFile
{
    private GameManager gameManagerDelJuego;

    public string filenamec;
    public string path1;
    public string jsonString;

    public FightEnd fightEnded;
    StatusForFight statusFight;

    public PlayerStatusGlobal P1;
    public PlayerStatusGlobal P2;
    public PlayerStatusGlobal P3;
    public PlayerStatusGlobal P4;

    public EnemyClass P1Monster;
    public EnemyClass P2Monster;
    public EnemyClass P3Monster;
    public EnemyClass P4Monster;

    public PlayerUber magician;
    public PlayerUber rice;
    public PlayerUber samurai;
    public PlayerUber undead;

    public CoreDatum coreD;

    public SaveFile()
    {
        ;
    }

    public void SaveGame(int i)
    {
        string thefolder = "save" + i;
        ReadAll();
        WriteAllSave(thefolder);
    }

    //To read all objects to Jsons
    public void ReadAll()
    {
        UltraReader( "fightended", 0);
        UltraReader( "statusFight", 1);
        UltraReader( "P1", 2);
        UltraReader( "P2", 3);
        UltraReader( "P3", 4);
        UltraReader( "P4", 5);
        UltraReader( "P1Monster", 6);
        UltraReader( "P2Monster", 7);
        UltraReader( "P3Monster", 8);
        UltraReader( "P4Monster", 9);
        UltraReader("magician", 10);
        UltraReader( "rice", 11);
        UltraReader( "samurai", 12);
        UltraReader("undead", 13);
        UltraReader("core", 14);
    }

    //To write all objects to Jsons
    public void WriteAll()
    {
        UltraWriter(-1, "fightended", 0);
        UltraWriter(-1, "statusFight", 1);
        UltraWriter(1, "P", 2);
        UltraWriter(2, "P", 3);
        UltraWriter(3, "P", 4);
        UltraWriter(4, "P", 5);
        UltraWriter(-1, "P1Monster", 6);
        UltraWriter(-1, "P2Monster", 7);
        UltraWriter(-1, "P3Monster", 8);
        UltraWriter(-1, "P4Monster", 9);
        UltraWriter(-1, "magician", 10);
        UltraWriter(-1, "rice", 11);
        UltraWriter(-1, "samurai", 12);
        UltraWriter(-1, "undead", 13);
        UltraWriter(-1, "core", 14);
    }

    //To read all objects to Jsons on the new folder
    public void ReadAllSave(string theFolder)
    {
        SaveReader("fightended", 0, theFolder);
        SaveReader("statusFight", 1, theFolder);
        SaveReader("P1", 2, theFolder);
        SaveReader("P2", 3, theFolder);
        SaveReader("P3", 4, theFolder);
        SaveReader("P4", 5, theFolder);
        SaveReader("P1Monster", 6, theFolder);
        SaveReader("P2Monster", 7, theFolder);
        SaveReader("P3Monster", 8, theFolder);
        SaveReader("P4Monster", 9, theFolder);
        SaveReader("magician", 10, theFolder);
        SaveReader("rice", 11, theFolder);
        SaveReader("samurai", 12, theFolder);
        SaveReader("undead", 13, theFolder);
        SaveReader("core", 14, theFolder);
    }

    //To write all objects to Jsons on the new folder
    public void WriteAllSave(string theFolder)
    {
        SaveWriter(-1, "fightended", 0, theFolder);
        SaveWriter(-1, "statusFight", 1, theFolder);
        SaveWriter(1, "P", 2, theFolder);
        SaveWriter(2, "P", 3, theFolder);
        SaveWriter(3, "P", 4, theFolder);
        SaveWriter(4, "P", 5, theFolder);
        SaveWriter(-1, "P1Monster", 6, theFolder);
        SaveWriter(-1, "P2Monster", 7, theFolder);
        SaveWriter(-1, "P3Monster", 8, theFolder);
        SaveWriter(-1, "P4Monster", 9, theFolder);
        SaveWriter(-1, "magician", 10, theFolder);
        SaveWriter(-1, "rice", 11, theFolder);
        SaveWriter(-1, "samurai", 12, theFolder);
        SaveWriter(-1, "undead", 13, theFolder);
        SaveWriter(-1, "core", 14, theFolder);
    }

    //Writes jsons  to main files
    //0:FightEnd ,1:statusFight,2:P1,3:P2,4:P3,5:P4,6:P1Monster,7:P2Monster,8:P3Monster,9:P4Monster,10:magician ,11:rice, 12:samurai, 13:undead,14:core
    public void UltraWriter(int nummer,string jsonName, int objectNumber)
    {
        filenamec = nummer > -1 ? jsonName +nummer+ ".json" :  jsonName + ".json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, serializeObj(objectNumber));
    }

    public void UltraReader(string jsonName, int objectNumber)
    {
        filenamec = jsonName + ".json";
        path1 = Application.persistentDataPath + "/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        deSerializeObj(objectNumber);
    }

    //Writes jsons  to main files
    //0:FightEnd ,1:statusFight,2:P1,3:P2,4:P3,5:P4,6:P1Monster,7:P2Monster,8:P3Monster,9:P4Monster,10:magician ,11:rice, 12:samurai, 13:undead,14:core
    public void SaveWriter(int nummer, string jsonName, int objectNumber, string folder)
    {
        filenamec = nummer > -1 ? jsonName + nummer + ".json" : jsonName + ".json";
        path1 = Application.persistentDataPath + "/" + folder + "/" + filenamec;
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, serializeObj(objectNumber));
    }

    public void SaveReader(string jsonName, int objectNumber, string folder)
    {
        filenamec = jsonName + ".json";
        path1 = Application.persistentDataPath + "/" + folder+"/" + filenamec;
        jsonString = System.IO.File.ReadAllText(path1);
        deSerializeObj(objectNumber);
    }

    //0:FightEnd ,1:statusFight,2:P1,3:P2,4:P3,5:P4,6:P1Monster,7:P2Monster,8:P3Monster,9:P4Monster,10:magician ,11:rice, 12:samurai, 13:undead,14:core
    public string serializeObj(int objectNumber)
    {
        switch (objectNumber)
        {
            case 0:
                jsonString = JsonConvert.SerializeObject(fightEnded, Formatting.Indented);
                break;
            case 1:
                jsonString = JsonConvert.SerializeObject(statusFight, Formatting.Indented);
                break;
            case 2:
                jsonString = JsonConvert.SerializeObject(P1, Formatting.Indented);
                break;
            case 3:
                jsonString = JsonConvert.SerializeObject(P2, Formatting.Indented);
                break;
            case 4:
                jsonString = JsonConvert.SerializeObject(P3, Formatting.Indented);
                break;
            case 5:
                jsonString = JsonConvert.SerializeObject(P4, Formatting.Indented);
                break;
            case 6:
                jsonString = JsonConvert.SerializeObject(P1Monster, Formatting.Indented);
                break;
            case 7:
                jsonString = JsonConvert.SerializeObject(P2Monster, Formatting.Indented);
                break;
            case 8:
                jsonString = JsonConvert.SerializeObject(P3Monster, Formatting.Indented);
                break;
            case 9:
                jsonString = JsonConvert.SerializeObject(P4Monster, Formatting.Indented);
                break;
            case 10:
                jsonString = JsonConvert.SerializeObject(magician, Formatting.Indented);
                break;
            case 11:
                jsonString = JsonConvert.SerializeObject(rice, Formatting.Indented);
                break;
            case 12:
                jsonString = JsonConvert.SerializeObject(samurai, Formatting.Indented);
                break;
            case 13:
                jsonString = JsonConvert.SerializeObject(undead, Formatting.Indented);
                break;
            case 14:
                jsonString = JsonConvert.SerializeObject(coreD, Formatting.Indented);
                break;
            default:
                break;
        }
        return jsonString;
    }

    //0:FightEnd ,1:statusFight,2:P1,3:P2,4:P3,5:P4,6:P1Monster,7:P2Monster,8:P3Monster,9:P4Monster,10:magician ,11:rice, 12:samurai, 13:undead,14:core
    public string deSerializeObj(int objectNumber)
    {
        switch (objectNumber)
        {
            case 0:
                fightEnded = JsonConvert.DeserializeObject<FightEnd>(jsonString);
                break;
            case 1:
                statusFight = JsonConvert.DeserializeObject<StatusForFight>(jsonString);
                break;
            case 2:
                P1 = JsonConvert.DeserializeObject<PlayerStatusGlobal>(jsonString);
                break;
            case 3:
                P2 = JsonConvert.DeserializeObject<PlayerStatusGlobal>(jsonString);
                break;
            case 4:
                P3 = JsonConvert.DeserializeObject<PlayerStatusGlobal>(jsonString);
                break;
            case 5:
                P4 = JsonConvert.DeserializeObject<PlayerStatusGlobal>(jsonString);
                break;
            case 6:
                P1Monster = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
                break;
            case 7:
                P2Monster = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
                break;
            case 8:
                P3Monster = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
                break;
            case 9:
                P4Monster = JsonConvert.DeserializeObject<EnemyClass>(jsonString);
                break;
            case 10:
                magician = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
                break;
            case 11:
                rice = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
                break;
            case 12:
                samurai = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
                break;
            case 13:
                undead = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
                break;
            case 14:
                coreD = JsonConvert.DeserializeObject<CoreDatum>(jsonString);
                break;
            default:
                break;
        }
        return jsonString;
    }

}

public class CoreDatum
{
    public int algo = 0;

    public CoreDatum(int one)
    {
        algo = one;
    }
}

