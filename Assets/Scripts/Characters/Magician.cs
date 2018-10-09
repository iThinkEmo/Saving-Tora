using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : Saviour
{
    public Magician(string gen)
    {
		hp = 80;
		mp = 18;
		ap =  6;
		dp =  6;
		sp =  8;
		gender = gen;
		money = 0;
		fans = 50;
		items = new ArrayList();
    }
}