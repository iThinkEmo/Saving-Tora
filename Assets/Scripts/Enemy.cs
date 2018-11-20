using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClass
{
    //maximum health points
    public int maxHp;
    //current health points
    public int hp;
    //magic points
    public int mp;
    //attack points
    public int ap;
    //defense points
    public int dp;
    //speed points
    public int sp;
    //MonsterNumber
    public int moNo;


    //(Attack Power, Type)
    // So return types will be: (1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
    public int[] Attack(int power)
    {
        switch (power)
        {
            case 1:
                return new int[] {dp, power };
            case 2:
                return new int[] {0, power };
            case 3:
                return new int[] {ap, power };
            case 4:
                return new int[] { (int)(ap * 1.8), power };
            case 5:
                return new int[] {0, power };
            default:
                break;
        }
        return new int[] {0,5};
    }

    //Method to get the monster to Attack
    // Condition 1= attack, 1: defend, 2: nothing
    // Condition 1.1= 3: normal,  4: critical, 5: miss
    // So return types will be: (1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
    public int AttackPower()
    {
        System.Random randomizerMax = new System.Random();

        int rr = randomizerMax.Next(0, 100);
        Debug.Log("MEEEEEEEEEEEEEEEEEEEEEEEEEE:"+ rr);
        if (rr < 80)
        {
            rr = randomizerMax.Next(0, 100);
            if (rr > 79)
            {
                return 4;
            }
            if(rr<10)
            {
                return 5;
            }
            return 3;
        }
        else 
        {
            rr = randomizerMax.Next(0, 100);
            if ( rr < 50)
            {
                return 1;
            }
            return 2;
        }
    }

    //moreLess; 0 less, an attack. 1 more, an Item or heal
    public void RecalculateHealth(int moreLess, int hpChange)
    {
        if (moreLess == 0)
        {
            this.hp -= hpChange;
        }
        else
        {
            this.hp += hpChange;
        }
    }

    public float HPPercentage()
    {
        float lol = (float)this.hp / this.maxHp;
        return lol;
    }

    //To initalize enemies
    public EnemyClass(int i)
    {
        InitializeEnemy(i+1);
    }

    //(Eyes,LILRobots, Turrets, Crocs, LAMEBots)
    public void InitializeEnemy(int i)
    {
        switch (i)
        {
            case 0:
                ForInit();
                break;
            case 1:
                Eyes(1);
                break;
            case 2:
                Eyes(2);
                break;
            case 3:
                Eyes(3);
                break;
            case 4:
                Eyes(4);
                break;
            case 5:
                Robotos(1);
                break;
            case 6:
                Robotos(2);
                break;
            case 7:
                Robotos(3);
                break;
            case 8:
                Robotos(4);
                break;
            case 9:
                Turrets(1);
                break;
            case 10:
                Turrets(2);
                break;
            case 11:
                Turrets(3);
                break;
            case 12:
                Turrets(4);
                break;
            case 13:
                Crocs(1);
                break;
            case 14:
                Crocs(2);
                break;
            case 15:
                Crocs(3);
                break;
            case 16:
                Crocs(4);
                break;
            case 17:
                Lames(1);
                break;
            case 18:
                Lames(2);
                break;
            case 19:
                Lames(3);
                break;
            case 20:
                Lames(4);
                break;
            case 21:
                Bosses(1);
                break;
            case 22:
                Bosses(2);
                break;
            case 23:
                Bosses(3);
                break;
            case 24:
                Bosses(4);
                break;
            case 25:
                Bosses(5);
                break;
            case 26:
                Bosses(7);
                break;
            default:
                break;
        }
    }

    public void Eyes(int enemynum)
    {
        maxHp = 30 * enemynum;
        hp = 30 * enemynum;
        mp = 20 * enemynum;
        ap = 5 * enemynum;
        dp = 5 * enemynum;
        sp = 6 * enemynum;
        moNo = enemynum;
    }

    public void Robotos(int enemynum)
    {
        maxHp = 20 * enemynum;
        hp = 20 * enemynum;
        mp = 0 * enemynum;
        ap = 25  * enemynum;
        dp = 0  * enemynum;
        sp = 30  * enemynum;
        moNo = enemynum+4;
    }

    public void Turrets(int enemynum)
    {
        maxHp = 40 * enemynum;
        hp = 40 * enemynum;
        mp = 2 * enemynum;
        ap = 5  * enemynum;
        dp = 5  * enemynum;
        sp = 0  * enemynum;
        moNo = enemynum+8;
    }

    public void Crocs(int enemynum)
    {
        maxHp = 40 * enemynum;
        hp = 40 * enemynum;
        mp = 0 * enemynum;
        ap = 10 * enemynum;
        dp = 10 * enemynum;
        sp = 6 * enemynum;
        moNo = enemynum + 12;
    }

    public void Lames(int enemynum)
    {
        maxHp = 20 * enemynum;
        hp = 20 * enemynum;
        mp = 20 * enemynum;
        ap = 25 * enemynum;
        dp = 25 * enemynum;
        sp = 26 * enemynum;
        moNo = enemynum + 16;
    }

    public void Bosses(int enemynum)
    {
        maxHp = 150 * enemynum;
        hp = 150 * enemynum;
        mp = 60 * enemynum;
        ap = 60 * enemynum;
        dp = 50 * enemynum;
        sp = 50 * enemynum;
        moNo = 20 +enemynum;
    }


    public void ForInit()
    {
        maxHp = 0;
        hp = 0;
        mp = 0;
        ap = 0;
        dp = 0;
        sp = 0;
        moNo = 0;
    }
}