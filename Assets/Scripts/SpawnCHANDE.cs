using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnCHANDE : MonoBehaviour {

    #region Characters

    public GameObject witch;
    public GameObject riceMonk;
    public GameObject deady;
    public GameObject thatSam;

    public PlayerUber playerStats;

    #endregion

    #region Enemies

    public GameObject tony;
    public GameObject eyeM8;
    public GameObject Silene;

    public EnemyClass enemyFight;
    #endregion

    // Use this for initialization
    void Start()
    {
        StatusMaker mk1 = new StatusMaker();
        StatusForFight sF = mk1.FightGetter();
        //for intializing scripts
        mk1.InitFight();
        int plaier = sF.currentPlayer;

        //Gets the player that summoned the fight
        playerStats = mk1.getPlayer(plaier);
        //Gets player enemy
        enemyFight = mk1.ToContinueJSONFight(plaier);
        //mk1.MakeAndPostJSONFight();
        //mk1.ToprepareTheContinuedFight(plaier);


        //ToSpawn player and to check if monster was previously spawned
        SpawnChar(plaier);

        //ToSpawn enemy and to check if monster was previously spawned
        int u = SpawnEnemy(sF.area, sF.typeOfFight);
        CheckIfContinued(plaier,u);
        //ForTheInitialJson();
    }

    //Checks hp to see if it is 0, in that case initializes the monster because 
    //It has not begun to fight
    //if it is not 0 doesnt do anything
    public void CheckIfContinued(int playNum,int enemynum)
    {
        if (enemyFight.hp <= 0)
        {
            StatusMaker mk1 = new StatusMaker();
            enemyFight = mk1.InitMonster(playNum, enemynum);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    //1:Witch, 2:Samurai,3:Undead,4: RiceMonk

    private void SpawnChar(int charM8)
    {
        switch (charM8)
        {
            case 1:
                Instantiate(witch, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 2:
                Instantiate(thatSam, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 3:
                Instantiate(deady, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 4:
                Instantiate(riceMonk, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            default:
                break;
        }
    }

    //Areas : 1,2,3,4
    //a  = Area         : To know which monster to spawn based on the area
    //tOF= Type of Fight: 0:Normal, 1:BOSS, 2:ULTRABOSS
    private int SpawnEnemy(int areaM8, int typeOfFight)
    {
        int toReturn = 0;
        int trueRand = UnityEngine.Random.Range(1,3);
        Debug.Log(trueRand);
        if (typeOfFight == 0)
        {
            switch (areaM8)
            {
                case 1:
                    switch (trueRand)
                    {
                        case 1:
                            Instantiate(tony, new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -50.0f, 0.0f));
                            toReturn = trueRand;
                            break;
                        case 2:
                            Instantiate(eyeM8, new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -50.0f, 0.0f));
                            toReturn = trueRand;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (typeOfFight == 1)
        {
        }
        else
        {
        }
        return toReturn;
    }

    public void ForTheInitialJson() 
    {
        var filenamec = "undead.json";
        var path1 = Application.persistentDataPath + "/" + filenamec;
        var filenamec2 = "magician.json";
        var path2 = Application.persistentDataPath + "/" + filenamec2;
        var filename3 = "samurai.json";
        var path3 = Application.persistentDataPath + "/" + filename3;
        var filename4 = "rice.json";
        var path4 = Application.persistentDataPath + "/" + filename4;


        PlayerUber myP1 = new PlayerUber(1);
        PlayerUber myP2 = new PlayerUber(2);
        PlayerUber myP3 = new PlayerUber(3);
        PlayerUber myP4 = new PlayerUber(4);
        var jsonString = JsonConvert.SerializeObject(myP1);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
        jsonString = JsonConvert.SerializeObject(myP2);
        Debug.Log(path2);
        System.IO.File.WriteAllText(path2, jsonString);
        jsonString = JsonConvert.SerializeObject(myP3);
        Debug.Log(path3);
        System.IO.File.WriteAllText(path3, jsonString);
        jsonString = JsonConvert.SerializeObject(myP4);
        Debug.Log(path4);
        System.IO.File.WriteAllText(path4, jsonString);
        //PlayerUber newPlayer = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
        //jsonString = JsonConvert.SerializeObject(newPlayer);


    }
}
