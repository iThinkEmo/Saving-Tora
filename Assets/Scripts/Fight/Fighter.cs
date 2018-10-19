using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterHP", menuName = "FightStats")]
public class Fighter : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;

    public float healthPercentage;

    public Sprite artwork;


    public float CalculateHealthPercentage()
    {
        return maxHealth / currentHealth;
    }

}