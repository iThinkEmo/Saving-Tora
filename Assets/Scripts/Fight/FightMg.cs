using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Class Responsible for the actual fight
class FightMg : MonoBehaviour
{
    string thisMagi;

    //To be used with the button
    public void ItemsButton()
    {
        string yes = "Not Yet m8";
    }

    //To be used with the button
    public string[] AttackButton()
    {
        StatusMaker mk1 = new StatusMaker();
        int i = Starter(mk1);
        PlayerUber p1 = StarterP(i,mk1);
        EnemyClass e1 = StarterE(i,mk1);

        int[] p1DMG = p1.Attack();
        int eStat = e1.AttackPower();
        int[] e1DMG = e1.Attack(eStat);
        string[] attres =  AttackResults(p1DMG,e1DMG);
        int[] results = ResultsCalculator(p1DMG,e1DMG);
        p1.RecalculateHealth(0,results[1]);
        e1.RecalculateHealth(0, results[0] - (e1.dp ));
        mk1.SetPlayer(i,p1);
        mk1.SetMonster(i, e1);

        //(user stat, user action, monster stat, monster action)
        //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
        //StatUSER   (0 attack missed, 1 normal, 2 critical)
        return new string[] { p1DMG[0].ToString(),attres[0], eStat.ToString(), attres[1] };
    }

    //To be used with the button
    public string[] DFenceButton()
    {
        StatusMaker mk1 = new StatusMaker();
        int i = Starter(mk1);
        PlayerUber p1 = StarterP(i, mk1);
        EnemyClass e1 = StarterE(i, mk1);

        int p1DMG = p1.Defend();
        int eStat = e1.AttackPower();
        int[] e1DMG = e1.Attack(eStat);
        string[] attres = DefendResults(p1DMG,e1DMG);
        int[] results = ResultsCalculator(p1DMG, e1DMG);
        p1.RecalculateHealth(0, results[1]);
        mk1.SetPlayer(i, p1);
        mk1.SetMonster(i, e1);


        //(user stat, user action, monster stat, monster action)
        //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
        //StatUSER   (3 defend)
        return new string[] { "3", attres[0], eStat.ToString(), attres[1] };
    }

    //To be used with the button
    public string[] MagicButton()
    {
        StatusMaker mk1 = new StatusMaker();
        int i = Starter(mk1);
        PlayerUber p1 = StarterP(i, mk1);
        EnemyClass e1 = StarterE(i, mk1);
        thisMagi = p1.magica.GetName();
        int[] p1DMG = p1.AttackMagi();
        int eStat = e1.AttackPower();
        int[] e1DMG = e1.Attack(eStat);
        string[] attres = MagicResults(p1DMG, e1DMG, thisMagi);
        int[] results = ResultsCalculator(p1DMG, e1DMG);

        p1.RecalculateHealth(0, results[1]);
        e1.RecalculateHealth(0, results[0]-(e1.dp));
        mk1.SetPlayer(i, p1);
        mk1.SetMonster(i, e1);
        int maginum = p1DMG[0];
        switch (maginum)
        {
            case 1:
                maginum = 4;
                break;
            case 2:
                maginum = 5;
                break;
            default:
                break;
        }

        //(user stat, user action, monster stat, monster action)
        //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
        //StatUSER   (0 attack missed, 4 normal, 5 critical)
        return new string[] { maginum.ToString(), attres[0], eStat.ToString(), attres[1] };
    }

    //-1,-1 means failed flee
    public string[] Flee()
    {
        StatusMaker mk1 = new StatusMaker();
        int i = Starter(mk1);
        PlayerUber p1 = StarterP(i, mk1);
        EnemyClass e1 = StarterE(i, mk1);

        int[] p1DMG = {-1,-1};
        int eStat = e1.AttackPower();
        int[] e1DMG = e1.Attack(eStat);
        string yes = "Failed to flee...";
        int[] results = {0,e1DMG[0] };
        string[] attres = AttackResults(p1DMG, e1DMG);
        p1.RecalculateHealth(0, results[1]);
        mk1.SetPlayer(i, p1);
        mk1.SetMonster(i, e1);
        Debug.Log("AL FINAL DE FLEE");
        //(user stat, user action, monster stat, monster action)
        //STATMONSTER(1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
        //StatUSER   (6 flee fail)
        return new string[] { "6", yes, eStat.ToString(), attres[1] };

    }

