using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceMonk : Saviour
{
    public RiceMonk(int gen)
    {
        maxhp = 60;
        hp = 60;
        mp = 6;
        ap = 16;
        dp = 4;
        sp = 16;
        lv = 1;
        gender = gen;
        money = 0;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(2);
        magica = new Magica(0, 0);
    }
}