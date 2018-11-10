using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class FightTextMGR : MonoBehaviour {

    //isThedialogue
    public string dialogue;
    //number of dialogue in the array
    public int i;
    //array of seconds
    public int[] ies;
    //Theobject to which the text will be assigned
    public GameObject topText;
    string[] um;

    public void OnEnable()
    {
        foreach (var item in ies)
        {
            Invoke("changeDialogue", item);
        }
    }

    public void changeDialogue()
    {
        topText.gameObject.GetComponent<Text>().text = um[i];
        i++;
    }
}
