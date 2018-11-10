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
    int globalSoundsFun;
    bool fightmode = false;
    bool finished = false;
    int maT;
    string usedItemName;
    #endregion

    #region Enemies

    public GameObject tony;
    public GameObject eyeM8;
    public GameObject Silene;

    public EnemyClass enemyFight;
    public List<PlayableDirector> playableEnemies;
    public List<TimelineAsset> timelinesEnemies;
    #endregion

    #region timeLine
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public GameObject theOneInstantiated;
    public GameObject theOneMagic;
    #endregion

    #region Areas
    public GameObject area1;
    public GameObject area2;
    public GameObject area3;
    public GameObject area4;
    public GameObject factory;
    #endregion

    #region Magi
    //0:"Basic Magic", 1:"Fire", 2:"Blizzard", 3:"Shock", 4:"Natura", 5:"CIRCLE OF MAGIC"
    public List<GameObject> magiArr;
    #endregion

    FightMg fightMg;
    private GameObject Menu;
	public bool MenuBool=false;

    // Use this for initialization
    void Start()
    {
        gameManagerDelJuego = GameManager.Instance;
        PlayBMusic();
        StatusMaker mk1 = new StatusMaker();
        StatusForFight sF = mk1.FightGetter();
        //for intializing scripts
        mk1.InitFight();
        //int plaier = sF.currentPlayer;
        int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());

        //Gets the player that summoned the fight
        playerStats = gameManagerDelJuego.GetPlayerUber();
        //Gets player enemy
        enemyFight = mk1.ToContinueJSONFight(plaier);
        //mk1.MakeAndPostJSONFight();
        //mk1.ToprepareTheContinuedFight(plaier);

        ImagePlayer(plaier - 1);

        //ToSpawn player and to check if monster was previously spawned
        SpawnChar(plaier);

        //ToSpawn enemy and to check if monster was previously spawned
        int u = SpawnEnemy(sF.area, sF.typeOfFight);
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
            //DeadAnim();
            Invoke("FinishedAndDead", 0F);
            finished = true;
        }
        if (isWin() && !finished)
        {
            CancelInvoke();
            //WinAnim();
            Invoke("FinishedAndWin", 0F);
            finished = true;
        }
    }

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

    public void DeadAnim()
    {
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
    }

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
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "lvUP";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    public void FinishedAndWin()
    {
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
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "lvUP";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    public int MoneyLoster()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.fans * 0.25));
        return one;
    }

    public int ExpCalculator()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.maxExp * 0.32));
        return one;
    }

    public int FansLoster()
    {
        int one = Convert.ToInt32(Math.Round(playerStats.fans * 0.30));
        return one;
    }

    public void updaterP()
    {
        int mo = mk2.FightGetter().currentPlayer;
        playerStats = gameManagerDelJuego.GetPlayerUber();
        enemyFight = mk2.ToContinueJSONFight(mo);
        UpdateHealthBar(playerStats, enemyFight);
    }

    public void updaterE()
    {
        int mo = mk2.FightGetter().currentPlayer;
        playerStats = gameManagerDelJuego.GetPlayerUber();
        enemyFight = mk2.ToContinueJSONFight(mo);
        UpdateHealthBarE(playerStats, enemyFight);
    }

	public void itemSelected() {
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
        Invoke("ToBeContinued", 7F);
        Invoke("healther", 1.9F);
        HPTimeCaller(globalFun,2.5F);
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


    #region trueThings

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

    public bool isDead()
    {
        if (playerLife.fillAmount <= 0.0F)
        {
            return true;
        }
        return false;
    }

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
    //tOF= Type of Fight: 0:Normal, 1:BOSS, 2:ULTRABOSS
    private int SpawnEnemy(int areaM8, int typeOfFight)
    {
        int toReturn = 0;
        int trueRand = UnityEngine.Random.Range(1, 3);
        Debug.Log("TrueRand: " + trueRand);
        if (typeOfFight == 0)
        {
            switch (areaM8)
            {
                default:
                    switch (trueRand)
                    {
                        case 1:
                            Instantiate(tony, new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -100.0f, 0.0f));
                            toReturn = trueRand;
                            enemyFrame.sprite = eIcons[0];
                            break;
                        case 2:
                            Instantiate(eyeM8, new Vector3(72.96f, -6.4f, 37f), Quaternion.Euler(0.0f, -50.0f, 0.0f));
                            toReturn = trueRand;
                            enemyFrame.sprite = eIcons[1];
                            break;
                        default:
                            break;
                    }
                    break;
                    //default:
                    //    break;
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

    public void UpdateHealthBar(PlayerUber p1, EnemyClass p2)
    {
        UpdateHPUser(p1);
    }

    public void UpdateHealthBarE(PlayerUber p1, EnemyClass p2)
    {
        UpdateHPEnemy(p2);
    }

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

    void PlayBMusic()
    {
        //Should be a clip ere m8
        //int randClip = UnityEngine.Random.Range(0, stings.Length);
        BSource.clip = BackMusic[0];
        BSource.Play();
        //Invoke("PlayLoopR", BSource.clip.length);

    }

    public void PlayLoopR()
    {
        BSource.clip = BackMusic[1];
        BSource.Play();
    }

    #endregion


    #region Buttons

    public void ItemsB()
    {
        Menu.SetActive(true);
        fightmode = false;
        fightMg.ItemsButton();
        TextArea.SetActive(false);
    }

    public void AttackB()
    {
        fightmode = true;
        float trueVV = 0;
        arr = fightMg.AttackButton();
        ultraTexter[0] = arr[1];
        //ultraTexter[0] = "You Missed";
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting",0.1F);
        globalFun = Int32.Parse(arr[2]);
        HitChooser(ultraTexter[0]);
        Invoke("soundFXPlayer", 1F);
        AttackAnim();

        if (globalFun != 1)
        {
            Invoke("soundFXPlayer", 2.5F);
            Invoke("updaterE", 2.6F);
            Invoke("HitHit", 2.5F);
            float enemyTime = 5F;
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime;
        }
        else
        {
            float enemyTime = 2.5F;
            Invoke("soundFXPlayer", enemyTime);
            Invoke("updaterE", enemyTime +0.1F);
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime;
        }
        Invoke("ToBeContinued", trueVV + 2.5F);
    }

    public void DefenceB()
    {
        fightmode = true;
        arr = fightMg.DFenceButton();
        ultraTexter[0] = arr[1];
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        DefendAnimation();

        Invoke("EnemyAnimater", 2.5F);
        Invoke("UltraTexting", 2.6F);

        Invoke("ToBeContinued", 5F);

    }

    public void MagiB()
    {
        fightmode = true;
        float trueVV = 0;
        Invoke("magiSound", 0.5F);
        MagiChooser();
        arr = fightMg.MagicButton();
        ultraTexter[0] = arr[1];
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        MagicAnim();

        if (globalFun != 1)
        {
            Invoke("soundFXPlayer", 2F);
            Invoke("updaterE", 2.1F);
            Invoke("MagicHit", 2F);
            float enemyTime = 4.5F;
            Invoke("EnemyAnimater", enemyTime);
            Invoke("UltraTexting", enemyTime + 0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime;
        }
        else
        {
            Invoke("soundFXPlayer", 2F);
            Invoke("updaterE", 2.1F);
            Invoke("MagicDef", 2F);
            float enemyTime = 2F;
            Invoke("UltraTexting", enemyTime+0.1F);
            HPTimeCaller(globalFun, enemyTime);
            trueVV = enemyTime;
        }

        Invoke("ToBeContinued", trueVV + 2.5F);

    }

    public void ToBeContinued()
    {
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


    public void soundFXPlayer()
    {
        sFXSource.clip = sFX[globalSoundsFun];
        sFXSource.Play();
    }

    public void magiSound()
    {
        sFXSource.clip = sFX[5];
        sFXSource.Play();
    }

    public void FleeB()
    {
        fightmode = true;
        arr = fightMg.Flee();
        ultraTexter[0] = arr[1];
        ultraTexter[1] = arr[3];
        Invoke("UltraTexting", 0.1F);
        globalFun = Int32.Parse(arr[2]);
        if (Int32.Parse(arr[0]) == 6)
        {
            FailAnimation();
            Invoke("EnemyAnimater", 2.5F);
            Invoke("UltraTexting", 2.6F);
            Invoke("ToBeContinued", 5F);
        }
        else
        {
            fleeAnimation();
            //Return to world map
            int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
            mk2.SetEnd(plaier, 0, 0, 0, 0, true);
            Invoke("FleeReturner", 6);
        }


    }

    public void FleeReturner()
    {
        ReturnToMain();
    }


    public void quitItemsMenu()
    {
        fightMg.quitMenu();
        TextArea.SetActive(true);
        fightmode = true;
    }

    #endregion


    #region Timeline

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


    //Enemies anismssss
    public void HitHit()
    {
        PlayableDirector director = playableDirectors[7];
        IEnumerable<TrackAsset> tr = timelines[7].GetOutputTracks();
        //int cc = 0;
        //foreach (var item in tr)
        //{
        //    if (cc == 3)
        //    {
        //        director.SetGenericBinding(item, theOneMagic);
        //    }
        //    cc++;
        //}
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
        foreach (var item in tr)
        {
            if (cc == 1)
            {
                //director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
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

}
