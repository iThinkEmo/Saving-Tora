using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FighterDisplay: MonoBehaviour
{
    public Fighter fighter;

    public Text hpText;

    public Image artwworkImage;

    void Start()
    {
        hpText.text = fighter.currentHealth.ToString() + "/" + fighter.maxHealth.ToString();
        artwworkImage.sprite = fighter.artwork;
    }
}

