using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Samurai : Saviour
{
    public Samurai(int gen)
    {
        maxhp = 100;
        hp = 100;
        mp = 10;
        ap = 10;
        dp = 8;
        sp = 10;
        lv = 1;
        gender = gen;
        money = 0;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(0);
        magica = new Magica(0, 0);
    }
}