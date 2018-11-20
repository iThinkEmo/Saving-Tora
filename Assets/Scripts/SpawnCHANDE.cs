using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Text;

public class SpawnCHANDE : MonoBehaviour {

    private GameManager gameManagerDelJuego;

    #region Characters
    bool bossed = false;
    public GameObject witch;
    public GameObject riceMonk;
    public GameObject deady;
    public GameObject thatSam;
    StatusMaker mk2 = new StatusMaker();
    public PlayerUber playerStats;
    public AudioClip[] BackMusic;
    public AudioSource BSource;
    public AudioClip[] sFX;
    public AudioSource sFXSource;
    [SerializeField] public Sprite[] pIcons;
    [SerializeField] public Sprite[] eIcons;
    [SerializeField] public Sprite[] bIcons;
    public Image thisframe;
    public Image enemyFrame;
    public Image playerLife;
    public Image enemyLife;
    string[] arr;
    [SerializeField]
    GameObject dialogText;
    bool criticalATK = false;
    [SerializeField]
    GameObject TextArea;
    string[] ultraTexter = {"",""};
    int ultraCounter = 0;
    int globalFun;
    int globalUnFun;
    int globalSoundsFun;
    bool fightmode = false;
    bool finished = false;
    int maT;
    StatusForFight sF;
    string usedItemName;
    bool deathMoon = false;
    int[] finalBosses = { 0, 1, 2 };
    #endregion

    #region Enemies

    //Overall Order should be: 
    //(Eyes,LILRobots, Turrets, Crocs, LAMEBots)
    //And in all clases they should be ordered by areas eg. (1,2,3,4)
    //Finally the bosses are to be in order of ascending power.
    //boss 0 is the weakest and the highest numbered boss is the strongest
    public List<GameObject> EnemyMeshes;
    public List<GameObject> Bosses;

    public EnemyClass enemyFight;
    public List<PlayableDirector> playableEnemies;
    public List<TimelineAsset> timelinesEnemies;
    public GameObject theOneMonster;
    #endregion

    #region timeLine
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public PlayableDirector pMon;
    public TimelineAsset tiMon;
    public GameObject theOneInstantiated;
    public GameObject theOneMagic;
    #endregion

    #region Areas
    public GameObject area1;
    public GameObject area2;
    public GameObject area3;
    public GameObject area4;
    public GameObject factory;
    public GameObject MainLight;
    #endregion

    #region Magi
    //0:"Basic Magic", 1:"Fire", 2:"Blizzard", 3:"Shock", 4:"Natura", 5:"CIRCLE OF MAGIC"
    public List<GameObject> magiArr;
    #endregion

    FightMg fightMg;
    private GameObject Menu;
	public bool MenuBool=false;
    int monsterAnimNum;

    // Use this for initialization
    void Start()
    {
        // blueMoon = true;
        // if (blueMoon)
        // {
        //     var lt = MainLight.GetComponent<Light>();
        //     lt.color = Color.blue;
        // }
        gameManagerDelJuego = GameManager.Instance;
        StatusMaker mk1 = new StatusMaker();
        sF = mk1.FightGetter();
        //for intializing scripts
        mk1.InitFight();
        int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
        PlayBMusic();
        Invoke("MonstersIdle", 1.18f);
        //Gets the player that summoned the fight
        playerStats = gameManagerDelJuego.GetPlayerUber();
        //Gets player enemy
        enemyFight = mk1.ToContinueJSONFight(plaier);
        //mk1.MakeAndPostJSONFight();
        //mk1.ToprepareTheContinuedFight(plaier);
        if (enemyFight.hp<=0)
        {
            enemyFight.moNo = 0;
        }

        ImagePlayer(plaier - 1);

        //ToSpawn player and to check if monster was previously spawned
        //(0: Eye,1: LILRobots,2: Turrets,3: Crocs,4: LAMEBots)
        SpawnChar(plaier);
        int spawnable;
        if (enemyFight.moNo != 0)
        {
            if (enemyFight.moNo < 5)
            {
                Debug.Log(enemyFight.moNo);
                spawnable =0;
            }
            else if (enemyFight.moNo < 9 && enemyFight.moNo >=5)
            {
                spawnable = 1;
            }
            else if (enemyFight.moNo < 13 && enemyFight.moNo >= 9)
            {
                spawnable =2;
            }
            else if (enemyFight.moNo < 17 && enemyFight.moNo >= 13)
            {
                spawnable = 3;
            }
            else
            {
                spawnable = 4;
            }
        }
        else
        {
            spawnable = -1;
        }
        //ToSpawn enemy and to check if monster was previously spawned
        //(0: Eye,1: LILRobots,2: Turrets,3: Crocs,4: LAMEBots)
        int u = SpawnEnemy(sF.area, sF.typeOfFight,spawnable);
        CheckIfContinued(plaier, u);
        selectArea(sF.area, sF.typeOfFight);
        //ForTheInitialJson();
        //UpdateHealthBar(playerStats, enemyFight);
        fightMg = GameObject.Find("ButtonsFight").GetComponent<FightMg>();

        Menu = GameObject.Find("MenuItems");
        Menu.SetActive(false);
        fightMg.gameManagerDelJuego = this.gameManagerDelJuego;
        fightMg.player = this.playerStats;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the player that summoned the fight
        if (!fightmode)
        {
            updaterE();
            updaterP();
        }

        if (isDead() && !finished)
        {
            CancelInvoke();
            DeadAnim();
            ultraTexter[1] = "You fainted...";
            ultraTexter[0] = "You fainted...";
            Invoke("UltraTexting", 0.1F);
            Invoke("FinishedAndDead", 2.5F);
            finished = true;
        }
        if (isWin() && !finished)
        {
            CancelInvoke();
            WinAnim();
            ultraTexter[1] = "Monster fainted!";
            ultraTexter[0] = "Monster fainted!";
            Invoke("UltraTexting", 0.1F);
            Invoke("FinishedAndWin", 3.5F);
            finished = true;
        }
    }

