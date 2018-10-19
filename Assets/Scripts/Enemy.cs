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

    //Method to get the monster to Attack
    // Condition 1= attack, 1: defend, 2: nothing
    // Condition 1.1= 3: normal,  4: critical, 5: miss
    // So return types will be: (1: defend,2:nothing,3:normal attack,4:critical attack,5: attack missed)
    public int Attack()
    {
        System.Random randomizerMax = new System.Random();
        int rr = randomizerMax.Next(0, 100);
        if (rr < 80)
        {
            rr = randomizerMax.Next(0, 100);
            if (rr < 20 + (sp / 2))
            {
                return 4;
            }
            if (sp * 3 < rr)
            {
                return 5;
            }
            return 3;
        }
        else if (rr >= 80 && rr < 90)
        {
            return 1;
        }
        return 2;
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

    //Tony
    public EnemyClass(int i)
    {
        InitializeEnemy(i);
    }

    //1 tony, 2 theEye (MORE TO COME)
    public void InitializeEnemy(int i)
    {
        switch (i)
        {
            case 0:
                ForInit();
                break;
            case 1:
                Tony();
                break;
            case 2:
                TheEye();
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    public void Tony()
    {
        maxHp = 30;
        hp = 30;
        mp = 0;
        ap = 10;
        dp = 10;
        sp = 6;
        moNo = 1;
    }

    public void TheEye()
    {
        maxHp = 20;
        hp = 20;
        mp = 20;
        ap = 5;
        dp = 5;
        sp = 6;
        moNo = 2;
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