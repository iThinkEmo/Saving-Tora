using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class unpequenomanager : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;

    // Use this for initialization
    void Start () {
        part1.SetActive(false);
        part2.SetActive(false);
	}

    public void killPartidas()
    {
        if(File.Exists(Application.persistentDataPath + "/save0/" + "core.json"))
        { 
            part1.SetActive(false);
        }
        if (File.Exists(Application.persistentDataPath + "/save1/" + "core.json"))
        {
            part2.SetActive(false);
        }
    }

    public void livePartidas()
    {
        if (File.Exists(Application.persistentDataPath + "/save0/" + "core.json"))
        {
            part1.SetActive(true);
        }
        if (File.Exists(Application.persistentDataPath + "/save1/" + "core.json"))
        {
            part2.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update () {
		
	}
}
