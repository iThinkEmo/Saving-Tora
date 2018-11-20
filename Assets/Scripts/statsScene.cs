using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class statsScene : MonoBehaviour
{
    public Image[] circlesmen;
    public Sprite[] pIcons;
    public List<GameObject> Players;
    public List<GameObject> fanTexts;
    public List<GameObject> hpTexts;
    public List<GameObject> moneyTexts;
    List<MiniPlayerDef> playersLittle;
    SaveFile sFl = new SaveFile();
    GameManager gameManagerDelJuego;
    public GameObject savingG;
    public GameObject upperText;

    // Use this for initialization
    void Start ()
    {
        StatusMaker sm = new StatusMaker();
        sm.SetCore(1,"");
        gameManagerDelJuego = GameManager.Instance;
        savingG.gameObject.GetComponent<Text>().text = " ";
        sFl.ReadAll();
        MiniPlayerDef m1 = new MiniPlayerDef(sFl.magician.fans,sFl.magician.money,sFl.magician.hp, pIcons[0]);
        MiniPlayerDef m2 = new MiniPlayerDef(sFl.undead.fans,sFl.undead.money,sFl.undead.hp, pIcons[1]);
        MiniPlayerDef m3 = new MiniPlayerDef(sFl.samurai.fans,sFl.samurai.money,sFl.samurai.hp, pIcons[2]);
        MiniPlayerDef m4 = new MiniPlayerDef(sFl.rice.fans,sFl.rice.money,sFl.rice.hp, pIcons[3]);
        List<MiniPlayerDef> unordered = new List<MiniPlayerDef>();
        unordered.Add(m1);
        unordered.Add(m2);
        unordered.Add(m3);
        unordered.Add(m4);
        List<MiniPlayerDef> ordered = unordered.OrderBy(o => o.fans).ToList();
        ordered.Reverse();
        for (int iccc = 0; iccc < 4; iccc++)
        {
            if (ordered[iccc].fans != 0 || ordered[iccc].wealth != 0 || ordered[iccc].health != 0)
            {
                fanTexts[iccc].gameObject.GetComponent<Text>().text = "Fans: " + ordered[iccc].fans;
                hpTexts[iccc].gameObject.GetComponent<Text>().text = "Health: " + ordered[iccc].health;
                moneyTexts[iccc].gameObject.GetComponent<Text>().text = "Wealth: " + ordered[iccc].wealth;
                circlesmen[iccc].sprite = ordered[iccc].iconnn;
            }
            else
            {
                Players[iccc].SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SaveButton()
    {
        savingG.gameObject.GetComponent<Text>().text = "Saving Game";
        upperText.gameObject.GetComponent<Text>().text = "Please Wait...";
        //GAMEMANAGER NUMJUEGO
        sFl.SaveGame(0);
        Invoke("returnToGame", 4f);
    }

    public void NotSaveButton()
    {
        Invoke("returnToGame", 0f);
    }

    public void returnToGame()
    {
        Debug.Log("ya me fui");
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }
}

public class MiniPlayerDef
{
    public int position;
    public int fans;
    public int wealth;
    public int health;
    public Sprite iconnn;

    public MiniPlayerDef(int fan,int wealt,int healt, Sprite iconnmn)
    {
        fans   = fan;
        wealth = wealt;
        health = healt>=0 ? healt : 0;
        iconnn = iconnmn;
    }
}
