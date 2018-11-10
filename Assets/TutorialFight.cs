using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TutorialFight : MonoBehaviour {

    //for the INIT timeline
    int intialSeconds    = 12;
    //for before finalizer timeline
    int lastSeconds      = 12;
    //for the finalizer 
    int finalizerSeconds = 34;
    bool fleeB = false;
    bool attB  = false;
    bool defB  = false;
    bool magB  = false;
    bool itemB = false;
    bool skipb = false;
    bool tutoF = true;

    #region timeLine
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    #endregion

    StatusMaker sm = new StatusMaker();

    #region OBJECTS UI

    //health area conntains all of the health area stuff, for its use
    //public List<GameObject> HealthArea;

    //Crosses and arrows are the icons that show to the user what to press and what is disabled
    //Order is (0:Flee,1:Magi,2:Attack,3:Dfence,4:Items)
    public List<GameObject> crosses;
    public List<GameObject> arrows;
    public GameObject bottomText;

    //For the life points calculations
    public int myMaxhp = 10;
    public int eMaxhp  = 10;
    public int myhp    = 10;
    public int ehp     = 10;
    public Image playerLife;
    public Image enemyLife;
    #endregion


    // Use this for initialization
    void Start ()
    {
        Invoke("FightIdler", intialSeconds);
        //LooperLupe();
        foreach (var item in crosses)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (AllbuttonsPressend())
        {
            if (tutoF)
            {
                enemyLife.fillAmount = 1;
                Invoke("TutorialFinisher", lastSeconds);
                Invoke("Minus2", lastSeconds+11);
                Invoke("Minus6", lastSeconds+14.5F);
                Invoke("Minus2", lastSeconds+18);
                Invoke("Plus1", lastSeconds+22);
                Invoke("KillEnemy", lastSeconds+26.2F);
                Invoke("FinishedAndLeaving", lastSeconds + finalizerSeconds);
                tutoF = false;
            }
        }
        if (magiIsMissing())
        {
            lastSeconds = 15;
        }
        if (skipb)
        {
            SkipAction();
        }
        activateArrows();
    }

    //To finish and leave
    public void FinishedAndLeaving()
    {
        GameManager gameManagerDelJuego = GameManager.Instance;
        int plaier = PlayerUber.normalizeCurrentPlayer(gameManagerDelJuego.GetCurrentPlayer());
        sm.SetEnd(plaier,-1,250,50,0,true);
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "lvUP";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    //To deactivate buttons
    //(0:Flee,1:Magi,2:Attack,3:Dfence,4:Items)
    public void activateArrows()
    {
        if(fleeB)
        {
            crosses[0].SetActive(true);
            arrows[0].SetActive(false);
        }
        if(attB)
        {
            crosses[2].SetActive(true);
            arrows[2].SetActive(false);
        }
        if (defB)
        {
            crosses[3].SetActive(true);
            arrows[3].SetActive(false);
        }
        if (magB)
        {
            crosses[1].SetActive(true);
            arrows[1].SetActive(false);
        }
        if (itemB)
        {
            crosses[4].SetActive(true);
            arrows[4].SetActive(false);
        }
    }

    //Last method, use it for asking user if he is sure about skipping tutorial 
    public void SkipAction()
    {
        //.setactive un boton
        FinishedAndLeaving();
    }

    //to use on the imsure button
    public void ImSure()
    {
    }

    //Forplaying idleFight animation
    public void FightIdler()
    {
        PlayableDirector director = playableDirectors[1];
        director.Play();
        foreach (var item in arrows)
        {
            item.SetActive(true);
        }
        Invoke("LooperLupe", 4);
    }

    //For looping idleFight animation
    public void LooperLupe()
    {
        PlayableDirector director = playableDirectors[2];
        director.Play();
    }

    //To link the flee button
    public void FleeButton()
    {
        if (!fleeB)
        {
            Debug.Log("Clickado Flee");
            PlayableDirector director = playableDirectors[3];
            director.Play();
        }
        fleeB = true;
    }

    //To link the attack button
    public void AttackButton()
    {
        if (!attB)
        {
            Debug.Log("Clickado ATK");
            PlayableDirector director = playableDirectors[4];
            director.Play();
        }
        attB = true;

    }

    //To link the defence button
    public void DefenceButton()
    {
        if (!defB)
        {
            Debug.Log("Clickado def");
            PlayableDirector director = playableDirectors[5];
            director.Play();
        }
        defB = true;
    }

    //To link the magic button
    public void MagicButton()
    {
        if (!magB)
        {
            Debug.Log("Clickado MAGI");
            PlayableDirector director = playableDirectors[6];
            director.Play();
        }
        magB = true;
    }

    //To link the items button
    public void ItemsButton()
    {
        if (!itemB)
        {
            Debug.Log("Clickado Items");
            PlayableDirector director = playableDirectors[7];
            director.Play();
        }
        itemB = true;
    }

    //To know if all buttons have been pressed
    public bool AllbuttonsPressend()
    {
        if (fleeB && attB && defB && magB && itemB )
        {
            return true;
        }
        return false;
    }

    //This one is for knowing if magi is the last one missing
    public bool magiIsMissing()
    {
        if (fleeB && attB && defB && itemB)
        {
            if (!magB)
            {
                return true;
            }
        }
        return false;
    }

    //activates finalizer
    public void TutorialFinisher()
    {
        Debug.Log("You finished");
        PlayableDirector director = playableDirectors[8];
        director.Play();
    }

    //To be linked with skipped button 
    public void Skipper()
    {
        skipb = true;
        Debug.Log("SKIP THIS");
        SkipAction();
    }

    public void Minus2()
    {
        UpdateHPUser(2);
    }

    public void Minus6()
    {
        UpdateHPUser(6);
    }

    public void Plus1()
    {
        UpdateHPUser(-1);
    }

    public void UpdateHPUser(int lifetoLess)
    {
        myhp -= lifetoLess;
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpMeFight");
        if (dialogText)
        {
            dialogText.gameObject.GetComponent<Text>().text = myhp + "/" + myMaxhp;
        }
        playerLife.fillAmount = HPPercentage(myhp, myMaxhp);
    }

    public void KillEnemy()
    {
        ehp -=10;
        GameObject dialogText = GameObject.FindGameObjectWithTag("hpEnemyFight");
        if (dialogText)
        {
            dialogText.gameObject.GetComponent<Text>().text = ehp + "/" + eMaxhp;
        }
        enemyLife.fillAmount = HPPercentage(ehp,eMaxhp);
    }

    public float HPPercentage(int hp, int maxhp)
    {
        float lol = (float)hp / maxhp;
        //Debug.Log(lol);
        return lol;
    }
}