    #region calculators

    //Areas : 1,2,3,4
    //a  = Area         : To know which monster to spawn based on the area
    //tOF= Type of Fight: 0:Normal, 1:BOSS, 2:ULTRABOSS
    public void selectArea(int area, int typeF)
    {
        if (typeF == 0)
        {
            switch (area)
            {
                case 1:
                    area1.SetActive(true);
                    break;
                case 2:
                    area2.SetActive(true);
                    break;
                case 3:
                    area3.SetActive(true);
                    break;
                case 4:
                    area4.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            factory.SetActive(true);
        }
    }

    //to go to LV screen dead
    public void FinishedAndDead()
    {
        List<PlayerStatusGlobal> mySuperList = mk2.GetUltrajson();
        //Gets the player that summoned the fight
        int i = mk2.FightGetter().currentPlayer - 1;
        Debug.Log("summoned: " + i);
        mySuperList[i].dead = 2;
        mk2.setUltrajson(mySuperList);
        GameManager gameManagerDelJuego = GameManager.Instance;
        int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
        int fanses = -FansLoster();
        int moneys = -MoneyLoster();
        mk2.SetEnd(plaier, 0, fanses, moneys, 2, false);

        enemyFight.hp = 0;
        enemyFight.moNo = 0;
        mk2.SetMonster(plaier, enemyFight);

        gameManagerDelJuego.NombreNivelQueSeVaCargar = "lvUP";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    //To go to LV screen Win
    public void FinishedAndWin()
    {
        Debug.Log("fight: "+mk2.fStat.typeOfFight);
        List<PlayerStatusGlobal> mySuperList = mk2.GetUltrajson();
        //Gets the player that summoned the fight
        int i = mk2.FightGetter().currentPlayer - 1;
        Debug.Log("summoned: " + i);
        mySuperList[i].win = true;
        mk2.setUltrajson(mySuperList);
        GameManager gameManagerDelJuego = GameManager.Instance;
        int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
        int fanses = FansLoster();
        int moneys = MoneyLoster();
        mk2.SetEnd(plaier, ExpCalculator(), fanses, moneys, 0, true);

		if (mk2.fStat.typeOfFight == 1) {
			gameManagerDelJuego.wonBossFigth = true;
			Saviour sv = gameManagerDelJuego.GetCurrentSaviour();
			int node = sv.currentNode;
			//se esta seteando al nodo correspondiente de la factory 
			//el número de jugador que ganó
			//versión Aldo
			gameManagerDelJuego.bossWinners[node] = gameManagerDelJuego.GetCurrentPlayer();
			Debug.Log("Winner: " + gameManagerDelJuego.bossWinners[node]);
            int area = gameManagerDelJuego.GetCurrentPlayerArea();
            gameManagerDelJuego.areaCleared[area] = true;

            // Aquí hay que validar que si estamos en el área 4,
            // deben ser tres fábricas las que hay por derrotar
		}

        fightMg.ChangeOnDutyStatus(status:false);

		gameManagerDelJuego.NombreNivelQueSeVaCargar = "lvUP";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
		
    }

    //To calculate money lost
    public int MoneyLoster()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.fans * 0.25));
        return one;
    }

    //Toggle calculate EXp
    public int ExpCalculator()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.maxExp * 0.32));
        return one;
    }

    //To calculate Fans
    public int FansLoster()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.fans * 0.30));
        return one;
    }

    //To update the player
    public void updaterP()
    {
        int mo = mk2.FightGetter().currentPlayer;
        playerStats = gameManagerDelJuego.GetPlayerUber();
        enemyFight = mk2.ToContinueJSONFight(mo);
        UpdateHealthBar(playerStats, enemyFight);
    }

    //To update the Enemy
    public void updaterE()
    {
        int mo = mk2.FightGetter().currentPlayer;
        playerStats = gameManagerDelJuego.GetPlayerUber();
        enemyFight = mk2.ToContinueJSONFight(mo);
        UpdateHealthBarE(playerStats, enemyFight);
    }

    #endregion

    #region Items

    public void itemSelected()
    {
        Debug.Log("Item Selected");
        TextArea.SetActive(true);
        ItemAnim();
        arr = fightMg.ITEMER();
        ultraTexter[1] = arr[3];
        Invoke("itemNamer", 0.1F);
        Invoke("UltraTexting", 0.2F);
        Invoke("updaterP", 1F);
        globalFun = Int32.Parse(arr[2]);
        ItemAnim();
        maT = Int32.Parse(arr[0]);
        Invoke("EnemyAnimater", 2.5F);
        Invoke("UltraTexting", 2.6F);
        Invoke("healther",3.5F);
        HPTimeCaller(globalFun, 3.6F);
        //if(true)
        if (sF.typeOfFight == 1)
        {
            Invoke("ToContinue", 7F);
        }
        else
        {
            Invoke("ToBeContinued", 7F);
        }
    }

    public void itemNamer()
    {
        ultraTexter[0] = arr[1] + usedItemName;
    }

    public void IS()
    {
        playerStats = gameManagerDelJuego.GetPlayerUber();
        int itemno = fightMg.itemSelected();
        playerStats.hp += playerStats.UseItem(itemno);
        usedItemName = playerStats.GetItemName(itemno);
        Debug.Log("used item"+ usedItemName);
        mk2.SetPlayer(PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer()), playerStats);
      //  Debug.Log("NEW HP in Spwan" + playerStats.hp);
    }

    public void healther()
    {
        playerStats = gameManagerDelJuego.GetPlayerUber();
        playerStats.hp -= maT;
        mk2.SetPlayer(PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer()), playerStats);
       // Debug.Log("NEW HP in Spwan" + playerStats.hp);
    }

    #endregion

    #region trueThings

    //to put player icon on its place
    public void ImagePlayer(int numplayer)
    {
        thisframe.sprite = pIcons[numplayer];
    }

    //Checks hp to see if it is 0, in that case initializes the monster because 
    //It has not begun to fight
    //if it is not 0 doesnt do anything
    public void CheckIfContinued(int playNum, int enemynum)
    {
        if (enemyFight.hp <= 0)
        {
            StatusMaker mk1 = new StatusMaker();
            enemyFight = mk1.InitMonster(playNum, enemynum);
        }
    }

    //Todetermiune dead
    public bool isDead()
    {
        if (playerLife.fillAmount <= 0.0F)
        {
            return true;
        }
        return false;
    }

    //to determine win
    public bool isWin()
    {
        if (enemyLife.fillAmount <= 0.0F)
        {
            return true;
        }
        return false;
    }

    // 1:Witch, 2:RiceMonk, 3:Samurai, 4:Undead
    private void SpawnChar(int charM8)
    {
        switch (charM8)
        {
            case 1:
                theOneInstantiated = Instantiate(witch, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 2:
                theOneInstantiated = Instantiate(thatSam, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 3:
                theOneInstantiated = Instantiate(deady, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            case 4:
                theOneInstantiated = Instantiate(riceMonk, new Vector3(64f, -6.4f, 35f), Quaternion.Euler(0.0f, 75.0f, 0.0f));
                break;
            default:
                break;
        }
    }

    //Areas : 1,2,3,4
    //a  = Area         : To know which monster to spawn based on the area
    //(0: Eye,1: LILRobots,2: Turrets,3: Crocs,4: LAMEBots)
    //tOF= Type of Fight: 0:Normal, 1:BOSS
    private int SpawnEnemy(int areaM8, int typeOfFight, int spawns)
    {
        int tocomply = 0;
        int toReturn = 0;
        int trueRand = spawns != -1 ? spawns : UnityEngine.Random.Range(0, 5);
        Debug.Log("TrueRand: " + trueRand);

        if (typeOfFight == 0)
        {
            tocomply = areaM8 - 1;
            switch (trueRand)
            {
                case 0:
                    //Eyes
                    theOneMonster = Instantiate(EnemyMeshes[0 + tocomply], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -50.0f, 0.0f));
                    if (areaM8==1)
                    { theOneMonster.transform.localScale = new Vector3(78F, 78F, 78F);}
                    toReturn = 0 + tocomply + 1;
                    enemyFrame.sprite = eIcons[0 + tocomply];
                    break;
                case 1:
                    //LilBots
                    theOneMonster = Instantiate(EnemyMeshes[4 + tocomply], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -100.0f, 0.0f));
                    toReturn = 4 + tocomply + 1;
                    enemyFrame.sprite = eIcons[4 + tocomply];
                    break;
                case 2:
                    //Turrets
                    theOneMonster = Instantiate(EnemyMeshes[8 + tocomply], new Vector3(71.43f, -5.68f, 36.38f), Quaternion.Euler(0.0f, 0f, -90f));
                    theOneMonster.transform.localScale = new Vector3(0.5902917F, 0.5902917F, 0.5902917F);
                    toReturn = 8 + tocomply + 1;
                    enemyFrame.sprite = eIcons[8 + tocomply];
                    break;
                case 3:
                    //Crocs
                    theOneMonster = Instantiate(EnemyMeshes[12 + tocomply], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -100.0f, 0.0f));
                    toReturn = 12 + tocomply +1;
                    enemyFrame.sprite = eIcons[12 + tocomply];
                    break;
                case 4:
                    //LAMEBots
                    theOneMonster = Instantiate(EnemyMeshes[16 + tocomply], new Vector3(72.93f, -5.76f, 37f), Quaternion.Euler(0.0f, -133.313f, 0.0f));
                    //theOneMonster = Instantiate(EnemyMeshes[16 + tocomply], new Vector3(0, -0, 0), Quaternion.Euler(0.0f, -0, 0.0f));
                    toReturn = 16 + tocomply + 1;
                    enemyFrame.sprite = eIcons[16 + tocomply];
                    break;
                default:
                    break;
            }
            monsterAnimNum = trueRand;
        }
        else if (typeOfFight == 1)
        {
            tocomply = areaM8 - 1;
            //Bosses: (0:Natalia,1:BIGLAM,2:Silene,3:meKPurple,4:redMeK,5:ULTIMATEROBOT)
            switch (tocomply)
            {
                //NATALIA
                case 0:
                    theOneMonster = Instantiate(Bosses[0], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -50.0f, 0.0f));
                    toReturn = 21;
                    enemyFrame.sprite = bIcons[0];
                    monsterAnimNum = 5;
                    break;
                //BIGLAM
                case 1:
                    theOneMonster = Instantiate(Bosses[1], new Vector3(72.02f, -3.9f, 36.6f), Quaternion.Euler(0.0f, 246.653f, 0.0f));
                    theOneMonster.transform.localScale = new Vector3(11.24931F, 11.24931F, 11.24931F);
                    toReturn = 22;
                    enemyFrame.sprite = bIcons[1];
                    monsterAnimNum = 6;
                    break;
                //Silene
                case 2:
                    theOneMonster = Instantiate(Bosses[2], new Vector3(72.78f, -5.63f, 37f), Quaternion.Euler(-3.5f, -182.632f, 11.339f));
                    theOneMonster.transform.localScale = new Vector3(22.5387F, 22.5387F, -22.5387F);
                    toReturn = 23;
                    enemyFrame.sprite = bIcons[2];
                    monsterAnimNum = 7;
                    break;
                case 3:
                    if (finalBosses[0] != 0)
                    {
                        //meKPurple
                        theOneMonster = Instantiate(Bosses[3], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -100.0f, 0.0f));
                        toReturn = 24;
                        enemyFrame.sprite = bIcons[3];
                        monsterAnimNum = 8;
                       // finalBosses[0] = 0;
                    }
                    else if (finalBosses[1] == 0)
                    {
                        //redMeK
                        theOneMonster = Instantiate(Bosses[4], new Vector3(72.96f, -6.43f, 37f), Quaternion.Euler(0.0f, -135.504f, 0.0f));
                        theOneMonster.transform.localScale = new Vector3(2.359379F, 2.359379F, 2.359379F);
                        toReturn = 25;
                        enemyFrame.sprite = bIcons[4];
                       // finalBosses[1] = 0;
                        monsterAnimNum = 9;
                    }
                    else
                    {
                        //ULTIMATEROBOT
                        theOneMonster = Instantiate(Bosses[5], new Vector3(73.909f, -6.45f, 37.324f), Quaternion.Euler(0.0f, -77.777f, 0.0f));
                        theOneMonster.transform.localScale = new Vector3(2.0282F, 2.0282F, 2.0282F);
                        toReturn = 26;
                        enemyFrame.sprite = bIcons[5];
                        monsterAnimNum = 10;
                    }
                    break;
                default:
                    break;
            }
        }
        return toReturn;
    }

    //To fill up the jsons to be read
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
        var jsonString = JsonConvert.SerializeObject(myP1, Formatting.Indented);
        Debug.Log(path1);
        System.IO.File.WriteAllText(path1, jsonString);
        jsonString = JsonConvert.SerializeObject(myP2, Formatting.Indented);
        Debug.Log(path2);
        System.IO.File.WriteAllText(path2, jsonString);
        jsonString = JsonConvert.SerializeObject(myP3, Formatting.Indented);
        Debug.Log(path3);
        System.IO.File.WriteAllText(path3, jsonString);
        jsonString = JsonConvert.SerializeObject(myP4, Formatting.Indented);
        Debug.Log(path4);
        System.IO.File.WriteAllText(path4, jsonString);
        //PlayerUber newPlayer = JsonConvert.DeserializeObject<PlayerUber>(jsonString);
        //jsonString = JsonConvert.SerializeObject(newPlayer, Formatting.Indented);
    }

    //See method below
    public void UpdateHealthBar(PlayerUber p1, EnemyClass p2)
    {
        UpdateHPUser(p1);
    }

    //See method below
    public void UpdateHealthBarE(PlayerUber p1, EnemyClass p2)
    {
        UpdateHPEnemy(p2);
    }

    //To update user health bar
    public void UpdateHPUser(PlayerUber p1)
    {
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpMeFight");
        if (dialogText)
        {
            if (p1.hp<0)
            {
                p1.hp = 0;
            }
            dialogText.gameObject.GetComponent<Text>().text = p1.hp + "/" + p1.maxhp;
        }
        playerLife.fillAmount = p1.HPPercentage();
    }

    //To update enemy healthbar
    public void UpdateHPEnemy(EnemyClass p1)
    {
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpEnemyFight");
        if (dialogText)
        {
            if (p1.hp < 0)
            {
                p1.hp = 0;
            }
            dialogText.gameObject.GetComponent<Text>().text = p1.hp + "/" + p1.maxHp;
        }
        enemyLife.fillAmount = p1.HPPercentage();
    }

    //sy 0 hospital, 1 exp, 2 flee
    public void ReturnToMain()
    {
        List<PlayerStatusGlobal> mySuperList = mk2.GetUltrajson();
        //Gets the player that summoned the fight
        int i = mk2.FightGetter().currentPlayer - 1;
        Debug.Log("summoned: " + i);

        mk2.setUltrajson(mySuperList);
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    //To play background music
    void PlayBMusic()
    {
        if (sF.typeOfFight == 0)
        {
            BSource.clip = BackMusic[0];
            BSource.Play();
        }
        else
        {
            BSource.clip = BackMusic[1];
            BSource.Play();
        }

    }

    //To playbossloop
    public void PlayLoopR()
    {
        BSource.clip = BackMusic[3];
        BSource.Play();
    }

    //To playbosstheme
    public void PlayTheme()
    {
        BSource.clip = BackMusic[2];
        BSource.Play();
        Invoke("PlayLoopR",BSource.clip.length);
    }

    #endregion

    #region Buttons

    //To open the items menu
    public void ItemsB()
    {
        Menu.SetActive(true);
        fightmode = false;
        fightMg.ItemsButton();
        TextArea.SetActive(false);
    }

    //To Invoque the attack sequence via the button
    public void AttackB()
    {
        fightmode = true;
        float trueVV = 0;

        if (gameManagerDelJuego.deadMoonPhase){
            arr = fightMg.AttackButton(1);
            BlueAttack(Int32.Parse(arr[4]));
        }
        else
        {
            arr = fightMg.AttackButton();
            Invoke("soundFXPlayer", 1F);
            AttackAnim();
        }
        ultraTexter[0] = arr[1];
        ultraTexter[1] = arr[3];
        HitChooser(ultraTexter[0]);
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);

        if (globalFun != 1)
        {
            //Invoke("soundFXPlayer", 2.5F);
            Invoke("updaterE", 2.8F);
            //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
            if (globalSoundsFun == 7)
            {
                Invoke("HitHit", 2.5F);
            }
            else if (globalSoundsFun == 8)
            {
                Invoke("HitCritical", 2.5F);
            }
            else
            {
                Invoke("HitMiss", 2.5F);
            }
            float enemyTime = 4F;
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
            if (globalFun == 3)
            {
                Invoke("EnemyHit", enemyTime +2.5F);
            }
            else if (globalFun == 4)
            {
                Invoke("EnemyCritical", enemyTime + 2.5F);
            }
            else
            {
                Invoke("EnemyMiss", enemyTime + 2.5F);
            }
            HPTimeCaller(globalFun, enemyTime+.6f);
            trueVV = enemyTime +4;
        }
        else
        {
            float enemyTime = 2.5F;
            Invoke("soundFXPlayer", enemyTime);
            Invoke("updaterE", enemyTime +0.1F);
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime+2.5f;
        }
        //if(true)
        if (sF.typeOfFight==1)
        {
            Invoke("ToContinue", trueVV);
        }
        else
        {
            Invoke("ToBeContinued", trueVV);
        }
    }

    //To Invoque the defence sequence via the button
    public void DefenceB()
    {
        fightmode = true;
        arr = fightMg.DFenceButton();
        ultraTexter[0] = arr[3];
        ultraTexter[1] = arr[1];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        EnemyAnimater();
        HPTimeCaller(globalFun, 0.6f);

        Invoke("DefendAnimation", 2.5F);
        Invoke("UltraTexting", 2.6F);

        if (sF.typeOfFight == 1)
        {
            Invoke("ToContinue", 5f);
        }
        else
        {
            Invoke("ToBeContinued", 5f);
        }
    }

    //To Invoque the magi sequence via the button
    public void MagiB()
    {
        fightmode = true;
        float trueVV = 0;
        Invoke("magiSound", 0.5F);
        if (gameManagerDelJuego.deadMoonPhase)
        {
            BlueMagic();
            arr = fightMg.MagicButton(1);
        }
        else
        {
            MagiChooser();
            arr = fightMg.MagicButton();
        }
        ultraTexter[0] = gameManagerDelJuego.deadMoonPhase == true ? "Too much magic!" : arr[1];
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        MagicAnim();

        if (globalFun != 1)
        {
            Invoke("soundFXPlayer", 2F);
            Invoke("updaterE", 2.1F);
            if (!gameManagerDelJuego.deadMoonPhase)
            {
                Invoke("MagicHit", 2F);
            }
            float enemyTime = 4.5F;
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
            if (globalFun == 3)
            {
                Invoke("EnemyHit", enemyTime + 2.5F);
            }
            else if (globalFun == 4)
            {
                Invoke("EnemyCritical", enemyTime + 2.5F);
            }
            else
            {
                Invoke("EnemyMiss", enemyTime + 2.5F);
            }
            HPTimeCaller(globalFun, enemyTime + .8f);
            trueVV = enemyTime + 4;
        }
        else
        {
            Invoke("soundFXPlayer", 2F);
            Invoke("updaterE", 2.1F);
            Invoke("MagicDef", 2F);
            float enemyTime = 2F;
            Invoke("UltraTexting", enemyTime+0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime + 2.5f;
        }
        //if(true)
        if (sF.typeOfFight == 1)
        {
            Invoke("ToContinue", trueVV);
        }
        else
        {
            Invoke("ToBeContinued", trueVV);
        }
    }

    //To activate the: "To be continued" Sequence 
    public void ToBeContinued(){
        fightMg.ChangeOnDutyStatus(status:true);

        ultraTexter[0] = "Time is up!";
        UltraTexting();
        PlayableDirector director = playableDirectors[9];
        IEnumerable<TrackAsset> tr = timelines[9].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
        Invoke("ReturnToMain", 2F);
    }

    //To activate the: "continued" Sequence 
    public void ToContinue()
    {
        if (!bossed)
        {
            PlayTheme();
            bossed = true;
        }
        ultraTexter[0] = "NO ESCAPE!\nWIN OR LOSE.";
        ultraTexter[1] = "NO ESCAPE!\nWIN OR LOSE.";
        UltraTexting();
        UltraTexting();
        PlayableDirector director = playableDirectors[16];
        IEnumerable<TrackAsset> tr = timelines[16].GetOutputTracks();
        director.Play();
    }

    //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
    //To calculate time for health recalculation based on action 
    public void HPTimeCaller(int enemyA, float enmyT)
    {
        switch (enemyA)
        {
            case 1:
                Invoke("updaterP", enmyT);
                break;
            case 2:
                Invoke("updaterP", enmyT + 2.1F);
                break;
            case 3:
                Invoke("updaterP", enmyT + 2.1F);
                break;
            case 4:
                Invoke("updaterP", enmyT + 2.1F);
                break;
            case 5:
                Invoke("updaterP", enmyT + 2.1F);
                break;
            case 6:
                Invoke("updaterP", enmyT + 2.1F);
                break;
            default:
                break;
        }
    }

    //To play various sounds
    public void soundFXPlayer()
    {
        sFXSource.clip = sFX[globalSoundsFun];
        sFXSource.Play();
    }

    //To play magi summon sound
    public void magiSound()
    {
        sFXSource.clip = sFX[5];
        sFXSource.Play();
    }

    //To activate que button is pressed
    public void FleeB()
    {
        fightmode = true;
        arr = fightMg.Flee();
        ultraTexter[0] = arr[1];
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        if (sF.typeOfFight == 1)
        {
            if (!bossed)
            {
                PlayTheme();
                bossed = true;
            }
            ultraTexter[0] = "There's no escape from a BOSS!";
            FailAnimation();
            Invoke("EnemyAnimater", 2.5F);
            Invoke("UltraTexting", 2.6F);
            //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
            float eT = 0f;
            if (globalFun == 3)
            {
                eT = 5f;
                Invoke("EnemyHit", eT);
            }
            else if (globalFun == 4)
            {
                eT = 5f;
                Invoke("EnemyCritical", eT);
            }
            else if(globalFun == 5|| globalFun == 2)
            {
                eT = 5f;
                Invoke("EnemyMiss", eT);
            }
            if (eT>0.0f)
            {
                HPTimeCaller(globalFun, eT-2 + 0.4f);
                eT = 1.5f;
            }
            Invoke("ToContinue", eT+5f);
        }
        else
        {
            if (Int32.Parse(arr[0]) == 6)
            {
                FailAnimation();
                Invoke("EnemyAnimater", 2.5F);
                Invoke("UltraTexting", 2.6F);
                //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
                float eT = 0f;
                if (globalFun == 3)
                {
                    eT = 5f;
                    Invoke("EnemyHit", eT);
                }
                else if (globalFun == 4)
                {
                    eT = 5f;
                    Invoke("EnemyCritical", eT);
                }
                else if (globalFun == 5)
                {
                    eT = 5f;
                    Invoke("EnemyMiss", eT);
                }
                if (eT > 0.0f)
                {
                    HPTimeCaller(globalFun, eT-2 + 0.4f);
                    eT = 1.5f;
                }
                Invoke("ToContinue", eT + 5f);
            }
            else
            {
                fleeAnimation();
                enemyFight.hp = 0;
                enemyFight.moNo = 0;
                int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
                mk2.SetMonster(plaier, enemyFight);
                //Return to world map
                mk2.SetEnd(plaier, 0, 0, 0, 0, true);
                Invoke("FleeReturner", 6);
            }
        }
    }

    //To return to main without going into the XP area
    public void FleeReturner()
    {
        ReturnToMain();
    }

    //To quit the menu
    public void quitItemsMenu()
    {
        fightMg.quitMenu();
        TextArea.SetActive(true);
        fightmode = true;
    }

    #endregion

    #region Timeline

    public void WinAnim()
    {
        PlayableDirector director = playableDirectors[8];
        IEnumerable<TrackAsset> tr = timelines[8].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //Toget the monsters Idleanim
    public void MonstersIdle()
    {
        PlayableDirector director = pMon;
        IEnumerable<TrackAsset> tr = tiMon.GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == monsterAnimNum)
            {
                director.SetGenericBinding(item, theOneMonster);
            }
            cc++;
        }
        director.Play();
    }

    public void DeadAnim()
    {
        PlayableDirector director = playableEnemies[4];
        IEnumerable<TrackAsset> tr = timelinesEnemies[4].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //To animate itemtaking
    public void ItemAnim()
    {
        PlayableDirector director = playableDirectors[10];
        IEnumerable<TrackAsset> tr = timelines[10].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //To control text of fight
    public void UltraTexting()
    {
        dialogText.gameObject.GetComponent<Text>().text = ultraTexter[ultraCounter];
        ultraCounter = ultraCounter > 0 ? 0 : 1;
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void AttackAnim()
    {

        PlayableDirector director = playableDirectors[0];
        IEnumerable<TrackAsset> tr = timelines[0].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //Enemy hits you 
    public void EnemyHit()
    {
        PlayableDirector director = playableDirectors[13];
        IEnumerable<TrackAsset> tr = timelines[13].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //Enemy smashes you
    public void EnemyCritical()
    {
        PlayableDirector director = playableDirectors[14];
        IEnumerable<TrackAsset> tr = timelines[14].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //Enemy misses attack
    public void EnemyMiss()
    {
        PlayableDirector director = playableDirectors[15];
        IEnumerable<TrackAsset> tr = timelines[15].GetOutputTracks();
        director.Play();
    }

    //Animation for when magic power gets countered by a defence
    public void MagicDef()
    {
        PlayableDirector director = playableDirectors[6];
        IEnumerable<TrackAsset> tr = timelines[6].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 3)
            {
                director.SetGenericBinding(item, theOneMagic);
            }
            cc++;
        }
        director.Play();
    }

    //Animation for when magic power hits
    public void MagicHit()
    {
        PlayableDirector director = playableDirectors[5];
        IEnumerable<TrackAsset> tr = timelines[5].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 3)
            {
                director.SetGenericBinding(item, theOneMagic);
            }
            cc++;
        }
        director.Play();
    }


    //EnemieWasHit
    public void HitHit()
    {
        PlayableDirector director = playableDirectors[7];
        IEnumerable<TrackAsset> tr = timelines[7].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneMonster);
            }
            cc++;
        }
        director.Play();
    }

    //EnemieWasHit
    public void HitMiss()
    {
        PlayableDirector director = playableDirectors[12];
        IEnumerable<TrackAsset> tr = timelines[12].GetOutputTracks();
        director.Play();
    }

    //EnemyWasCriticalHit
    public void HitCritical()
    {
        PlayableDirector director = playableDirectors[11];
        IEnumerable<TrackAsset> tr = timelines[11].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneMonster);
            }
            cc++;
        }
        director.Play();
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void MagicAnim()
    {
        PlayableDirector director = playableDirectors[3];
        IEnumerable<TrackAsset> tr = timelines[3].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            if (cc == 5)
            {
                director.SetGenericBinding(item, theOneMagic);
            }
            cc++;
        }
        director.Play();
    }


    //To choose magi sound and animation based on magic type
    public void MagiChooser()
    {
        //0:"Basic Magic",1:"Fire",2:"Blizzard",3:"Shock",4:"Natura"
        String thisMagi = playerStats.magica.GetName();
        if (thisMagi.Contains("Fire"))
        {
            theOneMagic = Instantiate(magiArr[1], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            globalSoundsFun = 1;
        }
        else if (thisMagi.Contains("Blizzard"))
        {
            theOneMagic = Instantiate(magiArr[2], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            globalSoundsFun = 2;
        }
        else if (thisMagi.Contains("Shock"))
        {
            theOneMagic = Instantiate(magiArr[3], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            globalSoundsFun = 3;
        }
        else if (thisMagi.Contains("Natura"))
        {
            theOneMagic = Instantiate(magiArr[4], new Vector3(72.96f, -16.4f, 37f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            globalSoundsFun = 4;
        }
        else
        {
            theOneMagic = Instantiate(magiArr[0], new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            globalSoundsFun = 0;
        }
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void HitChooser(String hititpe)
    {
        if (hititpe.Contains("Missed"))
        {
            globalSoundsFun = 6;

        }
        else if (hititpe.Contains("Attacked"))
        {
            globalSoundsFun = 7;

        }
        else
        {
            globalSoundsFun = 8;
        }
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void fleeAnimation()
    {
        PlayableDirector director = playableDirectors[1];
        IEnumerable<TrackAsset> tr = timelines[1].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void FailAnimation()
    {
        PlayableDirector director = playableDirectors[4];
        IEnumerable<TrackAsset> tr = timelines[4].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee, 7: flee)
    public void DefendAnimation()
    {
        PlayableDirector director = playableDirectors[2];
        IEnumerable<TrackAsset> tr = timelines[2].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
    public void EnemyAnimater()
    {
        int animType = SomeAnimator(globalFun);
        PlayableDirector director = playableEnemies[animType];
        IEnumerable<TrackAsset> tr = timelinesEnemies[animType].GetOutputTracks();
        int cc = 0;
        if (globalFun < 7 && globalFun > 2)
        { 
            foreach (var item in tr)
            {
                if (cc == 1)
                {
                    director.SetGenericBinding(item, theOneMonster);
                }
                cc++;
            }
        }
        director.Play();
    }

    //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed, 6:ded)
    //To be used solely on the monster, 
    public int SomeAnimator(int animType)
    {
        int animNum = 2;
        switch (animType)
        {
            case 1:
                animNum = 3;
                break;
            case 2:
                animNum = 0;
                break;
            case 3:
                animNum = 1;
                break;
            case 4:
                animNum = 2;
                break;
            case 5:
                animNum = 1;
                break;
            case 6:
                animNum = 4;
                break;
            default:
                animNum = 0;
                break;
        }
        return animNum;
    }

    #endregion

    #region BlueMoon
    
    //to play blue moon animation according to the character attack
    public void BlueAttack(int plaier)
    {
        //(1:Undead,2:Witch,3:Samurai,4:RiceMan)
        int directornum = 0;
        switch (plaier)
        {
            case 1:
                directornum = 18;
                break;
            case 2:
                directornum = 21;
                break;
            case 3:
                directornum = 19;
                break;
            case 4:
                directornum = 20;
                break;
            default:
                break;
        }
        PlayableDirector director = playableDirectors[directornum];
        IEnumerable<TrackAsset> tr = timelines[directornum].GetOutputTracks();
        int cc = directornum == 18 ? 2 : 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    //to play blue moon animation according to the character magi
    public void BlueMagic()
    {
        PlayableDirector director = playableDirectors[17];
        IEnumerable<TrackAsset> tr = timelines[17].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    #endregion
}
