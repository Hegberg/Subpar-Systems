﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HubConvo : MonoBehaviour {
    Button go;
    string sentence;
    private List<string> convo = new List<string>();
    private List<int> speaker = new List<int>();
    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;
    protected string text = " ";

    // Use this for initialization
    void Start() {
        var UI1 = GameObject.FindWithTag("MissionHubConvo2");
        var UI2 = GameObject.FindWithTag("MissionHubConvo1");
        var convo2 = GameObject.FindWithTag("Convo1");
        var convo1 = GameObject.FindWithTag("Convo2");
        theSourceFile = new FileInfo("HubScene1level1.txt");
        reader = theSourceFile.OpenText();
        go = GetComponent<Button>();
        UI1.GetComponent<Image>().enabled = false;
        convo1.GetComponent<Text>().enabled = false;
        UI2.GetComponent<Image>().enabled = false;
        convo2.GetComponent<Text>().enabled = false;
        go.onClick.AddListener(clicked);
    }
    private void clicked() {
        var UI1 = GameObject.FindWithTag("MissionHubConvo2");
        var UI2 = GameObject.FindWithTag("MissionHubConvo1");
        var convo2 = GameObject.FindWithTag("Convo1");
        var convo1 = GameObject.FindWithTag("Convo2");
        if (text != null)
        {
            text = reader.ReadLine();
            UI1.GetComponent<Image>().enabled = false;
            convo1.GetComponent<Text>().enabled = false;
            UI2.GetComponent<Image>().enabled = false;
            convo2.GetComponent<Text>().enabled = false;
        }
        if (text == null) {
            //error handle
        }
        else if (text.Substring(0, 1) == ("G"))
        {
            UI1.GetComponent<Image>().enabled = true;
            convo1.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo1.GetComponent<Text>().text = sentence;
        }
        else if (text.Substring(0, 1) == ("T"))
        {
            UI2.GetComponent<Image>().enabled = true;
            convo2.GetComponent<Text>().enabled = true;
            sentence = text.Substring(2);
            convo2.GetComponent<Text>().text = sentence;
        }
    }
}
    // Update is called once per frame


