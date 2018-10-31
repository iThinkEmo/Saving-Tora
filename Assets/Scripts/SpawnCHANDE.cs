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

public class SpawnCHANDE : MonoBehaviour {

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
    #endregion

    #region Enemies

    public GameObject tony;
    public GameObject eyeM8;
    public GameObject Silene;

    public EnemyClass enemyFight;
    #endregion

    #region timeLine
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public GameObject theOneInstantiated;
    #endregion

    FightMg fightMg = new FightMg();

    // Use this for initialization
    void Start()
    {
        PlayBMusic();
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

        ImagePlayer(plaier - 1);

        //ToSpawn player and to check if monster was previously spawned
        SpawnChar(plaier);

        //ToSpawn enemy and to check if monster was previously spawned
        int u = SpawnEnemy(sF.area, sF.typeOfFight);
        CheckIfContinued(plaier, u);
        //ForTheInitialJson();
        //UpdateHealthBar(playerStats, enemyFight);
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the player that summoned the fight
        int mo = mk2.FightGetter().currentPlayer;
        playerStats = mk2.getPlayer(mo);
        enemyFight = mk2.ToContinueJSONFight(mo);
        UpdateHealthBar(playerStats, enemyFight);
        if (isDead(playerStats.hp))
        {
            //Goto Hospital
            ReturnToMain(0);
        }
        if (isDead(enemyFight.hp))
        {
            //Goto exp
            ReturnToMain(1);
        }
    }

    #region trueThings

    public void ImagePlayer(int numplayer)
    {
        thisframe.sprite = pIcons[numplayer];
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

    public bool isDead(int currentHp)
    {
        if (currentHp <= 0)
        {
            return true;
        }
        return false;
    }

    //1:Witch, 2:Samurai,3:Undead,4: RiceMonk
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
        UpdateHPEnemy(p2);
    }

    public void UpdateHPUser(PlayerUber p1)
    {
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpMeFight");
        if (dialogText)
        {
            dialogText.gameObject.GetComponent<Text>().text = p1.hp + "/" + p1.maxhp;
        }
        playerLife.fillAmount = p1.HPPercentage();
    }

    public void UpdateHPEnemy(EnemyClass p1)
    {
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpEnemyFight");
        if (dialogText)
        {
            dialogText.gameObject.GetComponent<Text>().text = p1.hp + "/" + p1.maxHp;
        }
        enemyLife.fillAmount = p1.HPPercentage();
    }

    //sy 0 hospital, 1 exp, 2 flee
    public void ReturnToMain(int sy)
    {
        if (sy==1)
        {ExpMan();}
        List<PlayerStatusGlobal> mySuperList = mk2.GetUltrajson();
        //Gets the player that summoned the fight
        int i = mk2.FightGetter().currentPlayer - 1;
        if (sy == 0)
        {
            //Isdead gets two turns off
            mySuperList[i].dead = 2;
        }
        else if(sy == 1)
        {
            mySuperList[i].win = true;
        }

        mk2.setUltrajson(mySuperList);
        GameManager gameManagerDelJuego = GameManager.Instance;
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    public int ExpMan()
    {
        return 0;
    }

    void PlayBMusic()
    {
        //Should be a clip ere m8
        //int randClip = UnityEngine.Random.Range(0, stings.Length);
        BSource.clip = BackMusic[0];
        BSource.Play();
        Invoke("PlayLoopR", BSource.clip.length);

    }

    public void PlayLoopR()
    {
        BSource.clip = BackMusic[1];
        BSource.Play();
    }

    #endregion

    #region Timeline

    //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
    //StatUSER   (0: missed,1: normalATK,2:criticalATK,3:defence ,4: normal magi,5:critical magi ,6:  failed flee)

    public void PlayingWithTimeline()
    {
        PlayableDirector director = playableDirectors[0];
        IEnumerable<TrackAsset> tr = timelines[0].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc==1)
            {
                director.SetGenericBinding(item, theOneInstantiated);
            }
            cc++;
        }
        director.Play();
    }

    // should be when user flees 
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

    #endregion

    #region Buttons

    public void ItemsB()
    {
        fightMg.ItemsButton();
    }

    public void AttackB()
    {
        string[] arr = fightMg.AttackButton();

        foreach (var item in arr)
        {
            Debug.Log(item);

        }

        PlayingWithTimeline();
    }

    public void DefenceB()
    {
        string[] arr = fightMg.DFenceButton();
        foreach (var item in arr)
        {
            Debug.Log(item);

        }
    }

    public void MagiB()
    {
        string[] arr = fightMg.MagicButton();
        foreach (var item in arr)
        {
            Debug.Log(item);

        }
    }

    public void FleeB()
    {
        if (!CalculateFlee())
        {
            fleeAnimation();
            //Return to world map
            Invoke("FleeReturner", 6);
            
        }
        string[] arr = fightMg.Flee();

        foreach (var item in arr)
        {
            Debug.Log(item);

        }
    }

    public void FleeReturner()
    {
        ReturnToMain(2);
    }

    //To calculate if flee is successful
    public bool CalculateFlee()
    {
        System.Random randomizerMax = new System.Random();
        int rr = randomizerMax.Next(0, 100);
        if (rr > 60)
        {
            return true;
        }
        return false;
    }

    #endregion
}
