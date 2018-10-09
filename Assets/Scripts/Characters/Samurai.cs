using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Samurai : Saviour
{
    public Samurai(string gen)
    {
		hp = 100; 
		mp =  10;
		ap =  10;
		dp =   8;
		sp =  10;
		gender = gen;
		money = 0;
		fans = 50;
		items = new ArrayList();
    }
}