using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelUpManager : MonoBehaviour
{
    GameManager gameManagerDelJuego;
    StatusMaker sm = new StatusMaker();
    PlayerUber playerStats;
    bool levLUP = false;
    int oldLV;
    int newLV;
    int oldEXP;
    int oldMEXP;
    bool ultra = false;
    bool superLVLUP = false;
    FightEnd fEnd;
    public AudioClip[] BackMusic;
    public AudioSource BSource;
    #region Objects
    [SerializeField]
    GameObject Loser;
    [SerializeField]
    GameObject levelUpperBars;
    [SerializeField]
    Image Replaceable;
    [SerializeField]
    Sprite[] playerIcons;
    [SerializeField]
    GameObject deadman;
    [SerializeField]
    GameObject dialogText;
    [SerializeField]
    Image playerExp;
    [SerializeField]
    GameObject playerLevel;
    [SerializeField]
    GameObject theNurse;



    #endregion

    // Use this for initialization
    void Start ()
    {
        Invoke("Ultron", 3);
        gameManagerDelJuego = GameManager.Instance;
        fEnd = sm.GetEnd();
        playerStats = gameManagerDelJuego.GetPlayerUber();
        oldLV = playerStats.lv;
        newLV = playerStats.lv;
        oldEXP = playerStats.exp;
        oldMEXP = playerStats.maxExp;
        levLUP = LevelBool();
        IconsApplier();


        if (fEnd.win)
        {
            ExpApplier();
        }

        if (levLUP)
        {
            newLV = playerStats.lv+playerStats.NumLVUP(fEnd.exp);
        }

        FansApplier();
        MoneyApplier();
        WinLoseApplier();
        Texter();
    }

    void Update()
    {
        if (ultra)
        {
            ExpUpdater();
        }
    }

    //To show lv upper
    public void Texter()
    {
        string[] textsLVW = { "You Won!", " You got $" + fEnd.money + ", and ", fEnd.fans+" fans.","You got "+fEnd.exp +" EXP.","You leveled UP!" };
        string[] textsLVL = { "You Lost...", " You dropped $" + Math.Abs(fEnd.money) + ", and ", Math.Abs(fEnd.fans) + " fans stopped believing in you.", " Don't worry, we brought you to the closest hospital and we will heal you." };
        if (fEnd.win)
        {
            if (fEnd.exp == -1)
            {
                dialogText.gameObject.GetComponent<Text>().text = textsLVW[0] +"\n\n"+ textsLVW[1]  + textsLVW[2] + "\n\n" + textsLVW[4];
            }
            else if (levLUP)
            {
                dialogText.gameObject.GetComponent<Text>().text = textsLVW[0] + "\n\n" + textsLVW[1] + textsLVW[2] + textsLVW[3] + "\n\n" + textsLVW[4];
            }
            else
            {
                dialogText.gameObject.GetComponent<Text>().text = textsLVW[0] + "\n\n" + textsLVW[1] + textsLVW[2] + textsLVW[3];
            }
        }
        else
        {
            dialogText.gameObject.GetComponent<Text>().text = textsLVL[0] + "\n\n" + textsLVL[1] + textsLVL[2] + "\n\n" + textsLVL[3];
        }
    }

    // return to main and  write to player
    public void OKButton()
    {
        sm.SetPlayer(fEnd.numplayer, playerStats);
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    //To levelUpthePlayer
    public void LevelUpper()
    {
        BSource.clip = BackMusic[2];
        BSource.Play();
        playerStats.LevelUp();
    }

    //To put the bar at its position and the level
    public void ExpApplier()
    {
        playerLevel.gameObject.GetComponent<Text>().text = "Lv " + playerStats.lv;
        playerExp.fillAmount = playerStats.ExpPercentage();
    }

    public void ExpUpdater()
    {
        playerLevel.gameObject.GetComponent<Text>().text = "Lv " + oldLV;
        //EXP that will be alloted to the user
        if (!superLVLUP && fEnd.exp != -1)
        {
            if (oldLV == newLV)
            {
                int newExp = oldEXP + fEnd.exp;
                if (playerStats.exp < newExp)
                {
                    playerStats.exp++;
                }
                if (playerStats.exp <= newExp)
                {
                    playerExp.fillAmount = playerStats.ExpPercentage();
                }
            }
            else
            {
                if (oldLV < newLV)
                {
                    int newExp = oldMEXP;
                    if (playerStats.exp < newExp)
                    {
                        playerStats.exp++;
                        fEnd.exp--;
                        playerExp.fillAmount = playerStats.ExpPercentage();
                    }
                    if (playerStats.exp == newExp)
                    {
                        playerExp.fillAmount = playerStats.ExpPercentage();
                        oldLV++;
                        playerLevel.gameObject.GetComponent<Text>().text = "Lv " + oldLV;
                        LevelUpper();
                        oldEXP = 0;
                    }
                }
            }
        }
        else
        {
            if (oldLV < newLV)
            {
                int newExp = playerStats.maxExp;
                if (playerStats.exp < newExp)
                {
                    playerStats.exp++;
                    playerExp.fillAmount = playerStats.ExpPercentage();
                }
                if (playerStats.exp == newExp)
                {
                    LevelUpper();

                    playerExp.fillAmount = playerStats.ExpPercentage();
                    oldLV++;
                    playerLevel.gameObject.GetComponent<Text>().text = "Lv " + oldLV;
                }
            }
        }

    }

    //To determine if player is going to level up 
    public bool LevelBool()
    {
        if (playerStats.exp + fEnd.exp > playerStats.maxExp || fEnd.exp == -1)
        {
            return true;
        }
        return false;
    }

    //fanses that were won or lost
    public void FansApplier()
    {
        playerStats.RecalculateFans(fEnd.fans);
    }

    //money that will be lost or won
    public void MoneyApplier()
    {
        playerStats.RecalculateFightWealth(fEnd.money);
    }

    //For Charging up the Icon of the player
    //It depends on the player and on the status of lose or win
    public void IconsApplier()
    {
        if (fEnd.win)
        {
            int nump = fEnd.exp == -1 ?  0 : fEnd.numplayer;
            deadman.SetActive(false);
            Replaceable.sprite = playerIcons[nump];
        }
        else
        {
            Replaceable.fillAmount = 0;
        }
    }

    //To change the back of the scene depending on if it was win or lose
    //Also the music
    public void WinLoseApplier()
    {
        if (fEnd.win)
        {
            Loser.SetActive(false);
            theNurse.SetActive(false);
            BSource.clip = BackMusic[0];
        }
        else
        {
            levelUpperBars.SetActive(false);
            BSource.clip = BackMusic[1];
        }
        BSource.Play();
    }

    //To know if new level will be multipple
    public void Ultron()
    {
        ultra = true;
        if (oldLV+1<newLV)
        {
            superLVLUP = true;
        }
    }
}
