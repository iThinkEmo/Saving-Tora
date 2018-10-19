using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Undead : Saviour
{
    public Undead(int gen)
    {
        maxhp = 60;
        hp = 150;
        mp = 6;
        ap = 20;
        dp = 20;
        sp = 6;
        lv = 1;
        gender = gen;
        money = 0;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(3);
        magica = new Magica(0, 0);
    }
}