    //To get player and such
    public int Starter(StatusMaker mk1 )
    {
        StatusForFight sF = mk1.FightGetter();
        return sF.currentPlayer;
    }
    
    //To get player 
    public PlayerUber StarterP(int player, StatusMaker mk1)
    {
        return mk1.getPlayer(player);
    }

    //To get enemy
    public EnemyClass StarterE(int player, StatusMaker mk1)
    {
        return mk1.ToContinueJSONFight(player);
    }

    //results (player, monster)
    public string[] AttackResults(int[] player1,int[] enemy1)
    {
        string[] results = { PlayerResulter(player1), MonsterResulter(enemy1) };
        return results;
    }

    //results (player, monster)
    public string[] MagicResults(int[] player1, int[] enemy1, string oneee)
    {
        string[] results = { PlayerResulter(player1, oneee), MonsterResulter(enemy1) };
        return results;
    }

    //results (player, monster)
    public string[] DefendResults(int player1, int[] enemy1)
    {
        string[] results = { "You Defended", MonsterResulter(enemy1) };
        return results;
    }

    //To get a string of what monster did
    public string MonsterResulter(int[] enemy1)
    {
        switch (enemy1[1])
        {
            case 1:
                return "Monster Defended";
            case 2:
                return "Monster Was Confused About This Fight So It Did Nothing.";
            case 3:
                return "Monster Attacked";
            case 4:
                return "Monster Smashed You!";
            case 5:
                return "Monster Missed The Attack." ;
            default:
                break;
        }
        return "";
    }

    //To get a string of what you did
    public string PlayerResulter(int[] player1)
    {
        switch (player1[1])
        {
            case 0:
                return "You Missed.";
            case 1:
                return "You Attacked";
            case 2:
                return "You Smashed the monster!";
            default:
                break;
        }
        return "";
    }

    //To get a string of what you did
    public string PlayerResulter(int[] player1,string magi)
    {
        switch (player1[1])
        {
            case 0:
                return "You Missed.";
            case 1:
                return "You Used "+magi+" Magic";
            case 2:
                return "Your" + magi +" Magic Smashed the monster!";
            default:
                break;
        }
        return "";
    }

    //To calculate what needs to be done
    //(myAttackPower, enemyAttackPower)
    public int[] ResultsCalculator(int[] playe, int[] enem)
    {
        if (enem[1] == 1)
        {
            int u = playe[0] - enem[0];
            if (u <= 0)
            {
                if (enem[0] != 0)
                {
                    u = 5;
                }
                else { u = 0; }
            }
            return new int[] {u,0};
        }
        return new int[] { playe[0], enem[0]};
    }

    //To calculate what needs to be done
    //(myAttackPower, enemyAttackPower)
    public int[] ResultsCalculator(int playe, int[] enem)
    {
        if (enem[1] == 1)
        {
            return new int[] { 0, 0 };
        }
        int u = enem[0] - playe;
        if (u<=0)
        {
            if (enem[0] != 0)
            {
                u = 5;
            }
            else { u = 0; }
        }
        return new int[] { 0, u};
    }

    //To calculate if its dead
    public bool isDead(int currentHp)
    {
        if (currentHp<=0)
        {
            return true;
        }
        return false;
    }

    public void ReturnToMain()
    {
        StatusMaker mk2 = new StatusMaker();
        List<PlayerStatusGlobal> mySuperList = mk2.GetUltrajson();
        int i = Starter(mk2) - 1;
        mySuperList[i].statusScene = 2;
        mk2.setUltrajson(mySuperList);
        GameManager gameManagerDelJuego = GameManager.Instance;
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

}

