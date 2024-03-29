﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelectScript : MonoBehaviour
{

    // Use this for initialization
    public static CharacterSelectScript control;
    public Button UIButt;
    string CharName;
    public bool happened = false;
    GameObject[] Characters1;
	public int characterPlace;
    int x = 0;

    void Start()  {
        UIButt = GetComponent<Button>();
        UIButt.onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mainmenu() //trying to clear chars if we go to mainmenu
    {
		/*
        happened = false;
        for (int i = 0; i < GameControlScript.control.GetTeam().Count; ++i)
        {
            name = GameControlScript.control.GetTeam()[0];
            Debug.Log(name);
            GameControlScript.control.SelectCharacter(name);
        }
        */
    }

    private void clicked()
    {
        if (happened == false)
        {
            OnMouseOver();
            happened = true;
        }
        else
        {
            if (UIButt.name.ToString() == "Character5" || UIButt.name.ToString() == "Character6")
            {
                GameControlScript.control.SelectSideMissionCharacter(GetComponentInChildren<Text>().text.ToString().ToLower());

            }
            else { GameControlScript.control.SelectCharacter(GetComponentInChildren<Text>().text.ToString().ToLower());
            }
            
            OnMouseOver();
        }
    }

    public void OnMouseOver()
    {
        charselect.control.buttselected(UIButt);
        charselect.control.AddButtonClicked(UIButt);
        GameObject.FindWithTag("Mission Text").GetComponent<Canvas>().enabled = false;
        GameObject.FindWithTag("Mission").GetComponent<Canvas>().enabled = false;
        var UI = GameObject.FindWithTag("CharSelect");
        Canvas UI1;
        UI1 = UI.GetComponent<Canvas>();
        Characters1 = GameObject.FindGameObjectsWithTag("Characters");
        UI1.enabled = true;
        foreach (GameObject character in Characters1)
        {
            
            character.GetComponent<Button>().interactable = true;
            character.GetComponent<CanvasGroup>().alpha = 1;
            CharName = character.GetComponent<Button>().name.ToString().ToLower();
			characterPlace = character.GetComponent<CharacterSelectionForSlot> ().GetCharacterPlace();
            for (int i = 0; i < GameControlScript.control.GetChosen().Count; ++i)
            {
                if (GameControlScript.control.GetChosen()[characterPlace] || GameControlScript.control.GetSideMissionChosen()[characterPlace])
                {
                    character.GetComponent<Button>().interactable = false;
                    character.GetComponent<CanvasGroup>().alpha = 0.5f;
                }
            }


            for (int i = 0; i < GameControlScript.control.GetDeadCharacters().Count; ++i)
            {
                Debug.Log("DEAD");
                Debug.Log(GameControlScript.control.GetDeadCharacters()[i].ToLower());
                Debug.Log("IM DEAD");
                Debug.Log(CharName);
                Debug.Log(character.GetComponentInChildren<Text>().text.ToString().ToLower());
                Debug.Log("HERE");
                if (GameControlScript.control.GetDeadCharacters()[i].ToLower() == CharName || GameControlScript.control.GetDeadCharacters()[i].ToLower() == character.GetComponentInChildren<Text>().text.ToString().ToLower()) 
                {
                    character.GetComponent<Button>().interactable = false;
                    character.GetComponent<CanvasGroup>().alpha = 0.5f;
                }
            }
        }
    }
}