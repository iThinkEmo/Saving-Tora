using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : Saviour
{

    public Magician(int gen)
    {
        maxhp = 80;
        hp = 80;
        mp = 18;
        ap = 6;
        dp = 6;
        sp = 8;
        lv = 1;
        gender = gen;
        money = 0;
        fans = 50;
        exp = 0;
        maxExp = 5;
        items = new PlayerItems();
        level = new LV(1);
        magica = new Magica(0, 0);
    }
}