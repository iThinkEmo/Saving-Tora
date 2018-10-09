using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Undead : Saviour
{
    public Undead(string gen)
    {
		hp = 60;
		mp =  6;
		ap = 16;
		dp =  4;
		sp = 16;
		gender = gen;
		money = 0;
		fans = 50;
		items = new ArrayList();
    }
}